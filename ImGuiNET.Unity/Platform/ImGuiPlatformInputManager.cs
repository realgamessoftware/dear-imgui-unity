using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace ImGuiNET.Unity
{
    // Implemented features:
    // [x] Platform: Clipboard support.
    // [x] Platform: Mouse cursor shape and visibility. Disable with io.ConfigFlags |= ImGuiConfigFlags.NoMouseCursorChange.
    // [x] Platform: Keyboard arrays indexed using KeyCode codes, e.g. ImGui.IsKeyPressed(KeyCode.Space).
    // [ ] Platform: Gamepad support. Enabled with io.ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad.
    // [~] Platform: IME support.
    // [~] Platform: INI settings support.

    /// <summary>
    /// Platform bindings for ImGui in Unity in charge of: mouse/keyboard/gamepad inputs, cursor shape, timing, windowing.
    /// </summary>
    sealed class ImGuiPlatformInputManager : IImGuiPlatform
    {
        int[] _mainKeys;                                                        // main keys
        readonly Event _e = new Event();                                        // to get text input

        readonly CursorShapesAsset _cursorShapes;                               // cursor shape definitions
        ImGuiMouseCursor _lastCursor = ImGuiMouseCursor.COUNT;                  // last cursor requested by ImGui

        readonly IniSettingsAsset _iniSettings;                                 // ini settings data

        readonly PlatformCallbacks _callbacks = new PlatformCallbacks
        {
            GetClipboardText = (_) => GUIUtility.systemCopyBuffer,
            SetClipboardText = (_, text) => GUIUtility.systemCopyBuffer = text,
#if IMGUI_FEATURE_CUSTOM_ASSERT
            LogAssert = (condition, file, line) => Debug.LogError($"[DearImGui] Assertion failed: '{condition}', file '{file}', line: {line}."),
            DebugBreak = () => System.Diagnostics.Debugger.Break(),
#endif
        };

        public ImGuiPlatformInputManager(CursorShapesAsset cursorShapes, IniSettingsAsset iniSettings)
        {
            _cursorShapes = cursorShapes;
            _iniSettings = iniSettings;
            _callbacks.ImeSetInputScreenPos = (x, y) => Input.compositionCursorPos = new Vector2(x, y);
        }

        public bool Initialize(ImGuiIOPtr io)
        {
            io.SetBackendPlatformName("Unity Input Manager");                   // setup backend info and capabilities
            io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors;               // can honor GetMouseCursor() values
            io.BackendFlags &= ~ImGuiBackendFlags.HasSetMousePos;               // can't honor io.WantSetMousePos requests
            // io.BackendFlags |= ImGuiBackendFlags.HasGamepad;                 // set by UpdateGamepad()

            _callbacks.Assign(io);                                              // assign platform callbacks
            io.ClipboardUserData = IntPtr.Zero;

            if (_iniSettings != null)                                           // ini settings
            {
                io.SetIniFilename(null);                                        // handle ini saving manually
                ImGui.LoadIniSettingsFromMemory(_iniSettings.Load());           // call after CreateContext(), before first call to NewFrame()
            }

            SetupKeyboard(io);                                                  // sets key mapping, text input, and IME

            return true;
        }

        public void Shutdown(ImGuiIOPtr io)
        {
            _callbacks.Unset(io);
            io.SetBackendPlatformName(null);
        }

        public void PrepareFrame(ImGuiIOPtr io, Rect displayRect)
        {
            Assert.IsTrue(io.Fonts.IsBuilt(), "Font atlas not built! Generally built by the renderer. Missing call to renderer NewFrame() function?");

            io.DisplaySize = new Vector2(displayRect.width, displayRect.height);// setup display size (every frame to accommodate for window resizing)
            // TODO: dpi aware, scale, etc

            io.DeltaTime = Time.unscaledDeltaTime;                              // setup timestep

            // input
            UpdateKeyboard(io);                                                 // update keyboard state
            UpdateMouse(io);                                                    // update mouse state
            UpdateCursor(io, ImGui.GetMouseCursor());                           // update Unity cursor with the cursor requested by ImGui

            // ini settings
            if (_iniSettings != null && io.WantSaveIniSettings)
            {
                _iniSettings.Save(ImGui.SaveIniSettingsToMemory());
                io.WantSaveIniSettings = false;
            }
        }

        void SetupKeyboard(ImGuiIOPtr io)
        {
            _mainKeys = new int[] {
                // map and store new keys by assigning io.KeyMap and setting value of array
                io.KeyMap[(int)ImGuiKey.Tab        ] = (int)KeyCode.Tab,
                io.KeyMap[(int)ImGuiKey.LeftArrow  ] = (int)KeyCode.LeftArrow,
                io.KeyMap[(int)ImGuiKey.RightArrow ] = (int)KeyCode.RightArrow,
                io.KeyMap[(int)ImGuiKey.UpArrow    ] = (int)KeyCode.UpArrow,
                io.KeyMap[(int)ImGuiKey.DownArrow  ] = (int)KeyCode.DownArrow,
                io.KeyMap[(int)ImGuiKey.PageUp     ] = (int)KeyCode.PageUp,
                io.KeyMap[(int)ImGuiKey.PageDown   ] = (int)KeyCode.PageDown,
                io.KeyMap[(int)ImGuiKey.Home       ] = (int)KeyCode.Home,
                io.KeyMap[(int)ImGuiKey.End        ] = (int)KeyCode.End,
                io.KeyMap[(int)ImGuiKey.Insert     ] = (int)KeyCode.Insert,
                io.KeyMap[(int)ImGuiKey.Delete     ] = (int)KeyCode.Delete,
                io.KeyMap[(int)ImGuiKey.Backspace  ] = (int)KeyCode.Backspace,
                io.KeyMap[(int)ImGuiKey.Space      ] = (int)KeyCode.Space,
                io.KeyMap[(int)ImGuiKey.Enter      ] = (int)KeyCode.Return,
                io.KeyMap[(int)ImGuiKey.Escape     ] = (int)KeyCode.Escape,
                io.KeyMap[(int)ImGuiKey.KeyPadEnter] = (int)KeyCode.KeypadEnter,
                io.KeyMap[(int)ImGuiKey.A          ] = (int)KeyCode.A,           // for text edit CTRL+A: select all
                io.KeyMap[(int)ImGuiKey.C          ] = (int)KeyCode.C,           // for text edit CTRL+C: copy
                io.KeyMap[(int)ImGuiKey.V          ] = (int)KeyCode.V,           // for text edit CTRL+V: paste
                io.KeyMap[(int)ImGuiKey.X          ] = (int)KeyCode.X,           // for text edit CTRL+X: cut
                io.KeyMap[(int)ImGuiKey.Y          ] = (int)KeyCode.Y,           // for text edit CTRL+Y: redo
                io.KeyMap[(int)ImGuiKey.Z          ] = (int)KeyCode.Z,           // for text edit CTRL+Z: undo
            };
        }

        void UpdateKeyboard(ImGuiIOPtr io)
        {
            // main keys
            foreach (var key in _mainKeys)
                io.KeysDown[key] = Input.GetKey((KeyCode)key);

            // keyboard modifiers
            io.KeyShift = Input.GetKey(KeyCode.LeftShift  ) || Input.GetKey(KeyCode.RightShift  );
            io.KeyCtrl  = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            io.KeyAlt   = Input.GetKey(KeyCode.LeftAlt    ) || Input.GetKey(KeyCode.RightAlt    );
            io.KeySuper = Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand)
                       || Input.GetKey(KeyCode.LeftWindows) || Input.GetKey(KeyCode.RightWindows);

            // text input
            while (Event.PopEvent(_e))
                if (_e.rawType == EventType.KeyDown && _e.character != 0 && _e.character != '\n')
                    io.AddInputCharacter(_e.character);
        }

        static void UpdateMouse(ImGuiIOPtr io)
        {
            io.MousePos = ImGuiUn.ScreenToImGui(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            io.MouseWheel  = Input.mouseScrollDelta.y;
            io.MouseWheelH = Input.mouseScrollDelta.x;

            io.MouseDown[0] = Input.GetMouseButton(0);
            io.MouseDown[1] = Input.GetMouseButton(1);
            io.MouseDown[2] = Input.GetMouseButton(2);
        }

        void UpdateCursor(ImGuiIOPtr io, ImGuiMouseCursor cursor)
        {
            if (io.MouseDrawCursor)
                cursor = ImGuiMouseCursor.None;

            if (_lastCursor == cursor)
                return;
            if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0)
                return;

            _lastCursor = cursor;
            Cursor.visible = cursor != ImGuiMouseCursor.None;                   // hide cursor if ImGui is drawing it or if it wants no cursor
            if (_cursorShapes != null)
                Cursor.SetCursor(_cursorShapes[cursor].texture, _cursorShapes[cursor].hotspot, CursorMode.Auto);
        }
    }
}

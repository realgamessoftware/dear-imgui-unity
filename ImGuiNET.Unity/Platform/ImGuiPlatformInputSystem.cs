#if HAS_INPUTSYSTEM
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace ImGuiNET.Unity
{
    // Implemented features:
    // [x] Platform: Clipboard support.
    // [x] Platform: Mouse cursor shape and visibility. Disable with io.ConfigFlags |= ImGuiConfigFlags.NoMouseCursorChange.
    // [x] Platform: Keyboard arrays indexed using InputSystem.Key codes, e.g. ImGui.IsKeyPressed(Key.Space).
    // [x] Platform: Gamepad support. Enabled with io.ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad.
    // [~] Platform: IME support.
    // [~] Platform: INI settings support.

    /// <summary>
    /// Platform bindings for ImGui in Unity in charge of: mouse/keyboard/gamepad inputs, cursor shape, timing, windowing.
    /// </summary>
    sealed class ImGuiPlatformInputSystem : IImGuiPlatform
    {
        int[] _mainKeys;                                                        // main keys
        readonly List<char> _textInput = new List<char>();                      // accumulate text input

        readonly CursorShapesAsset _cursorShapes;                               // cursor shape definitions
        ImGuiMouseCursor _lastCursor = ImGuiMouseCursor.COUNT;                  // last cursor requested by ImGui
        Keyboard _keyboard = null;                                              // currently setup keyboard, need to reconfigure on changes

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

        public ImGuiPlatformInputSystem(CursorShapesAsset cursorShapes, IniSettingsAsset iniSettings)
        {
            _cursorShapes = cursorShapes;
            _iniSettings = iniSettings;
            _callbacks.ImeSetInputScreenPos = (x, y) => _keyboard.SetIMECursorPosition(new Vector2(x, y));
        }

        public bool Initialize(ImGuiIOPtr io)
        {
            InputSystem.onDeviceChange += OnDeviceChange;                       // listen to keyboard device and layout changes

            io.SetBackendPlatformName("Unity Input System");                    // setup backend info and capabilities
            io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors;               // can honor GetMouseCursor() values
            io.BackendFlags |= ImGuiBackendFlags.HasSetMousePos;                // can honor io.WantSetMousePos requests
            // io.BackendFlags |= ImGuiBackendFlags.HasGamepad;                 // set by UpdateGamepad()

            _callbacks.Assign(io);                                              // assign platform callbacks
            io.ClipboardUserData = IntPtr.Zero;

            if (_iniSettings != null)                                           // ini settings
            {
                io.SetIniFilename(null);                                        // handle ini saving manually
                ImGui.LoadIniSettingsFromMemory(_iniSettings.Load());           // call after CreateContext(), before first call to NewFrame()
            }

            SetupKeyboard(io, Keyboard.current);                                // sets key mapping, text input, and IME

            return true;
        }

        public void Shutdown(ImGuiIOPtr io)
        {
            _callbacks.Unset(io);
            io.SetBackendPlatformName(null);

            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        public void PrepareFrame(ImGuiIOPtr io, Rect displayRect)
        {
            Assert.IsTrue(io.Fonts.IsBuilt(), "Font atlas not built! Generally built by the renderer. Missing call to renderer NewFrame() function?");

            io.DisplaySize = new Vector2(displayRect.width, displayRect.height);// setup display size (every frame to accommodate for window resizing)
            // TODO: dpi aware, scale, etc

            io.DeltaTime = Time.unscaledDeltaTime;                              // setup timestep

            // input
            UpdateKeyboard(io, Keyboard.current);                               // update keyboard state
            UpdateMouse(io, Mouse.current);                                     // update mouse state
            UpdateCursor(io, ImGui.GetMouseCursor());                           // update Unity cursor with the cursor requested by ImGui
            UpdateGamepad(io, Gamepad.current);                                 // update game controllers (if enabled and available)

            // ini settings
            if (_iniSettings != null && io.WantSaveIniSettings)
            {
                _iniSettings.Save(ImGui.SaveIniSettingsToMemory());
                io.WantSaveIniSettings = false;
            }
        }

        void SetupKeyboard(ImGuiIOPtr io, Keyboard kb)
        {
            if (_keyboard != null)
            {
                for (int i = 0; i < (int)ImGuiKey.COUNT; ++i)
                    io.KeyMap[i] = -1;
                _keyboard.onTextInput -= _textInput.Add;
            }
            _keyboard = kb;

            _mainKeys = new int[] {
                // map and store new keys by assigning io.KeyMap and setting value of array
                io.KeyMap[(int)ImGuiKey.Tab        ] = (int)Key.Tab,
                io.KeyMap[(int)ImGuiKey.LeftArrow  ] = (int)Key.LeftArrow,
                io.KeyMap[(int)ImGuiKey.RightArrow ] = (int)Key.RightArrow,
                io.KeyMap[(int)ImGuiKey.UpArrow    ] = (int)Key.UpArrow,
                io.KeyMap[(int)ImGuiKey.DownArrow  ] = (int)Key.DownArrow,
                io.KeyMap[(int)ImGuiKey.PageUp     ] = (int)Key.PageUp,
                io.KeyMap[(int)ImGuiKey.PageDown   ] = (int)Key.PageDown,
                io.KeyMap[(int)ImGuiKey.Home       ] = (int)Key.Home,
                io.KeyMap[(int)ImGuiKey.End        ] = (int)Key.End,
                io.KeyMap[(int)ImGuiKey.Insert     ] = (int)Key.Insert,
                io.KeyMap[(int)ImGuiKey.Delete     ] = (int)Key.Delete,
                io.KeyMap[(int)ImGuiKey.Backspace  ] = (int)Key.Backspace,
                io.KeyMap[(int)ImGuiKey.Space      ] = (int)Key.Space,
                io.KeyMap[(int)ImGuiKey.Enter      ] = (int)Key.Enter,
                io.KeyMap[(int)ImGuiKey.Escape     ] = (int)Key.Escape,
                io.KeyMap[(int)ImGuiKey.KeyPadEnter] = (int)Key.NumpadEnter,
                // letter keys mapped by display name to avoid being layout agnostic (used as shortcuts)
                io.KeyMap[(int)ImGuiKey.A          ] = (int)((KeyControl)kb["#(a)"]).keyCode, // for text edit CTRL+A: select all
                io.KeyMap[(int)ImGuiKey.C          ] = (int)((KeyControl)kb["#(c)"]).keyCode, // for text edit CTRL+C: copy
                io.KeyMap[(int)ImGuiKey.V          ] = (int)((KeyControl)kb["#(v)"]).keyCode, // for text edit CTRL+V: paste
                io.KeyMap[(int)ImGuiKey.X          ] = (int)((KeyControl)kb["#(x)"]).keyCode, // for text edit CTRL+X: cut
                io.KeyMap[(int)ImGuiKey.Y          ] = (int)((KeyControl)kb["#(y)"]).keyCode, // for text edit CTRL+Y: redo
                io.KeyMap[(int)ImGuiKey.Z          ] = (int)((KeyControl)kb["#(z)"]).keyCode, // for text edit CTRL+Z: undo
            };
            _keyboard.onTextInput += _textInput.Add;
        }

        void UpdateKeyboard(ImGuiIOPtr io, Keyboard keyboard)
        {
            if (keyboard == null)
                return;

            // main keys
            foreach (var key in _mainKeys)
                io.KeysDown[key] = keyboard[(Key)key].isPressed;

            // keyboard modifiers
            io.KeyShift = keyboard[Key.LeftShift].isPressed || keyboard[Key.RightShift].isPressed;
            io.KeyCtrl  = keyboard[Key.LeftCtrl ].isPressed || keyboard[Key.RightCtrl ].isPressed;
            io.KeyAlt   = keyboard[Key.LeftAlt  ].isPressed || keyboard[Key.RightAlt  ].isPressed;
            io.KeySuper = keyboard[Key.LeftMeta ].isPressed || keyboard[Key.RightMeta ].isPressed;

            // text input
            for (int i = 0, iMax = _textInput.Count; i < iMax; ++i)
                io.AddInputCharacter(_textInput[i]);
            _textInput.Clear();
        }

        static void UpdateMouse(ImGuiIOPtr io, Mouse mouse)
        {
            if (mouse == null)
                return;

            if (io.WantSetMousePos) // set Unity mouse position if requested
                mouse.WarpCursorPosition(ImGuiUn.ImGuiToScreen(io.MousePos));

            io.MousePos = ImGuiUn.ScreenToImGui(mouse.position.ReadValue());

            Vector2 mouseScroll = mouse.scroll.ReadValue() / 120f;
            io.MouseWheel   = mouseScroll.y;
            io.MouseWheelH  = mouseScroll.x;

            io.MouseDown[0] = mouse.leftButton.isPressed;
            io.MouseDown[1] = mouse.rightButton.isPressed;
            io.MouseDown[2] = mouse.middleButton.isPressed;
        }

        static void UpdateGamepad(ImGuiIOPtr io, Gamepad gamepad)
        {
            io.BackendFlags = gamepad == null
                ? io.BackendFlags & ~ImGuiBackendFlags.HasGamepad
                : io.BackendFlags |  ImGuiBackendFlags.HasGamepad;

            if (gamepad == null || (io.ConfigFlags & ImGuiConfigFlags.NavEnableGamepad) == 0)
                return;

            io.NavInputs[(int)ImGuiNavInput.Activate   ] = gamepad.buttonSouth.ReadValue();     // A / Cross
            io.NavInputs[(int)ImGuiNavInput.Cancel     ] = gamepad.buttonEast.ReadValue();      // B / Circle
            io.NavInputs[(int)ImGuiNavInput.Menu       ] = gamepad.buttonWest.ReadValue();      // X / Square
            io.NavInputs[(int)ImGuiNavInput.Input      ] = gamepad.buttonNorth.ReadValue();     // Y / Triangle
            io.NavInputs[(int)ImGuiNavInput.DpadLeft   ] = gamepad.dpad.left.ReadValue();       // D-Pad Left
            io.NavInputs[(int)ImGuiNavInput.DpadRight  ] = gamepad.dpad.right.ReadValue();      // D-Pad Right
            io.NavInputs[(int)ImGuiNavInput.DpadUp     ] = gamepad.dpad.up.ReadValue();         // D-Pad Up
            io.NavInputs[(int)ImGuiNavInput.DpadDown   ] = gamepad.dpad.down.ReadValue();       // D-Pad Down
            io.NavInputs[(int)ImGuiNavInput.FocusPrev  ] = gamepad.leftShoulder.ReadValue();    // LB / L1
            io.NavInputs[(int)ImGuiNavInput.FocusNext  ] = gamepad.rightShoulder.ReadValue();   // RB / R1
            io.NavInputs[(int)ImGuiNavInput.TweakSlow  ] = gamepad.leftShoulder.ReadValue();    // LB / L1
            io.NavInputs[(int)ImGuiNavInput.TweakFast  ] = gamepad.rightShoulder.ReadValue();   // RB / R1
            io.NavInputs[(int)ImGuiNavInput.LStickLeft ] = gamepad.leftStick.left.ReadValue();
            io.NavInputs[(int)ImGuiNavInput.LStickRight] = gamepad.leftStick.right.ReadValue();
            io.NavInputs[(int)ImGuiNavInput.LStickUp   ] = gamepad.leftStick.up.ReadValue();
            io.NavInputs[(int)ImGuiNavInput.LStickDown ] = gamepad.leftStick.down.ReadValue();
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

        void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (device is Keyboard kb)
            {
                if (change == InputDeviceChange.ConfigurationChanged)           // keyboard layout change, remap main keys
                    SetupKeyboard(ImGui.GetIO(), kb);
                if (Keyboard.current != _keyboard)                              // keyboard device changed, setup again
                    SetupKeyboard(ImGui.GetIO(), Keyboard.current);
            }
        }
    }
}
#endif

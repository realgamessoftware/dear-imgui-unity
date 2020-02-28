using UnityEngine;

namespace ImGuiNET.Unity
{
    [System.Serializable]
    struct IOConfig
    {
        [Tooltip("Enable keyboard navigation.")]
        public bool KeyboardNavigation;

        [Tooltip("Enable gamepad navigation.")]
        public bool GamepadNavigation;

        [Tooltip("Instruct navigation to move the move the mouse cursor.")]
        public bool NavSetMousePos;

        [Tooltip("Instruct navigation to not set the io.WantCaptureKeyboard when io.NavActive is set.")]
        public bool NavNoCaptureKeyboard;

        [Tooltip("Time for a double-click, in seconds.")]
        public float DoubleClickTime;

        [Tooltip("Distance threshold to stay in to validate a double-click, in pixels.")]
        public float DoubleClickMaxDist;

        [Tooltip("Distance threshold before considering we are dragging.")]
        public float DragThreshold;

        [Tooltip("When holding a key/button, time before it starts repeating, in seconds.")]
        public float KeyRepeatDelay;

        [Tooltip("When holding a key/button, rate at which it repeats, in seconds.")]
        public float KeyRepeatRate;

        [Tooltip("Global scale all fonts.")]
        public float FontGlobalScale;

        [Tooltip("Allow user scaling text of individual window with CTRL+Wheel.")]
        public bool FontAllowUserScaling;

        [Tooltip("Set to false to disable blinking cursor.")]
        public bool TextCursorBlink;

        [Tooltip("Enable resizing from the edges and from the lower-left corner.")]
        public bool ResizeFromEdges;

        [Tooltip("[BETA] Set to true to only allow moving windows when clicked+dragged from the title bar. Windows without a title bar are not affected.")]
        public bool MoveFromTitleOnly;

        [Tooltip("[BETA] Compact window memory usage when unused. Set to -1.0f to disable.")]
        public float MemoryCompactTimer;

        public void SetDefaults()
        {
            var context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);
            SetFrom(ImGui.GetIO());
            ImGui.DestroyContext(context);
        }

        public void ApplyTo(ImGuiIOPtr io)
        {
            io.ConfigFlags = KeyboardNavigation ? io.ConfigFlags | ImGuiConfigFlags.NavEnableKeyboard : io.ConfigFlags & ~ImGuiConfigFlags.NavEnableKeyboard;
            io.ConfigFlags = GamepadNavigation ? io.ConfigFlags | ImGuiConfigFlags.NavEnableGamepad : io.ConfigFlags & ~ImGuiConfigFlags.NavEnableGamepad;
            io.ConfigFlags = NavSetMousePos ? io.ConfigFlags | ImGuiConfigFlags.NavEnableSetMousePos : io.ConfigFlags & ~ImGuiConfigFlags.NavEnableSetMousePos;
            io.ConfigFlags = NavNoCaptureKeyboard ? io.ConfigFlags | ImGuiConfigFlags.NavNoCaptureKeyboard : io.ConfigFlags & ~ImGuiConfigFlags.NavNoCaptureKeyboard;
            io.MouseDoubleClickTime = DoubleClickTime;
            io.MouseDoubleClickMaxDist = DoubleClickMaxDist;
            io.MouseDragThreshold = DragThreshold;
            io.KeyRepeatDelay = KeyRepeatDelay;
            io.KeyRepeatRate = KeyRepeatRate;
            io.FontGlobalScale = FontGlobalScale;
            io.FontAllowUserScaling = FontAllowUserScaling;
            io.ConfigInputTextCursorBlink = TextCursorBlink;
            io.ConfigWindowsResizeFromEdges = ResizeFromEdges;
            io.ConfigWindowsMoveFromTitleBarOnly = MoveFromTitleOnly;
            io.ConfigWindowsMemoryCompactTimer = MemoryCompactTimer;
        }

        public void SetFrom(ImGuiIOPtr io)
        {
            KeyboardNavigation = (io.ConfigFlags & ImGuiConfigFlags.NavEnableKeyboard) != 0;
            GamepadNavigation = (io.ConfigFlags & ImGuiConfigFlags.NavEnableGamepad) != 0;
            NavSetMousePos = (io.ConfigFlags & ImGuiConfigFlags.NavEnableSetMousePos) != 0;
            NavNoCaptureKeyboard = (io.ConfigFlags & ImGuiConfigFlags.NavNoCaptureKeyboard) != 0;
            DoubleClickTime = io.MouseDoubleClickTime;
            DoubleClickMaxDist = io.MouseDoubleClickMaxDist;
            DragThreshold = io.MouseDragThreshold;
            KeyRepeatDelay = io.KeyRepeatDelay;
            KeyRepeatRate = io.KeyRepeatRate;
            FontGlobalScale = io.FontGlobalScale;
            FontAllowUserScaling = io.FontAllowUserScaling;
            TextCursorBlink = io.ConfigInputTextCursorBlink;
            ResizeFromEdges = io.ConfigWindowsResizeFromEdges;
            MoveFromTitleOnly = io.ConfigWindowsMoveFromTitleBarOnly;
            MemoryCompactTimer = io.ConfigWindowsMemoryCompactTimer;
        }
    }
}

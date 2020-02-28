using UnityEngine;

namespace ImGuiNET.Unity
{
    [CreateAssetMenu(menuName = "Dear ImGui/Style")]
    sealed class StyleAsset : ScriptableObject
    {
        [Tooltip("Global alpha applies to everything in ImGui.")]
        public float Alpha;

        [Tooltip("Padding within a window.")]
        public Vector2 WindowPadding;

        [Tooltip("Radius of window corners rounding. Set to 0.0f to have rectangular windows.")]
        public float WindowRounding;

        [Tooltip("Thickness of border around windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float WindowBorderSize;

        [Tooltip("Minimum window size. This is a global setting. If you want to constraint individual windows, use SetNextWindowSizeConstraints().")]
        public Vector2 WindowMinSize;

        [Tooltip("Alignment for title bar text. Defaults to (0.0f,0.5f) for left-aligned,vertically centered.")]
        public Vector2 WindowTitleAlign;

        [Tooltip("Side of the collapsing/docking button in the title bar (left/right). Defaults to ImGuiDir_Left.")]
        public ImGuiDir WindowMenuButtonPosition;

        [Tooltip("Radius of child window corners rounding. Set to 0.0f to have rectangular windows.")]
        public float ChildRounding;

        [Tooltip("Thickness of border around child windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float ChildBorderSize;

        [Tooltip("Radius of popup window corners rounding. (Note that tooltip windows use WindowRounding)")]
        public float PopupRounding;

        [Tooltip("Thickness of border around popup/tooltip windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float PopupBorderSize;

        [Tooltip("Padding within a framed rectangle (used by most widgets).")]
        public Vector2 FramePadding;

        [Tooltip("Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets).")]
        public float FrameRounding;

        [Tooltip("Thickness of border around frames. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float FrameBorderSize;

        [Tooltip("Horizontal and vertical spacing between widgets/lines.")]
        public Vector2 ItemSpacing;

        [Tooltip("Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).")]
        public Vector2 ItemInnerSpacing;

        [Tooltip("Expand reactive bounding box for touch-based system where touch position is not accurate enough. Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. So don't grow this too much!")]
        public Vector2 TouchExtraPadding;

        [Tooltip("Horizontal indentation when e.g. entering a tree node. Generally == (FontSize + FramePadding.x*2).")]
        public float IndentSpacing;

        [Tooltip("Minimum horizontal spacing between two columns. Preferably > (FramePadding.x + 1).")]
        public float ColumnsMinSpacing;

        [Tooltip("Width of the vertical scrollbar, Height of the horizontal scrollbar.")]
        public float ScrollbarSize;

        [Tooltip("Radius of grab corners for scrollbar.")]
        public float ScrollbarRounding;

        [Tooltip("Minimum width/height of a grab box for slider/scrollbar.")]
        public float GrabMinSize;

        [Tooltip("Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.")]
        public float GrabRounding;

        [Tooltip("Radius of upper corners of a tab. Set to 0.0f to have rectangular tabs.")]
        public float TabRounding;

        [Tooltip("Thickness of border around tabs.")]
        public float TabBorderSize;

        [Tooltip("Side of the color button in the ColorEdit4 widget (left/right). Defaults to ImGuiDir_Right.")]
        public ImGuiDir ColorButtonPosition;

        [Tooltip("Alignment of button text when button is larger than text. Defaults to (0.5f, 0.5f) (centered).")]
        public Vector2 ButtonTextAlign;

        [Tooltip("Alignment of selectable text when selectable is larger than text. Defaults to (0.0f, 0.0f) (top-left aligned).")]
        public Vector2 SelectableTextAlign;

        [Tooltip("Window position are clamped to be visible within the display area by at least this amount. Only applies to regular windows.")]
        public Vector2 DisplayWindowPadding;

        [Tooltip("If you cannot see the edges of your screen (e.g. on a TV) increase the safe area padding. Apply to popups/tooltips as well regular windows. NB: Prefer configuring your TV sets correctly!")]
        public Vector2 DisplaySafeAreaPadding;

        [Tooltip("Scale software rendered mouse cursor (when io.MouseDrawCursor is enabled). May be removed later.")]
        public float MouseCursorScale;

        [Tooltip("Enable anti-aliasing on lines/borders. Disable if you are really tight on CPU/GPU.")]
        public bool AntiAliasedLines;

        [Tooltip("Enable anti-aliasing on filled shapes (rounded rectangles, circles, etc.)")]
        public bool AntiAliasedFill;

        [Tooltip("Tessellation tolerance when using PathBezierCurveTo() without a specific number of segments. Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.")]
        public float CurveTessellationTol;

        [Tooltip("Maximum error (in pixels) allowed when using AddCircle()/AddCircleFilled() or drawing rounded corner rectangles with no explicit segment count specified. Decrease for higher quality but more geometry.")]
        public float CircleSegmentMaxError;

        [HideInInspector]
        public Color[] Colors = new Color[(int)ImGuiCol.COUNT];

        public unsafe void ApplyTo(ImGuiStylePtr s)
        {
            s.Alpha                  = Alpha;
            s.WindowPadding          = WindowPadding;
            s.WindowRounding         = WindowRounding;
            s.WindowBorderSize       = WindowBorderSize;
            s.WindowMinSize          = WindowMinSize;
            s.WindowTitleAlign       = WindowTitleAlign;
            s.WindowMenuButtonPosition = WindowMenuButtonPosition;
            s.ChildRounding          = ChildRounding;
            s.ChildBorderSize        = ChildBorderSize;
            s.PopupRounding          = PopupRounding;
            s.PopupBorderSize        = PopupBorderSize;
            s.FramePadding           = FramePadding;
            s.FrameRounding          = FrameRounding;
            s.FrameBorderSize        = FrameBorderSize;
            s.ItemSpacing            = ItemSpacing;
            s.ItemInnerSpacing       = ItemInnerSpacing;
            s.TouchExtraPadding      = TouchExtraPadding;
            s.IndentSpacing          = IndentSpacing;
            s.ColumnsMinSpacing      = ColumnsMinSpacing;
            s.ScrollbarSize          = ScrollbarSize;
            s.ScrollbarRounding      = ScrollbarRounding;
            s.GrabMinSize            = GrabMinSize;
            s.GrabRounding           = GrabRounding;
            s.TabRounding            = TabRounding;
            s.TabBorderSize          = TabBorderSize;
            s.ColorButtonPosition    = ColorButtonPosition;
            s.ButtonTextAlign        = ButtonTextAlign;
            s.SelectableTextAlign    = SelectableTextAlign;
            s.DisplayWindowPadding   = DisplayWindowPadding;
            s.DisplaySafeAreaPadding = DisplaySafeAreaPadding;
            s.MouseCursorScale       = MouseCursorScale;
            s.AntiAliasedLines       = AntiAliasedLines;
            s.AntiAliasedFill        = AntiAliasedFill;
            s.CurveTessellationTol   = CurveTessellationTol;
            s.CircleSegmentMaxError  = CircleSegmentMaxError;
            for (var i = 0; i < Colors.Length; ++i)
                s.Colors[i] = Colors[i];
        }

        public unsafe void SetFrom(ImGuiStylePtr s)
        {
            Alpha                  = s.Alpha;
            WindowPadding          = s.WindowPadding;
            WindowRounding         = s.WindowRounding;
            WindowBorderSize       = s.WindowBorderSize;
            WindowMinSize          = s.WindowMinSize;
            WindowTitleAlign       = s.WindowTitleAlign;
            WindowMenuButtonPosition = s.WindowMenuButtonPosition;
            ChildRounding          = s.ChildRounding;
            ChildBorderSize        = s.ChildBorderSize;
            PopupRounding          = s.PopupRounding;
            PopupBorderSize        = s.PopupBorderSize;
            FramePadding           = s.FramePadding;
            FrameRounding          = s.FrameRounding;
            FrameBorderSize        = s.FrameBorderSize;
            ItemSpacing            = s.ItemSpacing;
            ItemInnerSpacing       = s.ItemInnerSpacing;
            TouchExtraPadding      = s.TouchExtraPadding;
            IndentSpacing          = s.IndentSpacing;
            ColumnsMinSpacing      = s.ColumnsMinSpacing;
            ScrollbarSize          = s.ScrollbarSize;
            ScrollbarRounding      = s.ScrollbarRounding;
            GrabMinSize            = s.GrabMinSize;
            GrabRounding           = s.GrabRounding;
            TabRounding            = s.TabRounding;
            TabBorderSize          = s.TabBorderSize;
            ColorButtonPosition    = s.ColorButtonPosition;
            ButtonTextAlign        = s.ButtonTextAlign;
            SelectableTextAlign    = s.SelectableTextAlign;
            DisplayWindowPadding   = s.DisplayWindowPadding;
            DisplaySafeAreaPadding = s.DisplaySafeAreaPadding;
            MouseCursorScale       = s.MouseCursorScale;
            AntiAliasedLines       = s.AntiAliasedLines;
            AntiAliasedFill        = s.AntiAliasedFill;
            CurveTessellationTol   = s.CurveTessellationTol;
            CircleSegmentMaxError  = s.CircleSegmentMaxError;
            for (var i = 0; i < Colors.Length; ++i)
                Colors[i] = s.Colors[i];
        }

        void Reset()
        {
            var context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);
            SetFrom(ImGui.GetStyle());
            ImGui.DestroyContext(context);
        }
    }
}

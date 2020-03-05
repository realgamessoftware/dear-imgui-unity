using System.Runtime.CompilerServices;
using UnityEngine;

namespace ImGuiNET
{
    // ImGui extra functionality related with integrating with Unity
    public static partial class ImGuiUn
    {
        // convert from ImGui coordinates (origin at top left) to unity's screen coordinates (origin at bottom left)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ScreenToImGui(in Vector2 point)
        {
            return new Vector2(point.x, ImGui.GetIO().DisplaySize.y - point.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ImGuiToScreen(in Vector2 point)
        {
            return new Vector2(point.x, ImGui.GetIO().DisplaySize.y - point.y);
        }
    }
}

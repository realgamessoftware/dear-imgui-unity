using UnityEngine;

namespace ImGuiNET.Unity
{
    /// <summary>
    /// Platform bindings for ImGui in Unity in charge of: mouse/keyboard/gamepad inputs, cursor shape, timing, windowing.
    /// </summary>
    interface IImGuiPlatform
    {
        bool Initialize(ImGuiIOPtr io);
        void Shutdown(ImGuiIOPtr io);
        void PrepareFrame(ImGuiIOPtr io, Rect displayRect);
    }
}

using UnityEngine.Rendering;

namespace ImGuiNET.Unity
{
    /// <summary>
    /// Renderer bindings in charge of producing instructions for rendering ImGui draw data.
    /// </summary>
    interface IImGuiRenderer
    {
        void Initialize(ImGuiIOPtr io);
        void Shutdown(ImGuiIOPtr io);
        void RenderDrawLists(CommandBuffer cmd, ImDrawDataPtr drawData);
    }
}

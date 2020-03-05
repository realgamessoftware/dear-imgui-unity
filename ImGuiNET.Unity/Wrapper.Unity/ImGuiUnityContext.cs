using System;
using UnityEngine;
using ImGuiNET.Unity;

namespace ImGuiNET
{
    sealed class ImGuiUnityContext
    {
        public IntPtr state;            // ImGui internal state
        public TextureManager textures; // texture / font state
    }

    public static unsafe partial class ImGuiUn
    {
        // layout
        public static event Action Layout;    // global/default Layout event, each DearImGui instance also has a private one
        internal static void DoLayout() => Layout?.Invoke();

        // textures
        public static int GetTextureId(Texture texture) => s_currentUnityContext?.textures.GetTextureId(texture) ?? -1;
        internal static SpriteInfo GetSpriteInfo(Sprite sprite) => s_currentUnityContext?.textures.GetSpriteInfo(sprite) ?? null;

        internal static ImGuiUnityContext s_currentUnityContext;

        internal static ImGuiUnityContext CreateUnityContext()
        {
            return new ImGuiUnityContext
            {
                state = ImGui.CreateContext(),
                textures = new TextureManager(),
            };
        }

        internal static void DestroyUnityContext(ImGuiUnityContext context)
        {
            ImGui.DestroyContext(context.state);
        }

        internal static void SetUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = context;
            ImGui.SetCurrentContext(context?.state ?? IntPtr.Zero);
        }
    }
}

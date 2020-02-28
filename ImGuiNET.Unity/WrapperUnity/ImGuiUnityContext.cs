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
        public static event Action OnLayout;    // global/default Layout event, each ImGuiUnity instance also has a private one
        internal static void Layout() => OnLayout?.Invoke(); // expose Layout to ImGuiUnity without removing protection from 'event'

        // textures
        public static int RegisterTexture(Texture texture) => s_currentUnityContext?.textures.RegisterTexture(texture) ?? -1;
        public static void DeregisterTexture(int id) => s_currentUnityContext?.textures.DeregisterTexture(id);
        public static int GetTextureID(Texture texture) => s_currentUnityContext?.textures.GetTextureID(texture) ?? -1;
        static SpriteInfo GetSpriteInfo(Sprite sprite) => s_currentUnityContext?.textures.GetSpriteInfo(sprite) ?? null;

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

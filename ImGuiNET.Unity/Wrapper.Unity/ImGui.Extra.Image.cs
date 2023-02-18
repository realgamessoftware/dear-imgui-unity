using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using ImGuiNET.Unity;

namespace ImGuiNET
{
    // ImGui extra functionality related with Images
    public static partial class ImGuiUn
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Texture tex)
        {
            ImGui.Image((IntPtr)GetTextureId(tex), new Vector2(tex.width, tex.height));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Texture tex, Vector2 size)
        {
            ImGui.Image((IntPtr)GetTextureId(tex), size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Sprite sprite)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.Image((IntPtr)GetTextureId(info.texture), info.size, info.uv0, info.uv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Sprite sprite, Vector2 size)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.Image((IntPtr)GetTextureId(info.texture), size, info.uv0, info.uv1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ImageButton(Texture tex)
        {
            return ImGui.ImageButton((IntPtr)GetTextureId(tex), new Vector2(tex.width, tex.height));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ImageButton(Texture tex, Vector2 size)
        {
            return ImGui.ImageButton((IntPtr)GetTextureId(tex), size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ImageButton(Sprite sprite)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            return ImGui.ImageButton((IntPtr)GetTextureId(info.texture), info.size, info.uv0, info.uv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ImageButton(Sprite sprite, Vector2 size)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            return ImGui.ImageButton((IntPtr)GetTextureId(info.texture), size, info.uv0, info.uv1);
        }
    }
}

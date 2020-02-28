using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using ImGuiNET.Unity;

namespace ImGuiNET
{
    // ImGui extra functionality related with Images
    public static partial class ImGuiUn
    {
        /// <summary>Corrects UV from Unity textures (inverts y coordinate)</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector2 UVFix(in Vector2 uv) => new Vector2(uv.x, 1f - uv.y);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Texture tex)
            => ImGui.Image((IntPtr)GetTextureID(tex), new Vector2(tex.width, tex.height));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Texture tex, Vector2 size)
            => ImGui.Image((IntPtr)GetTextureID(tex), size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Sprite sprite)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.Image((IntPtr)GetTextureID(info.texture), info.size, info.uv0, info.uv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Sprite sprite, Vector2 size)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.Image((IntPtr)GetTextureID(info.texture), size, info.uv0, info.uv1);
            // Image(GetTextureID(sprite.texture), sprite.rect.size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImageButton(Texture tex)
            => ImGui.ImageButton((IntPtr)GetTextureID(tex), new Vector2(tex.width, tex.height));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImageButton(Texture tex, Vector2 size)
            => ImGui.ImageButton((IntPtr)GetTextureID(tex), size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImageButton(Sprite sprite)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.ImageButton((IntPtr)GetTextureID(info.texture), info.size, info.uv0, info.uv1);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ImageButton(Sprite sprite, Vector2 size)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.ImageButton((IntPtr)GetTextureID(info.texture), size, info.uv0, info.uv1);
        }
    }
}

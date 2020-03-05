using System.Runtime.CompilerServices;
using UnityEngine;

namespace ImGuiNET
{
    public static class ColorExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Color32 ToColor32(this uint rgba)
        {
            return Unsafe.AsRef<Color32>(&rgba);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Color ToColor(this uint rgba)
        {
            return Unsafe.AsRef<Color32>(&rgba); // implicit conversion to Color
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint ToUint(this Color32 c32)
        {
            return Unsafe.AsRef<uint>(&c32);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint ToUint(this Color color)
        {
            return ((Color32)color).ToUint();
        }
    }
}

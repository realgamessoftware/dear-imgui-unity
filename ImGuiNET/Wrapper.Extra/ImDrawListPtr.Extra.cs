using System;
using System.Runtime.CompilerServices;

namespace ImGuiNET
{
    // TODO: add to the Generator for all wrapper Ptr structs
    public unsafe partial struct ImDrawListPtr : IEquatable<ImDrawListPtr>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ImDrawListPtr other) => NativePtr == other.NativePtr;
    }
}

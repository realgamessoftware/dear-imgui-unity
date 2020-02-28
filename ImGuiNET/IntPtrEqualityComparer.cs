using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ImGuiNET
{
    // Avoid boxing IntPtr in collections.
    // IntPtr does not implement IEquatable<IntPtr> yet in 2019.3 Mono bleeding edge.
    // https://github.com/Unity-Technologies/mono/blob/unity-2019.3-mbe/mcs/class/corlib/System/IntPtr.cs

    public class IntPtrEqualityComparer : IEqualityComparer<IntPtr>
    {
        public static IntPtrEqualityComparer Instance { get; } = new IntPtrEqualityComparer();

        IntPtrEqualityComparer() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IntPtr p1, IntPtr p2) => p1 == p2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(IntPtr ptr) => ptr.GetHashCode();
    }
}

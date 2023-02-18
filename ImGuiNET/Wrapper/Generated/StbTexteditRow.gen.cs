using System;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using System.Text;

namespace ImGuiNET
{
    public unsafe partial struct StbTexteditRow
    {
        public float x0;
        public float x1;
        public float baseline_y_delta;
        public float ymin;
        public float ymax;
        public int num_chars;
    }
    public unsafe partial struct StbTexteditRowPtr
    {
        public StbTexteditRow* NativePtr { get; }
        public StbTexteditRowPtr(StbTexteditRow* nativePtr) => NativePtr = nativePtr;
        public StbTexteditRowPtr(IntPtr nativePtr) => NativePtr = (StbTexteditRow*)nativePtr;
        public static implicit operator StbTexteditRowPtr(StbTexteditRow* nativePtr) => new StbTexteditRowPtr(nativePtr);
        public static implicit operator StbTexteditRow* (StbTexteditRowPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator StbTexteditRowPtr(IntPtr nativePtr) => new StbTexteditRowPtr(nativePtr);
        public ref float x0 => ref UnsafeUtility.AsRef<float>(&NativePtr->x0);
        public ref float x1 => ref UnsafeUtility.AsRef<float>(&NativePtr->x1);
        public ref float baseline_y_delta => ref UnsafeUtility.AsRef<float>(&NativePtr->baseline_y_delta);
        public ref float ymin => ref UnsafeUtility.AsRef<float>(&NativePtr->ymin);
        public ref float ymax => ref UnsafeUtility.AsRef<float>(&NativePtr->ymax);
        public ref int num_chars => ref UnsafeUtility.AsRef<int>(&NativePtr->num_chars);
    }
}

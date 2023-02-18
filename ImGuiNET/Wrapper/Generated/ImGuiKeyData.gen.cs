using System;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using System.Text;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiKeyData
    {
        public byte Down;
        public float DownDuration;
        public float DownDurationPrev;
        public float AnalogValue;
    }
    public unsafe partial struct ImGuiKeyDataPtr
    {
        public ImGuiKeyData* NativePtr { get; }
        public ImGuiKeyDataPtr(ImGuiKeyData* nativePtr) => NativePtr = nativePtr;
        public ImGuiKeyDataPtr(IntPtr nativePtr) => NativePtr = (ImGuiKeyData*)nativePtr;
        public static implicit operator ImGuiKeyDataPtr(ImGuiKeyData* nativePtr) => new ImGuiKeyDataPtr(nativePtr);
        public static implicit operator ImGuiKeyData* (ImGuiKeyDataPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiKeyDataPtr(IntPtr nativePtr) => new ImGuiKeyDataPtr(nativePtr);
        public ref bool Down => ref UnsafeUtility.AsRef<bool>(&NativePtr->Down);
        public ref float DownDuration => ref UnsafeUtility.AsRef<float>(&NativePtr->DownDuration);
        public ref float DownDurationPrev => ref UnsafeUtility.AsRef<float>(&NativePtr->DownDurationPrev);
        public ref float AnalogValue => ref UnsafeUtility.AsRef<float>(&NativePtr->AnalogValue);
    }
}

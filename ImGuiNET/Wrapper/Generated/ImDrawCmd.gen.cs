using System;
using System.Text;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImDrawCmd
    {
        public uint ElemCount;
        public Vector4 ClipRect;
        public IntPtr TextureId;
        public uint VtxOffset;
        public uint IdxOffset;
        public IntPtr UserCallback;
        public void* UserCallbackData;
    }
    public unsafe partial struct ImDrawCmdPtr
    {
        public ImDrawCmd* NativePtr { get; }
        public ImDrawCmdPtr(ImDrawCmd* nativePtr) => NativePtr = nativePtr;
        public ImDrawCmdPtr(IntPtr nativePtr) => NativePtr = (ImDrawCmd*)nativePtr;
        public static implicit operator ImDrawCmdPtr(ImDrawCmd* nativePtr) => new ImDrawCmdPtr(nativePtr);
        public static implicit operator ImDrawCmd* (ImDrawCmdPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImDrawCmdPtr(IntPtr nativePtr) => new ImDrawCmdPtr(nativePtr);
        public ref uint ElemCount => ref UnsafeUtility.AsRef<uint>(&NativePtr->ElemCount);
        public ref Vector4 ClipRect => ref UnsafeUtility.AsRef<Vector4>(&NativePtr->ClipRect);
        public ref IntPtr TextureId => ref UnsafeUtility.AsRef<IntPtr>(&NativePtr->TextureId);
        public ref uint VtxOffset => ref UnsafeUtility.AsRef<uint>(&NativePtr->VtxOffset);
        public ref uint IdxOffset => ref UnsafeUtility.AsRef<uint>(&NativePtr->IdxOffset);
        public ref IntPtr UserCallback => ref UnsafeUtility.AsRef<IntPtr>(&NativePtr->UserCallback);
        public IntPtr UserCallbackData { get => (IntPtr)NativePtr->UserCallbackData; set => NativePtr->UserCallbackData = (void*)value; }
        public void Destroy()
        {
            ImGuiNative.ImDrawCmd_destroy(NativePtr);
        }
    }
}

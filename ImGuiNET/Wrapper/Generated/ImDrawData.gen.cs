using System;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using System.Text;

namespace ImGuiNET
{
    public unsafe partial struct ImDrawData
    {
        public byte Valid;
        public int CmdListsCount;
        public int TotalIdxCount;
        public int TotalVtxCount;
        public ImDrawList** CmdLists;
        public Vector2 DisplayPos;
        public Vector2 DisplaySize;
        public Vector2 FramebufferScale;
        public ImGuiViewport* OwnerViewport;
    }
    public unsafe partial struct ImDrawDataPtr
    {
        public ImDrawData* NativePtr { get; }
        public ImDrawDataPtr(ImDrawData* nativePtr) => NativePtr = nativePtr;
        public ImDrawDataPtr(IntPtr nativePtr) => NativePtr = (ImDrawData*)nativePtr;
        public static implicit operator ImDrawDataPtr(ImDrawData* nativePtr) => new ImDrawDataPtr(nativePtr);
        public static implicit operator ImDrawData* (ImDrawDataPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImDrawDataPtr(IntPtr nativePtr) => new ImDrawDataPtr(nativePtr);
        public ref bool Valid => ref UnsafeUtility.AsRef<bool>(&NativePtr->Valid);
        public ref int CmdListsCount => ref UnsafeUtility.AsRef<int>(&NativePtr->CmdListsCount);
        public ref int TotalIdxCount => ref UnsafeUtility.AsRef<int>(&NativePtr->TotalIdxCount);
        public ref int TotalVtxCount => ref UnsafeUtility.AsRef<int>(&NativePtr->TotalVtxCount);
        public IntPtr CmdLists { get => (IntPtr)NativePtr->CmdLists; set => NativePtr->CmdLists = (ImDrawList**)value; }
        public ref Vector2 DisplayPos => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DisplayPos);
        public ref Vector2 DisplaySize => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DisplaySize);
        public ref Vector2 FramebufferScale => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->FramebufferScale);
        public ImGuiViewportPtr OwnerViewport => new ImGuiViewportPtr(NativePtr->OwnerViewport);
        public void Clear()
        {
            ImGuiNative.ImDrawData_Clear((ImDrawData*)(NativePtr));
        }
        public void DeIndexAllBuffers()
        {
            ImGuiNative.ImDrawData_DeIndexAllBuffers((ImDrawData*)(NativePtr));
        }
        public void Destroy()
        {
            ImGuiNative.ImDrawData_destroy((ImDrawData*)(NativePtr));
        }
        public void ScaleClipRects(Vector2 fb_scale)
        {
            ImGuiNative.ImDrawData_ScaleClipRects((ImDrawData*)(NativePtr), fb_scale);
        }
    }
}

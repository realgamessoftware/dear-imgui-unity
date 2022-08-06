using System;
using System.Text;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiSizeCallbackData
    {
        public void* UserData;
        public Vector2 Pos;
        public Vector2 CurrentSize;
        public Vector2 DesiredSize;
    }
    public unsafe partial struct ImGuiSizeCallbackDataPtr
    {
        public ImGuiSizeCallbackData* NativePtr { get; }
        public ImGuiSizeCallbackDataPtr(ImGuiSizeCallbackData* nativePtr) => NativePtr = nativePtr;
        public ImGuiSizeCallbackDataPtr(IntPtr nativePtr) => NativePtr = (ImGuiSizeCallbackData*)nativePtr;
        public static implicit operator ImGuiSizeCallbackDataPtr(ImGuiSizeCallbackData* nativePtr) => new ImGuiSizeCallbackDataPtr(nativePtr);
        public static implicit operator ImGuiSizeCallbackData* (ImGuiSizeCallbackDataPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiSizeCallbackDataPtr(IntPtr nativePtr) => new ImGuiSizeCallbackDataPtr(nativePtr);
        public IntPtr UserData { get => (IntPtr)NativePtr->UserData; set => NativePtr->UserData = (void*)value; }
        public ref Vector2 Pos => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->Pos);
        public ref Vector2 CurrentSize => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->CurrentSize);
        public ref Vector2 DesiredSize => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DesiredSize);
    }
}

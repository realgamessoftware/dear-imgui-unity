using System;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using System.Text;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiWindowClass
    {
        public uint ClassId;
        public uint ParentViewportId;
        public ImGuiViewportFlags ViewportFlagsOverrideSet;
        public ImGuiViewportFlags ViewportFlagsOverrideClear;
        public ImGuiTabItemFlags TabItemFlagsOverrideSet;
        public ImGuiDockNodeFlags DockNodeFlagsOverrideSet;
        public byte DockingAlwaysTabBar;
        public byte DockingAllowUnclassed;
    }
    public unsafe partial struct ImGuiWindowClassPtr
    {
        public ImGuiWindowClass* NativePtr { get; }
        public ImGuiWindowClassPtr(ImGuiWindowClass* nativePtr) => NativePtr = nativePtr;
        public ImGuiWindowClassPtr(IntPtr nativePtr) => NativePtr = (ImGuiWindowClass*)nativePtr;
        public static implicit operator ImGuiWindowClassPtr(ImGuiWindowClass* nativePtr) => new ImGuiWindowClassPtr(nativePtr);
        public static implicit operator ImGuiWindowClass* (ImGuiWindowClassPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiWindowClassPtr(IntPtr nativePtr) => new ImGuiWindowClassPtr(nativePtr);
        public ref uint ClassId => ref UnsafeUtility.AsRef<uint>(&NativePtr->ClassId);
        public ref uint ParentViewportId => ref UnsafeUtility.AsRef<uint>(&NativePtr->ParentViewportId);
        public ref ImGuiViewportFlags ViewportFlagsOverrideSet => ref UnsafeUtility.AsRef<ImGuiViewportFlags>(&NativePtr->ViewportFlagsOverrideSet);
        public ref ImGuiViewportFlags ViewportFlagsOverrideClear => ref UnsafeUtility.AsRef<ImGuiViewportFlags>(&NativePtr->ViewportFlagsOverrideClear);
        public ref ImGuiTabItemFlags TabItemFlagsOverrideSet => ref UnsafeUtility.AsRef<ImGuiTabItemFlags>(&NativePtr->TabItemFlagsOverrideSet);
        public ref ImGuiDockNodeFlags DockNodeFlagsOverrideSet => ref UnsafeUtility.AsRef<ImGuiDockNodeFlags>(&NativePtr->DockNodeFlagsOverrideSet);
        public ref bool DockingAlwaysTabBar => ref UnsafeUtility.AsRef<bool>(&NativePtr->DockingAlwaysTabBar);
        public ref bool DockingAllowUnclassed => ref UnsafeUtility.AsRef<bool>(&NativePtr->DockingAllowUnclassed);
        public void Destroy()
        {
            ImGuiNative.ImGuiWindowClass_destroy((ImGuiWindowClass*)(NativePtr));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiIOPtr
    {
        // keep track of data allocated by the managed side
        static readonly HashSet<IntPtr> s_managedAllocations = new HashSet<IntPtr>(IntPtrEqualityComparer.Instance);

        public void SetBackendRendererName(string name)
        {
            if (NativePtr->BackendRendererName != (byte*)0)
            {
                if (s_managedAllocations.Contains((IntPtr)NativePtr->BackendRendererName))
                    Util.Free(NativePtr->BackendRendererName);
                NativePtr->BackendRendererName = (byte*)0;
            }
            if (name != null)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* nativeName = Util.Allocate(byteCount + 1);
                int offset = Util.GetUtf8(name, nativeName, byteCount);
                nativeName[offset] = 0;
                NativePtr->BackendRendererName = nativeName;
                s_managedAllocations.Add((IntPtr)nativeName);
            }
        }

        public void SetBackendPlatformName(string name)
        {
            if (NativePtr->BackendPlatformName != (byte*)0)
            {
                if (s_managedAllocations.Contains((IntPtr)NativePtr->BackendPlatformName))
                    Util.Free(NativePtr->BackendPlatformName);
                NativePtr->BackendPlatformName = (byte*)0;
            }
            if (name != null)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* nativeName = Util.Allocate(byteCount + 1);
                int offset = Util.GetUtf8(name, nativeName, byteCount);
                nativeName[offset] = 0;
                NativePtr->BackendPlatformName = nativeName;
                s_managedAllocations.Add((IntPtr)nativeName);
            }
        }

        public void SetIniFilename(string name)
        {
            if (NativePtr->IniFilename != (byte*)0)
            {
                if (s_managedAllocations.Contains((IntPtr)NativePtr->IniFilename))
                    Util.Free(NativePtr->IniFilename);
                NativePtr->IniFilename = (byte*)0;
            }
            if (name != null)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* nativeName = Util.Allocate(byteCount + 1);
                int offset = Util.GetUtf8(name, nativeName, byteCount);
                nativeName[offset] = 0;
                NativePtr->IniFilename = nativeName;
                s_managedAllocations.Add((IntPtr)nativeName);
            }
        }

        public void SetBackendPlatformUserData<T>(T? data)
        where T : unmanaged
        {
            if (NativePtr->BackendPlatformUserData != (void*)0)
            {
                if (s_managedAllocations.Contains((IntPtr)NativePtr->BackendPlatformUserData))
                    Marshal.FreeHGlobal((IntPtr)NativePtr->BackendPlatformUserData);
                NativePtr->BackendPlatformUserData = (void*)0;
            }
            if (data != null)
            {
                IntPtr dataPtr = Marshal.AllocHGlobal(sizeof(T));
                Marshal.StructureToPtr(data, dataPtr, false);
                NativePtr->BackendPlatformUserData = (void*)dataPtr;
                s_managedAllocations.Add(dataPtr);
            }
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.Text;

// Copy of ImGuiNET.Wrapper.Util because it is internal, to avoid changes to the wrapper

namespace ImGuiNET
{
    internal static unsafe class InteropUtil
    {
        internal const int StackAllocationSizeLimit = 2048;

        public static string StringFromPtr(byte* ptr)
        {
            int characters = 0;
            while (ptr[characters] != 0)
            {
                characters++;
            }

            return Encoding.UTF8.GetString(ptr, characters);
        }

        internal static byte* Allocate(int byteCount) => (byte*)Marshal.AllocHGlobal(byteCount);
        internal static void Free(byte* ptr) => Marshal.FreeHGlobal((IntPtr)ptr);
    }
}

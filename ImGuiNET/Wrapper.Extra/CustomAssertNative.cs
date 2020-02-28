using System.Runtime.InteropServices;

namespace ImGuiNET
{
    static unsafe partial class CustomAssertNative
    {
        [DllImport("cimgui", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PluginLogAssert(byte* condition, byte* file, int line);

        [DllImport("cimgui", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PluginDebugBreak();
    }
}

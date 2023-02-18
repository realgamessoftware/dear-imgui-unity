using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImFontConfigPtr
    {
        public ImFontConfigPtr(ref ImFontConfig fontConfig)
        {
            NativePtr = (ImFontConfig*)UnsafeUtility.AddressOf(ref fontConfig);
        }
    }
}

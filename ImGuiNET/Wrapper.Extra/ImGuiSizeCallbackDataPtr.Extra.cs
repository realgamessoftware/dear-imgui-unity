using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiSizeCallbackDataPtr
    {
        public ImGuiSizeCallbackDataPtr(ref ImGuiSizeCallbackData data)
        {
            NativePtr = (ImGuiSizeCallbackData*)UnsafeUtility.AddressOf(ref data);
        }
    }
}

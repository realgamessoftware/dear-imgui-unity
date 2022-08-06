using System;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    // ImGui extra functionality related with Drag and Drop
    public static partial class ImGui
    {
        // TODO: review
        // can now pass refs with UnsafeUtility.AsRef

        public static unsafe void SetDragDropPayload<T>(string type, T data, ImGuiCond cond = 0)
        where T : unmanaged
        {
            void* ptr = UnsafeUtility.AddressOf(ref data);
            SetDragDropPayload(type, new IntPtr(ptr), (uint)UnsafeUtility.SizeOf<T>(), cond);
        }

        public static unsafe bool AcceptDragDropPayload<T>(string type, out T payload, ImGuiDragDropFlags flags = ImGuiDragDropFlags.None)
        where T : unmanaged
        {
            ImGuiPayload* pload = AcceptDragDropPayload(type, flags);
            payload = (pload != null) ? UnsafeUtility.ReadArrayElement<T>(pload->Data, 0) : default;
            return pload != null;
        }

        public static unsafe void SetDragDropPayload(string type, string data, ImGuiCond cond = 0)
        {
            fixed (char* chars = data)
            {
                int byteCount = Encoding.Default.GetByteCount(data);
                byte* bytes = stackalloc byte[byteCount];
                Encoding.Default.GetBytes(chars, data.Length, bytes, byteCount);

                SetDragDropPayload(type, new IntPtr(bytes), (uint)byteCount, cond);
            }
        }

        public static unsafe bool AcceptDragDropPayload(string type, out string payload, ImGuiDragDropFlags flags = ImGuiDragDropFlags.None)
        {
            ImGuiPayload* pload = AcceptDragDropPayload(type, flags);
            payload = (pload != null) ? Encoding.Default.GetString((byte*)pload->Data, pload->DataSize) : null;
            return pload != null;
        }
    }
}

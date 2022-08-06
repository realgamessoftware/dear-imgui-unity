using System;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    // Same as ImGui.Manual.InputText but for InputTextWithHint
    public static unsafe partial class ImGui
    {
        public static bool InputTextWithHint(
            string label,
            string hint,
            ref string input,
            uint maxLength)
        {
            return InputTextWithHint(label, hint, ref input, maxLength, ImGuiInputTextFlags.None, null, IntPtr.Zero);
        }

        public static bool InputTextWithHint(
            string label,
            string hint,
            ref string input,
            uint maxLength,
            ImGuiInputTextFlags flags)
        {
            return InputTextWithHint(label, hint, ref input, maxLength, flags, null, IntPtr.Zero);
        }

        public static bool InputTextWithHint(
            string label,
            string hint,
            ref string input,
            uint maxLength,
            ImGuiInputTextFlags flags,
            ImGuiInputTextCallback callback)
        {
            return InputTextWithHint(label, hint, ref input, maxLength, flags, callback, IntPtr.Zero);
        }

        public static bool InputTextWithHint(
            string label,
            string hint,
            ref string input,
            uint maxLength,
            ImGuiInputTextFlags flags,
            ImGuiInputTextCallback callback,
            IntPtr user_data)
        {
            int utf8LabelByteCount = Encoding.UTF8.GetByteCount(label);
            byte* utf8LabelBytes;
            if (utf8LabelByteCount > Util.StackAllocationSizeLimit)
            {
                utf8LabelBytes = Util.Allocate(utf8LabelByteCount + 1);
            }
            else
            {
                byte* stackPtr = stackalloc byte[utf8LabelByteCount + 1];
                utf8LabelBytes = stackPtr;
            }
            Util.GetUtf8(label, utf8LabelBytes, utf8LabelByteCount);

            int utf8HintByteCount = Encoding.UTF8.GetByteCount(hint);
            byte* utf8HintBytes;
            if (utf8HintByteCount > Util.StackAllocationSizeLimit)
            {
                utf8HintBytes = Util.Allocate(utf8HintByteCount + 1);
            }
            else
            {
                byte* stackPtr = stackalloc byte[utf8HintByteCount + 1];
                utf8HintBytes = stackPtr;
            }
            Util.GetUtf8(hint, utf8HintBytes, utf8HintByteCount);

            int utf8InputByteCount = Encoding.UTF8.GetByteCount(input);
            int inputBufSize = Math.Max((int)maxLength + 1, utf8InputByteCount + 1);

            byte* utf8InputBytes;
            byte* originalUtf8InputBytes;
            if (inputBufSize > Util.StackAllocationSizeLimit)
            {
                utf8InputBytes = Util.Allocate(inputBufSize);
                originalUtf8InputBytes = Util.Allocate(inputBufSize);
            }
            else
            {
                byte* inputStackBytes = stackalloc byte[inputBufSize];
                utf8InputBytes = inputStackBytes;
                byte* originalInputStackBytes = stackalloc byte[inputBufSize];
                originalUtf8InputBytes = originalInputStackBytes;
            }
            Util.GetUtf8(input, utf8InputBytes, inputBufSize);
            uint clearBytesCount = (uint)(inputBufSize - utf8InputByteCount);
            UnsafeUtility.MemSet(utf8InputBytes + utf8InputByteCount, 0, clearBytesCount);
            UnsafeUtility.MemCpy(originalUtf8InputBytes, utf8InputBytes, (uint)inputBufSize);

            byte result = ImGuiNative.igInputTextWithHint(
                utf8LabelBytes,
                utf8HintBytes,
                utf8InputBytes,
                (uint)inputBufSize,
                flags,
                callback,
                user_data.ToPointer());
            if (!Util.AreStringsEqual(originalUtf8InputBytes, inputBufSize, utf8InputBytes))
            {
                input = Util.StringFromPtr(utf8InputBytes);
            }

            if (utf8LabelByteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(utf8LabelBytes);
            }
            if (inputBufSize > Util.StackAllocationSizeLimit)
            {
                Util.Free(utf8InputBytes);
                Util.Free(originalUtf8InputBytes);
            }

            return result != 0;
        }
    }
}

using System;
using System.Text;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiInputTextCallbackData
    {
        public ImGuiInputTextFlags EventFlag;
        public ImGuiInputTextFlags Flags;
        public void* UserData;
        public ushort EventChar;
        public ImGuiKey EventKey;
        public byte* Buf;
        public int BufTextLen;
        public int BufSize;
        public byte BufDirty;
        public int CursorPos;
        public int SelectionStart;
        public int SelectionEnd;
    }
    public unsafe partial struct ImGuiInputTextCallbackDataPtr
    {
        public ImGuiInputTextCallbackData* NativePtr { get; }
        public ImGuiInputTextCallbackDataPtr(ImGuiInputTextCallbackData* nativePtr) => NativePtr = nativePtr;
        public ImGuiInputTextCallbackDataPtr(IntPtr nativePtr) => NativePtr = (ImGuiInputTextCallbackData*)nativePtr;
        public static implicit operator ImGuiInputTextCallbackDataPtr(ImGuiInputTextCallbackData* nativePtr) => new ImGuiInputTextCallbackDataPtr(nativePtr);
        public static implicit operator ImGuiInputTextCallbackData* (ImGuiInputTextCallbackDataPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiInputTextCallbackDataPtr(IntPtr nativePtr) => new ImGuiInputTextCallbackDataPtr(nativePtr);
        public ref ImGuiInputTextFlags EventFlag => ref UnsafeUtility.AsRef<ImGuiInputTextFlags>(&NativePtr->EventFlag);
        public ref ImGuiInputTextFlags Flags => ref UnsafeUtility.AsRef<ImGuiInputTextFlags>(&NativePtr->Flags);
        public IntPtr UserData { get => (IntPtr)NativePtr->UserData; set => NativePtr->UserData = (void*)value; }
        public ref ushort EventChar => ref UnsafeUtility.AsRef<ushort>(&NativePtr->EventChar);
        public ref ImGuiKey EventKey => ref UnsafeUtility.AsRef<ImGuiKey>(&NativePtr->EventKey);
        public IntPtr Buf { get => (IntPtr)NativePtr->Buf; set => NativePtr->Buf = (byte*)value; }
        public ref int BufTextLen => ref UnsafeUtility.AsRef<int>(&NativePtr->BufTextLen);
        public ref int BufSize => ref UnsafeUtility.AsRef<int>(&NativePtr->BufSize);
        public ref bool BufDirty => ref UnsafeUtility.AsRef<bool>(&NativePtr->BufDirty);
        public ref int CursorPos => ref UnsafeUtility.AsRef<int>(&NativePtr->CursorPos);
        public ref int SelectionStart => ref UnsafeUtility.AsRef<int>(&NativePtr->SelectionStart);
        public ref int SelectionEnd => ref UnsafeUtility.AsRef<int>(&NativePtr->SelectionEnd);
        public void DeleteChars(int pos, int bytes_count)
        {
            ImGuiNative.ImGuiInputTextCallbackData_DeleteChars(NativePtr, pos, bytes_count);
        }
        public void Destroy()
        {
            ImGuiNative.ImGuiInputTextCallbackData_destroy(NativePtr);
        }
        public bool HasSelection()
        {
            byte ret = ImGuiNative.ImGuiInputTextCallbackData_HasSelection(NativePtr);
            return ret != 0;
        }
        public void InsertChars(int pos, string text)
        {
            byte* native_text;
            int text_byteCount = 0;
            if (text != null)
            {
                text_byteCount = Encoding.UTF8.GetByteCount(text);
                if (text_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_text = Util.Allocate(text_byteCount + 1);
                }
                else
                {
                    byte* native_text_stackBytes = stackalloc byte[text_byteCount + 1];
                    native_text = native_text_stackBytes;
                }
                int native_text_offset = Util.GetUtf8(text, native_text, text_byteCount);
                native_text[native_text_offset] = 0;
            }
            else { native_text = null; }
            byte* native_text_end = null;
            ImGuiNative.ImGuiInputTextCallbackData_InsertChars(NativePtr, pos, native_text, native_text_end);
            if (text_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_text);
            }
        }
    }
}

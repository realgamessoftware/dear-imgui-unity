using System;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using System.Text;

namespace ImGuiNET
{
    public unsafe partial struct StbUndoRecord
    {
        public int where;
        public int insert_length;
        public int delete_length;
        public int char_storage;
    }
    public unsafe partial struct StbUndoRecordPtr
    {
        public StbUndoRecord* NativePtr { get; }
        public StbUndoRecordPtr(StbUndoRecord* nativePtr) => NativePtr = nativePtr;
        public StbUndoRecordPtr(IntPtr nativePtr) => NativePtr = (StbUndoRecord*)nativePtr;
        public static implicit operator StbUndoRecordPtr(StbUndoRecord* nativePtr) => new StbUndoRecordPtr(nativePtr);
        public static implicit operator StbUndoRecord* (StbUndoRecordPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator StbUndoRecordPtr(IntPtr nativePtr) => new StbUndoRecordPtr(nativePtr);
        public ref int where => ref UnsafeUtility.AsRef<int>(&NativePtr->where);
        public ref int insert_length => ref UnsafeUtility.AsRef<int>(&NativePtr->insert_length);
        public ref int delete_length => ref UnsafeUtility.AsRef<int>(&NativePtr->delete_length);
        public ref int char_storage => ref UnsafeUtility.AsRef<int>(&NativePtr->char_storage);
    }
}

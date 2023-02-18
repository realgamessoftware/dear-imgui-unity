using System;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe struct ImVector
    {
        public readonly int Size;
        public readonly int Capacity;
        public readonly IntPtr Data;

        public ref T Ref<T>(int index) where T : struct
        {
            return ref UnsafeUtility.AsRef<T>((byte*)Data + index * UnsafeUtility.SizeOf<T>());
        }

        public IntPtr Address<T>(int index) where T : struct
        {
            return (IntPtr)((byte*)Data + index * UnsafeUtility.SizeOf<T>());
        }
    }

    public unsafe struct ImVector<T> where T : struct
    {
        public readonly int Size;
        public readonly int Capacity;
        public readonly IntPtr Data;

        public ImVector(ImVector vector)
        {
            Size = vector.Size;
            Capacity = vector.Capacity;
            Data = vector.Data;
        }

        public ImVector(int size, int capacity, IntPtr data)
        {
            Size = size;
            Capacity = capacity;
            Data = data;
        }

        public ref T this[int index] => ref UnsafeUtility.AsRef<T>((byte*)Data + index * UnsafeUtility.SizeOf<T>());
    }

    public unsafe struct ImPtrVector<T>
    {
        public readonly int Size;
        public readonly int Capacity;
        public readonly IntPtr Data;
        private readonly int _stride;

        public ImPtrVector(ImVector vector, int stride)
            : this(vector.Size, vector.Capacity, vector.Data, stride)
        { }

        public ImPtrVector(int size, int capacity, IntPtr data, int stride)
        {
            Size = size;
            Capacity = capacity;
            Data = data;
            _stride = stride;
        }

        public T this[int index]
        {
            get
            {
                byte* address = (byte*)Data + index * _stride;
                T ret = UnsafeUtility.ReadArrayElement<T>(&address, 0);
                return ret;
            }
        }
    }
}

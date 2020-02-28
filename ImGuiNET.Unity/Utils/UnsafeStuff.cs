using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

public static class MemoryMarshalEx
{
    // NOTE: MemoryMarshal.CreateSpan() only in netstandard2.1

    // public static Span<byte> AsSpan<T>(ref T val)
    // where T : unmanaged
    // {
    //     var span = MemoryMarshal.CreateSpan(ref val, 1);
    //     return MemoryMarshal.Cast<T, byte>(span);
    // }

    // public static ReadOnlySpan<byte> AsReadOnlySpan<T>(ref T val)
    // where T : unmanaged
    // {
    //     var span = MemoryMarshal.CreateReadOnlySpan(ref val, 1);
    //     return MemoryMarshal.Cast<T, byte>(span);
    // }
}

public static class ListExtensions
{
    public static unsafe void AddRange<T>(this List<T> list, void* arrayBuffer, int length)
    where T : struct
    {
        int index = list.Count;
        int newLength = index + length;

        if (list.Capacity < newLength)
            list.Capacity = newLength;

        T[] items = NoAllocHelpers.ExtractArrayFromListT(list);
        int size = UnsafeUtility.SizeOf<T>();

        // Get the pointer to the end of the list
        var bufferStart = (IntPtr)UnsafeUtility.AddressOf(ref items[0]);
        byte* buffer = (byte*)(bufferStart + (size * index));

        UnsafeUtility.MemCpy(buffer, arrayBuffer, length * (long)size);
        NoAllocHelpers.ResizeList(list, newLength);
    }

    public static void Resize<T>(this List<T> list, int sz, T c = default)
    {
        int cur = list.Count;

        if (sz < cur)
            list.RemoveRange(sz, cur - sz);
        else if (sz > cur)
            list.AddRange(Enumerable.Repeat(c, sz - cur));
    }

    public static void EnsureLength<T>(this List<T> list, int sz, T c = default)
    {
        int cur = list.Count;

        if (sz > cur)
            list.AddRange(Enumerable.Repeat(c, sz - cur));
    }
}

public static class NoAllocHelpers
{
    static readonly Dictionary<Type, Delegate> s_extractArrayFromListTDelegates = new Dictionary<Type, Delegate>();
    static readonly Dictionary<Type, Delegate> s_resizeListDelegates = new Dictionary<Type, Delegate>();

    public static T[] ExtractArrayFromListT<T>(List<T> list)
    {
        if (!s_extractArrayFromListTDelegates.TryGetValue(typeof(T), out Delegate obj))
        {
            var assembly = Assembly.GetAssembly(typeof(Mesh)); // any class in UnityEngine
            Type type = assembly.GetType("UnityEngine.NoAllocHelpers");
            MethodInfo methodInfo = type.GetMethod("ExtractArrayFromListT", BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(typeof(T));

            obj = s_extractArrayFromListTDelegates[typeof(T)] = Delegate.CreateDelegate(typeof(Func<List<T>, T[]>), methodInfo);
        }

        var func = (Func<List<T>, T[]>)obj;
        return func.Invoke(list);
    }

    public static void ResizeList<T>(List<T> list, int size)
    {
        if (!s_resizeListDelegates.TryGetValue(typeof(T), out Delegate obj))
        {
            var assembly = Assembly.GetAssembly(typeof(Mesh)); // any class in UnityEngine
            Type type = assembly.GetType("UnityEngine.NoAllocHelpers");
            MethodInfo methodInfo = type.GetMethod("ResizeList", BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(typeof(T));

            obj = s_resizeListDelegates[typeof(T)] = Delegate.CreateDelegate(typeof(Action<List<T>, int>), methodInfo);
        }

        var action = (Action<List<T>, int>)obj;
        action.Invoke(list, size);
    }
}

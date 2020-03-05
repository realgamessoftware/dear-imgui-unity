using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ImGuiNET.Unity
{
    // TODO: should return Utf8 byte*, how to deal with memory ownership?
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate string GetClipboardTextCallback(void* user_data);
    delegate string GetClipboardTextSafeCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void SetClipboardTextCallback(void* user_data, byte* text);
    delegate void SetClipboardTextSafeCallback(IntPtr user_data, string text);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void ImeSetInputScreenPosCallback(int x, int y);

#if IMGUI_FEATURE_CUSTOM_ASSERT
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void LogAssertCallback(byte* condition, byte* file, int line);
    delegate void LogAssertSafeCallback(string condition, string file, int line);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DebugBreakCallback();

    unsafe struct CustomAssertData
    {
        public IntPtr LogAssertFn;
        public IntPtr DebugBreakFn;
    }
#endif

    unsafe class PlatformCallbacks
    {
        // fields to keep delegates from being collected by the garbage collector
        // after assigning its function pointers to unmanaged code
        GetClipboardTextCallback _getClipboardText;
        SetClipboardTextCallback _setClipboardText;
        ImeSetInputScreenPosCallback _imeSetInputScreenPos;
#if IMGUI_FEATURE_CUSTOM_ASSERT
        LogAssertCallback _logAssert;
        DebugBreakCallback _debugBreak;
#endif

        public void Assign(ImGuiIOPtr io)
        {
            io.SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(_setClipboardText);
            io.GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(_getClipboardText);
            io.ImeSetInputScreenPosFn = Marshal.GetFunctionPointerForDelegate(_imeSetInputScreenPos);
#if IMGUI_FEATURE_CUSTOM_ASSERT
            io.SetBackendPlatformUserData<CustomAssertData>(new CustomAssertData
            {
                LogAssertFn = Marshal.GetFunctionPointerForDelegate(_logAssert),
                DebugBreakFn = Marshal.GetFunctionPointerForDelegate(_debugBreak),
            });
#endif
        }

        public void Unset(ImGuiIOPtr io)
        {
            io.SetClipboardTextFn = IntPtr.Zero;
            io.GetClipboardTextFn = IntPtr.Zero;
            io.ImeSetInputScreenPosFn = IntPtr.Zero;
#if IMGUI_FEATURE_CUSTOM_ASSERT
            io.SetBackendPlatformUserData<CustomAssertData>(null);
#endif
        }

        public GetClipboardTextSafeCallback GetClipboardText
        {
            set => _getClipboardText = (user_data) =>
            {
                // TODO: convert return string to Utf8 byte*
                try { return value(new IntPtr(user_data)); }
                catch (Exception ex) { Debug.LogException(ex); return null; }
            };
        }

        public SetClipboardTextSafeCallback SetClipboardText
        {
            set => _setClipboardText = (user_data, text) =>
            {
                try { value(new IntPtr(user_data), Util.StringFromPtr(text)); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }

        public ImeSetInputScreenPosCallback ImeSetInputScreenPos
        {
            set => _imeSetInputScreenPos = (x, y) =>
            {
                try { value(x, y); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }

#if IMGUI_FEATURE_CUSTOM_ASSERT
        public LogAssertSafeCallback LogAssert
        {
            set => _logAssert = (condition, file, line) =>
            {
                try { value(Util.StringFromPtr(condition), Util.StringFromPtr(file), line); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }

        public DebugBreakCallback DebugBreak
        {
            set => _debugBreak = () =>
            {
                try { value(); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }
#endif
    }
}

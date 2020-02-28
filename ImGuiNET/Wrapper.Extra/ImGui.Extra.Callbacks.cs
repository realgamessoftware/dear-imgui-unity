using System;
using UnityEngine;

namespace ImGuiNET
{
    // Callbacks to avoid using unsafe code in assemblies that don't allow it.
    // These get 'TDataPtr' as callback parameters instead of 'TData*'.
    public delegate int ImGuiInputTextSafeCallback(ImGuiInputTextCallbackDataPtr data);
    public delegate void ImGuiSizeSafeCallback(ImGuiSizeCallbackDataPtr data);

    // Helpers to create safe callbacks and hide unsafe code.
    // Also when a exception is not caught inside a callback the application hangs.
    // Use '_callback = CreateSizeCallback(MyCallback)' instead of 'unsafe { _callback = MyCallback; }'.
    public static unsafe partial class ImGui
    {
        public static ImGuiInputTextCallback CreateInputTextCallback(ImGuiInputTextSafeCallback callback)
        {
            return (data) =>
            {
                try { return callback(data); }
                catch (Exception ex) { Debug.LogException(ex); return -1; }
            };
        }

        public static ImGuiSizeCallback CreateSizeCallback(ImGuiSizeSafeCallback callback)
        {
            return (data) =>
            {
                try { callback(data); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }
    }
}

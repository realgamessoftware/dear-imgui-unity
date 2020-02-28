using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace ImGuiNET.Unity.Editor
{
    // Define the availability of extra features of the native plugin through compile time symbols.
    sealed class PluginFeatures
    {
        [InitializeOnLoadMethod]
        static void CheckScriptingDefineSymbols()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            (string symbol, bool enabled)[] symbols = new[]
            {
                // Plugin has redefined IM_ASSERT and IM_DEBUG_BREAK in 'imconfig.h'.
                // A structure with function pointers to managed functions that implement
                // the functionality should be set to ImGuiIO.BackendPlatformUserData.
                ("IMGUI_FEATURE_CUSTOM_ASSERT", HasCustomAssert()),

                // Font atlases can be built using FreeType instead of stb_truetype.
                ("IMGUI_FEATURE_FREETYPE",      HasFreetype()),
            };

            var buildTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
            var defineSymbols = definesString.Split(';').Where(s => !string.IsNullOrEmpty(s))
                                .Except(symbols.Where(s => !s.enabled).Select(s => s.symbol))
                                .Union(symbols.Where(s =>  s.enabled).Select(s => s.symbol));
            var newDefinesString = string.Join(";", defineSymbols);
            if (newDefinesString != definesString)
                // setting scripting define symbols also forces a recompile
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, newDefinesString);
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        static void DoPluginChecks()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            try
            {
                // check if the data layout of structures in the wrapper match the plugin
                if (!ImGui.DebugCheckVersionAndDataLayout(
                    ImGui.GetVersion(),
                    (uint)Marshal.SizeOf<ImGuiIO>(),
                    (uint)Marshal.SizeOf<ImGuiStyle>(),
                    (uint)Marshal.SizeOf<Vector2>(),
                    (uint)Marshal.SizeOf<Vector4>(),
                    (uint)Marshal.SizeOf<ImDrawVert>(),
                    sizeof(ushort)))
                {
                    Debug.LogWarning("[DearImGui] Data layout mismatch.");
                    Debug.Log(MarshalledOffsets<ImGuiIO>());
                    Debug.Log(MarshalledOffsets<ImGuiStyle>());
                    Debug.Log(MarshalledOffsets<ImDrawVert>());
                }
            }
            catch (DllNotFoundException)
            {
                Debug.LogWarning("[DearImGui] Could not check data layout, native plugin not loaded.");
            }
        }

        static bool HasCustomAssert() => CheckMethod(typeof(CustomAssertNative).GetMethod(nameof(CustomAssertNative.PluginLogAssert)));
        static bool HasFreetype() => CheckMethod(typeof(ImFreetypeNative).GetMethod(nameof(ImFreetypeNative.frBuildFontAtlas)));
        static bool CheckMethod(System.Reflection.MethodInfo method)
        {
            try
            {
                Marshal.Prelink(method);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static unsafe string MarshalledOffsets<T>()
        {
            var sb = new System.Text.StringBuilder();
            var structSize = Marshal.SizeOf<T>();
            sb.AppendLine($"{typeof(T).Name} size: {structSize}");

            var fieldNames = typeof(T).GetFields().Select(f => f.Name).ToArray();
            for (var i = 0; i < fieldNames.Length; ++i)
            {
                var offset = (int)Marshal.OffsetOf<T>(fieldNames[i]);
                var offsetNext = i == fieldNames.Length - 1 ? structSize : (int)Marshal.OffsetOf<T>(fieldNames[i + 1]);
                sb.AppendLine($"{fieldNames[i],-24} {offset,4} {offsetNext - offset,4}");
            }
            return sb.ToString();
        }
    }
}

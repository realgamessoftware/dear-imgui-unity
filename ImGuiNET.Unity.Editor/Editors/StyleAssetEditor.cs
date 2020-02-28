using UnityEngine;
using UnityEditor;

namespace ImGuiNET.Unity.Editor
{
    [CustomEditor(typeof(StyleAsset))]
    class StyleAssetEditor : UnityEditor.Editor
    {
        bool _showColors;

        public override void OnInspectorGUI()
        {
            var styleAsset = target as StyleAsset;

            bool noContext = ImGui.GetCurrentContext() == System.IntPtr.Zero;
            if (noContext)
                EditorGUILayout.HelpBox("Can't save or apply Style.\n"
                                      + "No active ImGui context.", MessageType.Warning, true);

            // apply and save buttons only when a context is active
            if (!noContext)
            {
                var style = ImGui.GetStyle();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Apply"))
                    styleAsset.ApplyTo(style);

                if (GUILayout.Button("Save")
                && EditorUtility.DisplayDialog("Save Style", "Do you want to save the current style to this asset?", "Ok", "Cancel"))
                {
                    styleAsset.SetFrom(style);
                    EditorUtility.SetDirty(target);
                }
                GUILayout.EndHorizontal();
            }

            // default
            DrawDefaultInspector();

            // colors
            bool changed = false;
            _showColors = EditorGUILayout.Foldout(_showColors, "Colors", true);
            if (_showColors)
            {
                for (int i = 0; i < (int)ImGuiCol.COUNT; ++i)
                {
                    var newColor = EditorGUILayout.ColorField(ImGui.GetStyleColorName((ImGuiCol)i), styleAsset.Colors[i]);
                    changed |= (newColor != styleAsset.Colors[i]);
                    styleAsset.Colors[i] = newColor;
                }
            }

            if (changed)
                EditorUtility.SetDirty(target);
        }
    }
}

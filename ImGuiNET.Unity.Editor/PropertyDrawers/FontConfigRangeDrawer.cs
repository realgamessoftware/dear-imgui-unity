using UnityEditor;
using UnityEngine;

namespace ImGuiNET.Unity.Editor
{
    [CustomPropertyDrawer(typeof(FontConfig.Range))]
    class FontConfigRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty start = property.FindPropertyRelative(nameof(FontConfig.Range.Start));
            SerializedProperty end = property.FindPropertyRelative(nameof(FontConfig.Range.End));

            if (start.intValue > end.intValue)
                EditorGUI.DrawRect(position, new Color(1, 0, 0, 0.2f));

            label = EditorGUI.BeginProperty(position, label, property);
            Rect fieldPos = EditorGUI.PrefixLabel(position, label);

            Rect startPos = fieldPos;
            startPos.width = (fieldPos.width - EditorGUIUtility.standardVerticalSpacing) * 0.5f;
            Rect endPos = startPos;
            endPos.x = startPos.xMax + EditorGUIUtility.standardVerticalSpacing;

            var prevIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0; // would not get aligned otherwise
            DrawValue(startPos, start);
            DrawValue(endPos, end);
            EditorGUI.indentLevel = prevIndent;

            EditorGUI.EndProperty();

            void DrawValue(Rect rect, SerializedProperty prop)
            {
                Color prevBackgroundColor = GUI.backgroundColor;
                if (prop.intValue <= 0)
                    GUI.backgroundColor = Color.red;
                EditorGUI.PropertyField(rect, prop, GUIContent.none);
                GUI.backgroundColor = prevBackgroundColor;
            }
        }
    }
}

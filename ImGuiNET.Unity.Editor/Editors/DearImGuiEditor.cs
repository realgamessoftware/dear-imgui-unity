using System.Text;
using UnityEditor;
using UnityEngine;

namespace ImGuiNET.Unity.Editor
{
    [CustomEditor(typeof(DearImGui))]
    class DearImGuiEditor : UnityEditor.Editor
    {
        SerializedProperty _doGlobalLayout;

        SerializedProperty _camera;
        SerializedProperty _renderFeature;

        SerializedProperty _renderer;
        SerializedProperty _platform;

        SerializedProperty _initialConfiguration;
        SerializedProperty _fontAtlasConfiguration;
        SerializedProperty _iniSettings;

        SerializedProperty _shaders;
        SerializedProperty _style;
        SerializedProperty _cursorShapes;

        readonly StringBuilder _messages = new StringBuilder();

        void OnEnable()
        {
            _doGlobalLayout = serializedObject.FindProperty("_doGlobalLayout");
            _camera = serializedObject.FindProperty("_camera");
            _renderFeature = serializedObject.FindProperty("_renderFeature");
            _renderer = serializedObject.FindProperty("_rendererType");
            _platform = serializedObject.FindProperty("_platformType");
            _initialConfiguration = serializedObject.FindProperty("_initialConfiguration");
            _fontAtlasConfiguration = serializedObject.FindProperty("_fontAtlasConfiguration");
            _iniSettings = serializedObject.FindProperty("_iniSettings");
            _shaders = serializedObject.FindProperty("_shaders");
            _style = serializedObject.FindProperty("_style");
            _cursorShapes = serializedObject.FindProperty("_cursorShapes");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CheckRequirements();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_doGlobalLayout);
            if (RenderUtils.IsUsingURP())
                EditorGUILayout.PropertyField(_renderFeature);
            EditorGUILayout.PropertyField(_camera);
            EditorGUILayout.PropertyField(_renderer);
            EditorGUILayout.PropertyField(_platform);
            EditorGUILayout.PropertyField(_initialConfiguration);
            EditorGUILayout.PropertyField(_fontAtlasConfiguration);
            EditorGUILayout.PropertyField(_iniSettings);
            EditorGUILayout.PropertyField(_shaders);
            EditorGUILayout.PropertyField(_style);
            EditorGUILayout.PropertyField(_cursorShapes);

            var changed = EditorGUI.EndChangeCheck();
            if (changed)
                serializedObject.ApplyModifiedProperties();

            if (!Application.isPlaying)
                return;

            var reload = GUILayout.Button("Reload");
            if (changed || reload)
                (target as DearImGui)?.Reload();
        }

        void CheckRequirements()
        {
            _messages.Clear();
            if (_camera.objectReferenceValue == null)
                _messages.AppendLine("Must assign a Camera.");
            if (RenderUtils.IsUsingURP() && _renderFeature.objectReferenceValue == null)
                _messages.AppendLine("Must assign a RenderFeature when using the URP.");
            if (!Platform.IsAvailable((Platform.Type)_platform.enumValueIndex))
                _messages.AppendLine("Platform not available.");
            if (_messages.Length > 0)
                EditorGUILayout.HelpBox(_messages.ToString(), MessageType.Error);
        }
    }
}

using UnityEngine;

namespace ImGuiNET.Unity
{
    // TODO: ability to save to asset, in player prefs with custom key, custom ini file, etc

    /// <summary>
    /// Used to store ImGui Ini settings in an asset instead of the default imgui.ini file
    /// </summary>
    [CreateAssetMenu(menuName = "Dear ImGui/Ini Settings")]
    sealed class IniSettingsAsset : ScriptableObject
    {
        [TextArea(3, 20)]
        [SerializeField] string _data;

        public void Save(string data)
        {
            _data = data;
        }

        public string Load()
        {
            return _data;
        }
    }
}

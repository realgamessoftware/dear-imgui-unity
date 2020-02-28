using UnityEngine;

namespace ImGuiNET.Unity
{
    static class Platform
    {
        public enum Type
        {
            LegacyInput = 0,
            InputSystem = 1,
        }

        public static bool IsAvailable(Type type)
        {
            switch (type)
            {
                case Type.LegacyInput: return true;
#if HAS_INPUTSYSTEM
                case Type.InputSystem: return true;
#endif
                default: return false;
            }
        }

        public static IImGuiPlatform Create(Type type, CursorShapesAsset cursors, IniSettingsAsset iniSettings)
        {
            switch (type)
            {
                case Type.LegacyInput: return new ImGuiPlatformUnityLegacy(cursors, iniSettings);
#if HAS_INPUTSYSTEM
                case Type.InputSystem: return new ImGuiPlatformUnity(cursors, iniSettings);
#endif
                default:
                    Debug.LogError($"[DearImGui] {type} platform not available.");
                    return null;
            }
        }
    }
}

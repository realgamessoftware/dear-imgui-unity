using UnityEngine;

namespace ImGuiNET.Unity
{
    static class Platform
    {
        public enum Type
        {
            InputManager = 0,
            InputSystem = 1,
        }

        public static bool IsAvailable(Type type)
        {
            switch (type)
            {
                case Type.InputManager: return true;
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
                case Type.InputManager: return new ImGuiPlatformInputManager(cursors, iniSettings);
#if HAS_INPUTSYSTEM
                case Type.InputSystem: return new ImGuiPlatformInputSystem(cursors, iniSettings);
#endif
                default:
                    Debug.LogError($"[DearImGui] {type} platform not available.");
                    return null;
            }
        }
    }
}

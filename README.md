# Dear ImGui for Unity

UPM package for the immediate mode GUI library, Dear ImGui (https://github.com/ocornut/imgui).

### Usage

- [Add package](https://docs.unity3d.com/Manual/upm-ui-giturl.html) from git URL: https://github.com/realgamessoftware/dear-imgui-unity.git .
- Add a `DearImGui` component to one of the objects in the scene.
- When using the Universal Render Pipeline, add a `Render Im Gui Feature` render feature to the renderer asset. Assign it to the `render feature` field of the DearImGui component.
- When using the High Definition Render Pipeline, add a custom render pass and select "DearImGuiPass" injected after post processing.
- Subscribe to the `ImGuiUn.Layout` event and use ImGui functions.
- Example script:
  ```cs
  using UnityEngine;
  using ImGuiNET;

  public class DearImGuiDemo : MonoBehaviour
  {
      void OnEnable()
      {
          ImGuiUn.Layout += OnLayout;
      }

      void OnDisable()
      {
          ImGuiUn.Layout -= OnLayout;
      }

      void OnLayout()
      {
          ImGui.ShowDemoWindow();
      }
  }
  ```
### Known Issues

- Alpha blending is incorrect when using HDRP. Still need to investigate why, it seems like the closer to 50% alpha you get the more opaque an object appears.
- Procedural rendering is not yet supported on HDRP.

### See Also

This package uses Dear ImGui C bindings by [cimgui](https://github.com/cimgui/cimgui) and the C# wrapper by [ImGui.NET](https://github.com/mellinoe/ImGui.NET).

The development project for the package can be found at https://github.com/realgamessoftware/dear-imgui-unity-dev .

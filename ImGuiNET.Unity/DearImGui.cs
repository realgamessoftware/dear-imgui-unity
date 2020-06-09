using UnityEngine;
using UnityEngine.Rendering;
using Unity.Profiling;

#if HAS_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace ImGuiNET.Unity
{
    // This component is responsible for setting up ImGui for use in Unity.
    // It holds the necessary context and sets it up before any operation is done to ImGui.
    // (e.g. set the context, texture and font managers before calling Layout)

    /// <summary>
    /// Dear ImGui integration into Unity
    /// </summary>
    public class DearImGui : MonoBehaviour
    {
        ImGuiUnityContext _context;
        IImGuiRenderer _renderer;
        IImGuiPlatform _platform;
        CommandBuffer _cmd;
        bool _usingURP;
        bool _usingHDRP;

        public event System.Action Layout;  // Layout event for *this* ImGui instance
        [SerializeField] bool _doGlobalLayout = true; // do global/default Layout event too
        
        [SerializeField] Camera _camera = null;
        [SerializeField] RenderImGuiFeature _renderFeature = null;

        #if HAS_HDRP
            HDAdditionalCameraData _hdCameraData;
        #endif

        [SerializeField] RenderUtils.RenderType _rendererType = RenderUtils.RenderType.Mesh;
        [SerializeField] Platform.Type _platformType = Platform.Type.InputManager;

        [Header("Configuration")]
        [SerializeField] IOConfig _initialConfiguration = default;
        [SerializeField] FontAtlasConfigAsset _fontAtlasConfiguration = null;
        [SerializeField] IniSettingsAsset _iniSettings = null;  // null: uses default imgui.ini file

        [Header("Customization")]
        [SerializeField] ShaderResourcesAsset _shaders = null;
        [SerializeField] StyleAsset _style = null;
        [SerializeField] CursorShapesAsset _cursorShapes = null;

        const string CommandBufferTag = "DearImGui";
        static readonly ProfilerMarker s_prepareFramePerfMarker = new ProfilerMarker("DearImGui.PrepareFrame");
        static readonly ProfilerMarker s_layoutPerfMarker = new ProfilerMarker("DearImGui.Layout");
        static readonly ProfilerMarker s_drawListPerfMarker = new ProfilerMarker("DearImGui.RenderDrawLists");

        void Awake()
        {
            _context = ImGuiUn.CreateUnityContext();
        }

        void OnDestroy()
        {
            ImGuiUn.DestroyUnityContext(_context);
        }

        void OnEnable()
        {
            _usingURP  = RenderUtils.IsUsingURP();

            if (_camera == null) Fail(nameof(_camera));
            if (_renderFeature == null && _usingURP) Fail(nameof(_renderFeature));


            _cmd = RenderUtils.GetCommandBuffer(CommandBufferTag);
#if HAS_HDRP
            _usingHDRP = RenderUtils.IsUsingHDRP();
            if (_usingHDRP)
            {    
                // I know... I know... using a goto, but in this instance it makes things a lot cleaner 
                // due to the preprocessor directives and how unity has grouped things.
                goto PostCommandBufferSetup;
            }
#endif

            if (_usingURP)
                _renderFeature.commandBuffer = _cmd;
            else
                _camera.AddCommandBuffer(CameraEvent.AfterEverything, _cmd);

            PostCommandBufferSetup:

            ImGuiUn.SetUnityContext(_context);
            ImGuiIOPtr io = ImGui.GetIO();

            _initialConfiguration.ApplyTo(io);
            _style?.ApplyTo(ImGui.GetStyle());

            _context.textures.BuildFontAtlas(io, _fontAtlasConfiguration);
            _context.textures.Initialize(io);

            SetPlatform(Platform.Create(_platformType, _cursorShapes, _iniSettings), io);
            SetRenderer(RenderUtils.Create(_rendererType, _shaders, _context.textures), io);
            if (_platform == null) Fail(nameof(_platform));
            if (_renderer == null) Fail(nameof(_renderer));

            void Fail(string reason)
            {
                OnDisable();
                enabled = false;
                throw new System.Exception($"Failed to start: {reason}");
            }
        }

        void OnDisable()
        {
            ImGuiUn.SetUnityContext(_context);
            ImGuiIOPtr io = ImGui.GetIO();

            SetRenderer(null, io);
            SetPlatform(null, io);

            ImGuiUn.SetUnityContext(null);

            _context.textures.Shutdown();
            _context.textures.DestroyFontAtlas(io);

#if HAS_HDRP
            _usingHDRP = RenderUtils.IsUsingHDRP();
            if (_usingHDRP)
            {
                // I know... I know... using a goto, but in this instance it makes things a lot cleaner 
                // due to the preprocessor directives and how unity has grouped things.
                goto PostCommandBufferTeardown;
            }
#endif

            if (_usingURP)
            {
                if (_renderFeature != null)
                    _renderFeature.commandBuffer = null;
            }
            else
            {
                if (_camera != null)
                    _camera.RemoveCommandBuffer(CameraEvent.AfterEverything, _cmd);
            }

            PostCommandBufferTeardown:


            if (_cmd != null)
                RenderUtils.ReleaseCommandBuffer(_cmd);
            _cmd = null;
        }

#if HAS_HDRP
        public CommandBuffer GetCommandBuffer()
        {
            return _cmd;
        }
#endif

        void Reset()
        {
            _camera = Camera.main;
            _initialConfiguration.SetDefaults();
        }

        public void Reload()
        {
            OnDisable();
            OnEnable();
        }

        void Update()
        {
            ImGuiUn.SetUnityContext(_context);
            ImGuiIOPtr io = ImGui.GetIO();

            s_prepareFramePerfMarker.Begin(this);
            _context.textures.PrepareFrame(io);
            _platform.PrepareFrame(io, _camera.pixelRect);
            ImGui.NewFrame();
            s_prepareFramePerfMarker.End();

            s_layoutPerfMarker.Begin(this);
            try
            {
                if (_doGlobalLayout)
                    ImGuiUn.DoLayout();   // ImGuiUn.Layout: global handlers
                
                Layout?.Invoke();     // this.Layout: handlers specific to this instance
            }
            finally
            {
                ImGui.Render();
                s_layoutPerfMarker.End();
            }

            s_drawListPerfMarker.Begin(this);
            _cmd.Clear();
            _renderer.RenderDrawLists(_cmd, ImGui.GetDrawData());
            s_drawListPerfMarker.End();
        }

        void SetRenderer(IImGuiRenderer renderer, ImGuiIOPtr io)
        {
            _renderer?.Shutdown(io);
            _renderer = renderer;
            _renderer?.Initialize(io);
        }

        void SetPlatform(IImGuiPlatform platform, ImGuiIOPtr io)
        {
            _platform?.Shutdown(io);
            _platform = platform;
            _platform?.Initialize(io);
        }
    }
}

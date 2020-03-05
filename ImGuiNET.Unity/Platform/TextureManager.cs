using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET.Unity
{
    class SpriteInfo
    {
        public Texture texture;
        public Vector2 size;
        public Vector2 uv0, uv1;
    }

    class TextureManager
    {
        Texture2D _atlasTex;
        int _currentTextureId;
        readonly Dictionary<int, Texture> _textures = new Dictionary<int, Texture>();
        readonly Dictionary<Texture, int> _textureIds = new Dictionary<Texture, int>();
        readonly Dictionary<Sprite, SpriteInfo> _spriteData = new Dictionary<Sprite, SpriteInfo>();
        readonly HashSet<IntPtr> _allocatedGlyphRangeArrays = new HashSet<IntPtr>(IntPtrEqualityComparer.Instance);

        public void PrepareFrame(ImGuiIOPtr io)
        {
            _currentTextureId = 0;
            _textures.Clear();
            _textureIds.Clear();
            int id = RegisterTexture(_atlasTex);
            io.Fonts.SetTexID((IntPtr)id);
        }

        int RegisterTexture(Texture texture)
        {
            _textures[++_currentTextureId] = texture;
            _textureIds[texture] = _currentTextureId;
            return _currentTextureId;
        }

        public Texture GetTexture(int id)
        {
            _textures.TryGetValue(id, out Texture texture);
            return texture;
        }

        public int GetTextureId(Texture texture)
        {
            return _textureIds.TryGetValue(texture, out int id)
                ? id
                : RegisterTexture(texture);
        }

        public SpriteInfo GetSpriteInfo(Sprite sprite)
        {
            if (!_spriteData.TryGetValue(sprite, out SpriteInfo sprInfo))
            {
                Vector2[] uvs = sprite.uv; // allocates
                _spriteData[sprite] = sprInfo = new SpriteInfo
                {
                    texture = sprite.texture,
                    size = sprite.rect.size,
                    uv0 = new Vector2(uvs[0].x, 1f - uvs[0].y),
                    uv1 = new Vector2(uvs[1].x, 1f - uvs[1].y),
                };
            }
            return sprInfo;
        }

        unsafe IntPtr AllocateGlyphRangeArray(in FontConfig fontConfig)
        {
            var values = fontConfig.BuildRanges();
            if (values.Count == 0)
                return IntPtr.Zero;

            int byteCount = sizeof(ushort) * (values.Count + 1); // terminating zero
            var ranges = (ushort*)Util.Allocate(byteCount);
            _allocatedGlyphRangeArrays.Add((IntPtr)ranges);
            for (var i = 0; i < values.Count; ++i)
                ranges[i] = values[i];
            ranges[values.Count] = 0;
            return (IntPtr)ranges;
        }

        unsafe void FreeGlyphRangeArrays()
        {
            foreach (var range in _allocatedGlyphRangeArrays)
                Util.Free((byte*)range);
            _allocatedGlyphRangeArrays.Clear();
        }

        public void BuildFontAtlas(ImGuiIOPtr io, in FontAtlasConfigAsset settings)
        {
            if (io.Fonts.IsBuilt())
                DestroyFontAtlas(io);

            // don't add cursors if not drawing them
            if (!io.MouseDrawCursor)
                io.Fonts.Flags |= ImFontAtlasFlags.NoMouseCursors;

            // no font config asset: use defaults
            if (settings == null)
            {
                io.Fonts.AddFontDefault();
                io.Fonts.Build();
                return;
            }

            // add fonts from config asset
            foreach (var fontDefinition in settings.Fonts)
            {
                var fontPath = System.IO.Path.Combine(Application.streamingAssetsPath, fontDefinition.FontPath);
                if (!System.IO.File.Exists(fontPath))
                {
                    Debug.Log($"Font file not found: {fontPath}");
                    continue;
                }

                var fontConfig = new ImFontConfig();
                var fontConfigPtr = new ImFontConfigPtr(ref fontConfig);
                fontDefinition.Config.ApplyTo(fontConfigPtr);
                fontConfigPtr.GlyphRanges = AllocateGlyphRangeArray(fontDefinition.Config);
                io.Fonts.AddFontFromFileTTF(fontPath, fontDefinition.Config.SizeInPixels, fontConfigPtr);
            }

            if (io.Fonts.Fonts.Size == 0)
                io.Fonts.AddFontDefault();

            switch (settings.Rasterizer)
            {
                case FontRasterizerType.StbTrueType:
                    io.Fonts.Build();
                    break;
#if IMGUI_FEATURE_FREETYPE
                case FontRasterizerType.FreeType:
                    ImFreetype.BuildFontAtlas(io.Fonts, (ImFreetype.RasterizerFlags)settings.RasterizerFlags);
                    break;
#endif
                default:
                    Debug.LogWarning($"{settings.Rasterizer:G} rasterizer not available, using {default(FontRasterizerType):G}. Check if feature is enabled (PluginFeatures.cs).");
                    io.Fonts.Build();
                    break;
            }
        }

        public unsafe void DestroyFontAtlas(ImGuiIOPtr io)
        {
            FreeGlyphRangeArrays();

            io.Fonts.Clear(); // previous FontDefault reference no longer valid
            io.NativePtr->FontDefault = default; // NULL uses Fonts[0]
        }

        public void Initialize(ImGuiIOPtr io)
        {
            // load and register font atlas
            _atlasTex = CreateAtlasTexture(io.Fonts);
        }

        public void Shutdown()
        {
            _currentTextureId = 0;
            _textures.Clear();
            _textureIds.Clear();
            _spriteData.Clear();
            if (_atlasTex != null) { GameObject.Destroy(_atlasTex); _atlasTex = null; }
        }

        unsafe Texture2D CreateAtlasTexture(ImFontAtlasPtr atlas)
        {
            atlas.GetTexDataAsRGBA32(out byte* pixels, out int width, out int height, out int bytesPerPixel);
            var atlasTexture = new Texture2D(width, height, TextureFormat.RGBA32, false, false) { filterMode = FilterMode.Point };

            NativeArray<byte> srcData = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(
                (void*)pixels, width * height * bytesPerPixel, Allocator.None);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref srcData, AtomicSafetyHandle.GetTempMemoryHandle());
#endif
            // invert y while copying the atlas texture
            NativeArray<byte> dstData = atlasTexture.GetRawTextureData<byte>();
            int stride = width * bytesPerPixel;
            for (int y = 0; y < height; ++y)
                NativeArray<byte>.Copy(srcData, y * stride, dstData, (height - y - 1) * stride, stride);
            atlasTexture.Apply();

            return atlasTexture;
        }
    }
}

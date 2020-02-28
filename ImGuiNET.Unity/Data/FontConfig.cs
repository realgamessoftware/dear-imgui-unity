using System.Collections.Generic;
using UnityEngine;

namespace ImGuiNET.Unity
{
    [System.Serializable]
    struct FontConfig
    {
        [Tooltip("Index of font within TTF/OTF file.")]
        public int FontIndexInFile;

        [Tooltip("Size in pixels for rasterizer.")]
        public float SizeInPixels;

        [Tooltip("Rasterize at higher quality for sub-pixel positioning.\nWe don't use sub-pixel positions on the Y-axis. Oversampling still benefits animation and rotation.\nRead https://github.com/nothings/stb/blob/master/tests/oversample/README.md for details.")]
        public Vector2Int Oversample;

        [Tooltip("Align every glyph to pixel boundary. Useful e.g. if you are merging a non-pixel aligned font with the default font. If enabled, you can set OversampleH/V to 1.")]
        public bool PixelSnapH;

        [Tooltip("Extra spacing (in pixels) between glyphs. Only X axis is supported for now.")]
        public Vector2 GlyphExtraSpacing;

        [Tooltip("Offest all glyphs from this font input.")]
        public Vector2 GlyphOffset;

        [Tooltip("Glyph ranges for different writing systems.")]
        public ScriptGlyphRanges GlyphRanges;

        [Tooltip("Minimum AdvanceX for glyphs, set Min to align font icons, set both Min/Max to enforce mono-space font.")]
        public float GlyphMinAdvanceX;

        [Tooltip("Maximum AdvanceX for glyphs.")]
        public float GlyphMaxAdvanceX;

        [Tooltip("Merge into previous ImFont, so you can combine multiple inputs font into one ImFont. You may want to use GlyphOffset.y when merge font of different heights.")]
        public bool MergeIntoPrevious;

        [Tooltip("Settings for custom font rasterizer (e.g. FreeType). Leave as zero if you aren't using one.")]
        public uint RasterizerFlags;

        [Tooltip("Brighten (>1.0f) or darken (<1.0f) font output. Brightening small fonts may be a good workaround to make them more readable.")]
        public float RasterizerMultiply;

        [Tooltip("Explicitly specify unicode codepoint of ellipsis character. When fonts are being merged first specified ellipsis will be used.")]
        public char EllipsisChar;

        [Tooltip("User-provided list of Unicode range (2 value per range, values are inclusive).")]
        public Range[] CustomGlyphRanges;

        public void SetDefaults()
        {
            unsafe
            {
                ImFontConfig* imFontConfig = ImGuiNative.ImFontConfig_ImFontConfig();
                SetFrom(imFontConfig);
                ImGuiNative.ImFontConfig_destroy(imFontConfig);
            }
        }

        public void ApplyTo(ImFontConfigPtr im)
        {
            im.FontNo = FontIndexInFile;
            im.SizePixels = SizeInPixels;
            im.OversampleH = Oversample.x;
            im.OversampleV = Oversample.y;
            im.PixelSnapH = PixelSnapH;
            im.GlyphExtraSpacing = GlyphExtraSpacing;
            im.GlyphOffset = GlyphOffset;
            im.GlyphMinAdvanceX = GlyphMinAdvanceX;
            im.GlyphMaxAdvanceX = GlyphMaxAdvanceX;
            im.MergeMode = MergeIntoPrevious;
            im.RasterizerFlags = RasterizerFlags;
            im.RasterizerMultiply = RasterizerMultiply;
            im.EllipsisChar = EllipsisChar;

            // setting GlyphRanges requires allocating memory so it is not done here
            // use BuildRanges to get a List with the values, then allocate memory and copy
            // (see TextureManager)
        }

        public void SetFrom(ImFontConfigPtr im)
        {
            FontIndexInFile = im.FontNo;
            SizeInPixels = im.SizePixels;
            Oversample = new Vector2Int(im.OversampleH, im.OversampleV);
            PixelSnapH = im.PixelSnapH;
            GlyphExtraSpacing = im.GlyphExtraSpacing;
            GlyphOffset = im.GlyphOffset;
            GlyphMinAdvanceX = im.GlyphMinAdvanceX;
            GlyphMaxAdvanceX = im.GlyphMaxAdvanceX;
            MergeIntoPrevious = im.MergeMode;
            RasterizerFlags = im.RasterizerFlags;
            RasterizerMultiply = im.RasterizerMultiply;
            EllipsisChar = (char)im.EllipsisChar;

            // no good way to set GlyphRanges, do manually
        }

        public unsafe List<ushort> BuildRanges()
        {
            var atlas = (ImFontAtlas*)0;
            var ranges = new List<ushort>();
            if ((GlyphRanges & ScriptGlyphRanges.Default) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesDefault(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.Cyrillic) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesCyrillic(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.Japanese) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesJapanese(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.Korean) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesKorean(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.Thai) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesThai(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.Vietnamese) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesVietnamese(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.ChineseSimplified) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesChineseSimplifiedCommon(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.ChineseFull) != 0)
                AddRangePtr(ImGuiNative.ImFontAtlas_GetGlyphRangesChineseFull(atlas));
            if ((GlyphRanges & ScriptGlyphRanges.Custom) != 0)
                foreach (Range range in CustomGlyphRanges)
                    ranges.AddRange(new[] { range.Start, range.End });
            return ranges;
            void AddRangePtr(ushort* r) { while (*r != 0) ranges.Add(*r++); };
        }

        [System.Flags]
        internal enum ScriptGlyphRanges
        {
            Default           = 1 << 0,
            Cyrillic          = 1 << 1,
            Japanese          = 1 << 2,
            Korean            = 1 << 3,
            Thai              = 1 << 4,
            Vietnamese        = 1 << 5,
            ChineseSimplified = 1 << 6,
            ChineseFull       = 1 << 7,
            Custom            = 1 << 8,
        }

        [System.Serializable]
        internal struct Range
        {
            public ushort Start;
            public ushort End;
        }
    }
}

namespace ImGuiNET
{
    public static unsafe partial class ImFreetype
    {
        public static bool BuildFontAtlas(ImFontAtlasPtr atlas, RasterizerFlags extra_flags)
        {
            return ImFreetypeNative.frBuildFontAtlas(atlas.NativePtr, (uint)extra_flags);
        }

        /// <summary>
        /// Hinting greatly impacts visuals (and glyph sizes).
        /// When disabled, FreeType generates blurrier glyphs, more or less matches the stb's output.
        /// The Default hinting mode usually looks good, but may distort glyphs in an unusual way.
        /// The Light hinting mode generates fuzzier glyphs but butter matches Microsoft's rasterizer.
        ///
        /// You can set those flags on a per font basis in ImFontConfigPtr.RasterizerFlags.
        /// Use the 'extra_flags' parameter of BuildFontAtlas() to force a flag on all your fonts.
        /// </summary>
        [System.Flags]
        public enum RasterizerFlags : uint
        {
            ///<summary>By default, hinting is enabled and the font's native hinter is preferred over the auto hinter.</summary>
            None          = 0,

            ///<summary>Disable hinting. This generally generates 'blurrier' bitmap glyphs when the glyph are rendered in any of the anti-aliased modes.</summary>
            NoHinting     = 1 << 0,

            ///<summary>Disable auto-hinter.</summary>
            NoAutoHint    = 1 << 1,

            ///<summary>Indicates that the auto-hinter is preferred over the font's native hinter.</summary>
            ForceAutoHint = 1 << 2,

            ///<summary>A lighter hinting algorithm for gray-level modes. Many generated glyphs are fuzzier but butter resemble original shape. This is achieved by snapping glyphs to the pixel grid only vertically (Y-axis), as is done by Microsoft's ClearType and Adobe's proprietary font renderer. This preserves inter-glyph spacing in horizontal text.</summary>
            LightHinting  = 1 << 3,

            ///<summary>Strong hinting algorithm that should only be used for monochrome output.</summary>
            MonoHinting   = 1 << 4,

            ///<summary>Styling: Should we artificially embolden the font?</summary>
            Bold          = 1 << 5,

            ///<summary>Styling: Should we slant the font, emulating italic style?</summary>
            Oblique       = 1 << 6,

            ///<summary>Disable anti-aliasing. Combine this with MonoHinting for best results!</summary>
            MonoChrome    = 1 << 7,
        }
    }
}

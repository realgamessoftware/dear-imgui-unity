using System.Text;
using UnityEngine;

namespace ImGuiNET
{
    public unsafe partial struct ImFontPtr
    {
        public Vector2 CalcTextSizeA(float font_size, float max_width, float wrap_width, string text)
        {
            byte* native_text;
            int text_byteCount = 0;
            if (text != null)
            {
                text_byteCount = Encoding.UTF8.GetByteCount(text);
                if (text_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_text = Util.Allocate(text_byteCount + 1);
                }
                else
                {
                    byte* native_text_stackBytes = stackalloc byte[text_byteCount + 1];
                    native_text = native_text_stackBytes;
                }
                int native_text_offset = Util.GetUtf8(text, native_text, text_byteCount);
                native_text[native_text_offset] = 0;
            }
            else { native_text = null; }
            byte* native_text_end = null;
            Vector2 ret = ImGuiNative.ImFont_CalcTextSizeA(NativePtr, font_size, max_width, wrap_width, native_text, native_text_end, (byte**)0);
            if (text_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_text);
            }
            return ret;
        }

        public Vector2 CalcTextSizeA(float font_size, float max_width, float wrap_width, char ch)
        {
            byte* native_text;
            int text_byteCount = 0;
            text_byteCount = Encoding.UTF8.GetByteCount(&ch, 1);
            byte* native_text_stackBytes = stackalloc byte[text_byteCount + 1];
            native_text = native_text_stackBytes;
            int native_text_offset = Encoding.UTF8.GetBytes(&ch, 1, native_text, text_byteCount);
            native_text[native_text_offset] = 0;
            byte* native_text_end = null;
            Vector2 ret = ImGuiNative.ImFont_CalcTextSizeA(NativePtr, font_size, max_width, wrap_width, native_text, native_text_end, (byte**)0);
            return ret;
        }

        public int CalcWordWrapPositionA(float scale, string text, float wrap_width)
        {
            byte* native_text;
            int text_byteCount = 0;
            if (text != null)
            {
                text_byteCount = Encoding.UTF8.GetByteCount(text);
                if (text_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_text = Util.Allocate(text_byteCount + 1);
                }
                else
                {
                    byte* native_text_stackBytes = stackalloc byte[text_byteCount + 1];
                    native_text = native_text_stackBytes;
                }
                int native_text_offset = Util.GetUtf8(text, native_text, text_byteCount);
                native_text[native_text_offset] = 0;
            }
            else { native_text = null; }
            byte* native_text_end = null;
            byte* ret = ImGuiNative.ImFont_CalcWordWrapPositionA(NativePtr, scale, native_text, native_text_end, wrap_width);
            if (text_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_text);
            }
            return Encoding.UTF8.GetCharCount(native_text, (int)(ret - native_text));
        }

        public void RenderText(ImDrawListPtr draw_list, float size, Vector2 pos, uint col, Vector4 clip_rect, string text, float wrap_width = 0.0f, bool cpu_fine_clip = false)
        {
            ImDrawList* native_draw_list = draw_list.NativePtr;
            byte* native_text;
            int text_byteCount = 0;
            if (text != null)
            {
                text_byteCount = Encoding.UTF8.GetByteCount(text);
                if (text_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_text = Util.Allocate(text_byteCount + 1);
                }
                else
                {
                    byte* native_text_stackBytes = stackalloc byte[text_byteCount + 1];
                    native_text = native_text_stackBytes;
                }
                int native_text_offset = Util.GetUtf8(text, native_text, text_byteCount);
                native_text[native_text_offset] = 0;
            }
            else { native_text = null; }
            byte* native_text_end = null;
            ImGuiNative.ImFont_RenderText(NativePtr, native_draw_list, size, pos, col, clip_rect, native_text, native_text_end, wrap_width, cpu_fine_clip ? (byte)1 : (byte)0);
            if (text_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_text);
            }
        }
    }
}

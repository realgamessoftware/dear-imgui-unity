using System;
using System.Text;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiStyle
    {
        public float Alpha;
        public Vector2 WindowPadding;
        public float WindowRounding;
        public float WindowBorderSize;
        public Vector2 WindowMinSize;
        public Vector2 WindowTitleAlign;
        public ImGuiDir WindowMenuButtonPosition;
        public float ChildRounding;
        public float ChildBorderSize;
        public float PopupRounding;
        public float PopupBorderSize;
        public Vector2 FramePadding;
        public float FrameRounding;
        public float FrameBorderSize;
        public Vector2 ItemSpacing;
        public Vector2 ItemInnerSpacing;
        public Vector2 TouchExtraPadding;
        public float IndentSpacing;
        public float ColumnsMinSpacing;
        public float ScrollbarSize;
        public float ScrollbarRounding;
        public float GrabMinSize;
        public float GrabRounding;
        public float TabRounding;
        public float TabBorderSize;
        public ImGuiDir ColorButtonPosition;
        public Vector2 ButtonTextAlign;
        public Vector2 SelectableTextAlign;
        public Vector2 DisplayWindowPadding;
        public Vector2 DisplaySafeAreaPadding;
        public float MouseCursorScale;
        public byte AntiAliasedLines;
        public byte AntiAliasedFill;
        public float CurveTessellationTol;
        public float CircleSegmentMaxError;
        public Vector4 Colors_0;
        public Vector4 Colors_1;
        public Vector4 Colors_2;
        public Vector4 Colors_3;
        public Vector4 Colors_4;
        public Vector4 Colors_5;
        public Vector4 Colors_6;
        public Vector4 Colors_7;
        public Vector4 Colors_8;
        public Vector4 Colors_9;
        public Vector4 Colors_10;
        public Vector4 Colors_11;
        public Vector4 Colors_12;
        public Vector4 Colors_13;
        public Vector4 Colors_14;
        public Vector4 Colors_15;
        public Vector4 Colors_16;
        public Vector4 Colors_17;
        public Vector4 Colors_18;
        public Vector4 Colors_19;
        public Vector4 Colors_20;
        public Vector4 Colors_21;
        public Vector4 Colors_22;
        public Vector4 Colors_23;
        public Vector4 Colors_24;
        public Vector4 Colors_25;
        public Vector4 Colors_26;
        public Vector4 Colors_27;
        public Vector4 Colors_28;
        public Vector4 Colors_29;
        public Vector4 Colors_30;
        public Vector4 Colors_31;
        public Vector4 Colors_32;
        public Vector4 Colors_33;
        public Vector4 Colors_34;
        public Vector4 Colors_35;
        public Vector4 Colors_36;
        public Vector4 Colors_37;
        public Vector4 Colors_38;
        public Vector4 Colors_39;
        public Vector4 Colors_40;
        public Vector4 Colors_41;
        public Vector4 Colors_42;
        public Vector4 Colors_43;
        public Vector4 Colors_44;
        public Vector4 Colors_45;
        public Vector4 Colors_46;
        public Vector4 Colors_47;
    }
    public unsafe partial struct ImGuiStylePtr
    {
        public ImGuiStyle* NativePtr { get; }
        public ImGuiStylePtr(ImGuiStyle* nativePtr) => NativePtr = nativePtr;
        public ImGuiStylePtr(IntPtr nativePtr) => NativePtr = (ImGuiStyle*)nativePtr;
        public static implicit operator ImGuiStylePtr(ImGuiStyle* nativePtr) => new ImGuiStylePtr(nativePtr);
        public static implicit operator ImGuiStyle* (ImGuiStylePtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiStylePtr(IntPtr nativePtr) => new ImGuiStylePtr(nativePtr);
        public ref float Alpha => ref UnsafeUtility.AsRef<float>(&NativePtr->Alpha);
        public ref Vector2 WindowPadding => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->WindowPadding);
        public ref float WindowRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->WindowRounding);
        public ref float WindowBorderSize => ref UnsafeUtility.AsRef<float>(&NativePtr->WindowBorderSize);
        public ref Vector2 WindowMinSize => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->WindowMinSize);
        public ref Vector2 WindowTitleAlign => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->WindowTitleAlign);
        public ref ImGuiDir WindowMenuButtonPosition => ref UnsafeUtility.AsRef<ImGuiDir>(&NativePtr->WindowMenuButtonPosition);
        public ref float ChildRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->ChildRounding);
        public ref float ChildBorderSize => ref UnsafeUtility.AsRef<float>(&NativePtr->ChildBorderSize);
        public ref float PopupRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->PopupRounding);
        public ref float PopupBorderSize => ref UnsafeUtility.AsRef<float>(&NativePtr->PopupBorderSize);
        public ref Vector2 FramePadding => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->FramePadding);
        public ref float FrameRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->FrameRounding);
        public ref float FrameBorderSize => ref UnsafeUtility.AsRef<float>(&NativePtr->FrameBorderSize);
        public ref Vector2 ItemSpacing => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->ItemSpacing);
        public ref Vector2 ItemInnerSpacing => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->ItemInnerSpacing);
        public ref Vector2 TouchExtraPadding => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->TouchExtraPadding);
        public ref float IndentSpacing => ref UnsafeUtility.AsRef<float>(&NativePtr->IndentSpacing);
        public ref float ColumnsMinSpacing => ref UnsafeUtility.AsRef<float>(&NativePtr->ColumnsMinSpacing);
        public ref float ScrollbarSize => ref UnsafeUtility.AsRef<float>(&NativePtr->ScrollbarSize);
        public ref float ScrollbarRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->ScrollbarRounding);
        public ref float GrabMinSize => ref UnsafeUtility.AsRef<float>(&NativePtr->GrabMinSize);
        public ref float GrabRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->GrabRounding);
        public ref float TabRounding => ref UnsafeUtility.AsRef<float>(&NativePtr->TabRounding);
        public ref float TabBorderSize => ref UnsafeUtility.AsRef<float>(&NativePtr->TabBorderSize);
        public ref ImGuiDir ColorButtonPosition => ref UnsafeUtility.AsRef<ImGuiDir>(&NativePtr->ColorButtonPosition);
        public ref Vector2 ButtonTextAlign => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->ButtonTextAlign);
        public ref Vector2 SelectableTextAlign => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->SelectableTextAlign);
        public ref Vector2 DisplayWindowPadding => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DisplayWindowPadding);
        public ref Vector2 DisplaySafeAreaPadding => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DisplaySafeAreaPadding);
        public ref float MouseCursorScale => ref UnsafeUtility.AsRef<float>(&NativePtr->MouseCursorScale);
        public ref bool AntiAliasedLines => ref UnsafeUtility.AsRef<bool>(&NativePtr->AntiAliasedLines);
        public ref bool AntiAliasedFill => ref UnsafeUtility.AsRef<bool>(&NativePtr->AntiAliasedFill);
        public ref float CurveTessellationTol => ref UnsafeUtility.AsRef<float>(&NativePtr->CurveTessellationTol);
        public ref float CircleSegmentMaxError => ref UnsafeUtility.AsRef<float>(&NativePtr->CircleSegmentMaxError);
        public RangeAccessor<Vector4> Colors => new RangeAccessor<Vector4>(&NativePtr->Colors_0, 48);
        public void Destroy()
        {
            ImGuiNative.ImGuiStyle_destroy(NativePtr);
        }
        public void ScaleAllSizes(float scale_factor)
        {
            ImGuiNative.ImGuiStyle_ScaleAllSizes(NativePtr, scale_factor);
        }
    }
}

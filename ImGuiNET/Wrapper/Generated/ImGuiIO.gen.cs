using System;
using System.Text;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

namespace ImGuiNET
{
    public unsafe partial struct ImGuiIO
    {
        public ImGuiConfigFlags ConfigFlags;
        public ImGuiBackendFlags BackendFlags;
        public Vector2 DisplaySize;
        public float DeltaTime;
        public float IniSavingRate;
        public byte* IniFilename;
        public byte* LogFilename;
        public float MouseDoubleClickTime;
        public float MouseDoubleClickMaxDist;
        public float MouseDragThreshold;
        public fixed int KeyMap[22];
        public float KeyRepeatDelay;
        public float KeyRepeatRate;
        public void* UserData;
        public ImFontAtlas* Fonts;
        public float FontGlobalScale;
        public byte FontAllowUserScaling;
        public ImFont* FontDefault;
        public Vector2 DisplayFramebufferScale;
        public byte MouseDrawCursor;
        public byte ConfigMacOSXBehaviors;
        public byte ConfigInputTextCursorBlink;
        public byte ConfigWindowsResizeFromEdges;
        public byte ConfigWindowsMoveFromTitleBarOnly;
        public float ConfigWindowsMemoryCompactTimer;
        public byte* BackendPlatformName;
        public byte* BackendRendererName;
        public void* BackendPlatformUserData;
        public void* BackendRendererUserData;
        public void* BackendLanguageUserData;
        public IntPtr GetClipboardTextFn;
        public IntPtr SetClipboardTextFn;
        public void* ClipboardUserData;
        public IntPtr ImeSetInputScreenPosFn;
        public void* ImeWindowHandle;
        public void* RenderDrawListsFnUnused;
        public Vector2 MousePos;
        public fixed byte MouseDown[5];
        public float MouseWheel;
        public float MouseWheelH;
        public byte KeyCtrl;
        public byte KeyShift;
        public byte KeyAlt;
        public byte KeySuper;
        public fixed byte KeysDown[512];
        public fixed float NavInputs[21];
        public byte WantCaptureMouse;
        public byte WantCaptureKeyboard;
        public byte WantTextInput;
        public byte WantSetMousePos;
        public byte WantSaveIniSettings;
        public byte NavActive;
        public byte NavVisible;
        public float Framerate;
        public int MetricsRenderVertices;
        public int MetricsRenderIndices;
        public int MetricsRenderWindows;
        public int MetricsActiveWindows;
        public int MetricsActiveAllocations;
        public Vector2 MouseDelta;
        public Vector2 MousePosPrev;
        public Vector2 MouseClickedPos_0;
        public Vector2 MouseClickedPos_1;
        public Vector2 MouseClickedPos_2;
        public Vector2 MouseClickedPos_3;
        public Vector2 MouseClickedPos_4;
        public fixed double MouseClickedTime[5];
        public fixed byte MouseClicked[5];
        public fixed byte MouseDoubleClicked[5];
        public fixed byte MouseReleased[5];
        public fixed byte MouseDownOwned[5];
        public fixed byte MouseDownWasDoubleClick[5];
        public fixed float MouseDownDuration[5];
        public fixed float MouseDownDurationPrev[5];
        public Vector2 MouseDragMaxDistanceAbs_0;
        public Vector2 MouseDragMaxDistanceAbs_1;
        public Vector2 MouseDragMaxDistanceAbs_2;
        public Vector2 MouseDragMaxDistanceAbs_3;
        public Vector2 MouseDragMaxDistanceAbs_4;
        public fixed float MouseDragMaxDistanceSqr[5];
        public fixed float KeysDownDuration[512];
        public fixed float KeysDownDurationPrev[512];
        public fixed float NavInputsDownDuration[21];
        public fixed float NavInputsDownDurationPrev[21];
        public ImVector InputQueueCharacters;
    }
    public unsafe partial struct ImGuiIOPtr
    {
        public ImGuiIO* NativePtr { get; }
        public ImGuiIOPtr(ImGuiIO* nativePtr) => NativePtr = nativePtr;
        public ImGuiIOPtr(IntPtr nativePtr) => NativePtr = (ImGuiIO*)nativePtr;
        public static implicit operator ImGuiIOPtr(ImGuiIO* nativePtr) => new ImGuiIOPtr(nativePtr);
        public static implicit operator ImGuiIO* (ImGuiIOPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiIOPtr(IntPtr nativePtr) => new ImGuiIOPtr(nativePtr);
        public ref ImGuiConfigFlags ConfigFlags => ref UnsafeUtility.AsRef<ImGuiConfigFlags>(&NativePtr->ConfigFlags);
        public ref ImGuiBackendFlags BackendFlags => ref UnsafeUtility.AsRef<ImGuiBackendFlags>(&NativePtr->BackendFlags);
        public ref Vector2 DisplaySize => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DisplaySize);
        public ref float DeltaTime => ref UnsafeUtility.AsRef<float>(&NativePtr->DeltaTime);
        public ref float IniSavingRate => ref UnsafeUtility.AsRef<float>(&NativePtr->IniSavingRate);
        public NullTerminatedString IniFilename => new NullTerminatedString(NativePtr->IniFilename);
        public NullTerminatedString LogFilename => new NullTerminatedString(NativePtr->LogFilename);
        public ref float MouseDoubleClickTime => ref UnsafeUtility.AsRef<float>(&NativePtr->MouseDoubleClickTime);
        public ref float MouseDoubleClickMaxDist => ref UnsafeUtility.AsRef<float>(&NativePtr->MouseDoubleClickMaxDist);
        public ref float MouseDragThreshold => ref UnsafeUtility.AsRef<float>(&NativePtr->MouseDragThreshold);
        public RangeAccessor<int> KeyMap => new RangeAccessor<int>(NativePtr->KeyMap, 22);
        public ref float KeyRepeatDelay => ref UnsafeUtility.AsRef<float>(&NativePtr->KeyRepeatDelay);
        public ref float KeyRepeatRate => ref UnsafeUtility.AsRef<float>(&NativePtr->KeyRepeatRate);
        public IntPtr UserData { get => (IntPtr)NativePtr->UserData; set => NativePtr->UserData = (void*)value; }
        public ImFontAtlasPtr Fonts => new ImFontAtlasPtr(NativePtr->Fonts);
        public ref float FontGlobalScale => ref UnsafeUtility.AsRef<float>(&NativePtr->FontGlobalScale);
        public ref bool FontAllowUserScaling => ref UnsafeUtility.AsRef<bool>(&NativePtr->FontAllowUserScaling);
        public ImFontPtr FontDefault => new ImFontPtr(NativePtr->FontDefault);
        public ref Vector2 DisplayFramebufferScale => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->DisplayFramebufferScale);
        public ref bool MouseDrawCursor => ref UnsafeUtility.AsRef<bool>(&NativePtr->MouseDrawCursor);
        public ref bool ConfigMacOSXBehaviors => ref UnsafeUtility.AsRef<bool>(&NativePtr->ConfigMacOSXBehaviors);
        public ref bool ConfigInputTextCursorBlink => ref UnsafeUtility.AsRef<bool>(&NativePtr->ConfigInputTextCursorBlink);
        public ref bool ConfigWindowsResizeFromEdges => ref UnsafeUtility.AsRef<bool>(&NativePtr->ConfigWindowsResizeFromEdges);
        public ref bool ConfigWindowsMoveFromTitleBarOnly => ref UnsafeUtility.AsRef<bool>(&NativePtr->ConfigWindowsMoveFromTitleBarOnly);
        public ref float ConfigWindowsMemoryCompactTimer => ref UnsafeUtility.AsRef<float>(&NativePtr->ConfigWindowsMemoryCompactTimer);
        public NullTerminatedString BackendPlatformName => new NullTerminatedString(NativePtr->BackendPlatformName);
        public NullTerminatedString BackendRendererName => new NullTerminatedString(NativePtr->BackendRendererName);
        public IntPtr BackendPlatformUserData { get => (IntPtr)NativePtr->BackendPlatformUserData; set => NativePtr->BackendPlatformUserData = (void*)value; }
        public IntPtr BackendRendererUserData { get => (IntPtr)NativePtr->BackendRendererUserData; set => NativePtr->BackendRendererUserData = (void*)value; }
        public IntPtr BackendLanguageUserData { get => (IntPtr)NativePtr->BackendLanguageUserData; set => NativePtr->BackendLanguageUserData = (void*)value; }
        public ref IntPtr GetClipboardTextFn => ref UnsafeUtility.AsRef<IntPtr>(&NativePtr->GetClipboardTextFn);
        public ref IntPtr SetClipboardTextFn => ref UnsafeUtility.AsRef<IntPtr>(&NativePtr->SetClipboardTextFn);
        public IntPtr ClipboardUserData { get => (IntPtr)NativePtr->ClipboardUserData; set => NativePtr->ClipboardUserData = (void*)value; }
        public ref IntPtr ImeSetInputScreenPosFn => ref UnsafeUtility.AsRef<IntPtr>(&NativePtr->ImeSetInputScreenPosFn);
        public IntPtr ImeWindowHandle { get => (IntPtr)NativePtr->ImeWindowHandle; set => NativePtr->ImeWindowHandle = (void*)value; }
        public IntPtr RenderDrawListsFnUnused { get => (IntPtr)NativePtr->RenderDrawListsFnUnused; set => NativePtr->RenderDrawListsFnUnused = (void*)value; }
        public ref Vector2 MousePos => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->MousePos);
        public RangeAccessor<bool> MouseDown => new RangeAccessor<bool>(NativePtr->MouseDown, 5);
        public ref float MouseWheel => ref UnsafeUtility.AsRef<float>(&NativePtr->MouseWheel);
        public ref float MouseWheelH => ref UnsafeUtility.AsRef<float>(&NativePtr->MouseWheelH);
        public ref bool KeyCtrl => ref UnsafeUtility.AsRef<bool>(&NativePtr->KeyCtrl);
        public ref bool KeyShift => ref UnsafeUtility.AsRef<bool>(&NativePtr->KeyShift);
        public ref bool KeyAlt => ref UnsafeUtility.AsRef<bool>(&NativePtr->KeyAlt);
        public ref bool KeySuper => ref UnsafeUtility.AsRef<bool>(&NativePtr->KeySuper);
        public RangeAccessor<bool> KeysDown => new RangeAccessor<bool>(NativePtr->KeysDown, 512);
        public RangeAccessor<float> NavInputs => new RangeAccessor<float>(NativePtr->NavInputs, 21);
        public ref bool WantCaptureMouse => ref UnsafeUtility.AsRef<bool>(&NativePtr->WantCaptureMouse);
        public ref bool WantCaptureKeyboard => ref UnsafeUtility.AsRef<bool>(&NativePtr->WantCaptureKeyboard);
        public ref bool WantTextInput => ref UnsafeUtility.AsRef<bool>(&NativePtr->WantTextInput);
        public ref bool WantSetMousePos => ref UnsafeUtility.AsRef<bool>(&NativePtr->WantSetMousePos);
        public ref bool WantSaveIniSettings => ref UnsafeUtility.AsRef<bool>(&NativePtr->WantSaveIniSettings);
        public ref bool NavActive => ref UnsafeUtility.AsRef<bool>(&NativePtr->NavActive);
        public ref bool NavVisible => ref UnsafeUtility.AsRef<bool>(&NativePtr->NavVisible);
        public ref float Framerate => ref UnsafeUtility.AsRef<float>(&NativePtr->Framerate);
        public ref int MetricsRenderVertices => ref UnsafeUtility.AsRef<int>(&NativePtr->MetricsRenderVertices);
        public ref int MetricsRenderIndices => ref UnsafeUtility.AsRef<int>(&NativePtr->MetricsRenderIndices);
        public ref int MetricsRenderWindows => ref UnsafeUtility.AsRef<int>(&NativePtr->MetricsRenderWindows);
        public ref int MetricsActiveWindows => ref UnsafeUtility.AsRef<int>(&NativePtr->MetricsActiveWindows);
        public ref int MetricsActiveAllocations => ref UnsafeUtility.AsRef<int>(&NativePtr->MetricsActiveAllocations);
        public ref Vector2 MouseDelta => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->MouseDelta);
        public ref Vector2 MousePosPrev => ref UnsafeUtility.AsRef<Vector2>(&NativePtr->MousePosPrev);
        public RangeAccessor<Vector2> MouseClickedPos => new RangeAccessor<Vector2>(&NativePtr->MouseClickedPos_0, 5);
        public RangeAccessor<double> MouseClickedTime => new RangeAccessor<double>(NativePtr->MouseClickedTime, 5);
        public RangeAccessor<bool> MouseClicked => new RangeAccessor<bool>(NativePtr->MouseClicked, 5);
        public RangeAccessor<bool> MouseDoubleClicked => new RangeAccessor<bool>(NativePtr->MouseDoubleClicked, 5);
        public RangeAccessor<bool> MouseReleased => new RangeAccessor<bool>(NativePtr->MouseReleased, 5);
        public RangeAccessor<bool> MouseDownOwned => new RangeAccessor<bool>(NativePtr->MouseDownOwned, 5);
        public RangeAccessor<bool> MouseDownWasDoubleClick => new RangeAccessor<bool>(NativePtr->MouseDownWasDoubleClick, 5);
        public RangeAccessor<float> MouseDownDuration => new RangeAccessor<float>(NativePtr->MouseDownDuration, 5);
        public RangeAccessor<float> MouseDownDurationPrev => new RangeAccessor<float>(NativePtr->MouseDownDurationPrev, 5);
        public RangeAccessor<Vector2> MouseDragMaxDistanceAbs => new RangeAccessor<Vector2>(&NativePtr->MouseDragMaxDistanceAbs_0, 5);
        public RangeAccessor<float> MouseDragMaxDistanceSqr => new RangeAccessor<float>(NativePtr->MouseDragMaxDistanceSqr, 5);
        public RangeAccessor<float> KeysDownDuration => new RangeAccessor<float>(NativePtr->KeysDownDuration, 512);
        public RangeAccessor<float> KeysDownDurationPrev => new RangeAccessor<float>(NativePtr->KeysDownDurationPrev, 512);
        public RangeAccessor<float> NavInputsDownDuration => new RangeAccessor<float>(NativePtr->NavInputsDownDuration, 21);
        public RangeAccessor<float> NavInputsDownDurationPrev => new RangeAccessor<float>(NativePtr->NavInputsDownDurationPrev, 21);
        public ImVector<ushort> InputQueueCharacters => new ImVector<ushort>(NativePtr->InputQueueCharacters);
        public void AddInputCharacter(uint c)
        {
            ImGuiNative.ImGuiIO_AddInputCharacter(NativePtr, c);
        }
        public void AddInputCharactersUTF8(string str)
        {
            byte* native_str;
            int str_byteCount = 0;
            if (str != null)
            {
                str_byteCount = Encoding.UTF8.GetByteCount(str);
                if (str_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_str = Util.Allocate(str_byteCount + 1);
                }
                else
                {
                    byte* native_str_stackBytes = stackalloc byte[str_byteCount + 1];
                    native_str = native_str_stackBytes;
                }
                int native_str_offset = Util.GetUtf8(str, native_str, str_byteCount);
                native_str[native_str_offset] = 0;
            }
            else { native_str = null; }
            ImGuiNative.ImGuiIO_AddInputCharactersUTF8(NativePtr, native_str);
            if (str_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_str);
            }
        }
        public void ClearInputCharacters()
        {
            ImGuiNative.ImGuiIO_ClearInputCharacters(NativePtr);
        }
        public void Destroy()
        {
            ImGuiNative.ImGuiIO_destroy(NativePtr);
        }
    }
}

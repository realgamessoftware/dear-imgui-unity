﻿Shader "DearImGui/Mesh"
{
    // shader for Universal render pipeline
    SubShader
    {
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" "PreviewType" = "Plane" }
        LOD 100

        Lighting Off
        Cull Off ZWrite On ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "DEARIMGUI URP"

            HLSLPROGRAM
            #pragma vertex ImGuiPassVertex
            #pragma fragment ImGuiPassFrag
            #include "Packages/com.realgames.dear-imgui/Resources/Shaders/PassesUniversal.hlsl"
            ENDHLSL
        }
    }

    // shader for HD render pipeline
    SubShader
    {
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "HDRenderPipeline" "PreviewType" = "Plane" }
        LOD 100

        Lighting Off
        Cull Off ZWrite On ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "DEARIMGUI HDRP"

            HLSLPROGRAM
            #pragma vertex ImGuiPassVertex
            #pragma fragment ImGuiPassFrag
            #include "Packages/com.realgames.dear-imgui/Resources/Shaders/PassesHD.hlsl"
            ENDHLSL
        }
    }

    // shader for builtin rendering
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
        LOD 100

        Lighting Off
        Cull Off ZWrite On ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "DEARIMGUI BUILTIN"

            CGPROGRAM
            #pragma vertex ImGuiPassVertex
            #pragma fragment ImGuiPassFrag
            #include "Packages/com.realgames.dear-imgui/Resources/Shaders/PassesBuiltin.hlsl"
            ENDCG
        }
    }
}

Shader "DearImGui/Procedural"
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
            Name "DEARIMGUI PROCEDURAL URP"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment ImGuiPassFrag
            #include "Packages/com.realgames.dear-imgui/Resources/Shaders/PassesUniversal.hlsl"

            StructuredBuffer<ImVert> _Vertices;
            int _BaseVertex;

            Varyings vert(uint id : SV_VertexID)
            {
                // BaseVertexLocation is not automatically added to SV_VertexID
                // https://twitter.com/iquilezles/status/986212611669700609?lang=en
                ImVert v = _Vertices[id + _BaseVertex];
                return ImGuiPassVertex(v);
            }
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
            Name "DEARIMGUI PROCEDURAL BUILTIN"

            CGPROGRAM
            #pragma target 4.5
            #pragma vertex vert
            #pragma fragment ImGuiPassFrag
            #include "UnityCG.cginc"
            #include "Packages/com.realgames.dear-imgui/Resources/Shaders/PassesBuiltin.hlsl"

            StructuredBuffer<ImVert> _Vertices;
            int _BaseVertex;

            Varyings vert(uint id : SV_VertexID)
            {
                // BaseVertexLocation is not automatically added to SV_VertexID
                // https://twitter.com/iquilezles/status/986212611669700609?lang=en
                ImVert v = _Vertices[id + _BaseVertex];
                return ImGuiPassVertex(v);
            }
            ENDCG
        }
    }
}

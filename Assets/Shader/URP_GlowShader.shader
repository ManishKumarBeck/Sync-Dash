Shader "Custom/URP_GlowShader"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _EmissionColor("Emission Color", Color) = (1,1,1,1)
        _EmissionStrength("Emission Strength", Range(0,10)) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            float4 _BaseColor;
            float4 _EmissionColor;
            float _EmissionStrength;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half3 color = _BaseColor.rgb + _EmissionColor.rgb * _EmissionStrength;
                return half4(color, 1);
            }
            ENDHLSL
        }
    }

    FallBack Off
}

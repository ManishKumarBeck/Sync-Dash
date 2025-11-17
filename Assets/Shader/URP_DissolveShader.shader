Shader "Custom/URP_DissolveShader"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0,1)) = 0
        _EdgeColor("Edge Color", Color) = (1,0.5,0,1)
        _EdgeWidth("Edge Width", Range(0,0.5)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _BaseColor;
            float _DissolveAmount;
            float4 _EdgeColor;
            float _EdgeWidth;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half noise = tex2D(_NoiseTex, IN.uv).r;

                // how far we are into the dissolve
                half diff = noise - _DissolveAmount;

                // edge band (0 near dissolve line)
                half edge = smoothstep(0.0, _EdgeWidth, diff);

                // base texture
                half3 baseCol = tex2D(_MainTex, IN.uv).rgb * _BaseColor.rgb;

                // mix bright edge color where diff is close to 0
                half edgeFactor = smoothstep(0.0, _EdgeWidth * 0.5, abs(diff));
                half3 col = lerp(_EdgeColor.rgb, baseCol, edgeFactor);

                // actually remove pixels beyond the dissolve line
                clip(edge);

                return half4(col, 1);
            }

            ENDHLSL
        }
    }

    FallBack Off
}

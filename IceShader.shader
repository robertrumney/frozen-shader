Shader "Custom/IceShader" 
{
    Properties
    {
        _Color("Albedo Color", Color) = (1, 1, 1, 1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpScale("Normal Scale", Range(0,1)) = 0.1
        _Refraction("Refraction", Range(0,1)) = 0.2
        _Translucency("Translucency", Range(0,1)) = 0.8
        _SubsurfaceColor("Subsurface Color", Color) = (1,1,1,1)
        _FrozenAmount("Frozen Amount", Range(0,1)) = 0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard
            #pragma target 3.0

            #include "UnityPBSLighting.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _BumpMap;
            half _BumpScale;
            half _Refraction;
            half _Translucency;
            fixed3 _SubsurfaceColor;
            half _Glossiness;
            half _Metallic;
            half _FrozenAmount;

            struct Input {
                float2 uv_MainTex;
                float3 viewDir;
                float3 worldNormal;
                INTERNAL_DATA
            };

            void surf(Input IN, inout SurfaceOutputStandard o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;;

                o.Albedo = c.rgb;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;

                o.Normal = UnpackScaleNormal(tex2D(_BumpMap, IN.uv_MainTex), _BumpScale);
                o.Emission = 0;

                // Apply refraction
                float3 worldRefractedDir = refract(-IN.viewDir, IN.worldNormal, _Refraction);
                half4 reflColor = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefractedDir);
                o.Albedo = lerp(o.Albedo, reflColor, _Refraction);

                // Apply fake subsurface scattering
                half rim = 1.0 - max(0.0, dot(IN.viewDir, IN.worldNormal));
                half3 scatter = _SubsurfaceColor.rgb * rim * _Translucency * _FrozenAmount;
                o.Albedo += scatter;
            }
            ENDCG
        }
            FallBack "Diffuse"
}

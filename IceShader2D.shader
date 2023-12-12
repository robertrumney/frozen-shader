Shader "Custom/IceShader2D"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpScale("Normal Scale", Range(0,1)) = 0.1
        _Refraction("Refraction", Range(0,1)) = 0.2
        _Translucency("Translucency", Range(0,1)) = 0.8
        _SubsurfaceColor("Subsurface Color", Color) = (1,1,1,1)
        _FrozenAmount("Frozen Amount", Range(0,1)) = 0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BumpMap;
            float4 _MainTex_ST;
            float _BumpScale;
            float _Refraction;
            float _Translucency;
            float4 _SubsurfaceColor;
            float _FrozenAmount;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Sample the texture and normal map
                fixed4 col = tex2D(_MainTex, IN.uv);
                fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv)).xyz * 2.0 - 1.0;
                
                // Refraction (texture coordinate distortion)
                float2 refractedUV = IN.uv + normal.xy * _Refraction;
                fixed4 refractedColor = tex2D(_MainTex, refractedUV);

                // Combine original and refracted color
                col = lerp(col, refractedColor, _FrozenAmount);

                // Translucency and Subsurface Scattering
                // Assuming light direction is from the top-right for demonstration
                fixed3 lightDir = normalize(float3(1, -1, 0));
                float rimLight = 1 - saturate(dot(normal, lightDir));
                fixed4 subsurfaceColor = _SubsurfaceColor * rimLight * _Translucency * _FrozenAmount;

                // Combine original color with subsurface scattering effect
                col.rgb += subsurfaceColor.rgb;
                
                return col;
            }
            ENDCG
        }
    }
}

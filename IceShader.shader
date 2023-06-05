Shader "Custom/IceShader" {
    Properties{
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _Specular("Specular", Range(0,1)) = 1
        _FrozenAmount("Frozen Amount", Range(0,1)) = 0
        _IceColor("Ice Color", Color) = (0.75, 0.75, 1, 1)
        _Transparency("Transparency", Range(0,1)) = 0.5
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
            LOD 100

            Pass {
                Blend SrcAlpha OneMinusSrcAlpha
                ZWrite On

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float3 worldNormal : TEXCOORD1;
                    float3 worldPos : TEXCOORD2;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _BumpMap;
                float _FrozenAmount;
                float4 _IceColor;
                float _Transparency;
                float _Specular;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed3 worldNormal = normalize(i.worldNormal + 2.0 * (tex2D(_BumpMap, i.uv).rgb - 0.5));
                    fixed3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                    fixed3 reflectDir = reflect(-viewDir, worldNormal);
                    float fresnel = pow(1.0 - max(0.0, dot(viewDir, worldNormal)), 1.0 - _FrozenAmount);

                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed4 iceCol = _IceColor;
                    iceCol *= fresnel * _Specular;
                    fixed4 finalColor = lerp(col, iceCol, _FrozenAmount);
                    finalColor.a = _Transparency;
                    return finalColor;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}

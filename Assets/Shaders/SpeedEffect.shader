Shader "Unlit/SpeedEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
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
            float4 _MainTex_ST;
            float _Speed;
            float _Strength;
            float _EffectSpeed;
            float _MinRange;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float dist = length(uv - 0.5);
                float2 s;
                float range = lerp(0.6, _MinRange, _Speed);
                
                fixed4 c = tex2D(_MainTex, i.uv);
                
                if (dist > range)
                {
                    s = normalize(uv - 0.5) * (sin(_Time.w * _EffectSpeed) + 1) * 0.5 * _Strength * _Speed * (dist - range);
                    c = (c + tex2D(_MainTex, i.uv - s)) * 0.5;
                }
                return c;
            }
            ENDCG
        }
    }
}

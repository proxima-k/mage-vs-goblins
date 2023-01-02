Shader "Unlit/HealthBar" {
    Properties
    {
        _Health ("Health", Range(0,1)) = 0.5
        _MinHealth ("Min Health Color", Color) = (0,0,0,1)
        _MaxHealth ("Max Health Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Health;
            float4 _MinHealth;
            float4 _MaxHealth;

            Interpolators vert (MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float4 healthColor = lerp(_MinHealth, _MaxHealth, _Health);
                healthColor *= (i.uv.x < _Health);
                return healthColor;
            }
            ENDCG
        }
    }
}

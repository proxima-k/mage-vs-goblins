Shader "Unlit/DashBar" {
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DashEnergy ("DashEnergy", Range(0,1)) = 0.5
        _Recharging ("Recharging", Color) = (0,0,0,1)
        _FullyCharged ("FullyCharged", Color) = (0,0,0,1)
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

            float _DashEnergy;
            float4 _Recharging;
            float4 _FullyCharged;

            Interpolators vert (MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float4 dashEnergyColor = (_DashEnergy < 1) * _Recharging + (_DashEnergy >= 1) * _FullyCharged;
                dashEnergyColor *= (i.uv.x < _DashEnergy);
                return dashEnergyColor;
            }
            ENDCG
        }
    }
}

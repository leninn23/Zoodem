Shader "Uwu/TextureExample"
{
      Properties
      {
            _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
            _BaseTex    ("Base Texture",2D) = "black"{}
            _Size ("Size", Range(0,1000)) = 1.0
      }
      SubShader
      {
            Tags
            {
                  "RenderType" = "Opaque"
                  "Queue" = "Geometry"
                  "RenderPipeline" = "UniversalPipeline"
            }
            Pass
            {
                  Tags
                  {
                        "LightMode" = "UniversalForward"
                  }
                  HLSLPROGRAM
                  #pragma vertex vert
                  #pragma fragment frag
                  #include "UnityCG.cginc"
                  
                  struct appdata
                  {
                        float2 uv : TEXCOORD0;
                        float4 positionOS : POSITION;
                  };
                  struct v2f
                  {
                        float4 positionCS : SV_Position;
                        float2 uv : TEXCOORD0;
                        float3 world_pos : TEXCOORD1;
                  };
                  
                  sampler2D  _BaseTex;
                  float _Size;
                  float4 _BaseColor;
                  float4 _BaseTex_ST;
                  
                  v2f vert (appdata v)
                  {
                        v2f o;
                        o.positionCS = o.positionCS = UnityObjectToClipPos(v.positionOS);
                        o.uv = TRANSFORM_TEX(v.uv, _BaseTex);
                        o.world_pos = mul(unity_ObjectToWorld, float4(v.positionOS.xyz, 1.0)).xyz;
                        return o;
                  }
                  
                  float4 frag (const v2f i) : SV_Target
                  {
                        // float4 textureSample = _BaseTex.Sample(sampler_BaseTex, i.uv);
                        float4 textureSample = tex2D(_BaseTex, i.uv);
                        return float4(_BaseColor);
                  }
                  ENDHLSL
            }
      }
}
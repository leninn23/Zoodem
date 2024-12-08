Shader "Examples/TextureExample"
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
                        float4 vertex : VERTE
                  };
                  struct v2f
                  {
                        float4 positionCS : SV_Position;
                        float2 uv : TEXCOORD0;
                        float3 worldPos;
                  };
                  
                  Texture2D  _BaseTex;
                  Texture2D  _SecondTex;
                  Texture2D  _ThirdTex;
                  CBUFFER_START(UnityPerMaterial)
                        SamplerState sampler_BaseTex;
                        float4 _BaseColor;
                        float4 _BaseTex_ST;
                        float _Size;
                  CBUFFER_END
                  
                  v2f vert (appdata v)
                  {
                        v2f o;
                        o.positionCS = o.positionCS = UnityObjectToClipPos(v.positionOS);
                        o.uv = TRANSFORM_TEX(v.uv, _BaseTex);
                        o.worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
                        return o;
                  }
                  
                  float4 frag (v2f i) : SV_Target
                  {
                        float4 textureSample = _BaseTex.Sample(sampler_BaseTex, i.uv);
                        textureSample = max(textureSample, _SecondTex.Sample(sampler_BaseTex,i.uv));
                        textureSample = max(textureSample, _ThirdTex.Sample(sampler_BaseTex,i.uv));
                        return textureSample * _BaseColor;
                  }
                  ENDHLSL
            }
      }
}
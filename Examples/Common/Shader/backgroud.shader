Shader "P2A/backgroud"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SwichXY ("SwichXY",Range(0.0, 1.0)) = 0.0
		_FlipX ("FlipX",Range(0.0, 1.0)) = 0.0
		_FlipY ("FlipY",Range(0.0, 1.0)) = 0.0
		_sRGBToLinear ("sRGBToLinear",Range(0.0, 1.0)) = 0.0
		_LinearTosRGB ("LinearTosRGB",Range(0.0, 1.0)) = 0.0
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
			float _SwichXY;
			float _FlipX;
			float _FlipY;
			float _sRGBToLinear;
			float _LinearTosRGB;

			float sRGBToLinearFast(float c)
			{
				return pow(c, 2.2);
			}
			float3 sRGBToLinearFast(float3 c)
			{
				return float3(sRGBToLinearFast(c.x), sRGBToLinearFast(c.y), sRGBToLinearFast(c.z));
			}
			float LinearToSrgbBranchlessChannel(float lin) 
			{
				lin = max(6.10352e-5, lin);
				return min(lin * 12.92, pow(max(lin, 0.00313067), 1.0/2.4) * 1.055 - 0.055);
			}
			float3 LinearToSrgbBranchless(float3 lin) 
			{
				return float3(
					LinearToSrgbBranchlessChannel(lin.r),
					LinearToSrgbBranchlessChannel(lin.g),
					LinearToSrgbBranchlessChannel(lin.b));
			}
			float3 LinearToSrgb(float3 lin) 
			{
				return LinearToSrgbBranchless(lin);
			}
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				if(_SwichXY>0.5)
					o.uv=v.uv.yx;
				else
					o.uv=v.uv;
				if(_FlipX>0.5)
					o.uv.x=1.0-o.uv.x;
				if(_FlipY>0.5)
					o.uv.y=1.0-o.uv.y;
				o.uv = TRANSFORM_TEX(o.uv, _MainTex);
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);

				if(_sRGBToLinear>0.5)
					col.rgb=sRGBToLinearFast(col.rgb);
				else if(_LinearTosRGB>0.5)
					col.rgb=LinearToSrgb(col.rgb);

				return col;
			}
			ENDCG
		}
	}
}

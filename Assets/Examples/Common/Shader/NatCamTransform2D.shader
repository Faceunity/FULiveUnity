// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/* 
*   NatCam Core
*	Image Transformation Shader
*	Built on algorithms from http://forum.unity3d.com/threads/rotation-of-texture-uvs-directly-from-a-shader.150482/
*   Copyright (c) 2016 Yusuf Olokoba
*/


Shader "Hidden/NatCam/Transform2D" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Rotation("Rotation", float) = 0
		_Mirror("Mirror", float) = 0
		_Zoom ("Zoom Ratio", float) = 0
		[HideInInspector] _Pan ("Pan Vector", Vector) = (0, 0, 0, 0)
		[HideInInspector] _StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector] _Stencil("Stencil ID", Float) = 0
		[HideInInspector] _StencilOp("Stencil Operation", Float) = 0
		[HideInInspector] _StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector] _StencilReadMask("Stencil Read Mask", Float) = 255
		[HideInInspector] _ColorMask("Color Mask", Float) = 15
	}
	SubShader {
		Tags {
			"Queue"="Transparent"
			"RenderType"="Transparent" 
			"IgnoreProjector"="True"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil {
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode off }
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#define PI 3.1415
			#define DIV 180
			
			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				half2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};
			
			uniform sampler2D _MainTex;
			uniform fixed4 _Color;
			uniform fixed4 _BackgroundColor;
			uniform fixed _Rotation;
			uniform fixed _Mirror;
			uniform float _Zoom;
			uniform half4 _Pan;
			uniform float4 _MainTex_ST;

			float2 ViewUV (float2 uv) {
				float zoom = 1.0 / _Zoom;
				float2 ret = (uv - float2(0.5, 0.5)) * zoom + float2(0.5, 0.5) + _Pan.xy;
				return ret;
			}

			float2 RotateUV (float2 input) {
				input -= float2(0.5, 0.5);
				float s = sin (PI * _Rotation);
				float c = cos (PI * _Rotation);
				float2x2 m_rotation = (float2x2(float2(c, s),float2(-s, c)) * float2x2(float2(0.5, 0.5), float2(0.5, 0.5)) + float2x2(float2(0.5, 0.5), float2(0.5, 0.5))) * float2x2(float2(2.0, 2.0), float2(2.0, 2.0)) - float2x2(float2(1.0, 1.0), float2(1.0, 1.0));
				float2x2 m_mirror = float2x2(float2(1.0 - 2.0 * _Mirror, 0.0), float2(0.0, 1.0));
				float2 output = mul(input, m_mirror);
				output = mul(output, m_rotation);
				output += float2(0.5, 0.5);
				return output;
			}

			v2f vert (appdata_t v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = ViewUV(RotateUV(TRANSFORM_TEX(v.texcoord, _MainTex)));
				o.color = v.color * _Color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target	{
				fixed4 col = tex2D(_MainTex, i.uv) * i.color;
				if (i.uv.x > 1.0 || i.uv.x < 0.0 || i.uv.y > 1.0 || i.uv.y < 0.0) col = _BackgroundColor;
				return col;
			}
			ENDCG
		}
	}
}

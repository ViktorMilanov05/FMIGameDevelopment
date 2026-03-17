Shader "Custom/RedVignette"
{
	Properties
	{
		_Color ("RedVignette Color", Color) = (1,0,0,1)
		_Intensity ("Intensity", Range(0,1)) = 0
		_InnerRadius ("Inner Radius", Range(0,1)) = 0.3
		_OuterRadius ("Outer Radius", Range(0,1)) = 0.75
	}

	SubShader
	{
		Tags
		{
			"RenderType"="Transparent"
			"Queue"="Overlay"
		}

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		Pass
		{
			HLSLPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			
			#include "UnityCG.cginc"

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4 _Color;
			float _Intensity;
			float _InnerRadius;
			float _OuterRadius;

			Varyings vert (Attributes IN)
			{
				Varyings OUT;
				OUT.positionHCS = UnityObjectToClipPos(IN.positionOS);
				OUT.uv = IN.uv;
				return OUT;
			}

			float4 frag (Varyings IN) : SV_Target
			{
				float2 center = float2(0.5, 0.5);
				float dist = distance(IN.uv, center);

				float vignette = smoothstep(_InnerRadius, _OuterRadius, dist);

				float alpha = vignette * _Intensity;

				return float4(_Color.rgb, alpha);
			}

			ENDHLSL
		}
	}
}

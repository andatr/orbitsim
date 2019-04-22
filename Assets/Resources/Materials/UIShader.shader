Shader "Unlit/UIShader"
{
	Properties
	{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Texture", 2D) = "white" {}
		_PlanetPosition("Planet Angle", float) = 0.0
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment fragment_shader
			
			#include "UnityCG.cginc"

			fixed4 _Color;
			fixed _PlanetPosition;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct app_data
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent: TANGENT;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
			};

			v2f vertex_shader(app_data input)
			{
				v2f output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				float3 vertex1 = output.vertex / output.vertex.w;
				float4 vertex2 = UnityObjectToClipPos(input.normal);
				vertex2 /= vertex2.w;
				float3 direction = normalize(vertex2 - vertex1);
				output.vertex.x += 8.0 * direction.y * output.vertex.w / _ScreenParams.x;
				output.vertex.y -= 8.0 * direction.x * output.vertex.w / _ScreenParams.y;
				output.uv = float2(input.tangent.x, 0.0);
				output.uv1 = float2(0.0, input.tangent.y);
				return output;
			}

			fixed4 fragment_shader(v2f input) : SV_Target
			{
				float t = 0.3;
				float opacity = 0.0;
				float x = _PlanetPosition - input.uv.x;
				if (x < 0.0 && (x < (_PlanetPosition - t)))
					x += 1.0;
				if (x >= 0.0 && x <= t)
				{
					opacity = 1.0 - x / t;
				}
				if (opacity <= 0.2)
					opacity = 0.2;
				fixed4 texColor = tex2D(_MainTex, input.uv1);
				return float4(_Color.x * texColor.y + texColor.x, _Color.y * texColor.y + texColor.x, _Color.z * texColor.y + texColor.x, opacity * texColor.a);
			}

			ENDCG
		}
	}
}

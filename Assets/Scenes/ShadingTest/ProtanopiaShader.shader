Shader "Custom/ProtanopiaShader"
{
	Properties
	{
		_MainTex("MainTexture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			static const half PI = 3.1415926535897931;
			half3 hue_rotation(half3 color_rgb, half hue);
			half3 hsl_rgb_convert(half hue, half saturation, half lightness);
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
			// vert: called once/vertex in the object the shader is being applied to (8 for a cube)
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			// frag: called once/pixel
			half4 frag(v2f i) : SV_Target
			{
				// color from texture at coordinate
				half4 original = tex2D(_MainTex, i.uv); // uv coordinate of pixel
				half4 alter = original;

				half max_val = max(max(alter.r, alter.g), alter.b);
				half min_val = min(min(alter.r, alter.g), alter.b);
				half hue;
				half saturation;
				half lightness;
				half delta = max_val - min_val;

				if (max_val == min_val) {
					hue = 0.0;
				} else if (alter.r == max_val) {
					hue = (alter.g - alter.b) / delta;
				}
				else if (alter.g == max_val) {
					hue = 2.0 + (alter.b - alter.r) / delta;
				}
				else {
					hue = 4.0 + (alter.r - alter.g) / delta;
				}
				hue = 60.0 * hue;
				if (hue < 0.0) hue += 360.0;

				if (min_val == max_val) {
					saturation = 0;
				}
				else {
					saturation = (max_val - min_val) / (1 - abs(max_val + min_val - 1));
				}
				lightness = (max_val + min_val) / 2.0;
					
				// change ranges
				if (hue < 14 || hue > 346) {
					//hue = (hue + 60.0) % 360.0;
					//lightness = 0.65;
					alter.rgb = hue_rotation(alter.rgb, -60);
				}
				if (hue > 81 && hue < 160) {
					alter.rgb = hue_rotation(alter.rgb, -80);
					//hue = (hue + 100.0) % 360.0;
				}
				if (hue > 180 && hue < 300) {
				}

				// alter.rgb = hsl_rgb_convert(hue, saturation, lightness);
				return alter;
			}

			half3 hue_rotation(half3 color_rgb, half hue) {
				half U = cos(hue*PI / 180);
				half W = sin(hue*PI / 180);

				half3 color_shifted;
				color_shifted.r = (.299 + .701*U + .168*W)*color_rgb.r
					+ (.587 - .587*U + .330*W)*color_rgb.g
					+ (.114 - .114*U - .497*W)*color_rgb.b;
				color_shifted.g = (.299 - .299*U - .328*W)*color_rgb.r
					+ (.587 + .413*U + .035*W)*color_rgb.g
					+ (.114 - .114*U + .292*W)*color_rgb.b;
				color_shifted.b = (.299 - .3*U + 1.25*W)*color_rgb.r
					+ (.587 - .588*U - 1.05*W)*color_rgb.g
					+ (.114 + .886*U - .203*W)*color_rgb.b;
				return color_shifted;
			}

			// hue: [0,360]
			// saturation/lightness: [0,1]
			half3 hsl_rgb_convert(half hue, half saturation, half lightness) {
				half chroma = (1.0 - abs(2.0 * lightness - 1.0)) * saturation;
				half relative_hue = hue / 60.0;
				half intermediate = chroma * (1.0 - abs((relative_hue % 2.0) - 1.0));
				half3 return_rgb;
				return_rgb.r = 0.0;
				return_rgb.g = 0.0;
				return_rgb.b = 0.0;
				
				if (0.0 <= relative_hue && relative_hue <= 1.0) {
					return_rgb.r = chroma;
					return_rgb.g = intermediate;
				} else if (1.0 <= relative_hue && relative_hue <= 2.0) {
					return_rgb.r = intermediate;
					return_rgb.g = chroma;
				} else if (2.0 <= relative_hue && relative_hue <= 3.0) {
					return_rgb.g = chroma;
					return_rgb.b = intermediate;
				} else if (3.0 <= relative_hue && relative_hue <= 4.0) {
					return_rgb.g =intermediate;
					return_rgb.b = chroma;
				} else if (4.0 <= relative_hue && relative_hue <= 5.0) {
					return_rgb.r = intermediate;
					return_rgb.b = chroma;
				} else if (5.0 <= relative_hue && relative_hue <= 6.0) {
					return_rgb.r = chroma;
					return_rgb.b = intermediate;
				}

				half lightness_match = lightness - (chroma / 2.0);
				return_rgb.r += lightness_match;
				return_rgb.g += lightness_match;
				return_rgb.b += lightness_match;
				return return_rgb.rgb;
			}

			

			ENDCG
		}
	}
}

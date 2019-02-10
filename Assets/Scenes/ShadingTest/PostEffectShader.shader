Shader "Custom/PostEffectShader"
{
    Properties
    {
        _MainTex ("MainTexture", 2D) = "white" {}
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
			// we have a vertex shater vert and fragment shader frag
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
			// vert: called once/vertex in the object the shader is being applied to (8 for a cube)
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			// frag: called once/pixel
            fixed4 frag (v2f i) : SV_Target
            {
				// color from texture at coordinate
                fixed4 original = tex2D(_MainTex, i.uv); // uv coordinate of pixel
				fixed4 alter = original;
				// red, green, blue, alpha (transparency)
				alter.rgb = original.rgb; // fixed3(0.0, 0.49, 0.49);
				alter.a = original.a;
                // col.rgb = 1 - col.rgb; // just invert the colors
				// 1. detect what color this is <- this is the problem
				
				
				
				fixed max_val = max(max(alter.r, alter.g), alter.b);
				fixed min_val = min(min(alter.r, alter.g), alter.b);
				fixed hue;
				fixed delta = max_val - min_val;
				// if delta isn't big enough, simply ignore this color: it is close enough to black/grey/white
				// change "!= 0.0" to ">= X", where X depends on whatever is best from experimentation
				if (delta != 0.0) {
					if (alter.r == max_val) {
						hue = (alter.g - alter.b) / delta;
					}
					else if (alter.g == max_val) {
						hue = 2.0 + (alter.b - alter.r) / delta;
					}
					else {
						hue = 4.0 + (alter.g - alter.b) / delta;
					}
					hue = 60.0 * hue;
					if (hue < 0) hue += 360;
						
					if (hue < 60 || hue > 300) {
						// alter.rgb = 1 - alter.rgb;
					}
					if (hue > 60 && hue < 180) {
						// alter.rgb = 1 - alter.rgb;
					}
					if (hue > 180 && hue < 300) {
						// alter.rgb = 1 - alter.rgb;
					}
				}
				

				// 2. change the color (should be change the *hue*, but we can figure that out later)
				//    what color that should be? We'll figure that out later. Right now, we need to make the tech work
				return alter;
            }
            ENDCG
        }
    }
}

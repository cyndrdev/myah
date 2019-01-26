Shader "Unlit/EyeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MainColor ("White Color", COLOR) = (1, 1, 1, 1)
		_SubColor ("Pupil Color", COLOR) = (0, 0, 0, 1)
		_SubRot	("Pupil Rotation", Range(-3.141592653, 3.141592653)) = 0
		_SubSize ("Pupil Reduction", Range(0, 1)) = 0.7
		_SubDist ("Pupil Distance", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { 
			"RenderType"="Opaque" 
			"Queue"="100"
		}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _MainColor;
			float4 _SubColor;
			float4 _SubPos;
			float _SubRot;
			float _SubSize;
			float _SubDist;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

				float2 subpos = (_SubDist * (_SubSize)) * float2(sin(_SubRot), cos(_SubRot));
				float2 eyeUV = (i.uv * (1 + _SubSize)) - subpos - float2(_SubSize / 2, _SubSize / 2);

                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_MainTex, eyeUV);
                // apply fog

                UNITY_APPLY_FOG(i.fogCoord, col);
				float4 eyecol = lerp(col, _SubColor, col2.a);

				float amt = min(
					step(max(eyeUV.x, eyeUV.y), 1), 
					step(0, min(eyeUV.x, eyeUV.y)));
				return eyecol * amt + (col * (1 - amt));
            }
            ENDCG
        }
    }
}

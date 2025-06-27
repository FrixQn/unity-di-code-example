Shader "Custom/HoleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HoleRadius ("Hole Radius", Float) = 0.5
        _HoleCenter ("Hole Center", Vector) = (0.5, 0.5, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } // Рендерится после стандартных прозрачных объектов
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
            float _HoleRadius;
            float2 _HoleCenter;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float dist = distance(uv, _HoleCenter);

                if (dist < _HoleRadius)
                {
                    return fixed4(0, 0, 0, 1); // Черный цвет для дыры
                }
                else
                {
                    return fixed4(0, 0, 0, 0); // Прозрачный фон
                }
            }
            ENDCG

            ZWrite Off
        }
    }
}
Shader "Unlit/Invert" {
Properties {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        
        ZWrite Off
        Blend OneMinusDstColor Zero 
        
        Pass {  
                CGPROGRAM
                        #pragma vertex vert
                        #pragma fragment frag
                        
                        #include "UnityCG.cginc"

                        struct appdata_t {
                                float4 vertex : POSITION;
                        };

                        struct v2f {
                                float4 vertex : SV_POSITION;
                        };
                        
                        v2f vert (appdata_t v)
                        {
                                v2f o;
                                o.vertex = UnityObjectToClipPos(v.vertex);
                                return o;
                        }
                        
                        fixed4 frag (v2f i) : COLOR
                        {
                                return 1;
                        }
                ENDCG
        }
}

}

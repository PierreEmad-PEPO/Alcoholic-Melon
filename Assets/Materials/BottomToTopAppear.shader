Shader "Custom/WorldPositionBasedClippingMaskWithColor"
{
    Properties
    {
        _ClippingPosition ("Clipping Position", Vector) = (0, 0, 0, 0)
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD0; // Store world space position
            };
            
            float4 _ClippingPosition;
            float4 _Color;
            
            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // Store clip space position
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); // Store world space position
                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                // Transform clipping position to clip space
                float4 clippingPosClip = UnityObjectToClipPos(_ClippingPosition);
                
                // Check if fragment's Y position in world space is above the clipping position
                if (i.worldPos.y > _ClippingPosition.y)
                {
                    discard;
                }
                
                // Return the dynamically set color
                return _Color;
            }
            ENDCG
        }
    }
}

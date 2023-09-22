Shader "_ Mega/Tint/Texture (Adative, RGBA)"
{
    Properties 
    {
        _color 					("Color",			Color) 	= (1,1,1,1)
        _tex 					("Diffuse", 		2D) 	= "white" {}
        _color_scale 			("Scale", 			float)	= 1
        _alpha_scale			("Alpha Scale",		float) 	= 1
    }
  
    Category 
	{
		Cull 					Back 
		Lighting 				Off 
		ZWrite 					Off 
		Blend  					SrcAlpha One
		Fog 					{Mode Off}

		Tags 
		{ 
			"RenderType"		= "Transparent" 
			"Queue"				= "Transparent"
			"IgnoreProjector"	= "True" 
		}
		
	    SubShader 
	    {
			CGINCLUDE
			#include "UnityCG.cginc"
			#include "m.cginc"
			#include "SVD.cginc"

			sampler2D 						_tex;

			// CBUFFER_START(UnityPerMaterial)
				float4 						_tex_ST;
				float4						_color;
				float 						_color_scale;
				float						_alpha_scale;
			// CBUFFER_END
		
			icbase vert (vctex v)
			{
				icbase 	o;

				// UNITY_SETUP_INSTANCE_ID		(v);
				// UNITY_TRANSFER_INSTANCE_ID	(v, o);
				
						o.pos 				= m_position	(v.vertex);
						o.uv				= m_uv			(v.texcoord, _tex_ST);
						
						o.color				= v.color * _color;
						o.color.rgb			*= _color_scale;
						o.color.a			*= _alpha_scale;

				return 	o;
			}
			
			float4 frag (icbase i) : COLOR 
			{
				// UNITY_SETUP_INSTANCE_ID		(i);

				return 	tex2D(_tex, i.uv) * i.color;
			}
					
			ENDCG
			
			Pass 
			{
				CGPROGRAM
				#pragma vertex 				vert
				#pragma fragment 			frag
				#pragma fragmentoption 		ARB_precision_hint_fastest		
				ENDCG 
			}	
		}
    }
}

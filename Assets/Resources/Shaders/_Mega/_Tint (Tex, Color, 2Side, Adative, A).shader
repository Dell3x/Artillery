Shader  "_ Mega/Tint/Texture (Color, 2Side, Adative, RGBA)"
{
    Properties 
    {
        _color					("_color", 		color)	= (1,1,1,1)
        _scale 					("_scale", 		float)	= 1
		_tex					("_tex", 		2D) 	= "white" {}
    }
  
	Category
	{
		Cull 					Off
		ZWrite 					Off
		Lighting 				Off 
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

			sampler2D 			_tex;

			CBUFFER_START(UnityPerMaterial)
				float4 			_tex_ST;
				float4 			_color;
				float 			_scale;
			CBUFFER_END
	
			icbase vert (vctex v)
			{
				icbase 	o;

				UNITY_SETUP_INSTANCE_ID		(v);
				UNITY_TRANSFER_INSTANCE_ID	(v, o);
				
						o.pos 				= m_position	(v.vertex);
						o.uv				= m_uv			(v.texcoord, _tex_ST);

						o.color				= v.color * _color;
						o.color.rgb			*= _scale;

				return 	o;
			}
			
			float4 frag (icbase i) : COLOR 
			{
				UNITY_SETUP_INSTANCE_ID		(i);

				return 	tex2D(_tex, i.uv) * i.color;
			}

			ENDCG

			Pass
			{
				CGPROGRAM
				#pragma target 4.5
				#pragma vertex 				vert
				#pragma fragment 			frag
				#pragma fragmentoption 		ARB_precision_hint_fastest 
				#pragma multi_compile_instancing
				ENDCG
			}
		}
    }
}

Shader  "_ Mega/SUN/Reflective [SHL, Tint]"
{	
	Properties 
	{
		_tex 				("Texture:", 					2D) 	= "grey" 	{}
		_normal 			("Normal:", 					2D) 	= "bump" 	{}
		
		_refl				("Reflection:", 				CUBE) 	= "black" 	{}
		_refl_tint_mask		("Scale: Refl 'r', Tint 'gba'", 2D)		= "black" 	{}
		_refl_tint_scale	("Scale: Refl 'x', Tint 'yzw'", Vector) = (1,1,1,1)

		_color_0			("Ilum: _color_0", 				color)	= (1,1,1,1)
		_color_1			("Ilum: _color_1", 				color)	= (1,1,1,1)
		_color_2			("Ilum: _color_2", 				color)	= (1,1,1,1)

		_global_scale 		("Scale: Global light", 		float) 	= 1.0
		_emiter_scale		("Scale: Emiters light",		float) 	= 1.0
		_saturation 		("Saturation", 					float) 	= 1
	}
    
	Category
	{

		Tags								
		{ 
			"RenderType"					= "Opaque" 
			"Queue"							= "Geometry" 
			"IgnoreProjector"				= "True" 
			//"LightMode"					= "ForwardBase"
		}

		Cull 								Back
		ZWrite 								On
		Lighting 							Off

		Fog 								{Mode Off}

		SubShader
		{
			CGINCLUDE
			#include "UnityCG.cginc"

			#include "m.cginc"
			#include "SVD.cginc"
			#include "SUN.cginc"
			#include "SHL.cginc"
			#include "Reflect.cginc"

			sampler2D 						_tex;
			sampler2D						_normal;
			sampler2D						_refl_tint_mask;

			samplerCUBE 					_refl;

			CBUFFER_START(UnityPerMaterial)
				float4 						_tex_ST;
				float4 						_refl_ST;
				float4 						_normal_ST;
				float4 						_refl_tint_mask_ST;

				float4 						_color_0;
				float4 						_color_1;
				float4 						_color_2;

				float4						_refl_tint_scale;

				float						_global_scale;
				float						_emiter_scale;
				float						_saturation;
			CBUFFER_END

			irefl3 vert(vrefl v)
			{
				irefl3 o;

				UNITY_SETUP_INSTANCE_ID		(v);
				UNITY_TRANSFER_INSTANCE_ID	(v, o);

							o.pos			= m_position			(v.vertex);
							o.uv			= m_uv					(v.texcoord, _tex_ST);
							o.uv1			= m_uv					(v.texcoord, _refl_tint_mask_ST);
							o.uv2			= m_uv					(v.texcoord, _normal_ST);
							o.normal		= m_normal				(v.normal);
							o.binormal 		= m_binormal			(o.normal, v.tangent);
							o.tangent		= v.tangent.xyz;

							o.view			= m_view_Camera			(o.normal);
							
							o.light			= sun_Light				(o.normal, _global_scale, _saturation);
							o.light			+= shl_Light			(o.normal, _emiter_scale);
				
				return o;
			}

			float4 frag(irefl3 i) : COLOR
			{
				UNITY_SETUP_INSTANCE_ID		(i);
						
				float4		t 				= tex2D					(_tex, i.uv);
				float4 		mask			= tex2D					(_refl_tint_mask, i.uv1);
				float3  	refl 			= reflect_CubeUV1		(_refl, _normal, i, mask.r * _refl_tint_scale.x);
				float3		tint			= m_Tint				(t, _color_0, _color_1, _color_2, mask, _refl_tint_scale);

							t.rgb 			= t.rgb * i.light + refl + tint;

				return 		t;
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

	/*
	SubShader
    {
		Cull 								Back 
		ZWrite 								On 
		Lighting 							Off 
		
		Fog 								{Mode Off}
		
		Tags 								{ "RenderType" = "Opaque" "Queue" = "Geometry" "IgnoreProjector" = "True" }
		
		Pass 
		{
			CGPROGRAM

			#pragma vertex 				vert
			#pragma fragment 			frag
			#pragma fragmentoption 		ARB_precision_hint_fastest 

			#include "Emiter.cginc"
					
			sampler2D 						_tex;
			float4 							_tex_ST;

			sampler2D						_normal;
			float4							_normal_ST;
			
			samplerCUBE 					_refl;
			float4 							_color_0;
			float4 							_color_1;
			float4 							_color_2;
			
			sampler2D						_refl_tint_mask;
			float4 							_refl_tint_mask_ST;
			
			float4							_refl_tint_scale;

			irefl vert (vrefl v)
			{
				irefl 		o;
							o.pos 			= UnityObjectToClipPos(v.vertex);
							o.uv			= v.texcoord;
							o.normal 		= mul((float3x3)unity_ObjectToWorld, v.normal.xyz);
							o.tangent		= v.tangent;
							o.binormal 		= cross(o.normal, o.tangent) * v.tangent.w;
							o.view_dir 		= normalize(o.normal - _WorldSpaceCameraPos.xyz);
							//o.view_dir 	= normalize(ObjSpaceViewDir(o.pos));
							o.light			= Light(o.normal);

				return 		o; 
			}

			float4 frag (irefl i) : SV_TARGET
			{					
				float4		t 				= tex2D		(_tex, 				i.uv * _tex_ST.xy + _tex_ST.zw);
				float4 		mask			= tex2D		(_refl_tint_mask,	i.uv * _refl_tint_mask_ST.xy + _refl_tint_mask_ST.zw);
				
				float3  	refl 			= Reflect	(i, i.uv * _normal_ST.xy + _normal_ST.zw, _normal, _refl, mask.r * _refl_tint_scale.x);
				float3		tint			= Tint		(t.rgb, _color_0.rgb, _color_1.rgb, _color_2.rgb, mask.gba * _refl_tint_scale.yzw);
				
							t.rgb 			= t.rgb * i.light + refl + tint;
				return 		t;
			}

			ENDCG
		}
	}
	*/
}
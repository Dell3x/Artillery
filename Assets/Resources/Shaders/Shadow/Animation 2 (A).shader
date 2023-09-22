// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CG/Shadow/Animated 2 (RGBA)"
{
    Properties 
    {
        _color 			("Color"				,Color) 	= (1,1,1,1)
        
        _tex_0 			("Texture Shadow 0"		,2D) 		= "black" {}
        _channel_0		("Scale: RGBA"			,Vector)	= (1,0,0,0)
        _tex_1 			("Texture Shadow 1"		,2D) 		= "black" {}
        _channel_1		("Scale: RGBA"			,Vector)	= (1,0,0,0)
        
        _offset			("Offset"				,Vector)	= (0,0.875,0,0.875)
		_tiling			("Tiling"				,float)		= 0.125
		
        _alpha_scale	("Scale: Alpha"			,float)		= 1
		_global_scale 	("Scale: Global"		,float) 	= 1.0
		_emiter_scale	("Scale: Emiters"		,float) 	= 1.0
    }
     
    Category 
    {	
		Blend 							SrcAlpha OneMinusSrcAlpha
		Cull 							Back 
		ZWrite 							Off 
		Lighting 						Off 
		Fog 							{Mode Off}
		
       	Tags 		
       	{
	       	"Queue"						= "Transparent+1" 
	       	"IgnoreProjector"			= "True"
			"RenderType"				= "Transparent" 
       	}
       	
	   	SubShader 
	   	{
			CGINCLUDE
			#include "UnityCG.cginc"			
			float 	_g_shw,	_e_shw;
			
			float4 						_color;
			
			sampler2D 					_tex_0;
			sampler2D 					_tex_1;
			float4 						_channel_0;
			float4 						_channel_1;
			
			float4 						_offset;
			float 						_tiling;
			
			float 						_alpha_scale;
			float 						_global_scale;
			float 						_emiter_scale;
			
			struct app_t 
			{
				float4 vertex 			: POSITION;
				float2 texcoord 		: TEXCOORD0;
			};
				
			struct v2f 
			{
				float4 	pos 			: SV_POSITION;
				float4 	color 			: COLOR;
				float2 	uv 				: TEXCOORD0;
			};
		
			v2f vert (app_t v)
			{
				v2f 	o;
						o.pos 			= UnityObjectToClipPos(v.vertex);
						o.uv 			= v.texcoord;
						o.color			= _color;
				return 	o;
			}
	        
			float4 frag (v2f i) : COLOR 
			{
				float2  uv 				= float2(1 - i.uv.x, 1 - i.uv.y) * _tiling;
				float4 	t0 				= tex2D(_tex_0, uv + _offset.xy) * _channel_0.xyzw;
				float4	t1 				= tex2D(_tex_1, uv + _offset.zw) * _channel_1.xyzw;
				float	alpha			= max(t0.r + t0.g + t0.b + t0.a, t1.r + t1.g + t1.b + t1.a);
				float	scale			= (_g_shw * _global_scale + _e_shw * _emiter_scale) * _alpha_scale;
				
						i.color.a		= alpha * scale;
				
				return  i.color;
			}	
			ENDCG
			
			Pass 
			{
				CGPROGRAM
				#pragma vertex 			vert
				#pragma fragment 		frag
				#pragma fragmentoption 	ARB_precision_hint_fastest	
				ENDCG 
			}	
		}
    }
}

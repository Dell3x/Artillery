#ifndef SHL_INCLUDED
#define	SHL_INCLUDED

float		_e_scl;
float		_e_max;

float4 		_SH_Ar;
float4 		_SH_Ag;
float4 		_SH_Ab; 

float4 		_SH_Br;
float4 		_SH_Bg;
float4 		_SH_Bb;

float4 		_SH_C;


float3 shl_Light(float3 normal, float scale)
{
	float4 		wn1 			= normal.xyzz * normal.yzzx;
	float		dxy				= normal.x*normal.x - normal.y*normal.y;

	float		r				= dot(_SH_Ar, normal) + dot(_SH_Br, wn1);
	float		g				= dot(_SH_Ag, normal) + dot(_SH_Bg, wn1);
	float		b				= dot(_SH_Ab, normal) + dot(_SH_Bb, wn1);
	
	// SH can generating negative on some side of the object
	float3		light			= max(float3(r,g,b) + _SH_C.rgb * dxy, 0);  
				light			= min(light, _e_max) * _e_scl; 
						
				return light * scale;
}


float3 LightSH(float3 WorldNormal, float scale)
{
	float4 		wn1 			= WorldNormal.xyzz * WorldNormal.yzzx;
	float		dxy				= WorldNormal.x*WorldNormal.x - WorldNormal.y*WorldNormal.y;

	float		r				= dot(_SH_Ar, WorldNormal) + dot(_SH_Br, wn1);
	float		g				= dot(_SH_Ag, WorldNormal) + dot(_SH_Bg, wn1);
	float		b				= dot(_SH_Ab, WorldNormal) + dot(_SH_Bb, wn1);
	
	// SH can generating negative on some side of the object
	float3		light			= max(float3(r,g,b) + _SH_C.rgb * dxy, 0);  
				light			= min(light, _e_max) * _e_scl; 
						
				return light * scale;
}
#endif

#ifndef Reflect_INCLUDED
#define Reflect_INCLUDED

#include "UnityCG.cginc"
#include "SVD.cginc"
#include "m.cginc"



float3 reflect_Cube(samplerCUBE cube, sampler2D normal, irefl i, float scale)
{
	if (scale <= 0)
	{
		return float3 (0, 0, 0);
	}

	float3 	_color				= m_normal_color		(normal, i.uv);
	float3 	_direction			= m_normal_direction	(_color, i.normal, i.binormal, i.tangent);
	float3	_reflection			= m_reflect_cube		(cube, i.view, _direction); 
				
	return _reflection * scale;
}

float3 reflect_Cube(samplerCUBE cube, sampler2D normal, irefl2 i, float scale)
{
	if (scale <= 0)
	{
		return float3 (0, 0, 0);
	}

	float3 	_color				= m_normal_color		(normal, i.uv);
	float3 	_direction			= m_normal_direction	(_color, i.normal, i.binormal, i.tangent);
	float3	_reflection			= m_reflect_cube		(cube, i.view, _direction); 
				
	return _reflection * scale;
}

float3 reflect_Cube(samplerCUBE cube, sampler2D normal, irefl3 i, float scale)
{
	if (scale <= 0)
	{
		return float3 (0, 0, 0);
	}

	float3 	_color				= m_normal_color		(normal, i.uv);
	float3 	_direction			= m_normal_direction	(_color, i.normal, i.binormal, i.tangent);
	float3	_reflection			= m_reflect_cube		(cube, i.view, _direction); 
				
	return _reflection * scale;
}

float3 reflect_CubeUV1(samplerCUBE cube, sampler2D normal, irefl3 i, float scale)
{
	if (scale <= 0)
	{
		return float3 (0, 0, 0);
	}

	float3 	_color				= m_normal_color		(normal, i.uv1);
	float3 	_direction			= m_normal_direction	(_color, i.normal, i.binormal, i.tangent);
	float3	_reflection			= m_reflect_cube		(cube, i.view, _direction); 
				
	return _reflection * scale;
}




float3 ReflectW(float2 uv, float3 normal, float3 tangent, float3 binormal, float3 view_dir, sampler2D NormalMap, samplerCUBE CubeMap, float Scale)
{
	float3  	refl			= float3 (0, 0, 0);

	if (Scale > 0)
	{
		float3 	nrml			= UnpackNormal(tex2D(NormalMap, uv));
		float3 	normalW			= (tangent * nrml.x) + (binormal * nrml.y) + (normal * nrml.z);

				refl			= texCUBE(CubeMap, reflect(view_dir, normalW)).rgb * Scale;
	}

	return refl;
}

float3 Reflect(irefl i, sampler2D NormalMap, samplerCUBE CubeMap, float Scale)
{
	float3 refl					= float3 (0, 0, 0);

	if (Scale > 0)
	{
		float3 	nrml			= UnpackNormal(tex2D(NormalMap, i.uv));
		float3 	normalW			= (i.tangent * nrml.x) + (i.binormal * nrml.y) + (i.normal * nrml.z);

				refl			= texCUBE(CubeMap, reflect(i.view, normalW)).rgb * Scale;
	}

	return refl;
}

float3 Reflect(irefl3 i, sampler2D NormalMap, samplerCUBE CubeMap, float Scale)
{
	float3 refl					= float3 (0, 0, 0);

	if (Scale > 0)
	{
		float3 	nrml			= UnpackNormal(tex2D(NormalMap, i.uv));
		float3 	normalW			= (i.tangent * nrml.x) + (i.binormal * nrml.y) + (i.normal * nrml.z);

				refl			= texCUBE(CubeMap, reflect(i.view, normalW)).rgb * Scale;
	}

	return refl;
}

float3 Reflect(irefl3 i, float2 normalUV, sampler2D NormalMap, samplerCUBE CubeMap, float Scale)
{
	float3 refl					= float3 (0, 0, 0);

	if (Scale > 0)
	{
		float3 	nrml			= UnpackNormal(tex2D(NormalMap, normalUV));
		float3 	normalW			= (i.tangent * nrml.x) + (i.binormal * nrml.y) + (i.normal * nrml.z);

				refl			= texCUBE(CubeMap, reflect(i.view, normalW)).rgb * Scale;
	}

	return refl;
}

float3 Tint(float3 V0, float3 V1, float3 V2, float3 V3, float3 Scale)
{			 
	V1  						= (V0 + V1) * Scale.x;
	V2  						= (V0 + V2) * Scale.y;
	V3  						= (V0 + V3) * Scale.z;
	
	return V1+V2+V3;
}
#endif

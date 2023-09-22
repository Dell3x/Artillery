#ifndef m_INCLUDED
#define m_INCLUDED

#include "UnityCG.cginc"
#include "SVD.cginc"

float3 m_normal_color(sampler2D map, float2 uv)
{
	return UnpackNormal(tex2D(map, uv));
}

float3 m_normal_direction(float3 normalColor, float3 normal, float3 binormal, float3 tangent)
{
	return (tangent * normalColor.x) + (binormal * normalColor.y) + (normal * normalColor.z);
}

float3 m_reflect_cube(samplerCUBE map, float3 view, float3 normal)
{
	return texCUBE(map, reflect(view, normal)).rgb;
}

float4 m_clamp(float4 color, float minValue, float maxValue)
{
	float	_max				= max				(max(color.r, color.g), color.b);
	float	_min_out			= min				(minValue, _max);
	float	_max_out			= max				(maxValue, _max);

	float	_kmax				= _max / _min_out;
	float	_kmin				= 1 + (_max_out - _max) / _max_out;

	color.r						= color.r * _kmin / _kmax;
	color.g						= color.g * _kmin / _kmax;
	color.b						= color.b * _kmin / _kmax;

	return color;
}

float3 m_Tint(float4 color, float4 tint_0, float4 tint_1, float4 tint_2, float4 mask, float4 scale)
{			 
	return	(color + tint_0) * mask.g * scale.y +
			(color + tint_1) * mask.b * scale.z +
			(color + tint_2) * mask.a * scale.w;
}

float4 m_position(float4 vertex)
{
	return UnityObjectToClipPos(vertex);
}

float3 m_position_World(float4 vertex)
{
	return mul(unity_ObjectToWorld, vertex).xyz;
}

float2 m_uv(float2 texcoord, float4 st)
{
	return texcoord * st.xy + st.zw;
}

float2 m_uv(float2 texcoord, float tile_x, float tile_y, float offset_x, float offset_y)
{
	texcoord.x			*= tile_x + offset_x;
	texcoord.y			*= tile_y + offset_y;

	return texcoord;
}

float3 m_normal(float3 normal)
{
	return normalize(mul((float3x3)unity_ObjectToWorld, normal).xyz );
}

float3 m_normal_World(float3 normal)
{
	return mul((float3x3)unity_ObjectToWorld, normal);
}

float3 m_binormal(float3 normal, float4 tangent)
{
	return cross(normal, tangent) * tangent.w;
}

float3 m_view_Camera(float3 normal)
{
	return normalize(normal - _WorldSpaceCameraPos.xyz);
}

float3 m_view(float3 normal, float3 view)
{
	return normalize(normal - view);
}
#endif

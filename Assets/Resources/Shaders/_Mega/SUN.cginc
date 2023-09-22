#ifndef SUN_INCLUDED
#define SUN_INCLUDED

float4		_g_cl;
float3		_g_dir;
float		_g_scl;
float		_g_sat;


float3 sun_Light(float3 normal, float scale)
{
	return max(dot(normal, _g_dir), _g_sat) * _g_cl * _g_scl * scale;
}

float3 sun_Light(float3 normal, float scale, float saturation)
{
	return max(dot(normal, _g_dir), _g_sat*saturation) * _g_cl * _g_scl * scale;
}



float3 LightSUN(float3 WorldNormal, float scale)
{
	return max(dot(WorldNormal, _g_dir), _g_sat) * _g_cl * _g_scl * scale;
}

float3 LightSUN(float3 WorldNormal, float lightScale, float saturationScale)
{
	return max(dot(WorldNormal, _g_dir), _g_sat*saturationScale) * _g_cl * _g_scl * lightScale;
}
#endif

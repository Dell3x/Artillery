#ifndef SVD_INCLUDED
#define SVD_INCLUDED

#include "UnityCG.cginc"


/// ---------------------------- VERT ----------------------------------

struct vcol
{
	float4 	vertex 				: POSITION;
	float4 	color 				: COLOR;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct vtex
{
	float4 	vertex 				: POSITION;
	float2 	texcoord 			: TEXCOORD;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct vctex
{
	float4 	vertex 				: POSITION;
	float4	color 				: COLOR;
	float2 	texcoord 			: TEXCOORD;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct vnorm
{
	float4 	vertex 				: POSITION;
	float2 	texcoord 			: TEXCOORD;
	float3 	normal 				: NORMAL;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct vcnorm
{
	float4 	vertex 				: POSITION;
	float4	color 				: COLOR;
	float2 	texcoord 			: TEXCOORD;
	float3 	normal 				: NORMAL;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};
			
struct vrefl 
{
    float4 	vertex 				: POSITION;
	float2 	texcoord 			: TEXCOORD;
    float3 	normal 				: NORMAL;
    float4 	tangent 			: TANGENT;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct vcrefl 
{
	float4 	vertex 				: POSITION;
	float4	color 				: COLOR;
	float2 	texcoord 			: TEXCOORD;
	float3 	normal 				: NORMAL;
	float4 	tangent 			: TANGENT;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

/// ---------------------------- FRAG ----------------------------------

struct icolor
{
	float4 	pos 				: POSITION;
	float4	color 				: COLOR;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct ibase
{
	float4 	pos 				: POSITION;
	float2 	uv 					: TEXCOORD0;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct icbase
{
	float4 	pos 				: POSITION;
	float4	color 				: COLOR;
	float2 	uv 					: TEXCOORD0;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct ibase2
{
	float4 	pos 				: POSITION;
	float2 	uv 					: TEXCOORD0;
	float2 	uv1					: TEXCOORD1;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct icbase2
{
	float4 	pos 				: POSITION;
	float4	color 				: COLOR;
	float2 	uv 					: TEXCOORD0;
	float2 	uv1					: TEXCOORD1;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct icbase3
{
	float4 	pos 				: POSITION;
	float4	color 				: COLOR;
	float2 	uv 					: TEXCOORD0;
	float2 	uv1					: TEXCOORD1;
	float2 	uv2					: TEXCOORD2;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct ic2base2
{
	float4 pos 					: SV_POSITION;
	float4 color 				: COLOR0;
	float4 color1 				: COLOR1;
	float2 uv 					: TEXCOORD0;
	float2 uv1 					: TEXCOORD1;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct ic2base4
{
	float4 pos 					: SV_POSITION;
	float4 color 				: COLOR0;
	float4 color1 				: COLOR1;
	float2 uv 					: TEXCOORD0;
	float2 uv1 					: TEXCOORD1;
	float2 uv2 					: TEXCOORD2;
	float2 uv3 					: TEXCOORD3;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct ic3base5
{
	float4 pos 					: SV_POSITION;
	float4 color 				: COLOR0;
	float4 color1 				: COLOR1;
	float4 color2 				: COLOR2;
	float2 uv 					: TEXCOORD0;
	float2 uv1 					: TEXCOORD1;
	float2 uv2 					: TEXCOORD2;
	float2 uv3 					: TEXCOORD3;
	float2 uv4					: TEXCOORD4;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct itex
{
	float4 	pos 				: POSITION;
	float2 	uv 					: TEXCOORD0;
	float3 	light				: TEXCOORD1;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};
        
struct inorm
{
	float4 	pos 				: POSITION;
	float2 	uv 					: TEXCOORD0;
	float3 	normal 				: TEXCOORD1;
	float3 	light				: TEXCOORD2;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};
			
struct iglass
{
	float4 	pos 				: POSITION;
	float4	color 				: COLOR;
	float2	uv					: TEXCOORD0;
	float3 	view 				: TEXCOORD1;
	float3 	tsBase0 			: TEXCOORD2;
	float3 	tsBase1 			: TEXCOORD3;
	float3 	tsBase2 			: TEXCOORD4;
};

struct irefl
{
	float4  pos 				: SV_POSITION;
	float2  uv 					: TEXCOORD0;
	float3  normal 				: TEXCOORD1;
	float3  tangent 			: TEXCOORD2;
	float3  binormal 			: TEXCOORD3;
	float3  view 				: TEXCOORD4;
	float3 	light				: TEXCOORD5;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct irefl2
{
	float4  pos 				: SV_POSITION;
	float2  uv 					: TEXCOORD0;
	float2  uv1 				: TEXCOORD1;
	float3  normal 				: TEXCOORD2;
	float3  tangent 			: TEXCOORD3;
	float3  binormal 			: TEXCOORD4;
	float3  view	 			: TEXCOORD5;
	float3 	light				: TEXCOORD6;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct irefl3
{
	float4  pos 				: SV_POSITION;
	float2  uv 					: TEXCOORD0;
	float2  uv1 				: TEXCOORD1;
	float2  uv2 				: TEXCOORD2;
	float3  normal 				: TEXCOORD3;
	float3  tangent 			: TEXCOORD4;
	float3  binormal 			: TEXCOORD5;
	float3  view	 			: TEXCOORD6;
	float3 	light				: TEXCOORD7;

	UNITY_VERTEX_INPUT_INSTANCE_ID
};
#endif

#ifndef DYNAMIC_RADIAL_MASKS_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL
#define DYNAMIC_RADIAL_MASKS_HEIGHTFIELD_4_ADVANCED_NORMALIZED_ID1_LOCAL


#ifndef DYNAMIC_RADIAL_MASKS_VARIABLES_DECLARED_IN_CBUFFER
float4 DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_DATA1[4];	
float4 DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_DATA2[4];	
#endif

#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local(float3 positionWS, float noise
#ifdef DYNAMIC_RADIAL_MASKS_VARIABLES_DECLARED_IN_CBUFFER
,
float4 DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_DATA1[4],
float4 DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_DATA2[4]
#endif
)
{
    float retValue = 1; 

	int i = 0;
[unroll]	for(i = 0; i < 4; i++)
	{
		float mask = ShaderExtensions_DynamicRadialMasks_HeightField_Advanced(positionWS, 
																					DYNAMIC_RADIAL_MASKS_READ_POSITION(DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local, i), 
																					DYNAMIC_RADIAL_MASKS_READ_RADIUS(DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local, i), 
																					DYNAMIC_RADIAL_MASKS_READ_INTENSITY(DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local, i), 
																					DYNAMIC_RADIAL_MASKS_READ_EDGE_SIZE(DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local, i), 
																					DYNAMIC_RADIAL_MASKS_READ_SMOOTH(DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local, i), 
																					DYNAMIC_RADIAL_MASKS_READ_NOISE_STRENGTH(DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local, i) * noise);

		retValue *= 1 - saturate(mask);	
	}		

    return 1 - retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_float(float3 positionWS, float noise, out float retValue)
{
    #ifdef DYNAMIC_RADIAL_MASKS_VARIABLES_DECLARED_IN_CBUFFER
		retValue = 0;
	#else
		retValue = DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local(positionWS, noise); 	
	#endif
}

void DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local_half(half3 positionWS, half noise, out half retValue)
{
    #ifdef DYNAMIC_RADIAL_MASKS_VARIABLES_DECLARED_IN_CBUFFER
		retValue = 0;
	#else
		retValue = DynamicRadialMasks_HeightField_4_Advanced_Normalized_ID1_Local(positionWS, noise); 	
	#endif
}

#endif

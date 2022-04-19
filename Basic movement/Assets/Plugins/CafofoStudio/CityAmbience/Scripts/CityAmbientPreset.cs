using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafofoStudio
{
    [CreateAssetMenu(fileName = "MyCityAmbientPreset", menuName = "CafofoStudio/Create Custom Preset Asset/City", order = 1)]
	public class CityAmbientPreset : AmbientPreset
    {
		[Range(0, 1)] public float trafficIntensity = 0f;
		[Range(0, 1)] public float trafficVolumeMultiplier = 1f;

		[Range(0, 1)] public float vehicleIntensity = 0f;
		[Range(0, 1)] public float vehicleVolumeMultiplier = 1f;

		[Range(0, 1)] public float crowdIntensity = 0f;
		[Range(0, 1)] public float crowdVolumeMultiplier = 1f;

		[Range(0, 1)] public float constructionIntensity = 0f;
		[Range(0, 1)] public float constructionVolumeMultiplier = 1f;

		[Range(0, 1)] public float birdsIntensity = 0f;
		[Range(0, 1)] public float birdsVolumeMultiplier = 1f;

		[Range(0, 1)] public float rainIntensity = 0f;
		[Range(0, 1)] public float rainVolumeMultiplier = 1f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CafofoStudio
{
	[CustomEditor(typeof(CityAmbientMixer))]
	public class CityAmbienceMixerEditor : AmbienceMixerEditor<CityAmbientMixer, CityAmbientPreset>
    {
		protected override List<string> GetSerializedElementProperties ()
		{
			return new List<string>() { "_traffic", "_vehicles", "_crowd", "_construction", "_birds", "_rain"};
		}

		protected override void ApplyPreset (CityAmbientPreset preset)
		{
			ApplyPresetConfig ("_traffic", preset.trafficIntensity, preset.trafficVolumeMultiplier);
			ApplyPresetConfig ("_vehicles", preset.trafficIntensity, preset.trafficVolumeMultiplier);
			ApplyPresetConfig ("_crowd", preset.crowdIntensity, preset.crowdVolumeMultiplier);
			ApplyPresetConfig ("_construction", preset.constructionIntensity, preset.constructionVolumeMultiplier);
			ApplyPresetConfig ("_birds",preset.birdsIntensity, preset.birdsVolumeMultiplier);
			ApplyPresetConfig ("_rain", preset.rainIntensity, preset.rainVolumeMultiplier);
		}
	
		protected override SoundElement GetSoundElementFromProperty (string propertyName)
		{
			switch (propertyName) {
			case "_traffic":
				return myTarget.Traffic;
			case "_vehicles":
				return myTarget.Vehicles;
			case "_crowd":
				return myTarget.Crowd;
			case "_construction":
				return myTarget.Construction;
			case "_birds":
				return myTarget.Birds;
			default:
				return myTarget.Rain;
			}
		}

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafofoStudio
{
	public class CityAmbientMixer : AmbienceMixer<CityAmbientPreset>
    {
		[SerializeField] private SoundElement _traffic;
		public SoundElement Traffic
		{
			get { return _traffic; }
			private set { _traffic = Traffic; }
		}

		[SerializeField] private SoundElement _vehicles;
		public SoundElement Vehicles
		{
			get { return _vehicles; }
			private set { _vehicles = Vehicles; }
		}

        [SerializeField] private SoundElement _crowd;
        public SoundElement Crowd
        {
            get { return _crowd; }
			private set { _crowd = Crowd; }
        }

		[SerializeField] private SoundElement _construction;
		public SoundElement Construction
		{
			get { return _construction; }
			private set { _construction = Construction; }
		}

		[SerializeField] private SoundElement _birds;
		public SoundElement Birds
		{
			get { return _birds; }
			private set { _birds = Birds; }
		}

        [SerializeField] private SoundElement _rain;
        public SoundElement Rain
        {
            get { return _rain; }
            private set { _rain = Rain; }
        }

		protected override List<SoundElement> elements {
			get {
				return new List<SoundElement>() { _traffic, _vehicles, _crowd, _construction, _birds, _rain};
			}
		}

		override public void ApplyPreset(CityAmbientPreset selectedPreset) 
		{
			_traffic.SetIntensity (selectedPreset.trafficIntensity);
			_traffic.SetVolumeMultiplier (selectedPreset.trafficVolumeMultiplier);

			_vehicles.SetIntensity (selectedPreset.vehicleIntensity);
			_vehicles.SetVolumeMultiplier (selectedPreset.vehicleVolumeMultiplier);

			_crowd.SetIntensity (selectedPreset.crowdIntensity);
			_crowd.SetVolumeMultiplier (selectedPreset.crowdVolumeMultiplier);

			_construction.SetIntensity (selectedPreset.constructionIntensity);
			_construction.SetVolumeMultiplier (selectedPreset.constructionVolumeMultiplier);

			_birds.SetIntensity(selectedPreset.birdsIntensity);
			_birds.SetVolumeMultiplier(selectedPreset.birdsVolumeMultiplier);

			_rain.SetIntensity(selectedPreset.rainIntensity);
			_rain.SetVolumeMultiplier(selectedPreset.rainVolumeMultiplier);
		}
			
    }
}
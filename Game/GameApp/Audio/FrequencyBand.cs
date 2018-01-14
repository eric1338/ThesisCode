using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class FrequencyBand
	{

		public class FBPoint
		{

			public float Time { get; set; }
			public float Amplitude { get; set; }
			public float MinimumSquaredErrorToPrevious { get; set; }

			public FBPoint(float time, float amplitude, float minimumSquaredErrorToPrevious)
			{
				Time = time;
				Amplitude = amplitude;
				MinimumSquaredErrorToPrevious = minimumSquaredErrorToPrevious;
			}

		}

		public List<FBPoint> FBPoints { get; set; }

		public List<int> SpectrumBands { get; set; }

		public static float ValueThreshold { get; set; }

		private float[] lastAmplitudes = null;

		public FrequencyBand()
		{
			FBPoints = new List<FBPoint>();

			SpectrumBands = new List<int>();

			ValueThreshold = 0.0001f;
		}

		public void AddSpectrumBand(int spectrumBand)
		{
			SpectrumBands.Add(spectrumBand);
		}

		public void AddAmplitudes(float time, float[] amplitudes)
		{
			float maximumAmplitude = -1;

			foreach (float amplitude in amplitudes)
			{
				if (amplitude > maximumAmplitude) maximumAmplitude = amplitude;
			}

			float minimumSquaredErrorToPrevious = 9999;

			if (lastAmplitudes != null)
			{
				minimumSquaredErrorToPrevious = GetMinimumSquaredError(amplitudes);
			}

			FBPoints.Add(new FBPoint(time, maximumAmplitude, minimumSquaredErrorToPrevious));

			lastAmplitudes = amplitudes;
		}

		private float GetMinimumSquaredError(float[] newAmplitudes)
		{
			float minimumError = 9999;

			foreach (float lastAmplitude in lastAmplitudes)
			{
				foreach (float newAmplitude in newAmplitudes)
				{
					if (lastAmplitude > ValueThreshold && newAmplitude > ValueThreshold)
					{
						float error = (float)Math.Pow(lastAmplitude - newAmplitude, 2);

						if (error < minimumError) minimumError = error;
					}
				}
			}

			return minimumError;
		}

	}
}

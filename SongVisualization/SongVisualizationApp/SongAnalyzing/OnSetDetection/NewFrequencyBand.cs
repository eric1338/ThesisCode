using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class NewFrequencyBand
	{

		public List<MyPoint> MinimumSquaredErrorsToPrevious { get; set; }
		public List<MyPoint> MaxAmplitudes { get; set; }
		public List<int> SpectrumBands { get; set; }

		public static float ValueThreshold = 0.01f;

		private float[] lastAmplitudes = null;

		public NewFrequencyBand()
		{
			MinimumSquaredErrorsToPrevious = new List<MyPoint>();
			MaxAmplitudes = new List<MyPoint>();
			SpectrumBands = new List<int>();
		}

		public void AddSpectrumBand(int spectrumBand)
		{
			SpectrumBands.Add(spectrumBand);
		}

		public void AddAmplitudes(float time, float[] amplitudes)
		{
			float maxAmplitude = -1;

			foreach (float amplitude in amplitudes)
			{
				if (amplitude > maxAmplitude) maxAmplitude = amplitude;
			}

			MaxAmplitudes.Add(new MyPoint(time, maxAmplitude));


			if (lastAmplitudes == null)
			{
				MinimumSquaredErrorsToPrevious.Add(new MyPoint(time, 9999));
			}
			else
			{
				float mininumSquaredError = GetMinimumSquaredError(amplitudes);

				MinimumSquaredErrorsToPrevious.Add(new MyPoint(time, mininumSquaredError));
			}

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

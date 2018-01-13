using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// code by Anthony Lee and slightly altered
// https://github.com/Teh-Lemon/Onset-Detection

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class OnsetDetection
	{

		private int sampleRate;
		private int sampleSize;
		private float timePerSample;

		public float[] Onsets { get; set; }

		private float[] previousSpectrum;
		private float[] spectrum;

		private bool rectify;

		private List<float> fluxes;

		public OnsetDetection(int sampleRate, int sampleSize, float timePerSample)
		{
			this.sampleRate = sampleRate;
			this.sampleSize = sampleSize;
			this.timePerSample = timePerSample;

			spectrum = new float[sampleSize / 2 + 1];
			previousSpectrum = new float[spectrum.Length];
			rectify = true;
			fluxes = new List<float>();
		}


		public void AddFlux(float[] fftSpectrum, bool hamming = true)
		{
			if (fftSpectrum == null) return;

			Array.Copy(spectrum, previousSpectrum, spectrum.Length);
			Array.Copy(fftSpectrum, spectrum, spectrum.Length);

			fluxes.Add(CompareSpectrums(spectrum, previousSpectrum, rectify));
		}


		public void FindOnsets(float sensitivity = 1.5f, float thresholdTimeSpan = 0.5f)
		{
			float[] thresholdAverage = GetThresholdAverage(fluxes, sampleSize,
			thresholdTimeSpan, sensitivity);

			Onsets = GetPeaks(fluxes, thresholdAverage, sampleSize);
		}

		public void NormalizeOnsets(int type)
		{
			if (Onsets != null)
			{
				float max = 0;
				float min = 0;
				float difference = 0;

				// Find strongest/weakest onset
				for (int i = 0; i < Onsets.Length; i++)
				{
					max = Math.Max(max, Onsets[i]);
					min = Math.Min(min, Onsets[i]);
				}
				difference = max - min;

				// Normalize the onsets
				switch (type)
				{
					case 0:
						for (int i = 0; i < Onsets.Length; i++)
						{
							Onsets[i] /= max;
						}
						break;
					case 1:
						for (int i = 0; i < Onsets.Length; i++)
						{
							if (Onsets[i] == min)
							{
								Onsets[i] = 0.01f;
							}
							else
							{
								Onsets[i] -= min;
								Onsets[i] /= difference;
							}
						}
						break;
					default:
						break;
				}
			}

			return;
		}


		float CompareSpectrums(float[] spectrum, float[] previousSpectrum, bool rectify)
		{
			// Find difference between each respective bins of each spectrum
			// Sum up these differences
			float flux = 0;
			for (int i = 0; i < spectrum.Length; i++)
			{
				float value = (spectrum[i] - previousSpectrum[i]);
				// If ignoreNegativeEnergy is true
				// Only interested in rise in energy, ignore negative values
				if (!rectify || value > 0)
				{
					flux += value;
				}
			}

			return flux;
		}

		// Finds the peaks in the flux above the threshold average
		float[] GetPeaks(List<float> data, float[] dataAverage, int sampleCount)
		{
			// Time window in which humans can not distinguish beats in seconds
			const float indistinguishableRange = 0.01f; // 10ms
														// Number of set of samples to ignore after an onset
			int immunityPeriod = (int)((float)sampleCount
				/ (float)sampleRate
				/ indistinguishableRange);

			// Results
			float[] peaks = new float[data.Count];

			// For each sample
			for (int i = 0; i < data.Count; i++)
			{
				// Add the peak if above the average, else 0
				if (data[i] >= dataAverage[i])
				{
					peaks[i] = data[i] - dataAverage[i];
				}
				else
				{
					peaks[i] = 0.0f;
				}
			}

			// Prune the peaks list
			peaks[0] = 0.0f;
			for (int i = 1; i < peaks.Length - 1; i++)
			{
				// If the next value is lower than the current value, that means it is end of the peak
				if (peaks[i] < peaks[i + 1])
				{
					peaks[i] = 0.0f;
					continue;
				}

				// Remove peaks too close to each other
				if (peaks[i] > 0.0f)
				{
					for (int j = i + 1; j < i + immunityPeriod; j++)
					{
						if (peaks[j] > 0)
						{
							peaks[j] = 0.0f;
						}
					}
				}
			}

			return peaks;
		}

		// Find the running average of the given list
		float[] GetThresholdAverage(List<float> data, int sampleWindow,
			float thresholdTimeSpan, float thresholdMultiplier)
		{
			List<float> thresholdAverage = new List<float>();

			// How many spectral fluxes to look at, at a time (approximation is fine)
			float sourceTimeSpan = (float)(sampleWindow) / (float)(sampleRate);
			int windowSize = (int)(thresholdTimeSpan / sourceTimeSpan / 2);

			for (int i = 0; i < data.Count; i++)
			{
				// Max/Min Prevent index out of bounds error
				// Look at values to the left and right of the current spectral flux
				int start = Math.Max(i - windowSize, 0);
				int end = Math.Min(data.Count, i + windowSize);
				// Current average
				float mean = 0;

				// Sum up the surrounding values
				for (int j = start; j < end; j++)
				{
					mean += data[j];
				}

				// Find the average
				mean /= (end - start);

				// Multiply mean to increase the sensitivity
				thresholdAverage.Add(mean * thresholdMultiplier);
			}

			return thresholdAverage.ToArray();
		}

	}
}

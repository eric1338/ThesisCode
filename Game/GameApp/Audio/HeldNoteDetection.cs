using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class HeldNoteDetection
	{

		private List<FrequencyBand> frequencyBands = new List<FrequencyBand>();

		private float fftFrequencyBandWidth = 43.1f;


		public HeldNoteDetection()
		{
			CreateFrequencyBands();
		}

		public void AnalyzeSpectrum(float[] fftSpectrum, float time)
		{
			foreach (FrequencyBand frequencyBand in frequencyBands)
			{
				AddFrequencyToFrequencyBand(frequencyBand, time, fftSpectrum);
			}
		}

		private void AddFrequencyToFrequencyBand(FrequencyBand frequencyBand, float time, float[] spectrum)
		{
			int numberOfSpectrumBands = frequencyBand.SpectrumBands.Count;

			float[] frequencyValues = new float[numberOfSpectrumBands];

			for (int i = 0; i < numberOfSpectrumBands; i++)
			{
				frequencyValues[i] = spectrum[frequencyBand.SpectrumBands[i]];
			}

			frequencyBand.AddAmplitudes(time, frequencyValues);
		}

		public void AddHeldNotes(SongElements songElements)
		{
			RectangleDetection rectangularDetection = new RectangleDetection();

			rectangularDetection.AddHeldNotesFromFrequencyBands(songElements, frequencyBands);
		}

		private float GetFrequency(float x)
		{
			return (float)Math.Pow(2, ((x - 49) / 12.0f)) * 440;
		}

		private void CreateFrequencyBands()
		{
			float firstKey = 16; // C2
			float lastKey = 75; // B6

			for (float i = firstKey; i < lastKey; i += 0.5f)
			{
				float lowestFrequency = GetFrequency(i);
				float highestFrequency = GetFrequency(i + 1);

				int lowestFFTBand = (int)Math.Floor(lowestFrequency / fftFrequencyBandWidth);
				int highestFFTBand = (int)Math.Ceiling(highestFrequency / fftFrequencyBandWidth);

				FrequencyBand frequencyBand = new FrequencyBand();

				for (int j = lowestFFTBand; j <= highestFFTBand; j++)
				{
					frequencyBand.AddSpectrumBand(j);
				}

				frequencyBands.Add(frequencyBand);
			}
		}

	}
}

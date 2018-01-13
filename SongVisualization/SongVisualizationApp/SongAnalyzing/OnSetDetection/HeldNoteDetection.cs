using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// code inspired by Anthony Lee
// https://github.com/Teh-Lemon/Onset-Detection

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	

	class HeldNoteDetection
	{

		private List<FrequencyBand> frequencyBands = new List<FrequencyBand>();
		private List<NewFrequencyBand> newFrequencyBands = new List<NewFrequencyBand>();
		
		public HeldNoteDetection()
		{
			CreateFrequencyBands();
		}

		public List<FrequencyBand> GetFrequencyBands()
		{
			return frequencyBands;
		}
		

		public void AnalyzeSpectrum(float[] fftSpectrum, float time)
		{
			foreach (FrequencyBand frequencyBand in frequencyBands)
			{
				//AddFrequencyToFrequencyBand(frequencyBand, time, fftSpectrum);
			}

			foreach (NewFrequencyBand newFrequencyBand in newFrequencyBands)
			{
				AddFrequencyToNewFrequencyBand(newFrequencyBand, time, fftSpectrum);
			}
		}

		private void AddFrequencyToNewFrequencyBand(NewFrequencyBand newFrequencyBand, float time, float[] spectrum)
		{
			int c = newFrequencyBand.SpectrumBands.Count;

			float[] frequencyValues = new float[c];

			for (int i = 0; i < c; i++)
			{
				frequencyValues[i] = spectrum[newFrequencyBand.SpectrumBands[i]];
			}

			newFrequencyBand.AddAmplitudes(time, frequencyValues);
		}

		private void AddFrequencyToFrequencyBand(FrequencyBand frequencyBand, float time, float[] spectrum)
		{
			float valueSum = 0;

			int numberOfValues = 0;

			foreach (int spectrumBand in frequencyBand.SpectrumBands)
			{
				valueSum += spectrum[spectrumBand];

				numberOfValues++;
			}

			frequencyBand.AddAmplitude(time, valueSum / numberOfValues);
		}

		private void AddFrequencyToFrequencyBandsOLD(FrequencyBand frequencyBand, float time, float[] spectrum)
		{
			float maxValue = -1;

			foreach (int spectrumBand in frequencyBand.SpectrumBands)
			{
				float value = spectrum[spectrumBand];

				if (value > maxValue) maxValue = value;
			}

			frequencyBand.AddAmplitude(time, maxValue);
		}

		// TEST

		public SongPropertyValues CreateHeldNoteTest()
		{
			//RectangleDetection recDetec = new RectangleDetection();

			//return recDetec.GetHeldNotes(GetFrequencyBands());

			NewRectangleDetection recDetec = new NewRectangleDetection();

			return recDetec.GetHeldNotes(newFrequencyBands);
		}


		private float fftFrequencyBandWidth = 43.1f;

		private float[] semitoneFrequencies;

		public static int TEST_FREQUENCY_BAND_WIDTH = 3;

		public static int TEST_STARTING_FREQUENCY_INDEX = 0;
		public static int TEST_NUMBER_OF_FREQUENCIES = 72;

		private void InitSemitoneFrequencies()
		{
			float[] semitoneFrequenciesBase = new float[72];

			semitoneFrequenciesBase[0] = 65.4064f;
			semitoneFrequenciesBase[1] = 69.2957f;
			semitoneFrequenciesBase[2] = 73.4162f;
			semitoneFrequenciesBase[3] = 77.7817f;
			semitoneFrequenciesBase[4] = 82.4069f;
			semitoneFrequenciesBase[5] = 87.3071f;
			semitoneFrequenciesBase[6] = 92.4986f;
			semitoneFrequenciesBase[7] = 97.9989f;
			semitoneFrequenciesBase[8] = 103.826f;
			semitoneFrequenciesBase[9] = 110.000f;
			semitoneFrequenciesBase[10] = 116.541f;
			semitoneFrequenciesBase[11] = 123.471f;
			semitoneFrequenciesBase[12] = 130.813f;
			semitoneFrequenciesBase[13] = 138.591f;
			semitoneFrequenciesBase[14] = 146.832f;
			semitoneFrequenciesBase[15] = 155.563f;
			semitoneFrequenciesBase[16] = 164.814f;
			semitoneFrequenciesBase[17] = 174.614f;
			semitoneFrequenciesBase[18] = 184.997f;
			semitoneFrequenciesBase[19] = 195.998f;
			semitoneFrequenciesBase[20] = 207.652f;
			semitoneFrequenciesBase[21] = 220.000f;
			semitoneFrequenciesBase[22] = 233.082f;
			semitoneFrequenciesBase[23] = 246.942f;
			semitoneFrequenciesBase[24] = 261.626f;
			semitoneFrequenciesBase[25] = 277.183f;
			semitoneFrequenciesBase[26] = 293.665f;
			semitoneFrequenciesBase[27] = 311.127f;
			semitoneFrequenciesBase[28] = 329.628f;
			semitoneFrequenciesBase[29] = 349.228f;
			semitoneFrequenciesBase[30] = 369.994f;
			semitoneFrequenciesBase[31] = 391.995f;
			semitoneFrequenciesBase[32] = 415.305f;
			semitoneFrequenciesBase[33] = 440.000f;
			semitoneFrequenciesBase[34] = 466.164f;
			semitoneFrequenciesBase[35] = 493.883f;
			semitoneFrequenciesBase[36] = 523.251f;
			semitoneFrequenciesBase[37] = 554.365f;
			semitoneFrequenciesBase[38] = 587.330f;
			semitoneFrequenciesBase[39] = 622.254f;
			semitoneFrequenciesBase[40] = 659.255f;
			semitoneFrequenciesBase[41] = 698.456f;
			semitoneFrequenciesBase[42] = 739.989f;
			semitoneFrequenciesBase[43] = 783.991f;
			semitoneFrequenciesBase[44] = 830.609f;
			semitoneFrequenciesBase[45] = 880.000f;
			semitoneFrequenciesBase[46] = 932.328f;
			semitoneFrequenciesBase[47] = 987.767f;
			semitoneFrequenciesBase[48] = 1046.50f;
			semitoneFrequenciesBase[49] = 1108.73f;
			semitoneFrequenciesBase[50] = 1174.66f;
			semitoneFrequenciesBase[51] = 1244.51f;
			semitoneFrequenciesBase[52] = 1318.51f;
			semitoneFrequenciesBase[53] = 1396.91f;
			semitoneFrequenciesBase[54] = 1479.98f;
			semitoneFrequenciesBase[55] = 1567.98f;
			semitoneFrequenciesBase[56] = 1661.22f;
			semitoneFrequenciesBase[57] = 1760.00f;
			semitoneFrequenciesBase[58] = 1864.66f;
			semitoneFrequenciesBase[59] = 1975.53f;
			semitoneFrequenciesBase[60] = 2093.00f;
			semitoneFrequenciesBase[61] = 2217.46f;
			semitoneFrequenciesBase[62] = 2349.32f;
			semitoneFrequenciesBase[63] = 2489.02f;
			semitoneFrequenciesBase[64] = 2637.02f;
			semitoneFrequenciesBase[65] = 2793.83f;
			semitoneFrequenciesBase[66] = 2959.96f;
			semitoneFrequenciesBase[67] = 3135.96f;
			semitoneFrequenciesBase[68] = 3322.44f;
			semitoneFrequenciesBase[69] = 3520.00f;
			semitoneFrequenciesBase[70] = 3729.31f;
			semitoneFrequenciesBase[71] = 3951.07f;

			int firstIndex = TEST_STARTING_FREQUENCY_INDEX;
			int max = Math.Min(72, TEST_STARTING_FREQUENCY_INDEX + TEST_NUMBER_OF_FREQUENCIES);

			semitoneFrequencies = new float[max - firstIndex];

			int n = 0;

			//Console.WriteLine(" ");
			//Console.Write("FreqSI=" + TEST_STARTING_FREQUENCY_INDEX + ", FreqN=" + TEST_NUMBER_OF_FREQUENCIES + " -> ");
			for (int i = firstIndex; i < max; i++)
			{
				//Console.Write(i + ", ");
				semitoneFrequencies[n] = semitoneFrequenciesBase[i];
				n++;
			}
		}

		private float GetFrequency(float x)
		{
			return (float) Math.Pow(2, ((x - 49) / 12.0f)) * 440;
			//return 65.40636393f * (float) Math.Pow(Math.E, 0.05776227579f * x);
		}

		private void CreateFrequencyBands()
		{
			InitSemitoneFrequencies();

			int nSemitonesPerFrequencyBand = TEST_FREQUENCY_BAND_WIDTH;

			float max = semitoneFrequencies.Length / (float)nSemitonesPerFrequencyBand - 2;

			float firstKey = 16; // C2
			float lastKey = 75; // B6

			for (float i = firstKey; i < lastKey; i += 0.5f)
			{
				float lowestFrequency = GetFrequency(i);
				float highestFrequency = GetFrequency(i + 1);

				int lowestFFTBand = (int)Math.Floor(lowestFrequency / fftFrequencyBandWidth);
				int highestFFTBand = (int)Math.Ceiling(highestFrequency / fftFrequencyBandWidth);

				FrequencyBand frequencyBand = new FrequencyBand();
				NewFrequencyBand newFrequencyBand = new NewFrequencyBand();

				List<int> fftBands = new List<int>();

				for (int j = lowestFFTBand; j <= highestFFTBand; j++)
				{
					frequencyBand.AddSpectrumBand(j);
					newFrequencyBand.AddSpectrumBand(j);
				}

				frequencyBands.Add(frequencyBand);
				newFrequencyBands.Add(newFrequencyBand);
			}

			


		}


	}
}

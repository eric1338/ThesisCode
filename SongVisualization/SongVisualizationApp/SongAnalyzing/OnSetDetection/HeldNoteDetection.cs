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
		FFT fft = new FFT();
		AudioFileReader PCM;
		int SampleSize;

		int nSamples = 0;


		private List<FrequencyBand> frequencyBands = new List<FrequencyBand>();

		


		// Constructor
		public HeldNoteDetection(AudioFileReader pcm, int sampleWindow)
		{
			PCM = pcm;
			SampleSize = sampleWindow;

			CreateFrequencyBands();
		}

		public List<FrequencyBand> GetFrequencyBands()
		{
			return frequencyBands;
		}


		private int GetMargin(float val)
		{
			float exponent = 2;

			float bandWidthFactor = 15;

			int frequencyOffset = 15;

			return frequencyOffset + (int) Math.Round(bandWidthFactor * Math.Pow(val, exponent));
		}

		private FrequencyBand CreateFrequencyBand(int index)
		{
			int leftFrequencyMargin = GetMargin(index * 0.5f);
			int rightFrequencyMargin = GetMargin(index * 0.5f + 1);

			Console.WriteLine(leftFrequencyMargin + " - " + rightFrequencyMargin);

			return new FrequencyBand(leftFrequencyMargin, rightFrequencyMargin);
		}

		private void CreateFrequencyBands()
		{
			for (int i = 0; i < 6; i++)
			{
				frequencyBands.Add(CreateFrequencyBand(i));
			}

			/*
			frequencyBands.Add(new FrequencyBand(0, 16));
			frequencyBands.Add(new FrequencyBand(16, 32));
			frequencyBands.Add(new FrequencyBand(32, 64));
			frequencyBands.Add(new FrequencyBand(64, 128));
			frequencyBands.Add(new FrequencyBand(128, 256));
			frequencyBands.Add(new FrequencyBand(256, 512));
			frequencyBands.Add(new FrequencyBand(512, 1024));

			frequencyBands.Add(new FrequencyBand(31, 62));
			frequencyBands.Add(new FrequencyBand(63, 125));
			frequencyBands.Add(new FrequencyBand(125, 250));
			frequencyBands.Add(new FrequencyBand(250, 500));
			frequencyBands.Add(new FrequencyBand(500, 1000));
			frequencyBands.Add(new FrequencyBand(1000, 2000));
			frequencyBands.Add(new FrequencyBand(2000, 4000));
			frequencyBands.Add(new FrequencyBand(4000, 8000));
			*/
		}


		/// <summary>
		///  Perform Spectral Flux onset detection on loaded audio file
		///  <para>Recommended onset detection algorithm for most needs</para>  
		/// </summary>
		///  <param name="hamming">Apply hamming window before FFT function. 
		///  <para>Smooths out the noise in between peaks.</para> 
		///  <para>Small improvement but isn't too costly.</para> 
		///  <para>Default: true</para></param>
		public bool AnalyzeSpectrum(float[] samples, bool hamming = true)
		{
			// Find the spectral flux of the audio
			if (samples != null)
			{
				// Perform Fast Fourier Transform on the audio samples
				fft.RealFFT(samples, hamming);

				float time = nSamples * GetTimePerSample();

				foreach (FrequencyBand frequencyBand in frequencyBands)
				{
					AddFrequencyToFrequencyBands(frequencyBand, time, fft.GetPowerSpectrum());
				}

				nSamples++;

				return false;
			}

			return true;
		}


		private void AddFrequencyToFrequencyBands(FrequencyBand frequencyBand, float time, float[] spectrum)
		{
			int start = frequencyBand.LeftFrequencyMargin;
			int end = Math.Min(frequencyBand.RightFrequencyMargin, spectrum.Length);

			float maxValue = 0;

			for (int i = start; i < end; i++)
			{
				if (spectrum[i] > maxValue) maxValue = spectrum[i];
			}

			frequencyBand.AddFrequency(time, maxValue);
		}


		public float GetTimePerSample()
		{
			return SampleSize / (float) PCM.WaveFormat.SampleRate;
		}

	}
}

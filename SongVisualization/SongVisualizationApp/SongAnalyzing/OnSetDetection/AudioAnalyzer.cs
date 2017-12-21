using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// code heavily inspired by Anthony Lee
// https://github.com/Teh-Lemon/Onset-Detection

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class AudioAnalyzer
	{
		private const int sampleSize = 1024;

		private int sampleRate = 0;
		private float timePerSample = 0;

		public float[] OnsetsFound { get; set; }

		private AudioFileReader pcmStream;

		private AmplitudeDetection amplitudeDetection;
		private OnsetDetection onsetDetection;
		private HeldNoteDetection heldNoteDetection;

		private FFT fft;

		public AudioAnalyzer()
		{
			fft = new FFT();
			
			fft.A = 0;
			fft.B = 1;
		}

		public void LoadAudioFromFile(string filePath)
		{
			if (filePath.EndsWith(".mp3") || filePath.EndsWith(".wav"))
			{
				pcmStream = new AudioFileReader(filePath);
			}
			else
			{
				throw new FormatException("Invalid audio file");
			}

			sampleRate = pcmStream.WaveFormat.SampleRate;

			timePerSample = sampleSize / (float) pcmStream.WaveFormat.SampleRate;

			OnsetsFound = null;
		}

		public void DisposeAudioAnalysis()
		{
			if (pcmStream != null)
			{
				pcmStream.Dispose();
				pcmStream = null;

				OnsetsFound = null;
			}
		}


		// Test
		public SongPropertyValues GetOnsetThingy()
		{
			SongPropertyValues onsetValues = new SongPropertyValues("Onsets02");

			float[] onsets = onsetDetection.Onsets;

			for (int i = 0; i < onsets.Length; i++)
			{
				float onset = onsets[i];

				onsetValues.AddPoint(timePerSample * i, onset);
			}

			return onsetValues;
		}

		public SongPropertyValues GetHeldNoteThingy()
		{
			return heldNoteDetection.CreateHeldNoteTest();
		}

		public SongPropertyValues GetAmplitudeThingy()
		{
			return amplitudeDetection.Values;
		}





		public SongPropertyValues GetSuperOnsetThingy()
		{
			SongPropertyValues onsetValues = new SongPropertyValues("SuperOnsets");

			List<Util.MyPoint> amplitudePoints = amplitudeDetection.Values.Points;

			float[] onsets = onsetDetection.Onsets;

			for (int i = 0; i < onsets.Length; i++)
			{
				float onset = onsets[i];

				float previousValuesSum = 0;

				for (int j = 1; j < 4; j++)
				{
					float val = amplitudePoints[Math.Max(0, i - j)].Y;

					previousValuesSum += val;
				}

				float bestAfterValue = -1;

				for (int k = 1; k < 6; k++)
				{
					float val = amplitudePoints[Math.Min(onsets.Length - 1, i + k)].Y;

					bestAfterValue = Math.Max(bestAfterValue, val);
				}

				float suitability = bestAfterValue / (previousValuesSum / 4.0f);

				onsetValues.AddPoint(timePerSample * i, onset * suitability);
			}

			onsetValues.AbsAndNormalize();

			return onsetValues;
		}









		public void Analyze()
		{
			amplitudeDetection = new AmplitudeDetection();
			onsetDetection = new OnsetDetection(sampleRate, sampleSize, timePerSample);
			heldNoteDetection = new HeldNoteDetection();

			bool finished = false;

			pcmStream.Position = 0;

			//float[] fftSpectrum = new float[sampleSize / 2 + 1];
			//float[] fftSpectrumCopy = new float[sampleSize / 2 + 1];

			int nSample = 0;

			do
			{
				float[] samples = ReadMonoPCM();

				if (samples == null) break;

				float time = nSample * timePerSample;

				fft.RealFFT(samples, true);
				float[] fftSpectrum = fft.GetPowerSpectrum();
				float[] fftSpectrumCopy = new float[fftSpectrum.Length];

				Array.Copy(fftSpectrum, fftSpectrumCopy, fftSpectrum.Length);

				amplitudeDetection.Analyze(fftSpectrum, time);

				// evtl nSamples * timePerSample als Parameter für Analyze-Methoden

				heldNoteDetection.AnalyzeSpectrum(fftSpectrumCopy, time);
				onsetDetection.AddFlux(fftSpectrum);

				nSample++;
			}
			while (!finished);

			amplitudeDetection.Values.AbsAndNormalize();

			// Find peaks
			onsetDetection.FindOnsets(1.5f);
			onsetDetection.NormalizeOnsets(0);
		}
		
		private float[] ReadMonoPCM()
		{
			int size = sampleSize;
			
			if (pcmStream.WaveFormat.Channels == 2) size *= 2;

			float[] output = new float[size];
			
			if (pcmStream.Read(output, 0, size) == 0)
			{
				// If end of audio file
				return null;
			}
			
			if (pcmStream.WaveFormat.Channels == 2)
			{
				return ConvertStereoToMono(output);
			}

			return output;
		}

		private float[] ConvertStereoToMono(float[] input)
		{
			float[] output = new float[input.Length / 2];
			int outputIndex = 0;
			
			for (int i = 0; i < input.Length; i += 2)
			{
				float leftChannel = input[i];
				float rightChannel = input[i + 1];
				
				output[outputIndex] = (leftChannel + rightChannel) / 2;
				outputIndex++;
			}

			return output;
		}
	}
}

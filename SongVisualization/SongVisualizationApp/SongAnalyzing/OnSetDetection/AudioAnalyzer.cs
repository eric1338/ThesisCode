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

		private int testVal1 = 0;
		private int testVal2 = 0;

		public AudioAnalyzer(int tv1 = 4, int tv2 = 6)
		{
			fft = new FFT();
			
			fft.A = 0;
			fft.B = 1;

			testVal1 = tv1;
			testVal2 = tv2;
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
			SongPropertyValues onsetValues = new SongPropertyValues("SuperOnsets [" + testVal1 + ", " + testVal2 + "]");

			List<Util.MyPoint> amplitudePoints = amplitudeDetection.Values.Points;

			float[] onsets = onsetDetection.Onsets;

			int sperrX = 0;

			for (int i = 0; i < onsets.Length; i++)
			{
				if (i < 40) continue;

				if (sperrX > 0)
				{
					sperrX--;
					onsetValues.AddPoint(timePerSample * i, 0);
					continue;
				}

				float onset = onsets[i];

				float previousValuesSum = 0;

				for (int j = 2; j < testVal1; j++)
				{
					float val = amplitudePoints[Math.Max(0, i - j)].Y;

					previousValuesSum += val;
				}

				float bestAfterValue = -1;

				for (int k = 0; k < testVal2; k++)
				{
					float val = amplitudePoints[Math.Min(onsets.Length - 1, i + k)].Y;

					bestAfterValue = Math.Max(bestAfterValue, val);
				}


				float suitability = bestAfterValue / (previousValuesSum / 4.0f);

				if (onset > 0.01f)
				{
					onsetValues.AddPoint(timePerSample * i, onset > 0 ? bestAfterValue : 0.0f);
					sperrX = 2;
				}
				else
				{
					onsetValues.AddPoint(timePerSample * i, 0);
				}


				//onsetValues.AddPoint(timePerSample * i, onset * suitability);
			}

			onsetValues.AbsAndNormalize();

			return onsetValues;
		}




		private SongPropertyValues freqValues = new SongPropertyValues("freqValues");

		public SongPropertyValues GetFreqValues()
		{
			return freqValues;
		}

		private List<string> freqLines = new List<string>();
		
		private void DoFreqTest(float[] samples, float time)
		{
			float sum = 0;
			int n = 0;

			if (time * 3 - Math.Floor(time * 3) < 0.04f)
			{
				int minutes = (int)Math.Floor(time / 60.0f);

				float seconds = (float)Math.Round(time - minutes * 60, 2);

				string freqLine = minutes + ":" + seconds + " | ";

				//for (int i = 0; i < samples.Length; i++)
				//{
				//	if (samples[i] > 0.9f) freqLine += (i + ", ");
				//}

				List<Tuple<int, float>> bands = new List<Tuple<int, float>>();

				float sum2 = 0;

				for (int i = 0; i < samples.Length; i++)
				{
					if (samples[i] > 0.2f)
					{
						sum2 += samples[i];
						bands.Add(new Tuple<int, float>(i, samples[i]));
					}
				}

				float val = 0;

				foreach (Tuple<int, float> band in bands)
				{
					val += band.Item1 * (band.Item2 / sum2);
				}

				freqLine += val;

				freqLines.Add(freqLine);
			}

			for (int i = 0; i < samples.Length; i++)
			{
				if (samples[i] > 0.2f)
				{
					sum += i;
					n++;
				}
			}

			n = Math.Max(n, 1);

			float value = sum / n;

			freqValues.AddPoint(time, value);
		}

		public void FinalizeFreqTest()
		{
			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(@"C:\ForVS\WriteFreqLines.txt"))
			{
				foreach (string line in freqLines)
				{
					file.WriteLine(line);
				}
			}
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
				//float[] fftSpectrum = GetAccordFFTValues(samples);

				float[] fftSpectrumCopy = new float[fftSpectrum.Length];

				Array.Copy(fftSpectrum, fftSpectrumCopy, fftSpectrum.Length);

				DoFreqTest(fftSpectrum, time);

				amplitudeDetection.Analyze(fftSpectrum, time);

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

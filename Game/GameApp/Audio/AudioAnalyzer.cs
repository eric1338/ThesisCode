using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class AudioAnalyzer
	{
		public SongElements SongElements { get; set; }

		private const int sampleSize = 1024;

		private int sampleRate = 0;
		private float timePerSample = 0;

		private AudioFileReader pcmStream;

		private AmplitudeDetection amplitudeDetection;
		private OnsetDetection onsetDetection;
		private HeldNoteDetection heldNoteDetection;

		private FFT fft;

		public AudioAnalyzer()
		{
			SongElements = new SongElements();

			fft = new FFT();

			fft.A = 0;
			fft.B = 1;
		}

		public static SongElements GetSongElements(string fileDirectory)
		{
			AudioAnalyzer audioAnalyzer = new AudioAnalyzer();

			audioAnalyzer.LoadAudioFromFile(fileDirectory);
			audioAnalyzer.Analyze();

			return audioAnalyzer.SongElements;
		}

		public void LoadAudioFromFile(string fileDirectory)
		{
			if (fileDirectory.EndsWith(".mp3") || fileDirectory.EndsWith(".wav"))
			{
				pcmStream = new AudioFileReader(fileDirectory);
			}
			else
			{
				throw new FormatException("Invalid audio file");
			}

			sampleRate = pcmStream.WaveFormat.SampleRate;

			timePerSample = sampleSize / (float)pcmStream.WaveFormat.SampleRate;
		}

		public void DisposeAudioAnalysis()
		{
			if (pcmStream != null)
			{
				pcmStream.Dispose();
				pcmStream = null;
			}
		}

		private void AddOnsets(SongElements songElements)
		{
			List<AmplitudeDetection.MyPoint> amplitudePoints = amplitudeDetection.Points;

			float[] onsets = onsetDetection.Onsets;

			int samplesNotUsed = 0;

			for (int i = 0; i < onsets.Length; i++)
			{
				if (samplesNotUsed > 0)
				{
					samplesNotUsed--;
					continue;
				}

				if (onsets[i] > 0.01f)
				{
					songElements.AddSingleBeat(timePerSample * i, amplitudePoints[i].Y);

					samplesNotUsed = 4;
				}
			}
		}

		public void Analyze()
		{
			amplitudeDetection = new AmplitudeDetection();
			onsetDetection = new OnsetDetection(sampleRate, sampleSize, timePerSample);
			heldNoteDetection = new HeldNoteDetection();

			pcmStream.Position = 0;

			int nSample = 0;

			do
			{
				float[] samples = ReadMonoPCM();

				if (samples == null) break;

				float time = nSample * timePerSample;

				fft.RealFFT(samples, true);
				float[] fftSpectrum = fft.GetPowerSpectrum();

				amplitudeDetection.AnalyzeSpectrum(fftSpectrum, time);
				heldNoteDetection.AnalyzeSpectrum(fftSpectrum, time);
				onsetDetection.AddFlux(fftSpectrum);

				nSample++;
			}
			while (true);

			amplitudeDetection.AbsAndNormalize();

			// Find peaks
			onsetDetection.FindOnsets(1.5f);
			onsetDetection.NormalizeOnsets();

			AddOnsets(SongElements);
			heldNoteDetection.AddHeldNotes(SongElements);
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

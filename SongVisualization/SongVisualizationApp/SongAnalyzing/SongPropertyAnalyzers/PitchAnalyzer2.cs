using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongVisualizationApp.FileReader;
using SongVisualizationApp.Util;
using MathNet.Numerics.IntegralTransforms;

namespace SongVisualizationApp.SongAnalyzing.SongPropertyAnalyzers
{
	class PitchAnalyzer2
	{

		public string FFTFunctionName { get; set; }
		public string WindowFunctionName { get; set; }

		private float sampleWidthTime = 0.2f;

		public PitchAnalyzer2(string fftFunctionName, string windowFunctionName)
		{
			FFTFunctionName = fftFunctionName;
			WindowFunctionName = windowFunctionName;
		}

		public FFTValues GetFFTValues(SongFile songFile)
		{
			FFTValues fftValues = new FFTValues(FFTFunctionName + " / " + WindowFunctionName);

			for (float i = 0; i < songFile.SongDuration; i += sampleWidthTime)
			{
				List<MyPoint> spectrum;

				if (FFTFunctionName == "MathNet")
				{
					spectrum = GetMathNetFFTValues(songFile, i, i + sampleWidthTime);
				}
				else
				{
					spectrum = GetAccordFFTValues(songFile, i, i + sampleWidthTime);
				}

				float time = i + (sampleWidthTime / 2.0f);

				fftValues.AddSpectrum(time, spectrum);
			}

			return fftValues;
		}


		private List<MyPoint> GetWindowedSamplePoints(List<MyPoint> points)
		{
			if (WindowFunctionName == "Hann") return Utils.GetHannWindowPoints(points);

			if (WindowFunctionName == "Hamming") return Utils.GetHammingWindowPoints(points);

			return points;
		}

		public List<MyPoint> GetMathNetFFTValues(SongFile songFile, double startingTime, double endTime)
		{
			List<MyPoint> songSamples = songFile.GetSamples(startingTime, endTime);

			songSamples = GetWindowedSamplePoints(songSamples);

			int nSamples = songSamples.Count;

			System.Numerics.Complex[] samplesForFFT = new System.Numerics.Complex[nSamples];

			for (int i = 0; i < nSamples; i++)
			{
				samplesForFFT[i] = new System.Numerics.Complex(songSamples[i].Y, 0);
			}

			if (nSamples <= 0) return new List<MyPoint>();

			Fourier.Forward(samplesForFFT, FourierOptions.NoScaling);

			List<MyPoint> fftPoints = new List<MyPoint>();

			float hzPerSample = songFile.SampleRate / (float) nSamples;

			int maxFrequency = Math.Min(samplesForFFT.Length / 2, 5000);

			for (int i = 1; i < maxFrequency; i++)
			{
				float mag = (2.0f / nSamples) * Utils.GetRealValue(samplesForFFT[i].Real, samplesForFFT[i].Imaginary);

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			return fftPoints;
		}

		public List<MyPoint> GetAccordFFTValues(SongFile songFile, double startingTime, double endTime)
		{
			List<MyPoint> songSamples = songFile.GetSamples(startingTime, endTime);

			int nSamples = songSamples.Count;

			nSamples = (int) Math.Pow(2, Math.Floor(Math.Log(nSamples, 2)));

			songSamples = GetWindowedSamplePoints(songSamples.GetRange(0, nSamples));

			System.Numerics.Complex[] samplesForFFT = new System.Numerics.Complex[nSamples];

			for (int i = 0; i < nSamples; i++)
			{
				samplesForFFT[i] = new System.Numerics.Complex(songSamples[i].Y, 0);
			}

			if (nSamples <= 0) return new List<MyPoint>();
			
			Accord.Math.FourierTransform.FFT(samplesForFFT, Accord.Math.FourierTransform.Direction.Forward);

			List<MyPoint> fftPoints = new List<MyPoint>();

			float hzPerSample = songFile.SampleRate / (float) nSamples;

			int maxFrequency = Math.Min(samplesForFFT.Length / 2, 5000);

			for (int i = 1; i < maxFrequency; i++)
			{
				float mag = (float) samplesForFFT[i].Magnitude;

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			return fftPoints;
		}

	}
}

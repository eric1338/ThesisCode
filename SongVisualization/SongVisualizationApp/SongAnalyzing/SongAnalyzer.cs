using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using NAudio.Wave;

using SongVisualizationApp.Util;
using SongVisualizationApp.FileReader;

namespace SongVisualizationApp.SongAnalyzing
{
	public class SongAnalyzer
	{

		private IVisualFacade visualFacade;

		public void SetVisualFacade(IVisualFacade visualFacade)
		{
			this.visualFacade = visualFacade;
		}

		public void AnalyzeSong(string fileDirectory)
		{
			SongFile songFile = MyAudioFileReader.ReadAudioFile(fileDirectory);

			visualFacade.SetSongInformation(songFile.SongName, songFile.SongDuration);

			Test2(songFile);
			//Test();
		}

		int numSamples = 1000;
		int sampleRate = 2000;

		double amplitude1 = 4;
		double amplitude2 = 2;
		double amplitude3 = 3;

		int delay2 = 0;
		int delay3 = 0;


		private void Test2(SongFile songFile)
		{
			visualFacade.SetSongFile(songFile);

			int nSamples = 10000;

			Complex[] samplesForFFT = new Complex[nSamples];

			for (int i = 0; i < nSamples; i++)
			{
				samplesForFFT[i] = new Complex(songFile.Samples[i].Y, 0);
			}

			FFTTest(samplesForFFT);
		}



		private void FFTTest(Complex[] samplesForFFT)
		{
			Fourier.Forward(samplesForFFT, FourierOptions.NoScaling);

			int nSamples = samplesForFFT.Length;

			List<MyPoint> fftPoints = new List<MyPoint>();

			double hzPerSample = sampleRate / nSamples;

			for (int i = 1; i < samplesForFFT.Length / 100; i++)
			{
				double mag = (2.0 / nSamples) * (Math.Abs(Math.Sqrt(Math.Pow(samplesForFFT[i].Real, 2) + Math.Pow(samplesForFFT[i].Imaginary, 2))));

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			visualFacade.SetFFTValues(fftPoints);
		}

		private void Test()
		{
			double[] first = Generate.Sinusoidal(numSamples, sampleRate, 60, amplitude1);
			double[] second = Generate.Sinusoidal(numSamples, sampleRate, 120, amplitude2, 0, delay2);
			double[] third = Generate.Sinusoidal(numSamples, sampleRate, 180, amplitude3, 0, delay3);

			Complex[] samples = new Complex[numSamples];
			Complex[] samplesForFFT = new Complex[numSamples];

			for (int i = 0; i < numSamples; i++)
			{
				samples[i] = new Complex(first[i] + second[i] + third[i], 0.0);
				samplesForFFT[i] = new Complex(first[i] + second[i] + third[i], 0.0);
			}

			List<MyPoint> points = new List<MyPoint>();

			int l = samples.Length;

			for (int i = 0; i < l / 5; i++)
			{
				double time = ((i + 1) / l) / 2;

				points.Add(new MyPoint(time, samples[i].Real));
			}

			//visualFacade.SetMusicValues(points);


			Fourier.Forward(samplesForFFT, FourierOptions.NoScaling);


			List<MyPoint> fftPoints = new List<MyPoint>();

			double hzPerSample = sampleRate / numSamples;

			for (int i = 1; i < samplesForFFT.Length / 10; i++)
			{
				double mag = (2.0 / numSamples) * (Math.Abs(Math.Sqrt(Math.Pow(samplesForFFT[i].Real, 2) + Math.Pow(samplesForFFT[i].Imaginary, 2))));

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			visualFacade.SetFFTValues(fftPoints);
		}

	}
}

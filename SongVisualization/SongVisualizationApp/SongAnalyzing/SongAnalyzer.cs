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

			visualFacade.SetSongFile(songFile);

			//DoFFT(songFile);
		}


		private void DoFFT(SongFile songFile)
		{
			int nSamples1 = 10000;

			Complex[] samplesForFFT = new Complex[nSamples1];

			for (int i = 0; i < nSamples1; i++)
			{
				samplesForFFT[i] = new Complex(songFile.Samples[i].Y, 0);
			}

			Fourier.Forward(samplesForFFT, FourierOptions.NoScaling);

			int nSamples = samplesForFFT.Length;

			List<MyPoint> fftPoints = new List<MyPoint>();

			// TODO: Sample Rate bestimmen
			float hzPerSample = 44100.0f / nSamples;

			for (int i = 1; i < samplesForFFT.Length / 100; i++)
			{
				float mag = (2.0f / nSamples) * ((float) Math.Abs(Math.Sqrt(Math.Pow(samplesForFFT[i].Real, 2) + Math.Pow(samplesForFFT[i].Imaginary, 2))));

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			visualFacade.SetFFTValues(fftPoints);
		}

	}
}

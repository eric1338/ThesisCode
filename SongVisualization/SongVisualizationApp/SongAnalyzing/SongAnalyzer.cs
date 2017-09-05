using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;



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
			Console.WriteLine(fileDirectory);

			SongFile songFile = new SongFile(fileDirectory);

			visualFacade.SetSongInformation(songFile.SongName, songFile.SongDuration);

			Test();
		}

		int numSamples = 1000;
		int sampleRate = 2000;

		double amplitude1 = 1;
		double amplitude2 = 2;
		double amplitude3 = 1;

		int delay2 = 0;
		int delay3 = 0;

		public void Test()
		{
			double[] first = Generate.Sinusoidal(numSamples, sampleRate, 60, amplitude1);
			double[] second = Generate.Sinusoidal(numSamples, sampleRate, 120, amplitude2, 0, delay2);
			double[] third = Generate.Sinusoidal(numSamples, sampleRate, 180, amplitude3, 0, delay3);

			Complex[] samples = new Complex[numSamples];

			for (int i = 0; i < numSamples; i++)
			{
				samples[i] = new Complex(first[i] + second[i] + third[i], 0.0);
			}

			visualFacade.SetMusicValues(new double[] { 2, 3, 4 });
			visualFacade.SetFFMValues(new double[] { 0, 1, 2 });
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

using SongVisualizationApp.Util;
using SongVisualizationApp.FileReader;
using NAudio.Dsp;
using SongVisualizationApp.SongAnalyzing.SongPropertyAnalyzers;

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
			visualFacade.SetProgress("Loading Song...", 0.0f);

			SongFile songFile = MyAudioFileReader.ReadAudioFile(fileDirectory);

			visualFacade.SetSongFile(songFile);

			List<SongPropertyValues> songPropertyValuesList = new List<SongPropertyValues>();

			SongPropertyValues fft1 = AnalyzeSongProperty(songFile, new PitchAnalyzer(), 0.4f);
			songPropertyValuesList.Add(fft1);

			SongPropertyValues amplitudeValues = AnalyzeSongProperty(songFile, new AmplitudeAnalyzer(), 0.6f);
			songPropertyValuesList.Add(amplitudeValues);

			visualFacade.SetProgress("Plotting...", 0.95f);

			visualFacade.PlotSongPropertyValues(songPropertyValuesList);

			visualFacade.SetProgress("done :)", 1);
		}


		private SongPropertyValues AnalyzeSongProperty(SongFile songFile, SongPropertyAnalyzer analyzer, float relativeProgressBefore)
		{
			visualFacade.SetProgress(analyzer.ProgressText, relativeProgressBefore);

			return analyzer.Analyze(songFile);
		}

		/*
		public static List<MyPoint> DoFFT3(SongFile songFile, double startingTime, double endTime)
		{
			List<MyPoint> songSamples = songFile.GetSamples(startingTime, endTime);

			int nSamples = songSamples.Count;

			NAudio.Dsp.Complex[] samplesForFFT = new NAudio.Dsp.Complex[nSamples];

			for (int i = 0; i < nSamples; i++)
			{
				NAudio.Dsp.Complex cpl = new NAudio.Dsp.Complex();
				cpl.X = songSamples[i].Y;
				cpl.Y = 0;

				samplesForFFT[i] = cpl;
			}

			int m = 0;
			int newNSamples = 0;

			for (int i = 0; i < 100; i++)
			{
				if (Math.Pow(2, i) < nSamples)
				{
					m = i;
					newNSamples = (int) Math.Pow(2, i);
				}
				else
				{
					break;
				}
			}
			
			NAudio.Dsp.FastFourierTransform.FFT(true, m, samplesForFFT);

			List<MyPoint> fftPoints = new List<MyPoint>();

			float hzPerSample = songFile.SampleRate / (float) nSamples;

			for (int i = 1; i < samplesForFFT.Length; i++)
			{
				float mag = (2.0f / nSamples) * Utils.GetRealValue(samplesForFFT[i].X, samplesForFFT[i].Y);
				//float mag = Utils.GetRealValue(samplesForFFT[i].X, samplesForFFT[i].Y);

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			return fftPoints;
		}
		*/




	}
}

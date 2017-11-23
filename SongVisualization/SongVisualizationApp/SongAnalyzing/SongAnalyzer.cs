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
using SongVisualizationApp.SongAnalyzing.OnSetDetection;

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

			visualFacade.SetProgress("Onsets...", 0.1f);

			// TEST

			AudioAnalysis au = new AudioAnalysis();
			au.LoadAudioFromFile(fileDirectory);

			au.DetectOnsets();

			au.NormalizeOnsets(0);

			SongPropertyValues onsetValues = new SongPropertyValues("Onsets");

			float[] onsets = au.GetOnsets();

			float timePerSample = au.GetTimePerSample();

			for (int i = 0; i < onsets.Length; i++)
			{
				float onset = onsets[i];

				onsetValues.AddPoint(timePerSample * i, onset);
			}

			//songPropertyValuesList.Add(onsetValues);



			visualFacade.SetProgress("Held Note...", 0.2f);


			AudioAnalysis2 au2 = new AudioAnalysis2();
			au2.LoadAudioFromFile(fileDirectory);

			au2.DetectOnsets();


			//songPropertyValuesList.Add(au2.CreateSongPropertyValuesTest(1));
			songPropertyValuesList.Add(au2.CreateSongPropertyValuesTest(2));
			songPropertyValuesList.Add(au2.CreateSongPropertyValuesTest(3));
			//songPropertyValuesList.Add(au2.CreateSongPropertyValuesTest(4));
			//songPropertyValuesList.Add(au2.CreateSongPropertyValuesTest(5));
			//songPropertyValuesList.Add(au2.CreateSongPropertyValuesTest(6));


			//au2.NormalizeOnsets(0);

			/*
			SongPropertyValues heldNoteValues = new SongPropertyValues("heldNote");

			float[] onsets2 = au2.GetOnsets();

			float timePerSample2 = au2.GetTimePerSample();

			for (int i = 0; i < onsets2.Length; i++)
			{
				float onset2 = onsets2[i];

				heldNoteValues.AddPoint(timePerSample2 * i, onset2);
			}

			heldNoteValues.Normalize();

			songPropertyValuesList.Add(heldNoteValues);
			*/







			// TEST ENDE



			//SongPropertyValues pitchValues = AnalyzeSongProperty(songFile, new PitchAnalyzer(), 0.4f);
			//songPropertyValuesList.Add(pitchValues);

			//SongPropertyValues pitchValues2 = AnalyzeSongProperty(songFile, new PitchAnalyzer2(), 0.5f);
			//songPropertyValuesList.Add(pitchValues2);

			List<FFTValues> fftValuesList = new List<FFTValues>();

			visualFacade.SetProgress("MathNet Pitches...", 0.2f);
			//FFTValues f1 = new PitchAnalyzer2("MathNet", "Hann").GetFFTValues(songFile);
			//FFTValues f2 = new PitchAnalyzer2("MathNet", "Hamming").GetFFTValues(songFile);
			//FFTValues f3 = new PitchAnalyzer2("MathNet", "None").GetFFTValues(songFile);

			visualFacade.SetProgress("Accord Pitches...", 0.4f);
			//FFTValues f4 = new PitchAnalyzer2("Accord", "Hann").GetFFTValues(songFile);
			//FFTValues f5 = new PitchAnalyzer2("Accord", "Hamming").GetFFTValues(songFile);
			//FFTValues f6 = new PitchAnalyzer2("Accord", "None").GetFFTValues(songFile);

			//fftValuesList.Add(f1);
			//fftValuesList.Add(f2);
			//fftValuesList.Add(f3);
			//fftValuesList.Add(f4);
			//fftValuesList.Add(f5);
			//fftValuesList.Add(f6);

			//visualFacade.PlotFFTValues(fftValuesList);

			//SongPropertyValues amplitudeValues = AnalyzeSongProperty(songFile, new AmplitudeAnalyzer(), 0.6f);
			//songPropertyValuesList.Add(amplitudeValues);

			visualFacade.SetProgress("Plotting...", 0.95f);

			visualFacade.PlotSongPropertyValues(songPropertyValuesList);

			visualFacade.SetProgress("done :)", 1);
		}

		private SongPropertyValues AnalyzeSongProperty(SongFile songFile, SongPropertyAnalyzer analyzer, float relativeProgressBefore)
		{
			visualFacade.SetProgress(analyzer.ProgressText, relativeProgressBefore);

			return analyzer.Analyze(songFile);
		}

	}
}

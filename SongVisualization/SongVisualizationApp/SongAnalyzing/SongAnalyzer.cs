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


			visualFacade.SetProgress("Analyze...", 0.1f);

			AudioAnalyzer audioAnalyzer = new AudioAnalyzer();
			audioAnalyzer.LoadAudioFromFile(fileDirectory);

			audioAnalyzer.Analyze();

			songPropertyValuesList.Add(audioAnalyzer.GetSuperOnsetThingy());

			//songPropertyValuesList.Add(audioAnalyzer.GetOnsetThingy());

			//songPropertyValuesList.Add(audioAnalyzer.GetHeldNoteThingy());

			//songPropertyValuesList.Add(audioAnalyzer.GetAmplitudeThingy());

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

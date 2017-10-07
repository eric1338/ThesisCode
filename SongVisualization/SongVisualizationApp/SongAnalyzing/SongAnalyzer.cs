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

			SongPropertyValues pitchValues = AnalyzeSongProperty(songFile, new PitchAnalyzer(), 0.4f);
			songPropertyValuesList.Add(pitchValues);

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

	}
}

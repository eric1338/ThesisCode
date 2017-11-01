using SongVisualizationApp.FileReader;
using SongVisualizationApp.SongAnalyzing;
using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp
{
	public class VisualFacade : IVisualFacade
	{

		private ISongAnalyzingFacade songAnalyzingFacade;

		private Form1 form;
		private VisualValues visualValues;

		public VisualFacade(Form1 form)
		{
			this.form = form;
			visualValues = new VisualValues();

			form.SetVisualValues(visualValues);
		}

		public void SetSongAnalyzingFacade(ISongAnalyzingFacade songAnalyzingFacade)
		{
			this.songAnalyzingFacade = songAnalyzingFacade;

			form.SetSongAnalyzingFacade(songAnalyzingFacade);
		}

		public void SetProgress(string progressText, float relativeProgress)
		{
			form.SetProgress(progressText, relativeProgress);
		}

		public void SetSongFile(SongFile songFile)
		{
			form.SetSongFile(songFile);
		}

		public void PlotFFTValues(List<FFTValues> fftValuesList)
		{
			form.SetFFTValuesList(fftValuesList);
		}

		public void PlotSongPropertyValues(List<SongPropertyValues> songPropertyValuesList)
		{
			form.PlotSongPropertyValues(songPropertyValuesList);
		}
	}
}

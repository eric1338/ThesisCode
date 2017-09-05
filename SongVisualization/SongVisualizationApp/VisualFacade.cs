using SongVisualizationApp.SongAnalyzing;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public void SetSongInformation(string songName, double songDuration)
		{
			visualValues.SongName = songName;
			visualValues.SongDuration = songDuration;

			form.UpdateEverything();
		}

		public void SetFFMValues(double[] values)
		{

		}

		public void SetMusicValues(double[] values)
		{

		}
	}
}

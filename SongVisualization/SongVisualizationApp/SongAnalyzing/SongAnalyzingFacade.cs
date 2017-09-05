using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing
{
	public class SongAnalyzingFacade : ISongAnalyzingFacade
	{

		private IVisualFacade visualFacade;

		private SongAnalyzer songAnalyzer;

		public SongAnalyzingFacade()
		{
			songAnalyzer = new SongAnalyzer();
		}

		public void SetVisualFacade(IVisualFacade visualFacade)
		{
			this.visualFacade = visualFacade;
			songAnalyzer.SetVisualFacade(visualFacade);
		}

		public void AnalyzeSong(string songFileDirectory)
		{
			songAnalyzer.AnalyzeSong(songFileDirectory);
		}
	}
}

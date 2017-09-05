using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SongVisualizationApp.SongAnalyzing;

namespace SongVisualizationApp
{
	static class Program
	{

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Form1 form = new Form1();

			VisualFacade visualFacade = new VisualFacade(form);
			SongAnalyzingFacade songAnalyzingFacade = new SongAnalyzingFacade();

			visualFacade.SetSongAnalyzingFacade(songAnalyzingFacade);
			songAnalyzingFacade.SetVisualFacade(visualFacade);

			Application.Run(form);
		}
	}
}

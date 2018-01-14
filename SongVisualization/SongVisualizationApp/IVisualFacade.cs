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
	public interface IVisualFacade
	{

		void SetProgress(string progressText, float relativeProgress);

		void SetSongFile(SongFile songFile);
		
		void PlotSongPropertyValues(List<SongPropertyValues> songPropertyValuesList);

	}
}

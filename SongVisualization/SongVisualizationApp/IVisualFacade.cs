using SongVisualizationApp.FileReader;
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

		void SetSongInformation(string songName, double songDuration);
		void SetSongFile(SongFile songFile);
		void SetFFTValues(List<MyPoint> points);

	}
}

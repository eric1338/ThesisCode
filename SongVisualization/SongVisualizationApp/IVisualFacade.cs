using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp
{
	public interface IVisualFacade
	{

		void SetSongInformation(string songName, double songDuration);
		void SetMusicValues(double[] values);
		void SetFFMValues(double[] values);

	}
}

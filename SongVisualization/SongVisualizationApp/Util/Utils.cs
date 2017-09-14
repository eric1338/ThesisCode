using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.Util
{
	class Utils
	{

		public static string GetTimeString(double secondsPassed)
		{
			int minutes = (int) Math.Floor(secondsPassed / 60);
			int seconds = (int) Math.Floor(secondsPassed % 60);

			string secondsString = seconds.ToString();

			if (seconds < 10) secondsString = "0" + secondsString;

			return minutes + ":" + secondsString;
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.Util
{
	public class MyPoint
	{

		public float X { get; set; }
		public float Y { get; set; }

		public MyPoint(float x, float y)
		{
			X = x;
			Y = y;
		}

		public MyPoint(double x, double y)
		{
			X = (float) x;
			Y = (float) y;
		}

	}
}

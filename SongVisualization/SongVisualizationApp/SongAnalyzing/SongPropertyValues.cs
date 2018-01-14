using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing
{
	public class SongPropertyValues
	{

		public string PropertyName { get; set; }

		public List<MyPoint> Points { get; set; }

		public SongPropertyValues(string propertyName)
		{
			PropertyName = propertyName;

			Points = new List<MyPoint>();
		}

		public void AddPoint(double time, double value)
		{
			Points.Add(new MyPoint(time, value));
		}
		
		public void Normalize()
		{
			float highestValue = -99999;

			foreach (MyPoint point in Points)
			{
				if (point.Y > highestValue) highestValue = point.Y;
			}

			float factor = 1.0f / highestValue;

			foreach (MyPoint point in Points)
			{
				point.Y = point.Y * factor;
			}
		}

		public void AbsAndNormalize()
		{
			float lowestValue = 99999;
			float highestValue = -99999;

			foreach (MyPoint point in Points)
			{
				if (point.Y < lowestValue) lowestValue = point.Y;

				if (point.Y > highestValue) highestValue = point.Y;
			}

			float factor = 1.0f / (highestValue - lowestValue);

			foreach (MyPoint point in Points)
			{
				point.Y = lowestValue + point.Y * factor;
			}
		}

	}
}

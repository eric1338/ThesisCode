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

		public List<Tuple<float, List<MyPoint>>> TestLists { get; set; }

		public SongPropertyValues(string propertyName)
		{
			PropertyName = propertyName;

			Points = new List<MyPoint>();

			TestLists = new List<Tuple<float, List<MyPoint>>>();
		}

		public void AddTestList(float time, List<MyPoint> points)
		{
			TestLists.Add(new Tuple<float, List<MyPoint>>(time, points));
		}

		public List<MyPoint> GetTestPoints(float time)
		{
			foreach (Tuple<float, List<MyPoint>> tupi in TestLists)
			{
				if (tupi.Item1 > time) return tupi.Item2;
			}

			return null;
		}

		public SongPropertyValues CreateNormalizedCopy(string copyName)
		{
			SongPropertyValues normalizedCopy = new SongPropertyValues(copyName);

			foreach (MyPoint point in Points)
			{
				normalizedCopy.AddPoint(point.X, point.Y);
			}

			normalizedCopy.AbsAndNormalize();

			return normalizedCopy;
		}

		public SongPropertyValues CreateThresholdCopy(string copyName, float threshold)
		{
			SongPropertyValues thresholdCopy = new SongPropertyValues(copyName);

			foreach (MyPoint point in Points)
			{
				float value = point.Y > threshold ? 1 : 0;

				thresholdCopy.AddPoint(point.X, value);
			}

			return thresholdCopy;
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
			float lowestValue = -99999;
			float highestValue = -99999;

			foreach (MyPoint point in Points)
			{
				if (point.Y < highestValue) lowestValue = point.Y;

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

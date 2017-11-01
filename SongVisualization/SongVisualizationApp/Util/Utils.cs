using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.Util
{
	class Utils
	{

		public static float GetRealValue(double real, double imaginary)
		{
			return GetRealValue((float)real, (float)imaginary);
		}

		public static float GetRealValue(float real, float imaginary)
		{
			return (float) Math.Abs(Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2)));
		}


		private static float GetHannWindowFactor(float relX)
		{
			return (float)Math.Pow(Math.Sin(Math.PI * relX), 2);
		}

		private static float GetHammingWindowFactor(float relX)
		{
			return 0.54f - 0.46f * (float)Math.Cos(2 * Math.PI * relX);
		}

		public static List<MyPoint> GetHannWindowPoints(List<MyPoint> points)
		{
			List<MyPoint> newPoints = new List<MyPoint>();

			float firstX = points[0].X;
			float xRange = points[points.Count - 1].X - firstX;

			foreach (MyPoint point in points)
			{
				float relX = (point.X - firstX) / xRange;

				newPoints.Add(new MyPoint(point.X, GetHannWindowFactor(relX) * point.Y));
			}

			return newPoints;
		}

		public static List<MyPoint> GetHammingWindowPoints(List<MyPoint> points)
		{
			List<MyPoint> newPoints = new List<MyPoint>();

			float firstX = points[0].X;
			float xRange = points[points.Count - 1].X - firstX;

			foreach (MyPoint point in points)
			{
				float relX = (point.X - firstX) / xRange;

				newPoints.Add(new MyPoint(point.X, GetHammingWindowFactor(relX) * point.Y));
			}

			return newPoints;
		}


		public static string GetTimeString(double secondsPassed)
		{
			int minutes = (int) Math.Floor(secondsPassed / 60);
			int seconds = (int) Math.Floor(secondsPassed % 60);

			string secondsString = seconds.ToString();

			if (seconds < 10) secondsString = "0" + secondsString;

			return minutes + ":" + secondsString;
		}

		public static List<List<MyPoint>> GetSubsets(List<MyPoint> samples, int subsetSize)
		{
			List<List<MyPoint>> subsets = new List<List<MyPoint>>();

			int nSubsets = (int) Math.Floor(samples.Count / subsetSize + 0.0);

			for (int i = 0; i < nSubsets; i++)
			{
				List<MyPoint> subset = new List<MyPoint>();

				for (int j = 0; j < subsetSize; j++)
				{
					subset.Add(samples[i * subsetSize + j]);
				}

				subsets.Add(subset);
			}

			return subsets;
		}


	}
}

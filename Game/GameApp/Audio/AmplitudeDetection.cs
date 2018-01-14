using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class AmplitudeDetection
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
		}

		public List<MyPoint> Points = new List<MyPoint>();

		public AmplitudeDetection()
		{

		}

		public void AnalyzeSpectrum(float[] samples, float time)
		{
			float maxValue = -1;

			foreach (float sample in samples)
			{
				if (sample > maxValue) maxValue = sample;
			}

			Points.Add(new MyPoint(time, maxValue));
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

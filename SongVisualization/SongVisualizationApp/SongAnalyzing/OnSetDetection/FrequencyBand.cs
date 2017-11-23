using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class FrequencyBand
	{

		public List<MyPoint> Points { get; set; }

		public int LeftFrequencyMargin { get; set; }
		public int RightFrequencyMargin { get; set; }

		public FrequencyBand(int leftFrequencyMargin, int rightFrequencyMargin)
		{
			LeftFrequencyMargin = leftFrequencyMargin;
			RightFrequencyMargin = rightFrequencyMargin;

			Points = new List<MyPoint>();
		}

		public bool IsFrequencyInBand(float frequency)
		{
			return LeftFrequencyMargin < frequency && frequency < RightFrequencyMargin;
		}

		public void AddFrequency(float time, float frequency)
		{
			Points.Add(new MyPoint(time, frequency));
		}

	}
}

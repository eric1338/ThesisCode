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

		public List<int> SpectrumBands { get; set; }

		public FrequencyBand()
		{
			SpectrumBands = new List<int>();

			Points = new List<MyPoint>();
		}

		public FrequencyBand(int leftFrequencyMargin, int rightFrequencyMargin)
		{
			LeftFrequencyMargin = leftFrequencyMargin;
			RightFrequencyMargin = rightFrequencyMargin;

			SpectrumBands = new List<int>();

			Points = new List<MyPoint>();
		}

		public void AddSpectrumBand(int spectrumBand)
		{
			SpectrumBands.Add(spectrumBand);
		}

		public void AddFrequency(float time, float frequency)
		{
			Points.Add(new MyPoint(time, frequency));
		}

	}
}

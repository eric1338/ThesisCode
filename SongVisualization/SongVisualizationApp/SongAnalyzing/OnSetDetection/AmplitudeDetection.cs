using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class AmplitudeDetection
	{

		public SongPropertyValues Values { get; set; }

		public AmplitudeDetection()
		{
			Values = new SongPropertyValues("Amplitude");
		}

		public void Analyze(float[] samples, float time)
		{
			float maxValue = -1;

			foreach (float sample in samples)
			{
				if (sample > maxValue) maxValue = sample;
			}

			Values.AddPoint(time, maxValue);
		}
	}
}

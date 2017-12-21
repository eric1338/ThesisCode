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
			float value = -1;

			foreach (float sample in samples)
			{
				if (sample > value) value = sample;
			}

			Values.AddPoint(time, value);
		}
	}
}

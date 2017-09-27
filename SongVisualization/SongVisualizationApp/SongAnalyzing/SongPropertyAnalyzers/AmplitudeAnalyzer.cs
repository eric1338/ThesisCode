using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongVisualizationApp.FileReader;
using SongVisualizationApp.Util;

namespace SongVisualizationApp.SongAnalyzing.SongPropertyAnalyzers
{
	class AmplitudeAnalyzer : SongPropertyAnalyzer
	{

		public AmplitudeAnalyzer()
		{
			PropertyName = "Amplitude";
			ProgressText = "Determining Amplitude...";
		}

		public override SongPropertyValues Analyze(SongFile songFile)
		{
			SongPropertyValues amplitudeValues = new SongPropertyValues("Amplitude");

			int n = 0;
			float tempMaxValue = -999;

			foreach (MyPoint sample in songFile.Samples)
			{
				n++;

				tempMaxValue = Math.Max(tempMaxValue, Math.Abs(sample.Y));

				if (n > 4000)
				{
					amplitudeValues.AddPoint(sample.X, tempMaxValue);

					n = 0;
					tempMaxValue = -999;
				}
			}

			//foreach (MyPoint sample in samples)
			//{
			//if (x++ % 100 == 0) amplitudeValues.AddPoint(sample.X, Math.Abs(sample.Y));
			//}

			return amplitudeValues;
		}

	}
}

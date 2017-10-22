﻿using System;
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

		private int pointsPerSecond = 10;

		public AmplitudeAnalyzer()
		{
			PropertyName = "Amplitude";
			ProgressText = "Determining Amplitude...";
		}

		public override SongPropertyValues Analyze(SongFile songFile)
		{
			SongPropertyValues amplitudeValues = new SongPropertyValues("Amplitude");

			int n = 0;
			float tempMaxValue = -1;

			float threshold = songFile.SampleRate * (1.0f / pointsPerSecond);

			foreach (MyPoint sample in songFile.Samples)
			{
				n++;

				tempMaxValue = Math.Max(tempMaxValue, Math.Abs(sample.Y));

				if (n > threshold)
				{
					amplitudeValues.AddPoint(sample.X, tempMaxValue);

					n = 0;
					tempMaxValue = -1;
				}
			}

			return amplitudeValues;
		}

	}
}
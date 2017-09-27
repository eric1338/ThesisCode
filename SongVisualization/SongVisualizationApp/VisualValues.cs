using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp
{
	public class VisualValues
	{
		
		public double SongDuration { get; private set; }

		private double timeCenter;

		public double LeftTimeMargin { get; set; }
		public double RightTimeMargin { get; set; }

		public double SecondsDisplayed { get; private set; }

		private double[] secondsDisplayedSteps;

		public VisualValues()
		{
			SongDuration = 10;
			timeCenter = 5;

			LeftTimeMargin = 0;
			RightTimeMargin = 0;

			SecondsDisplayed = 10;

			secondsDisplayedSteps = new double[8];
			secondsDisplayedSteps[0] = 1;
			secondsDisplayedSteps[1] = 2;
			secondsDisplayedSteps[2] = 5;
			secondsDisplayedSteps[3] = 10;
			secondsDisplayedSteps[4] = 20;
			secondsDisplayedSteps[5] = 40;
			secondsDisplayedSteps[6] = 80;
			secondsDisplayedSteps[7] = 240;
		}

		public void UpdateTimeMargins()
		{
			LeftTimeMargin = Clamp(timeCenter - (SecondsDisplayed / 2), 0, SongDuration - SecondsDisplayed);

			RightTimeMargin = Clamp(LeftTimeMargin + SecondsDisplayed, LeftTimeMargin + SecondsDisplayed, SongDuration);

			timeCenter = LeftTimeMargin + ((RightTimeMargin - LeftTimeMargin) / 2);
		}

		private double Clamp(double value, double min, double max)
		{
			return Math.Max(Math.Min(value, max), min);
		}

		public void SetSongDuration(double songDuration)
		{
			SongDuration = songDuration;
			secondsDisplayedSteps[7] = songDuration;
		}

		public string GetSongDurationString()
		{
			return Utils.GetTimeString(SongDuration);
		}

		public string GetLeftTimeMarginString()
		{
			return Utils.GetTimeString(LeftTimeMargin);
		}

		public string GetRightTimeMarginString()
		{
			return Utils.GetTimeString(RightTimeMargin);
		}

		public void SetSecondsDisplayed(int trackBarIndex)
		{
			if (trackBarIndex >= secondsDisplayedSteps.Length) return;

			SecondsDisplayed = secondsDisplayedSteps[trackBarIndex];

			UpdateTimeMargins();
		}

		public void SetTimeCenter(double percentage)
		{
			timeCenter = percentage * SongDuration;

			UpdateTimeMargins();
		}

	}
}

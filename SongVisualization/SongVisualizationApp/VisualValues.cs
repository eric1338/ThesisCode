using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp
{
	public class VisualValues
	{

		public string SongName { get; set; }

		public double SongDuration { get; set; }

		public double TimeCenter { get; set; }

		private double leftTimeMargin;
		private double rightTimeMargin;

		public double SecondsDisplayed { get; set; }


		public VisualValues()
		{
			SongName = "no song selected";
			SongDuration = 10;
			TimeCenter = 5;

			leftTimeMargin = 0;
			rightTimeMargin = 0;

			SecondsDisplayed = 10;
		}


		public void UpdateTimeMargins()
		{
			leftTimeMargin = Clamp(TimeCenter - (SecondsDisplayed / 2), 0, SongDuration - SecondsDisplayed);

			rightTimeMargin = Clamp(leftTimeMargin + SecondsDisplayed, leftTimeMargin + SecondsDisplayed, SongDuration);

			TimeCenter = leftTimeMargin + ((rightTimeMargin - leftTimeMargin) / 2);
		}

		private double Clamp(double value, double min, double max)
		{
			return Math.Max(Math.Min(value, max), min);
		}

		public string GetSongDurationString()
		{
			return GetTimeString(SongDuration);
		}

		public string GetLeftTimeMarginString()
		{
			return GetTimeString(leftTimeMargin);
		}

		public string GetRightTimeMarginString()
		{
			return GetTimeString(rightTimeMargin);
		}

		public void SetSecondsDisplayed(string secondsDisplayedComboBoxText)
		{
			SecondsDisplayed = GetSecondsDisplayed(secondsDisplayedComboBoxText);

			UpdateTimeMargins();
		}

		private double GetSecondsDisplayed(string secondsDisplayedComboBoxText)
		{
			if (secondsDisplayedComboBoxText == "all") return SongDuration;
			if (secondsDisplayedComboBoxText == "5 s") return 5;
			if (secondsDisplayedComboBoxText == "10 s") return 10;
			if (secondsDisplayedComboBoxText == "20 s") return 20;
			if (secondsDisplayedComboBoxText == "60 s") return 60;

			return 10;
		}

		public void SetTimeCenter(double percentage)
		{
			TimeCenter = percentage * SongDuration;

			UpdateTimeMargins();
		}

		public double GetZoomRatio()
		{
			if (SongDuration <= 0) return 1;

			return SecondsDisplayed / SongDuration;
		}

		private string GetTimeString(double time)
		{
			int minutes = (int) Math.Floor(time / 60);
			int seconds = (int) Math.Floor(time % 60);

			string secondsString = seconds.ToString();

			if (seconds < 10) secondsString = "0" + secondsString;

			return minutes + ":" + secondsString;
		}

	}
}

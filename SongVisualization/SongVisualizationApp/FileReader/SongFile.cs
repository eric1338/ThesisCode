using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.FileReader
{
	public class SongFile
	{

		private string fileDirectory;

		public string SongName { get; set; }
		public double SongDuration { get; set; }

		public int BitDepth { get; set; }
		public int SampleRate { get; set; }

		public List<MyPoint> Samples { get; set; }


		public SongFile(string fileDirectory)
		{
			this.fileDirectory = fileDirectory;

			SongName = GetSongName();
		}

		private string GetSongName()
		{
			string[] pieces = fileDirectory.Split('\\');

			if (pieces.Length < 2) return fileDirectory;

			return pieces[pieces.Length - 1];
		}

		public List<MyPoint> GetSamples(double startingTime, double endTime)
		{
			int startingIndex = (int) Math.Floor(startingTime * SampleRate);

			int range = (int)Math.Floor((endTime - startingTime) * SampleRate);

			startingIndex = Math.Min(startingIndex, Samples.Count - 1);
			range = Math.Min(range, Samples.Count - startingIndex - 1);

			return Samples.GetRange(startingIndex, range);
		}

	}
}

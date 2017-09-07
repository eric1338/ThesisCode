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

		public string FileDirectory { get; set; }
		public FileType FileType { get; set; }

		public string SongName { get; set; }
		public double SongDuration { get; set; }

		public int BitDepth { get; set; }
		public int SampleRate { get; set; }

		public List<MyPoint> Samples { get; set; }


		public SongFile(string fileDirectory)
		{
			FileDirectory = fileDirectory;
		}

		public List<MyPoint> GetSamples(double startingTime, double endTime)
		{
			int startingIndex = (int) Math.Floor(startingTime * SampleRate);

			int range = (int) Math.Floor((endTime - startingTime) * SampleRate);

			startingIndex = Math.Min(startingIndex, Samples.Count - 1);
			range = Math.Min(range, Samples.Count - startingIndex - 1);

			return Samples.GetRange(startingIndex, range);
		}

	}
}

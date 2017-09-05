using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing
{
	class SongFile
	{

		private string fileDirectory;

		public string SongName { get; set; }
		public double SongDuration { get; set; }

		public SongFile(string fileDirectory)
		{
			this.fileDirectory = fileDirectory;

			SongName = GetSongName();

			SongDuration = 200;
		}

		private string GetSongName()
		{
			string[] pieces = fileDirectory.Split('\\');

			if (pieces.Length < 2) return fileDirectory;

			return pieces[pieces.Length - 1];
		}

	}
}

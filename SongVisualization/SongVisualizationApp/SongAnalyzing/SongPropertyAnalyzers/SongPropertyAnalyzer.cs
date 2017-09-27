using SongVisualizationApp.FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing.SongPropertyAnalyzers
{
	abstract class SongPropertyAnalyzer
	{

		public string PropertyName { get; set; }
		public string ProgressText { get; set; }

		public abstract SongPropertyValues Analyze(SongFile songFile);

	}
}

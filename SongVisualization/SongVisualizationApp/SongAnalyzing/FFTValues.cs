using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing
{
	public class FFTValues
	{

		public string PropertyName { get; set; }

		private List<Tuple<float, List<MyPoint>>> sprectrums;

		public FFTValues(string propertyName)
		{
			PropertyName = propertyName;

			sprectrums = new List<Tuple<float, List<MyPoint>>>();
		}

		public void AddSpectrum(float time, List<MyPoint> spectrum)
		{
			sprectrums.Add(new Tuple<float, List<MyPoint>>(time, spectrum));
		}

		public List<MyPoint> GetSpectrum(float time)
		{
			foreach (Tuple<float, List<MyPoint>> spectrum in sprectrums)
			{
				if (spectrum.Item1 > time) return spectrum.Item2;
			}

			return null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class SingleBeat
	{
		public float Time { get; set; }
		public float Applicability { get; set; }

		public SingleBeat(float time, float applicability)
		{
			Time = time;
			Applicability = applicability;
		}
	}
}

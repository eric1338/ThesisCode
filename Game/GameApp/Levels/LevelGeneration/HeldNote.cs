using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class HeldNote
	{
		public float StartTime { get; set; }
		public float EndTime { get; set; }
		public float Applicability { get; set; }

		public HeldNote(float startingTime, float endTime, float applicability)
		{
			StartTime = startingTime;
			EndTime = endTime;
			Applicability = applicability;
		}
	}
}

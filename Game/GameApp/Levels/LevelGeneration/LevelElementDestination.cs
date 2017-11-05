using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelElementDestination
	{
		public LevelElementType Type { get; set; }

		public float StartingTime { get; set; }
		public float Duration { get; set; }

		public LevelElementDestination(LevelElementType type, float startingTime, float duration)
		{
			Type = type;
			StartingTime = startingTime;
			Duration = duration;
		}
	}
}

using GameApp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay
{
	class LevelAttempt
	{

		public Level Level { get; set; }
		public LevelProgression LevelProgression { get; set; }

		public LevelAttempt(Level level)
		{
			Level = level;
			LevelProgression = new LevelProgression(level);
		}

	}
}

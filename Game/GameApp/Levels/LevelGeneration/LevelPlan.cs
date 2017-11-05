using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelPlan
	{
		
		public List<LevelElementDestination> LevelElementDestinations { get; set; }


		public LevelPlan()
		{
			LevelElementDestinations = new List<LevelElementDestination>();
		}

		public void AddLevelElementDestination(LevelElementType type, float startingTime, float duration)
		{
			LevelElementDestinations.Add(new LevelElementDestination(type, startingTime, duration));
		}


		public static LevelPlan GetTestLevelPlan()
		{
			LevelPlan levelPlan = new LevelPlan();

			levelPlan.AddLevelElementDestination(LevelElementType.DuckObstacle, 5, 3);

			levelPlan.AddLevelElementDestination(LevelElementType.Chasm, 10, 1.3f);

			levelPlan.AddLevelElementDestination(LevelElementType.Chasm, 16, 1.6f);

			levelPlan.AddLevelElementDestination(LevelElementType.DuckObstacle, 21, 1.5f);

			return levelPlan;
		}


		public List<LevelElementDestination> GetChasms()
		{
			List<LevelElementDestination> chasms = new List<LevelElementDestination>();

			foreach (LevelElementDestination levelElementDestination in LevelElementDestinations)
			{
				if (levelElementDestination.Type == LevelElementType.Chasm) chasms.Add(levelElementDestination);
			}

			return chasms;
		}

		public List<LevelElementDestination> GetNonChasms()
		{
			List<LevelElementDestination> nonChasms = new List<LevelElementDestination>();

			foreach (LevelElementDestination levelElementDestination in LevelElementDestinations)
			{
				if (levelElementDestination.Type != LevelElementType.Chasm) nonChasms.Add(levelElementDestination);
			}

			return nonChasms;
		}


	}
}

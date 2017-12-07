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




		private void CreateSingleSynchronisation(LevelElementType type, float synchronisationTime)
		{
			LevelElementDestination destination;

			if (type == LevelElementType.JumpObstacle)
			{
				destination = LevelElementDestination.CreateJumpObstacle(synchronisationTime);
			}
			else if (type == LevelElementType.HighCollectible)
			{
				destination = LevelElementDestination.CreateHighCollectible(synchronisationTime);
			}
			else
			{
				destination = LevelElementDestination.CreateSingleProjectile(synchronisationTime);
			}

			AddLevelElementDestination(destination);
		}

		private void CreateProlongedSynchronisation(LevelElementType type,
			float synchronisationStartingTime, float synchronisationEndTime)
		{
			LevelElementDestination destination;

			if (type == LevelElementType.Chasm)
			{
				destination = LevelElementDestination.CreateChasm(synchronisationStartingTime,
					synchronisationEndTime);
			}
			else
			{
				destination = LevelElementDestination.CreateDuckingObstacle(synchronisationStartingTime,
					synchronisationEndTime);
			}

			AddLevelElementDestination(destination);
		}



		public void AddLevelElementDestination(LevelElementDestination destination)
		{
			LevelElementDestinations.Add(destination);
		}

		public void AddLevelElementDestination(LevelElementType type, float startingTime)
		{
			CreateSingleSynchronisation(type, startingTime);
			//LevelElementDestinations.Add(new LevelElementDestination(type, startingTime, 0));
		}

		public void AddLevelElementDestination(LevelElementType type, float startingTime, float duration)
		{
			CreateProlongedSynchronisation(type, startingTime, startingTime + duration);
			//LevelElementDestinations.Add(new LevelElementDestination(type, startingTime, duration));
		}


		public static LevelPlan GetTestLevelPlan()
		{
			LevelPlan levelPlan = new LevelPlan();

			//levelPlan.AddLevelElementDestination(LevelElementType.DuckObstacle, 5, 3);

			levelPlan.AddLevelElementDestination(LevelElementType.HighCollectible, 4);

			levelPlan.AddLevelElementDestination(LevelElementType.Projectile, 6);
			levelPlan.AddLevelElementDestination(LevelElementType.Projectile, 7);

			levelPlan.AddLevelElementDestination(LevelElementType.Chasm, 10, 1.3f);

			levelPlan.AddLevelElementDestination(LevelElementType.HighCollectible, 13);

			levelPlan.AddLevelElementDestination(LevelElementType.JumpObstacle, 14);

			levelPlan.AddLevelElementDestination(LevelElementType.Projectile, 19f);

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

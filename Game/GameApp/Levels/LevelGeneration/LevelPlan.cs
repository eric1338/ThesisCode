using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelPlan
	{
		
		public List<LevelElementPlacement> LevelElementPlacements { get; set; }


		public LevelPlan()
		{
			LevelElementPlacements = new List<LevelElementPlacement>();
		}


		public void SortByTime()
		{
			LevelElementPlacements.Sort(delegate (LevelElementPlacement p1, LevelElementPlacement p2)
			{
				return p1.LevelElementStartTime.CompareTo(p2.LevelElementStartTime);
			});
		}

		private void CreateSingleSynchronisation(LevelElementType type, float synchronisationTime)
		{
			AddLevelElementPlacement(LevelElementPlacement.CreateSingleSynchro(type, synchronisationTime));
		}

		private void CreateProlongedSynchronisation(LevelElementType type,
			float synchroStartTime, float synchroEndTime)
		{
			AddLevelElementPlacement(LevelElementPlacement.CreateProlongedSynchro(type, synchroStartTime, synchroEndTime));
		}

		public void AddLevelElementPlacement(LevelElementPlacement destination)
		{
			LevelElementPlacements.Add(destination);
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

			levelPlan.AddLevelElementDestination(LevelElementType.LowCollectible, 4);
			levelPlan.AddLevelElementDestination(LevelElementType.LowCollectible, 9);

			//levelPlan.AddLevelElementDestination(LevelElementType.DuckObstacle, 5, 3);

			//levelPlan.AddLevelElementDestination(LevelElementType.HighCollectible, 4);

			levelPlan.AddLevelElementDestination(LevelElementType.SingleProjectile, 6);
			levelPlan.AddLevelElementDestination(LevelElementType.SingleProjectile, 7);

			//levelPlan.AddLevelElementDestination(LevelElementType.Chasm, 10, 1.3f);

			//levelPlan.AddLevelElementDestination(LevelElementType.HighCollectible, 13);

			//levelPlan.AddLevelElementDestination(LevelElementType.JumpObstacle, 14);

			//levelPlan.AddLevelElementDestination(LevelElementType.SingleProjectile, 19f);

			//levelPlan.AddLevelElementDestination(LevelElementType.Chasm, 16, 1.6f);

			//levelPlan.AddLevelElementDestination(LevelElementType.DuckObstacle, 21, 1.5f);

			return levelPlan;
		}


		private bool IsLevelElementTypeAChasm(LevelElementType type)
		{
			return type == LevelElementType.Chasm || type == LevelElementType.ChasmWithCollectibles;
		}

		public List<LevelElementPlacement> GetChasms()
		{
			List<LevelElementPlacement> chasms = new List<LevelElementPlacement>();

			foreach (LevelElementPlacement placement in LevelElementPlacements)
			{
				if (IsLevelElementTypeAChasm(placement.Type)) chasms.Add(placement);
			}

			return chasms;
		}

		public List<LevelElementPlacement> GetNonChasms()
		{
			List<LevelElementPlacement> nonChasms = new List<LevelElementPlacement>();

			foreach (LevelElementPlacement placement in LevelElementPlacements)
			{
				if (!IsLevelElementTypeAChasm(placement.Type)) nonChasms.Add(placement);
			}

			return nonChasms;
		}


	}
}

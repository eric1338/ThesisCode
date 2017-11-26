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

		public float LevelElementStartTime { get; set; }
		public float LevelElementEndTime { get; set; }

		public float SynchronistationStartingTime { get; set; }
		public float SynchronistationEndTime { get; set; }

		public List<float> CollectibleTimes { get; set; }
		public List<float> ProjectileTimes { get; set; }

		public LevelElementDestination(LevelElementType type)
		{
			Type = type;

			CollectibleTimes = new List<float>();
			ProjectileTimes = new List<float>();
		}

		private static LevelElementDestination CreateSingleSynchronisation(LevelElementType type, float synchronisationTime)
		{
			LevelElementDestination destination = new LevelElementDestination(type);

			destination.SynchronistationStartingTime = synchronisationTime;
			destination.SynchronistationEndTime = synchronisationTime;

			return destination;
		}

		private static LevelElementDestination CreateProlongedSynchronisation(LevelElementType type,
			float synchronisationStartingTime, float synchronisationEndTime)
		{
			LevelElementDestination destination = new LevelElementDestination(type);

			destination.SynchronistationStartingTime = synchronisationStartingTime;
			destination.SynchronistationEndTime = synchronisationEndTime;

			return destination;
		}

		public static LevelElementDestination CreateChasm(float synchronisationStartingTime, float synchronisationEndTime)
		{
			LevelElementDestination destination = CreateProlongedSynchronisation(LevelElementType.Chasm,
				synchronisationStartingTime, synchronisationEndTime);

			destination.SetLevelElementTimesEqualToSynchronisationTimes();

			return destination;
		}

		public static LevelElementDestination CreateJumpObstacle(float synchronisationTime)
		{
			LevelElementDestination destination = CreateSingleSynchronisation(LevelElementType.JumpObstacle, synchronisationTime);

			destination.LevelElementStartTime = synchronisationTime;
			destination.LevelElementEndTime = synchronisationTime + LevelGenerationValues.GetJumpDuration();

			return destination;
		}

		public static LevelElementDestination CreateDuckingObstacle(float synchronisationStartingTime, float synchronisationEndTime)
		{
			LevelElementDestination destination = CreateProlongedSynchronisation(LevelElementType.DuckObstacle,
				synchronisationStartingTime, synchronisationEndTime);

			destination.SynchronistationStartingTime = synchronisationStartingTime;
			destination.SynchronistationEndTime = synchronisationEndTime;

			destination.SetLevelElementTimesEqualToSynchronisationTimes();

			return destination;
		}

		public static LevelElementDestination CreateSingleProjectile(float synchronisationTime)
		{
			LevelElementDestination destination = CreateSingleSynchronisation(LevelElementType.Projectile, synchronisationTime);

			destination.LevelElementStartTime = synchronisationTime - LevelGenerationValues.ProjectileSafetyTime;
			destination.LevelElementEndTime = synchronisationTime;

			return destination;
		}

		public static LevelElementDestination CreateHighCollectible(float synchronisationTime)
		{
			LevelElementDestination destination = CreateSingleSynchronisation(LevelElementType.HighCollectible, synchronisationTime);

			float halfJumpDuration = LevelGenerationValues.GetJumpDuration() / 2.0f;

			destination.LevelElementStartTime = synchronisationTime - halfJumpDuration;
			destination.LevelElementEndTime = synchronisationTime + halfJumpDuration;

			return destination;
		}






		public float GetSynchronisationDuration()
		{
			return SynchronistationEndTime - SynchronistationStartingTime;
		}

		private void SetLevelElementTimesEqualToSynchronisationTimes()
		{
			LevelElementStartTime = SynchronistationStartingTime;
			LevelElementEndTime = SynchronistationEndTime;
		}



	}
}

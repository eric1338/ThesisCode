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

		public float SynchroStartTime { get; set; }
		public float SynchroEndTime { get; set; }

		public LevelElementDestination(LevelElementType type)
		{
			Type = type;
		}


		public static LevelElementDestination CreateSingleSynchro(LevelElementType type, float synchroTime)
		{
			LevelElementDestination destination = new LevelElementDestination(type);

			destination.SynchroStartTime = synchroTime;
			destination.SynchroEndTime = synchroTime;

			return destination;
		}

		public static LevelElementDestination CreateProlongedSynchro(LevelElementType type,
			float synchroStartTime, float synchroEndTime)
		{
			if (type == LevelElementType.Chasm) return CreateChasm(synchroStartTime, synchroEndTime);

			return CreateDuckingObstacle(synchroStartTime, synchroEndTime);
		}

		private static LevelElementDestination CreateSingleSynchroBase(LevelElementType type, float synchroTime)
		{
			LevelElementDestination destination = new LevelElementDestination(type);

			destination.SynchroStartTime = synchroTime;
			destination.SynchroEndTime = synchroTime;

			return destination;
		}

		private static LevelElementDestination CreateProlongedSynchroBase(LevelElementType type,
			float synchroStartTime, float synchroEndTime)
		{
			LevelElementDestination destination = new LevelElementDestination(type);

			destination.SynchroStartTime = synchroStartTime;
			destination.SynchroEndTime = synchroEndTime;

			return destination;
		}

		public static LevelElementDestination CreateChasm(float synchroTime, float synchroEndTime)
		{
			LevelElementDestination destination = CreateProlongedSynchroBase(LevelElementType.Chasm,
				synchroTime, synchroEndTime);

			destination.SetLevelElementTimesEqualToSynchroTimes();

			return destination;
		}

		public static LevelElementDestination CreateJumpObstacle(float synchroTime)
		{
			LevelElementDestination destination = CreateSingleSynchroBase(LevelElementType.JumpObstacle, synchroTime);

			destination.LevelElementStartTime = synchroTime;
			destination.LevelElementEndTime = synchroTime + LevelGenerationValues.GetJumpDuration();

			return destination;
		}

		public static LevelElementDestination CreateDuckingObstacle(float synchroStartTime, float synchroEndTime)
		{
			LevelElementDestination destination = CreateProlongedSynchroBase(LevelElementType.DuckObstacle,
				synchroStartTime, synchroEndTime);

			destination.SynchroStartTime = synchroStartTime;
			destination.SynchroEndTime = synchroEndTime;

			destination.SetLevelElementTimesEqualToSynchroTimes();

			return destination;
		}

		public static LevelElementDestination CreateSingleProjectile(float synchroTime)
		{
			LevelElementDestination destination = CreateSingleSynchroBase(LevelElementType.Projectile, synchroTime);

			destination.LevelElementStartTime = synchroTime - LevelGenerationValues.ProjectileSafetyTime;
			destination.LevelElementEndTime = synchroTime;

			return destination;
		}

		public static LevelElementDestination CreateHighCollectible(float synchroTime)
		{
			LevelElementDestination destination = CreateSingleSynchroBase(LevelElementType.HighCollectible, synchroTime);

			float halfJumpDuration = LevelGenerationValues.GetJumpDuration() / 2.0f;

			destination.LevelElementStartTime = synchroTime - halfJumpDuration;
			destination.LevelElementEndTime = synchroTime + halfJumpDuration;

			return destination;
		}


		private void SetLevelElementTimesEqualToSynchroTimes()
		{
			LevelElementStartTime = SynchroStartTime;
			LevelElementEndTime = SynchroEndTime;
		}

		public float GetSynchroDuration()
		{
			return SynchroEndTime - SynchroStartTime;
		}



	}
}

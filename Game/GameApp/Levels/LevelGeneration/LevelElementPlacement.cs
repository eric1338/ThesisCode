using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelElementPlacement
	{
		public LevelElementType Type { get; set; }

		public float LevelElementStartTime { get; set; }
		public float LevelElementEndTime { get; set; }

		public float SynchroStartTime { get; set; }
		public float SynchroEndTime { get; set; }

		public List<float> SynchroTimes { get; set; }

		public LevelElementPlacement(LevelElementType type)
		{
			Type = type;

			SynchroTimes = new List<float>();
		}

		public float GetSynchroDuration()
		{
			return SynchroEndTime - SynchroStartTime;
		}

		public static LevelElementPlacement CreateSingleSynchro(LevelElementType type, float synchroTime)
		{
			LevelElementPlacement placement = new LevelElementPlacement(type);

			placement.SynchroStartTime = synchroTime;
			placement.SynchroEndTime = synchroTime;

			if (type == LevelElementType.JumpObstacle)
			{
				placement.LevelElementStartTime = synchroTime;
				placement.LevelElementEndTime = synchroTime + LevelGenerationValues.GetPlainJumpDuration();
			}
			if (type == LevelElementType.HighCollectible)
			{
				float halfJumpDuration = LevelGenerationValues.GetPlainJumpDuration() / 2.0f;

				placement.LevelElementStartTime = synchroTime - halfJumpDuration;
				placement.LevelElementEndTime = synchroTime + halfJumpDuration;
			}
			if (type == LevelElementType.SingleProjectile)
			{
				placement.LevelElementStartTime = synchroTime - LevelGenerationValues.ProjectileSafetyTime;
				placement.LevelElementEndTime = synchroTime;
			}

			return placement;
		}

		public static LevelElementPlacement CreateMultipleSynchro(LevelElementType type, List<float> synchroTimes)
		{
			LevelElementPlacement placement = new LevelElementPlacement(type);

			placement.SynchroStartTime = synchroTimes[0];
			placement.SynchroEndTime = synchroTimes[synchroTimes.Count - 1];

			placement.SynchroTimes = synchroTimes;

			if (type == LevelElementType.ChasmWithCollectibles)
			{
				placement.LevelElementStartTime = synchroTimes[0] - LevelGenerationValues.TimeBeforeFirstChasmCollectible;
				placement.LevelElementEndTime = synchroTimes[synchroTimes.Count - 1] +
					LevelGenerationValues.TimeAfterLastChasmCollectible;
			}
			if (type == LevelElementType.MultipleProjectiles)
			{
				placement.LevelElementStartTime = synchroTimes[0] - LevelGenerationValues.ProjectileSafetyTime;
				placement.LevelElementEndTime = synchroTimes[synchroTimes.Count - 1];
			}

			return placement;
		}

		public static LevelElementPlacement CreateProlongedSynchro(LevelElementType type,
			float synchroStartTime, float synchroEndTime)
		{
			LevelElementPlacement placement = new LevelElementPlacement(type);

			placement.SynchroStartTime = synchroStartTime;
			placement.SynchroEndTime = synchroEndTime;

			if (type == LevelElementType.Chasm)
			{
				placement.SetLevelElementTimesEqualToSynchroTimes();
			}
			if (type == LevelElementType.DuckObstacle)
			{
				placement.SetLevelElementTimesEqualToSynchroTimes();
			}

			return placement;
		}

		private void SetLevelElementTimesEqualToSynchroTimes()
		{
			LevelElementStartTime = SynchroStartTime;
			LevelElementEndTime = SynchroEndTime;
		}

	}
}

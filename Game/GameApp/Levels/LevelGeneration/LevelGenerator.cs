using GameApp.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelGenerator
	{

		public static Level GenerateLevel(SongElements songElements)
		{
			LevelPlanCreator levelPlanCreator = new LevelPlanCreator(songElements);

			levelPlanCreator.CreateLevelPlan();

			LevelPlan levelPlan = levelPlanCreator.LevelPlan;

			LevelGenerator levelGenerator = new LevelGenerator();

			Level level = levelGenerator.GenerateLevel(levelPlan);

			return level;
		}

		public Level GenerateLevel(LevelPlan levelPlan)
		{
			Level level = new Level();

			level.PlayerStartingPosition = LevelGenerationValues.PlayerStartPosition;

			level.GoalLineX = 99999;

			List<LevelElementPlacement> chasmsPlacements = levelPlan.GetChasms();
			List<LevelElementPlacement> nonChasmsPlacements = levelPlan.GetNonChasms();

			float currentLeftX = LevelGenerationValues.FirstGroundLeftX;
			float currentY = LevelGenerationValues.PlayerStartPosition.Y;

			foreach (LevelElementPlacement placement in chasmsPlacements)
			{
				float currentRightX = LevelGenerationValues.GetXPositionByTime(placement.LevelElementStartTime +
					LevelGenerationValues.SuggestedChasmJumpTimingOffset);

				level.AddGround(new Ground(currentLeftX, currentRightX, currentY));

				if (placement.Type == LevelElementType.ChasmWithCollectibles)
				{
					foreach (float synchroTime in placement.SynchroTimes)
					{
						float collectibleXPosition = LevelGenerationValues.GetXPositionByTime(synchroTime);

						float timeAfterJump = synchroTime - placement.LevelElementStartTime;

						float collectibleYPosition = currentY + LevelGenerationValues.GetYDifferenceAfterJump(timeAfterJump)
							+ LevelGenerationValues.GetLowCollectibleYOffset();

						level.AddCollectibleByPosition(new Vector2(collectibleXPosition, collectibleYPosition));
					}
				}

				//float hangTime = placement.GetSynchroDuration();
				float hangTime = placement.LevelElementEndTime - placement.LevelElementStartTime;

				currentLeftX = currentRightX + LevelGenerationValues.GetChasmXDifference(hangTime);

				currentY = currentY + LevelGenerationValues.GetChasmYDifference(hangTime);
			}

			float lastRightX = LevelGenerationValues.GetXPositionByTime(9999);

			level.AddGround(new Ground(currentLeftX, lastRightX, currentY));


			LevelElementCreator levelElementGenerator = new LevelElementCreator();

			foreach (LevelElementPlacement placement in nonChasmsPlacements)
			{
				float groundY = GetGroundY(level, placement.SynchroStartTime);

				if (placement.Type == LevelElementType.DuckObstacle)
				{
					Obstacle obstacle = levelElementGenerator.CreateDuckObstacle(placement, groundY);

					level.AddObstacle(obstacle);
				}
				if (placement.Type == LevelElementType.JumpObstacle)
				{
					Obstacle obstacle = levelElementGenerator.CreateJumpObstacle(placement, groundY);

					level.AddObstacle(obstacle);
				}
				if (placement.Type == LevelElementType.LowCollectible)
				{
					Vector2 collectiblePosition = levelElementGenerator.GetLowCollectiblePosition(placement, groundY);

					level.AddCollectibleByPosition(collectiblePosition);
				}
				if (placement.Type == LevelElementType.HighCollectible)
				{
					Vector2 collectiblePosition = levelElementGenerator.GetHighCollectiblePosition(placement, groundY);

					level.AddCollectibleByPosition(collectiblePosition);
				}
				if (placement.Type == LevelElementType.SingleProjectile)
				{
					Vector2 projectilePosition = levelElementGenerator.GetProjectilePosition(placement.SynchroStartTime, groundY);

					level.AddProjectileByPosition(projectilePosition);
				}
				if (placement.Type == LevelElementType.MultipleProjectiles)
				{
					foreach (float synchroTime in placement.SynchroTimes)
					{
						Vector2 projectilePosition = levelElementGenerator.GetProjectilePosition(synchroTime, groundY);

						level.AddProjectileByPosition(projectilePosition);
					}
				}
			}

			return level;
		}

		private float GetGroundY(Level level, float time)
		{
			float x = LevelGenerationValues.GetXPositionByTime(time);

			Ground ground = LevelAnalysis.GetGroundBelowVector(level, new Vector2(x, 999));

			if (ground == null) ground = LevelAnalysis.GetGroundLeftFromVector(level, new Vector2(x, 999));

			return ground.TopY;
		}

		public Level GenerateFirstTutorialLevel()
		{
			LevelPlan levelPlan = new LevelPlan();

			float segmentLength = 7;

			for (int i = 0; i < 30; i++)
			{
				float offset = 3 + i * segmentLength;

				LevelElementPlacement highCollectible = LevelElementPlacement.CreateSingleSynchro(
					LevelElementType.HighCollectible, offset);
				LevelElementPlacement jumpObstacle = LevelElementPlacement.CreateSingleSynchro(
					LevelElementType.JumpObstacle, offset + 2);
				LevelElementPlacement chasm = LevelElementPlacement.CreateProlongedSynchro(
					LevelElementType.Chasm, offset + 4, offset + 5);

				levelPlan.AddLevelElementPlacement(highCollectible);
				levelPlan.AddLevelElementPlacement(jumpObstacle);
				levelPlan.AddLevelElementPlacement(chasm);
			}

			Level level = GenerateLevel(levelPlan);

			level.Name = "TutorialLevel1";
			level.IsTutorial = true;

			return level;
		}

		public Level GenerateSecondTutorialLevel()
		{
			LevelPlan levelPlan = new LevelPlan();

			float segmentLength = 4;

			for (int i = 0; i < 30; i++)
			{
				float offset = 3 + i * segmentLength;

				LevelElementPlacement duckObstacle = LevelElementPlacement.CreateProlongedSynchro(
					LevelElementType.DuckObstacle, offset, offset + 2);

				levelPlan.AddLevelElementPlacement(duckObstacle);
			}

			Level level = GenerateLevel(levelPlan);

			level.Name = "TutorialLevel2";
			level.IsTutorial = true;

			return level;
		}

		public Level GenerateThirdTutorialLevel()
		{
			LevelPlan levelPlan = new LevelPlan();

			float segmentLength = 5;

			for (int i = 0; i < 30; i++)
			{
				float offset = 3 + i * segmentLength;

				LevelElementPlacement singleProjectile = LevelElementPlacement.CreateSingleSynchro(
					LevelElementType.SingleProjectile, offset);

				List<float> synchros = new List<float>() { offset + 2, offset + 2.5f, offset + 3 };

				LevelElementPlacement multipleProjectiles = LevelElementPlacement.CreateMultipleSynchro(
					LevelElementType.MultipleProjectiles, synchros);

				levelPlan.AddLevelElementPlacement(singleProjectile);
				levelPlan.AddLevelElementPlacement(multipleProjectiles);
			}

			Level level = GenerateLevel(levelPlan);

			level.Name = "TutorialLevel3";
			level.IsTutorial = true;

			return level;
		}


	}
}

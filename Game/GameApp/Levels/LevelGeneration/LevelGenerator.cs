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

		public Level GenerateTestLevel()
		{
			SongElements songElements = new SongElements();

			/*
			songElements.AddSingleBeat(2.1f, 0.4f);
			songElements.AddSingleBeat(2.9f, 0.5f);
			songElements.AddSingleBeat(3.6f, 0.6f);
			songElements.AddSingleBeat(4.64f, 0.3f);

			songElements.AddSingleBeat(7.0f, 0.8f);
			songElements.AddSingleBeat(7.1f, 0.9f);
			songElements.AddSingleBeat(7.2f, 0.9f);
			songElements.AddSingleBeat(7.3f, 0.9f);
			songElements.AddSingleBeat(7.4f, 0.9f);
			songElements.AddSingleBeat(7.5f, 0.9f);
			songElements.AddSingleBeat(7.6f, 0.9f);
			songElements.AddSingleBeat(7.7f, 0.9f);
			songElements.AddSingleBeat(7.5f, 0.9f);
			songElements.AddSingleBeat(9.0f, 1.0f);
			songElements.AddSingleBeat(19.0f, 1.0f);

			songElements.AddSingleBeat(12.0f, 0.5f);

			songElements.AddHeldNote(11f, 14f, 0.4f);
			songElements.AddHeldNote(18f, 20f, 0.3f);
			songElements.AddHeldNote(23f, 25f, 0.6f);
			songElements.AddHeldNote(32f, 32.4f, 0.12f);
			songElements.AddHeldNote(42f, 46f, 0.8f);



			

			songElements.AddSingleBeat(5, 1);
			songElements.AddSingleBeat(10, 1);
			songElements.AddSingleBeat(15, 1);
			songElements.AddSingleBeat(20, 1);
			songElements.AddSingleBeat(25, 1);
			songElements.AddSingleBeat(30, 1);
			*/

			songElements.AddSingleBeat(4, 1);
			songElements.AddSingleBeat(8, 1);
			songElements.AddSingleBeat(12, 1);
			songElements.AddSingleBeat(16, 1);
			songElements.AddSingleBeat(20, 1);
			songElements.AddSingleBeat(24, 1);

			LevelPlanCreator levelPlanCreator = new LevelPlanCreator(songElements);

			levelPlanCreator.CreateLevelPlan();

			LevelPlan test = levelPlanCreator.LevelPlan;

			foreach (LevelElementPlacement pl in test.LevelElementPlacements)
			{
				Console.WriteLine(pl.Type + " von " + Math.Round(pl.LevelElementStartTime, 1) +
					" bis " + Math.Round(pl.LevelElementEndTime, 1));
			}

			return GenerateLevel(levelPlanCreator.LevelPlan);

			//return GenerateLevel(LevelPlan.GetTestLevelPlan());
		}

		public Level GenerateLevel(LevelPlan levelPlan)
		{
			Level level = new Level();

			level.PlayerStartingPosition = LevelGenerationValues.PlayerStartPosition;

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

					level.AddSolidObstacle(obstacle);
				}
				if (placement.Type == LevelElementType.JumpObstacle)
				{
					Obstacle obstacle = levelElementGenerator.CreateJumpObstacle(placement, groundY);

					level.AddSolidObstacle(obstacle);
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


	}
}

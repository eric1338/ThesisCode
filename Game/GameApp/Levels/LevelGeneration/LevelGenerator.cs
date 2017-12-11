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

			songElements.AddSingleBeat(1f, 0.4f);
			songElements.AddSingleBeat(1.9f, 0.5f);
			songElements.AddSingleBeat(2.6f, 0.6f);
			songElements.AddSingleBeat(3.64f, 0.3f);

			songElements.AddSingleBeat(5.0f, 0.8f);
			songElements.AddSingleBeat(5.5f, 0.9f);
			songElements.AddSingleBeat(7.0f, 1.0f);

			songElements.AddSingleBeat(10.0f, 0.5f);

			songElements.AddHeldNote(9f, 12f, 0.4f);
			songElements.AddHeldNote(16f, 18f, 0.3f);
			songElements.AddHeldNote(21f, 26f, 0.6f);
			songElements.AddHeldNote(30f, 30.4f, 0.12f);
			songElements.AddHeldNote(40f, 46f, 0.8f);

			LevelPlanCreator levelPlanCreator = new LevelPlanCreator(songElements);

			levelPlanCreator.CreateLevelPlan();

			//return GenerateLevel(levelPlanCreator.LevelPlan);

			return GenerateLevel(LevelPlan.GetTestLevelPlan());
		}

		public Level GenerateLevel(LevelPlan levelPlan)
		{
			Level level = new Level();

			List<LevelElementPlacement> chasmsPlacements = new List<LevelElementPlacement>();
			List<LevelElementPlacement> nonChasmsPlacements = new List<LevelElementPlacement>();

			foreach (LevelElementPlacement levelElementDestination in levelPlan.LevelElementPlacements)
			{
				if (levelElementDestination.Type == LevelElementType.Chasm) chasmsPlacements.Add(levelElementDestination);
				else nonChasmsPlacements.Add(levelElementDestination);
			}
			

			float currentLeftX = LevelGenerationValues.FirstGroundLeftX;
			float currentY = LevelGenerationValues.PlayerStartPosition.Y;

			foreach (LevelElementPlacement placement in chasmsPlacements)
			{
				float currentRightX = LevelGenerationValues.GetXPositionByTime(placement.SynchroStartTime +
					LevelGenerationValues.SuggestedChasmJumpTimeOffset);

				level.AddGround(new Ground(currentLeftX, currentRightX, currentY));

				currentLeftX = currentRightX + LevelGenerationValues.GetChasmXDifference(placement.GetSynchroDuration());

				currentY = currentY + LevelGenerationValues.GetChasmYDifference(placement.GetSynchroDuration());
			}

			float lastRightX = LevelGenerationValues.GetXPositionByTime(9999);

			level.AddGround(new Ground(currentLeftX, lastRightX, currentY));


			LevelElementCreator levelElementGenerator = new LevelElementCreator();

			int projectileID = 0;
			int collectibleID = 0;

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
					Collectible collectible = levelElementGenerator.CreateLowCollectible(placement,
						collectibleID, groundY);

					collectibleID++;

					level.AddCollectible(collectible);
				}
				if (placement.Type == LevelElementType.HighCollectible)
				{
					Collectible collectible = levelElementGenerator.CreateHighCollectible(placement,
						collectibleID, groundY);

					collectibleID++;

					level.AddCollectible(collectible);
				}
				if (placement.Type == LevelElementType.SingleProjectile)
				{
					Projectile projectile = levelElementGenerator.CreateProjectile(placement, projectileID, groundY);

					projectileID++;

					level.AddProjectile(projectile);
				}
				if (placement.Type == LevelElementType.MultipleProjectiles)
				{

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

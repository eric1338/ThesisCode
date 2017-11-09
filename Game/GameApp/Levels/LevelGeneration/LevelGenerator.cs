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
			return GenerateLevel(LevelPlan.GetTestLevelPlan());
		}

		public Level GenerateLevel(LevelPlan levelPlan)
		{
			Level level = new Level();

			// Test
			level.AddProjectile(new Projectile(0, new Vector2(8, 0.1f)));
			level.AddProjectile(new Projectile(1, new Vector2(9, 0.1f)));
			level.AddProjectile(new Projectile(2, new Vector2(10, 0.1f)));




			List<LevelElementDestination> chasms = new List<LevelElementDestination>();
			List<LevelElementDestination> nonChasms = new List<LevelElementDestination>();

			foreach (LevelElementDestination levelElementDestination in levelPlan.LevelElementDestinations)
			{
				if (levelElementDestination.Type == LevelElementType.Chasm) chasms.Add(levelElementDestination);
				else nonChasms.Add(levelElementDestination);
			}
			

			float currentLeftX = LevelGenerationValues.FirstGroundLeftX;
			float currentY = LevelGenerationValues.PlayerStartPosition.Y;

			foreach (LevelElementDestination chasm in chasms)
			{
				float currentRightX = LevelGenerationValues.GetXPositionByTime(chasm.StartingTime + LevelGenerationValues.AverageTiming);

				level.AddGround(new Ground(currentLeftX, currentRightX, currentY));

				currentLeftX = currentRightX + LevelGenerationValues.GetChasmXDifference(chasm.Duration);

				currentY = currentY + LevelGenerationValues.GetChasmYDifference(chasm.Duration);
			}

			float lastRightX = LevelGenerationValues.GetXPositionByTime(9999);

			level.AddGround(new Ground(currentLeftX, lastRightX, currentY));


			LevelElementGenerator levelElementGenerator = new LevelElementGenerator();

			foreach (LevelElementDestination nonChasm in nonChasms)
			{
				if (nonChasm.Type == LevelElementType.DuckObstacle)
				{
					float groundY = GetGroundY(level, nonChasm.StartingTime);

					Obstacle obstacle = levelElementGenerator.CreateDuckObstacle(nonChasm.StartingTime, nonChasm.Duration, groundY);

					level.AddSolidObstacle(obstacle);
				}
				if (nonChasm.Type == LevelElementType.LowObstacle)
				{
					float groundY = GetGroundY(level, nonChasm.StartingTime);

					Obstacle obstacle = levelElementGenerator.CreateLowObstacle(nonChasm.StartingTime, groundY);

					level.AddSolidObstacle(obstacle);
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

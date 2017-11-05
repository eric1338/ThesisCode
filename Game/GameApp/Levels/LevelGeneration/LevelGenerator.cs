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
					float x = LevelGenerationValues.GetXPositionByTime(nonChasm.StartingTime);
					float groundY = LevelAnalysis.GetGroundBelowVector(level, new Vector2(x, 999)).TopY;

					Console.WriteLine("GROUND-Y: " + groundY);

					Obstacle obstacle = levelElementGenerator.CreateDuckObstacle(nonChasm.StartingTime, nonChasm.Duration, groundY);

					Console.WriteLine("OBSTACLE-Y1: " + obstacle.TopLeftCorner.Y);
					Console.WriteLine("OBSTACLE-Y2: " + obstacle.BottomRightCorner.Y);

					Console.WriteLine(" ");

					level.AddSolidObstacle(obstacle);
				}
			}

			return level;
		}


	}
}

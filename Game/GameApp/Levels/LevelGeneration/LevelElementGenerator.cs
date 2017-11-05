using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelElementGenerator
	{

		private float GetXPositionByTime(float time)
		{
			return LevelGenerationValues.GetXPositionByTime(time);
		}


		private Ground CreateGround(float startTime, float endTime, float groundY)
		{
			float leftX = GetXPositionByTime(startTime);
			float rightX = GetXPositionByTime(endTime);

			return new Ground(leftX, rightX, groundY);
		}


		private Ground CreateGround2(float leftX, float endTime, float groundY)
		{
			float rightX = GetXPositionByTime(endTime);

			return new Ground(leftX, rightX, groundY);
		}



		private float GetDuckObstacleGapHeight()
		{
			float playerWidth = Gameplay.Physics.PhysicsValues.PlayerHitboxWidth;
			float playerHeight = Gameplay.Physics.PhysicsValues.PlayerHitboxHeight;

			return playerWidth + (playerHeight - playerWidth) * 0.5f;
		}

		private float GetDuckObstacleHeight()
		{
			return Gameplay.Physics.PhysicsValues.PlayerHitboxWidth;
		}

		public Obstacle CreateDuckObstacle(float time, float duration, float groundY)
		{
			float pressBeforeObstacleTime = 0.3f;

			float releaseAfterObstacleTime = 0.3f;

			float obstacleStartTime = time + pressBeforeObstacleTime;
			float obstacleEndtime = obstacleStartTime + duration - releaseAfterObstacleTime;

			float leftX = GetXPositionByTime(obstacleStartTime);
			float rightX = GetXPositionByTime(obstacleEndtime);

			float topY = groundY + GetDuckObstacleGapHeight() + GetDuckObstacleHeight();
			float bottomY = groundY + GetDuckObstacleGapHeight();

			return new Obstacle(new Vector2(leftX, topY), new Vector2(rightX, bottomY));
		}


	}
}

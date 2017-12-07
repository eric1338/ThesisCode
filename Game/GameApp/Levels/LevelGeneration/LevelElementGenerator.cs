using GameApp.Gameplay.Physics;
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
			float playerWidth = PhysicsValues.PlayerHitboxWidth;
			float playerHeight = PhysicsValues.PlayerHitboxHeight;

			return playerWidth + (playerHeight - playerWidth) * 0.5f;
		}

		private float GetDuckObstacleHeight()
		{
			return PhysicsValues.PlayerHitboxWidth;
		}

		public Obstacle CreateDuckObstacle(LevelElementDestination duckObstacle, float groundY)
		{
			float obstacleStartTime = duckObstacle.SynchroStartTime + LevelGenerationValues.DuckingObstacleEnteringSafetyTime;
			float obstacleEndtime = duckObstacle.SynchroEndTime - LevelGenerationValues.DuckingObstacleLeavingSafetyTime;

			float leftX = GetXPositionByTime(obstacleStartTime);
			float rightX = GetXPositionByTime(obstacleEndtime);

			float topY = groundY + GetDuckObstacleGapHeight() + GetDuckObstacleHeight();
			float bottomY = groundY + GetDuckObstacleGapHeight();

			return new Obstacle(new Vector2(leftX, topY), new Vector2(rightX, bottomY));
		}

		public Obstacle CreateLowObstacle(float time, float groundY)
		{
			float halfJumpLength = PhysicsValues.GetJumpLength() / 2.0f;
			float jumpHeight = PhysicsValues.GetJumpHeight();

			float jumpPosition = GetXPositionByTime(time);

			float lengthFactor = 0.5f;
			float heightFactor = 0.2f;

			float leftX = jumpPosition + halfJumpLength - (halfJumpLength * lengthFactor);
			float rightX = jumpPosition + halfJumpLength + (halfJumpLength * lengthFactor);

			float topY = groundY + jumpHeight * heightFactor;

			return new Obstacle(new Vector2(leftX, topY), new Vector2(rightX, groundY));
		}

		public Projectile CreateProjectile(int id, float time, float groundY)
		{
			float hitXPosition = GetXPositionByTime(time);

			float startingXPosition = hitXPosition + time * PhysicsValues.ProjectileVelocity;

			float startingYPosition = groundY + PhysicsValues.PlayerHitboxHeight * 0.5f;

			return new Projectile(id, new Vector2(startingXPosition, startingYPosition));
		}

		public Collectible CreateHighCollectible(int id, float time, float groundY)
		{
			float xPosition = GetXPositionByTime(time);

			float yPosition = PhysicsValues.GetJumpHeight() + PhysicsValues.PlayerHitboxHeight * 0.8f;

			return new Collectible(id, new Vector2(xPosition, yPosition));
		}


	}
}

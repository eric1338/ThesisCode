using GameApp.Gameplay.Physics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelElementCreator
	{

		private float GetXPositionByTime(float time)
		{
			return LevelGenerationValues.GetXPositionByTime(time);
		}

		private float GetPlayerRightEdgeXByTime(float time)
		{
			return GetXPositionByTime(time) + PhysicsValues.GetHalfPlayerHitboxWidth();
		}

		public Obstacle CreateJumpObstacle(LevelElementPlacement placement, float groundY)
		{
			float centerOfObstacle = GetXPositionByTime(placement.SynchroStartTime) +
				PhysicsValues.GetPlainJumpLength() / 2.0f;

			float halfObstacleWidth = LevelGenerationValues.GetJumpObstacleWidth() / 2.0f;

			float leftX = centerOfObstacle - halfObstacleWidth;
			float rightX = centerOfObstacle + halfObstacleWidth;

			float topY = groundY + LevelGenerationValues.GetJumpObstacleHeight();

			return new Obstacle(new Vector2(leftX, topY), new Vector2(rightX, groundY));
		}

		public Obstacle CreateDuckObstacle(LevelElementPlacement placement, float groundY)
		{
			float obstacleStartTime = placement.SynchroStartTime + LevelGenerationValues.DuckingObstacleEnteringSafetyTime;
			float obstacleEndtime = placement.SynchroEndTime - LevelGenerationValues.DuckingObstacleLeavingSafetyTime;

			float leftX = GetXPositionByTime(obstacleStartTime);
			float rightX = GetXPositionByTime(obstacleEndtime);

			float topY = groundY + LevelGenerationValues.GetDuckObstacleGapHeight() + LevelGenerationValues.GetDuckObstacleHeight();
			float bottomY = groundY + LevelGenerationValues.GetDuckObstacleGapHeight();

			return new Obstacle(new Vector2(leftX, topY), new Vector2(rightX, bottomY));
		}

		public Vector2 GetLowCollectiblePosition(LevelElementPlacement placement, float groundY)
		{
			return GetCollectiblePosition(placement, LevelGenerationValues.GetLowCollectibleYOffset(), groundY);
		}

		public Vector2 GetHighCollectiblePosition(LevelElementPlacement placement, float groundY)
		{
			return GetCollectiblePosition(placement, LevelGenerationValues.GetHighCollectibleYOffset(), groundY);
		}

		private Vector2 GetCollectiblePosition(LevelElementPlacement placement, float collectibleYOffset, float groundY)
		{
			float xPosition = GetPlayerRightEdgeXByTime(placement.SynchroStartTime)
				+ (PhysicsValues.CollectibleHitboxWidth / 2.0f);

			float yPosition = groundY + collectibleYOffset;

			return new Vector2(xPosition, yPosition);
		}

		public Vector2 GetProjectilePosition(float time, float groundY)
		{
			float hitXPosition = GetPlayerRightEdgeXByTime(time);

			float startingXPosition = hitXPosition + time * PhysicsValues.ProjectileVelocity
				+ (PhysicsValues.ProjectileHitboxWidth / 2.0f);

			float startingYPosition = groundY + LevelGenerationValues.GetProjectileYOffset();

			return new Vector2(startingXPosition, startingYPosition);
		}


	}
}

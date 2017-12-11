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
		



		public Collectible CreateChasmCollectibles(LevelElementPlacement placement)
		{
			return new Collectible(0, new Vector2(0, 0));
		}




		public Obstacle CreateJumpObstacle(LevelElementPlacement placement, float groundY)
		{
			float halfJumpLength = PhysicsValues.GetPlainJumpLength() / 2.0f;
			float jumpHeight = PhysicsValues.GetPlainJumpHeight();

			float jumpPosition = GetXPositionByTime(placement.SynchroStartTime);

			float lengthFactor = 0.5f;
			float heightFactor = 0.2f;

			float leftX = jumpPosition + halfJumpLength - (halfJumpLength * lengthFactor);
			float rightX = jumpPosition + halfJumpLength + (halfJumpLength * lengthFactor);

			float topY = groundY + jumpHeight * heightFactor;

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

		public Collectible CreateLowCollectible(LevelElementPlacement placement, int id, float groundY)
		{
			return CreateCollectible(placement, LevelGenerationValues.GetLowObstacleYOffset(), id, groundY);
		}

		public Collectible CreateHighCollectible(LevelElementPlacement placement, int id, float groundY)
		{
			return CreateCollectible(placement, LevelGenerationValues.GetHighObstacleYOffset(), id, groundY);
		}

		private Collectible CreateCollectible(LevelElementPlacement placement, float collectibleYOffset, int id, float groundY)
		{
			float xPosition = GetPlayerRightEdgeXByTime(placement.SynchroStartTime)
				+ (PhysicsValues.CollectibleHitboxWidth / 2.0f);

			float yPosition = groundY + collectibleYOffset;

			return new Collectible(id, new Vector2(xPosition, yPosition));
		}

		public Projectile CreateProjectile(LevelElementPlacement placement, int id, float groundY)
		{
			float time = placement.SynchroStartTime;

			float hitXPosition = GetPlayerRightEdgeXByTime(time);

			float startingXPosition = hitXPosition + time * PhysicsValues.ProjectileVelocity
				+ (PhysicsValues.ProjectileHitboxWidth / 2.0f);

			float startingYPosition = groundY + LevelGenerationValues.GetProjectileYOffset();

			return new Projectile(id, new Vector2(startingXPosition, startingYPosition));
		}








	}
}

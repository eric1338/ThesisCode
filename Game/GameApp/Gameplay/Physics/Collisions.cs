using GameApp.Gameplay;
using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay.Physics
{
	class Collisions
	{

		private Level level;

		public Collisions(Level level)
		{
			this.level = level;
		}
		
		private Hitbox GetPlayerHitbox(Vector2 playerPosition, bool isPlayerStanding)
		{
			return Hitboxes.GetPlayerHitbox(playerPosition, isPlayerStanding);
		}

		public Ground GetPlayerGroundCollision(LevelProgression levelProgression)
		{
			return GetPlayerGroundCollision(levelProgression.CurrentPlayerPosition,
				levelProgression.IsPlayerStanding);
		}

		public Ground GetPlayerGroundCollision(Vector2 playerPosition, bool isPlayerStanding)
		{
			Hitbox playerHitbox = GetPlayerHitbox(playerPosition, isPlayerStanding);

			foreach (Ground ground in level.Grounds)
			{
				Hitbox groundHitbox = Hitboxes.GetGroundHitbox(ground);

				if (playerHitbox.CollidesWith(groundHitbox)) return ground;
			}

			return null;
		}

		public bool DoesPlayerCollideWithASolidObstacle(LevelProgression levelProgression)
		{
			Hitbox playerHitbox = GetPlayerHitbox(levelProgression.CurrentPlayerPosition, levelProgression.IsPlayerStanding);

			foreach (Obstacle obstacle in level.SolidObstacles)
			{
				Hitbox obstacleHitbox = Hitboxes.GetObstacleHitbox(obstacle);

				if (playerHitbox.CollidesWith(obstacleHitbox)) return true;
			}

			return false;
		}

		public Obstacle GetPlayerDestructibleObstacleCollision(LevelProgression levelProgression)
		{
			Hitbox playerHitbox = GetPlayerHitbox(levelProgression.CurrentPlayerPosition, levelProgression.IsPlayerStanding);

			foreach (Obstacle obstacle in level.DestructibleObstacles)
			{
				if (levelProgression.IsObstacleAlreadyDestructed(obstacle)) continue;

				Hitbox obstacleHitbox = Hitboxes.GetObstacleHitbox(obstacle);

				if (playerHitbox.CollidesWith(obstacleHitbox)) return obstacle;
			}

			return null;
		}


		/*
		public bool DoesPlayerCollideWithASolidObstacle(LevelProgression levelProgression)
		{
			return DoesPlayerCollideWithASolidObstacle(levelProgression.CurrentPlayerPosition,
				levelProgression.IsPlayerStanding);
		}

		private bool DoesPlayerCollideWithASolidObstacle(Vector2 playerPosition, bool isPlayerStanding)
		{
			Hitbox playerHitbox = GetPlayerHitbox(playerPosition, isPlayerStanding);

			foreach (Obstacle obstacle in level.SolidObstacles)
			{
				Hitbox obstacleHitbox = Hitboxes.GetObstacleHitbox(obstacle);

				if (playerHitbox.CollidesWith(obstacleHitbox)) return true;
			}

			return false;
		}
		*/

		public List<Collectible> GetPlayerCollectibleCollisions(LevelProgression levelProgression)
		{
			List<Collectible> collectedCollectibles = new List<Collectible>();

			Hitbox playerHitbox = GetPlayerHitbox(levelProgression.CurrentPlayerPosition, levelProgression.IsPlayerStanding);

			foreach (Collectible collectible in level.Collectibles)
			{
				if (levelProgression.IsCollectibleAlreadyCollected(collectible)) continue;

				Hitbox collectibleHitbox = Hitboxes.GetCollectibleHitbox(collectible);

				if (playerHitbox.CollidesWith(collectibleHitbox)) collectedCollectibles.Add(collectible);
			}

			return collectedCollectibles;
		}

	}
}

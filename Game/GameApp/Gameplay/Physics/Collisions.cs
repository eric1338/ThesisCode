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
		
		private Hitbox GetPlayerHitbox(Vector2 playerPosition)
		{
			return Hitboxes.GetPlayerHitbox(playerPosition);
		}


		public Ground GetPlayerGroundCollision(Vector2 playerPosition)
		{
			Hitbox playerHitbox = GetPlayerHitbox(playerPosition);

			foreach (Ground ground in level.Grounds)
			{
				Hitbox groundHitbox = Hitboxes.GetGroundHitbox(ground);

				if (playerHitbox.CollidesWith(groundHitbox)) return ground;
			}

			return null;
		}

		public bool DoesPlayerCollideWithAnObstacle(Vector2 playerPosition)
		{
			Hitbox playerHitbox = GetPlayerHitbox(playerPosition);

			foreach (Obstacle obstacle in level.Obstacles)
			{
				Hitbox obstacleHitbox = Hitboxes.GetObstacleHitbox(obstacle);

				if (playerHitbox.CollidesWith(obstacleHitbox)) return true;
			}

			return false;
		}

		public List<Collectible> GetPlayerCollectibleCollisions(LevelProgression levelProgression)
		{
			List<Collectible> collectedCollectibles = new List<Collectible>();

			Hitbox playerHitbox = GetPlayerHitbox(levelProgression.CurrentPlayerPosition);

			foreach (Collectible collectible in levelProgression.RemainingCollectibles)
			{
				Hitbox collectibleHitbox = Hitboxes.GetCollectibleHitbox(collectible);

				if (playerHitbox.CollidesWith(collectibleHitbox)) collectedCollectibles.Add(collectible);
			}

			return collectedCollectibles;
		}

	}
}

using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay.Physics
{
	class Hitboxes
	{

		public static Hitbox GetPlayerHitbox(Vector2 playerPosition, bool isPlayerStanding)
		{
			Vector2 topLeftCorner;
			Vector2 bottomRightCorner;

			float playerWidth = PhysicsValues.PlayerHitboxWidth;
			float playerHeight = PhysicsValues.PlayerHitboxHeight;

			if (isPlayerStanding)
			{
				topLeftCorner = new Vector2(playerPosition.X - (playerWidth / 2), playerPosition.Y + playerHeight);
				bottomRightCorner = new Vector2(playerPosition.X + (playerWidth / 2), playerPosition.Y);
			}
			else
			{
				topLeftCorner = new Vector2(playerPosition.X - (playerHeight / 2), playerPosition.Y + playerWidth);
				bottomRightCorner = new Vector2(playerPosition.X + (playerHeight / 2), playerPosition.Y);
			}

			return new BoxHitbox(topLeftCorner, bottomRightCorner);
		}

		public static Hitbox GetGroundHitbox(Ground ground)
		{
			Vector2 bottomRightCorner = new Vector2(ground.RightX, -100000.0f);

			return new BoxHitbox(ground.TopLeftCorner, bottomRightCorner);
		}

		public static Hitbox GetObstacleHitbox(Obstacle obstacle)
		{
			return new BoxHitbox(obstacle.TopLeftCorner, obstacle.BottomRightCorner);
		}

		public static Hitbox GetCollectibleHitbox(Collectible collectible)
		{
			float hitboxWidth = PhysicsValues.CollectibleHitboxWidth;
			float hitboxHeight = PhysicsValues.CollectibleHitboxHeight;

			return new BoxHitbox(collectible.Position, hitboxWidth, hitboxHeight);
		}

		public static Hitbox GetProjectileHitbox(Vector2 projectilePosition)
		{
			float hitboxWidth = PhysicsValues.ProjectileHitboxWidth;
			float hitboxHeight = PhysicsValues.ProjectileHitboxHeight;

			return new BoxHitbox(projectilePosition, hitboxWidth, hitboxHeight);
		}

	}
}

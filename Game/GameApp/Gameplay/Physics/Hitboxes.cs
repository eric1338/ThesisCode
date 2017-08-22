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

		public static Hitbox GetPlayerHitbox(Vector2 playerPosition)
		{
			float halfPlayerWidth = PhysicsValues.GetHalfPlayerHitboxWidth();
			float playerHeight = PhysicsValues.PlayerHitboxHeight;

			Vector2 topLeftCorner = new Vector2(playerPosition.X - halfPlayerWidth, playerPosition.Y + playerHeight);
			Vector2 bottomRightCorner = new Vector2(playerPosition.X + halfPlayerWidth, playerPosition.Y);

			return new BoxHitbox(topLeftCorner, bottomRightCorner);
		}

		public static Hitbox GetGroundHitbox(Ground ground)
		{
			Vector2 bottomRightCorner = new Vector2(ground.RightX, -1000.0f);

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

	}
}

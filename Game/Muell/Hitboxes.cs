using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	class Hitboxes
	{

		// TODO: Werte auslagern

		public static List<Tuple<Collectible, Hitbox>> GetCollectiblesWithHitboxes(Level level)
		{
			List<Tuple<Collectible, Hitbox>> hitboxes = new List<Tuple<Collectible, Hitbox>>();

			foreach (Collectible collectible in level.Collectibles)
			{
				BoxHitbox hitbox = new BoxHitbox(collectible.Position, 0.1f, 0.1f);

				hitboxes.Add(new Tuple<Collectible, Hitbox> (collectible, hitbox));
			}

			return hitboxes;
		}

		public static List<Tuple<Ground, Hitbox>> GetGroundsWithHitboxes(Level level)
		{
			List<Tuple<Ground, Hitbox>> hitboxes = new List<Tuple<Ground, Hitbox>>();

			foreach (Ground ground in level.Grounds)
			{
				Vector2 bottomRightCorner = new Vector2(ground.RightX, -1000.0f);
				
				BoxHitbox hitbox = new BoxHitbox(ground.TopLeftCorner, bottomRightCorner);

				hitboxes.Add(new Tuple<Ground, Hitbox>(ground, hitbox));
			}

			return hitboxes;
		}

		public static Hitbox GetPlayerHitbox(Vector2 playerPosition)
		{
			float halfPlayerWidth = 0.2f;
			float playerHeight = 0.8f;

			Vector2 topLeftCorner = new Vector2(playerPosition.X - halfPlayerWidth, playerPosition.Y + playerHeight);
			Vector2 bottomRightCorner = new Vector2(playerPosition.X + halfPlayerWidth, playerPosition.Y);

			return new BoxHitbox(topLeftCorner, bottomRightCorner);
		}

	}
}

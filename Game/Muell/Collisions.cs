using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	class Collisions
	{

		private Level level;

		private List<Tuple<Collectible, Hitbox>> collectiblesWithHitboxes = new List<Tuple<Collectible, Hitbox>>();
		private List<Tuple<Ground, Hitbox>> groundsWithHitboxes = new List<Tuple<Ground, Hitbox>>();

		public Collisions(Level level)
		{
			this.level = level;

			collectiblesWithHitboxes = Hitboxes.GetCollectiblesWithHitboxes(level);
			groundsWithHitboxes = Hitboxes.GetGroundsWithHitboxes(level);
		}
		
		private Hitbox GetPlayerHitbox(Vector2 playerPosition)
		{
			return Hitboxes.GetPlayerHitbox(playerPosition);
		}



		public List<Collectible> GetPlayerCollectibleCollisions(Vector2 playerPosition)
		{
			List<Collectible> collectibleCollisions = new List<Collectible>();

			Hitbox playerHitbox = GetPlayerHitbox(playerPosition);

			foreach (Tuple<Collectible, Hitbox> collectibleWithHitbox in collectiblesWithHitboxes)
			{
				if (playerHitbox.CollidesWith(collectibleWithHitbox.Item2))
				{
					collectibleCollisions.Add(collectibleWithHitbox.Item1);
				}
			}

			return collectibleCollisions;
		}

		public Ground GetPlayerGroundCollision(Vector2 playerPosition)
		{
			
		}

	}
}

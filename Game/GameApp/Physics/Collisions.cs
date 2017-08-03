using GameApp.Gameplay;
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

		public Collisions(Level level)
		{
			this.level = level;
		}
		
		private Hitbox GetPlayerHitbox(Vector2 playerPosition)
		{
			return Hitboxes.GetPlayerHitbox(playerPosition);
		}


		public Collectible GetPlayerCollectibleCollisions(LevelProgression levelProgression)
		{
			Hitbox playerHitbox = GetPlayerHitbox(levelProgression.CurrentPlayerPosition);

			Visual.LevelDrawer.AddPfusch1(playerHitbox.Corners, new Vector3(1, 0, 0));

			foreach (Collectible collectible in levelProgression.RemainingCollectibles)
			{
				Hitbox collectibleHitbox = Hitboxes.GetCollectibleHitbox(collectible);

				Visual.LevelDrawer.AddPfusch1(collectibleHitbox.Corners, new Vector3(1, 1, 0));

				if (playerHitbox.CollidesWith(collectibleHitbox)) return collectible;
			}

			return null;
		}

		public Ground GetPlayerGroundCollision(Vector2 playerPosition)
		{
			Hitbox playerHitbox = GetPlayerHitbox(playerPosition);

			foreach (Ground ground in level.Grounds)
			{
				Hitbox groundHitbox = Hitboxes.GetGroundHitbox(ground);

				Visual.LevelDrawer.AddPfusch1(groundHitbox.Corners, new Vector3(0, 1, 0));

				if (playerHitbox.CollidesWith(groundHitbox)) return ground;
			}

			return null;
		}

	}
}

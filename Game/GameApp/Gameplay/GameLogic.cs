using GameApp.Levels;
using GameApp.Physics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay
{
	class GameLogic
	{

		private LevelAttempt levelAttempt;

		private GamePhysics gamePhysics;
		private Collisions collisions;

		public GameLogic(LevelAttempt levelAttempt)
		{
			this.levelAttempt = levelAttempt;

			gamePhysics = new GamePhysics(levelAttempt.Level);
			collisions = new Collisions(levelAttempt.Level);
		}

		private LevelProgression GetLevelProgression()
		{
			return levelAttempt.LevelProgression;
		}

		public void PerformPlayerJump()
		{
			gamePhysics.PerformJump();
		}

		public void DoLogic()
		{
			// TODO: temp
			Visual.LevelDrawer.ClearPfuschs();

			Vector2 oldPosition = GetLevelProgression().CurrentPlayerPosition;

			// TODO: y-Adjustment (bzw. Gravity aufhören) nicht in Physics sondern Collision (oder doch nicht?)

			Vector2 newPosition = gamePhysics.DoPlayerPhysics(oldPosition);

			if (!gamePhysics.IsPlayerOnGround(newPosition))
			{
				Ground groundCollidedWith = collisions.GetPlayerGroundCollision(newPosition);

				if (groundCollidedWith != null)
				{
					Console.WriteLine("...");

					// halfPlayerWidth
					Vector2 rightFoot = newPosition + new Vector2(0.1f, 0.0f);

					float xDif = rightFoot.X - groundCollidedWith.LeftX;
					float yDif = groundCollidedWith.TopY - rightFoot.Y;

					if (yDif > xDif)
					{
						Console.WriteLine("failed :(");
						newPosition = new Vector2(newPosition.X, groundCollidedWith.TopY);
					}
				}
			}

			GetLevelProgression().CurrentPlayerPosition = newPosition;

			CheckPlayerCollectibleCollision();
		}

		private void CheckPlayerCollectibleCollision()
		{
			Collectible collectedCollectible = collisions.GetPlayerCollectibleCollisions(GetLevelProgression());

			if (collectedCollectible != null)
			{
				GetLevelProgression().CollectCollectible(collectedCollectible);
			}
		}
	}
}

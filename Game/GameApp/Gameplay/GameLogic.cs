using GameApp.Levels;
using GameApp.Gameplay.Physics;
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

		private Vector2 GetPlayerPosition()
		{
			return GetLevelProgression().CurrentPlayerPosition;
		}



		private Vector2 GetLeftFootPosition()
		{
			return PhysicsValues.GetLeftFootPosition(GetPlayerPosition());
		}

		private Vector2 GetRightFootPosition()
		{
			return PhysicsValues.GetRightFootPosition(GetPlayerPosition());
		}




		public void PerformPlayerJump()
		{
			gamePhysics.PerformJump(GetLevelProgression().CurrentPlayerPosition);
		}

		public void DoLogic()
		{
			SetNewPosition();

			CheckPlayerCollectibleCollision();
		}

		private void SetNewPosition()
		{
			// TODO: temp
			Visual.LevelDrawer.ClearPfuschs();

			Vector2 oldPosition = GetLevelProgression().CurrentPlayerPosition;

			Vector2 newPosition = gamePhysics.DoPlayerPhysics(oldPosition);

			if (!LevelAnalysis.IsPlayerOnGround(levelAttempt.Level, newPosition))
			{
				Ground groundCollidedWith = collisions.GetPlayerGroundCollision(newPosition);

				if (groundCollidedWith != null)
				{
					Vector2 rightFoot = newPosition + new Vector2(0.0f, 0.0f);

					float deltaX = Math.Abs(rightFoot.X - groundCollidedWith.LeftX);
					float deltaY = Math.Abs(groundCollidedWith.TopY - rightFoot.Y);

					if (deltaY > deltaX)
					{
						Console.WriteLine("failed :(");
						Console.WriteLine("x " + groundCollidedWith.LeftX);
						Console.WriteLine("~ " + deltaX + " | " + deltaY);
						Console.WriteLine("rightFoot.Y: " + rightFoot.Y);
						Console.WriteLine("ground.Y: " + groundCollidedWith.TopY);
						Console.WriteLine(" ");
					}

					newPosition = new Vector2(newPosition.X, groundCollidedWith.TopY);

					gamePhysics.ResetVerticalVelocity();
				}
			}

			GetLevelProgression().CurrentPlayerPosition = newPosition;
		}

		private void CheckPlayerCollectibleCollision()
		{
			List<Collectible> collectedCollectibles = collisions.GetPlayerCollectibleCollisions(GetLevelProgression());

			foreach (Collectible collectible in collectedCollectibles)
			{
				GetLevelProgression().CollectCollectible(collectible);
			}
		}
	}
}

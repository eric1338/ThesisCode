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

		public void PerformPlayerJump()
		{
			gamePhysics.PerformJump(GetLevelProgression().CurrentPlayerPosition);
		}

		public void DoLogic()
		{
			GetLevelProgression().UpdateTime(1.0f / GeneralValues.FPS);

			SetNewPosition();

			if (GetLevelProgression().IsPlayerInGodmode()) return;

			CheckPlayerObstacleCollision();
			CheckPlayerCollectibleCollision();
		}

		private void SetNewPosition()
		{
			Vector2 oldPosition = GetLevelProgression().CurrentPlayerPosition;

			Vector2 newPosition = gamePhysics.DoPlayerPhysics(oldPosition);

			if (!LevelAnalysis.IsVectorOnGround(levelAttempt.Level, newPosition))
			{
				Ground groundCollidedWith = collisions.GetPlayerGroundCollision(newPosition);

				if (groundCollidedWith != null)
				{
					Vector2 rightFoot = PhysicsValues.GetRightFootPosition(newPosition);

					float deltaX = Math.Abs(rightFoot.X - groundCollidedWith.LeftX);
					float deltaY = Math.Abs(groundCollidedWith.TopY - rightFoot.Y);

					if (deltaY > deltaX)
					{
						AddPlayerFail();
					}

					newPosition = new Vector2(newPosition.X, groundCollidedWith.TopY);

					gamePhysics.ResetVerticalVelocity();
				}
			}

			GetLevelProgression().CurrentPlayerPosition = newPosition;
		}

		private void AddPlayerFail()
		{
			GetLevelProgression().ActivateGodmode(GameplayValues.SecondsOfGodmodeAfterFail);

			Console.WriteLine("failed :(");
		}

		private void CheckPlayerObstacleCollision()
		{
			if (collisions.DoesPlayerCollideWithAnObstacle(GetLevelProgression().CurrentPlayerPosition))
			{
				AddPlayerFail();
			}
		}

		private void CheckPlayerCollectibleCollision()
		{
			List<Collectible> collectedCollectibles = collisions.GetPlayerCollectibleCollisions(GetLevelProgression());

			foreach (Collectible collectible in collectedCollectibles)
			{
				// TODO: Punkte hier berechnen, nicht in LevelProgression

				GetLevelProgression().CollectCollectible(collectible);
			}
		}
	}
}

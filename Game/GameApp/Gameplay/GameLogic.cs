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

		public void PerformPlayerHit()
		{
			GetLevelProgression().GetIntoHittingMode(GameplayValues.SecondsOfHittingMode);
		}

		public void SetPlayerIsStanding(bool isPlayerStanding)
		{
			GetLevelProgression().IsPlayerStanding = isPlayerStanding;
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
				bool isPlayerStanding = GetLevelProgression().IsPlayerStanding;

				Ground groundCollidedWith = collisions.GetPlayerGroundCollision(newPosition, isPlayerStanding);

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
			GetLevelProgression().ActivateGodMode(GameplayValues.SecondsOfGodModeAfterFail);

			GetLevelProgression().AddFailedAttempt();
		}

		private void CheckPlayerObstacleCollision()
		{
			LevelProgression levelProgression = GetLevelProgression();

			if (collisions.DoesPlayerCollideWithASolidObstacle(levelProgression))
			{
				AddPlayerFail();
			}

			Obstacle obstacleCollidedWith = collisions.GetPlayerDestructibleObstacleCollision(levelProgression);

			if (obstacleCollidedWith != null)
			{
				if (levelProgression.IsPlayerInHittingMode())
				{
					levelProgression.DestructObstacle(obstacleCollidedWith);
				}
				else
				{
					AddPlayerFail();
				}
			}
		}

		private void CheckPlayerCollectibleCollision()
		{
			LevelProgression levelProgression = GetLevelProgression();

			List<Collectible> collectedCollectibles = collisions.GetPlayerCollectibleCollisions(levelProgression);

			foreach (Collectible collectible in collectedCollectibles)
			{
				levelProgression.CollectCollectible(collectible);

				levelProgression.AddPoints(GameplayValues.PointsForCollectible);
			}
		}
	}
}

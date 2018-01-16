using GameApp.Levels;
using GameApp.Gameplay.Physics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameApp.Utils;

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

		public void SetPlayerIsDefending(bool isPlayerDefending)
		{
			GetLevelProgression().IsPlayerDefending = isPlayerDefending;
		}

		public void DoLogic()
		{


			GetLevelProgression().UpdateTime(1.0f / GeneralValues.FPS);

			SetNewPlayerPosition();
			SetNewProjectilePositions();

			if (GetLevelProgression().IsLevelComplete) return;
			if (GetLevelProgression().IsPlayerInGodmode()) return;

			CheckPlayerObstacleCollision();
			CheckPlayerCollectibleCollision();
			CheckPlayerProjectileCollision();

			CheckIfLevelIsComplete();
		}

		private void SetNewPlayerPosition()
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

		private void SetNewProjectilePositions()
		{
			LevelProgression levelProgression = GetLevelProgression();
			
			foreach (Projectile projectile in levelAttempt.Level.Projectiles)
			{
				Vector2 movementDelta;

				if (levelProgression.IsProjectileDeflected(projectile))
				{
					movementDelta = levelProgression.GetProjectileDeflectionDirection(projectile) / GeneralValues.FPS;
				}
				else
				{
					movementDelta = new Vector2(-PhysicsValues.GetProjectileVelocityPerFrame(), 0);
				}

				levelProgression.MoveProjectile(projectile, movementDelta);
			}
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
				if (!levelProgression.IsPlayerInHittingMode())
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

		private void CheckPlayerProjectileCollision()
		{
			LevelProgression levelProgression = GetLevelProgression();

			List<Projectile> projectilesCollidedWith = collisions.GetPlayerProjectileCollisions(levelProgression);

			foreach (Projectile projectile in projectilesCollidedWith)
			{
				if (levelProgression.IsPlayerDefending)
				{
					DeflectProjectile(projectile);
				}
				else
				{
					AddPlayerFail();
				}
			}
		}

		private void DeflectProjectile(Projectile projectile)
		{
			float yVelocity = MyMath.GetRandomNumber() * PhysicsValues.ProjectileMaximumYVelocity;

			if (MyMath.GetRandomNumber() > 0.5f) yVelocity *= -1;

			float xVelocity = (float)Math.Sqrt(Math.Pow(PhysicsValues.ProjectileVelocity * 3, 2) - Math.Pow(yVelocity, 2));

			Vector2 deflectionDirection = new Vector2(xVelocity, yVelocity);

			GetLevelProgression().DeflectProjectile(projectile, deflectionDirection);
		}

		private void CheckIfLevelIsComplete()
		{
			float playerX = GetLevelProgression().CurrentPlayerPosition.X;

			GetLevelProgression().IsLevelComplete = playerX >= levelAttempt.Level.GoalLineX;
		}

	}
}

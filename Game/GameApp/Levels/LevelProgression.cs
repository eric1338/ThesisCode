using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class LevelProgression
	{

		private Level level;

		public Vector2 CurrentPlayerPosition { get; set; }

		public int Points { get; set; }
		public int FailedAttempts { get; set; }

		private List<int> destructedObstaclesIDs;
		private List<int> collectedCollectibleIDs;

		private Dictionary<int, Vector2> projectilePositions;
		private Dictionary<int, bool> projectileDeflectionState;
		private Dictionary<int, Vector2> projectileDeflectionDirection;

		public bool IsPlayerStanding { get; set; }
		public bool IsPlayerDefending { get; set; }

		public bool IsLevelComplete { get; set; }
		public bool IsGamePaused { get; set; }

		private float secondsPlayed;
		private float secondsOfHittingModeLeft;
		private float secondsOfGodModeLeft;


		public LevelProgression(Level level)
		{
			this.level = level;

			Reset();
		}

		public void Reset()
		{
			CurrentPlayerPosition = level.PlayerStartingPosition;

			Points = 0;
			FailedAttempts = 0;

			destructedObstaclesIDs = new List<int>();
			collectedCollectibleIDs = new List<int>();

			projectilePositions = new Dictionary<int, Vector2>();
			projectileDeflectionState = new Dictionary<int, bool>();
			projectileDeflectionDirection = new Dictionary<int, Vector2>();

			foreach (Projectile projectile in level.Projectiles)
			{
				projectilePositions[projectile.ID] = projectile.StartingPosition;
				projectileDeflectionState[projectile.ID] = false;
				projectileDeflectionDirection[projectile.ID] = Vector2.Zero;
			}

			IsPlayerStanding = true;
			IsPlayerDefending = false;

			IsLevelComplete = false;
			IsGamePaused = false;

			secondsPlayed = 0;
			secondsOfHittingModeLeft = 0;
			secondsOfGodModeLeft = 0;
		}

		public void UpdateTime(float timeSinceLastUpdate)
		{
			secondsPlayed += timeSinceLastUpdate;

			if (secondsOfHittingModeLeft > 0) secondsOfHittingModeLeft -= timeSinceLastUpdate;

			if (secondsOfGodModeLeft > 0) secondsOfGodModeLeft -= timeSinceLastUpdate;
		}

		public void AddPoints(int pointsAdded)
		{
			Points += pointsAdded;
		}

		public void AddFailedAttempt()
		{
			FailedAttempts++;
		}

		public void GetIntoHittingMode(float secondsOfHitting)
		{
			if (IsPlayerInHittingMode()) return;

			secondsOfHittingModeLeft = secondsOfHitting;
		}

		public void ActivateGodMode(float secondsOfGodMode)
		{
			secondsOfGodModeLeft = secondsOfGodMode;
		}

		public float GetSecondsPlayed()
		{
			return secondsPlayed;
		}

		public bool IsPlayerInHittingMode()
		{
			return secondsOfHittingModeLeft > 0;
		}

		public bool IsPlayerInGodmode()
		{
			return secondsOfGodModeLeft > 0;
		}

		public void DestructObstacle(Obstacle obstacle)
		{
			destructedObstaclesIDs.Add(obstacle.ID);
		}

		public bool IsObstacleAlreadyDestructed(Obstacle obstacle)
		{
			return destructedObstaclesIDs.Contains(obstacle.ID);
		}

		public void CollectCollectible(Collectible collectible)
		{
			collectedCollectibleIDs.Add(collectible.ID);
		}

		public bool IsCollectibleAlreadyCollected(Collectible collectible)
		{
			return collectedCollectibleIDs.Contains(collectible.ID);
		}

		public Vector2 GetProjectilePosition(Projectile projectile)
		{
			return projectilePositions[projectile.ID];
		}

		public bool IsProjectileDeflected(Projectile projectile)
		{
			return projectileDeflectionState[projectile.ID];
		}

		public Vector2 GetProjectileDeflectionDirection(Projectile projectile)
		{
			return projectileDeflectionDirection[projectile.ID];
		}

		public void MoveProjectile(Projectile projectile, Vector2 movementDelta)
		{
			projectilePositions[projectile.ID] += movementDelta;
		}

		public void DeflectProjectile(Projectile projectile, Vector2 deflectionDirection)
		{
			projectileDeflectionState[projectile.ID] = true;
			projectileDeflectionDirection[projectile.ID] = deflectionDirection;
		}

	}
}

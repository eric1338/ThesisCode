﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay.Physics
{
	class PhysicsValues
	{

		public static readonly float PlayerHitboxWidth = 0.16f;
		public static readonly float PlayerHitboxHeight = 0.32f;

		public static float GetHalfPlayerHitboxWidth()
		{
			return PlayerHitboxWidth / 2.0f;
		}

		public static readonly float ProjectileHitboxWidth = 0.1f;
		public static readonly float ProjectileHitboxHeight = 0.1f;

		public static readonly float CollectibleHitboxWidth = 0.1f;
		public static readonly float CollectibleHitboxHeight = 0.1f;

		public static readonly float RightFootOffset = PlayerHitboxWidth / 2;
		public static readonly float LeftFootOffset = PlayerHitboxWidth / 2;

		public static Vector2 GetRightFootPosition(Vector2 playerPosition)
		{
			return playerPosition + new Vector2(RightFootOffset, 0);
		}

		public static Vector2 GetLeftFootPosition(Vector2 playerPosition)
		{
			return playerPosition + new Vector2(-LeftFootOffset, 0);
		}

		public static float GetHorizontalPlayerVelocity()
		{
			return 1.24f * GeneralValues.DifficultyFactor;
		}

		public static float GetGravityAcceleration()
		{
			return 0.1f * GeneralValues.DifficultyFactor;
		}

		public static float GetJumpAcceleration()
		{
			return 0.03f * GeneralValues.DifficultyFactor;
		}

		public static float GetProjectileVelocity()
		{
			return 0.8f * GeneralValues.DifficultyFactor;
		}
		
		public static readonly float ProjectileMaximumYVelocity = 0.4f;

		public static float GetHorizontalPlayerVelocityPerFrame()
		{
			return GetHorizontalPlayerVelocity() / GeneralValues.FPS;
		}

		public static float GetGravityAccelerationPerFrame()
		{
			return GetGravityAcceleration() / GeneralValues.FPS;
		}

		public static float GetProjectileVelocityPerFrame()
		{
			return GetProjectileVelocity() / GeneralValues.FPS;
		}


		private static float jumpHeight = -1;
		private static float jumpLength = -1;
		private static float jumpDuration = -1;

		public static float GetPlainJumpHeight()
		{
			CalculatePlainJumpValues();

			return jumpHeight;
		}

		public static float GetPlainJumpLength()
		{
			CalculatePlainJumpValues();

			return jumpLength;
		}

		public static float GetPlainJumpDuration()
		{
			CalculatePlainJumpValues();

			return jumpDuration;
		}

		private static void CalculatePlainJumpValues()
		{
			float y = 0.00001f;
			float yAcceleration = GetJumpAcceleration();

			float maximumJumpHeight = y;

			int iterations = 0;

			while (y > 0)
			{
				iterations++;

				y += yAcceleration;

				yAcceleration -= GetGravityAccelerationPerFrame();

				if (yAcceleration > 0) maximumJumpHeight = y;
			}

			jumpHeight = maximumJumpHeight;
			jumpLength = iterations * GetHorizontalPlayerVelocityPerFrame();
			jumpDuration = iterations / (float) GeneralValues.FPS;
		}


	}
}

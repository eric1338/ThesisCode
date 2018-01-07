using GameApp.Gameplay.Physics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelGenerationValues
	{

		// Difficulty

		public static readonly float TimeBeforeLevelElement = 2;


		// Applicability Thresholds

		public static readonly float HeldNoteApplicabilityThreshold = 0.2f;
		public static readonly float MultipleBeatsApplicabilityThreshold = 0.1f;
		public static readonly float SingleBeatsApplicabilityThreshold = 0.3f;
		public static readonly float FillUpElementsApplicabilityThreshold = 0.4f;

		// Rest

		public static readonly float MaximumTimeMarginForMultipleBeats = 1f;

		public static readonly Vector2 PlayerStartPosition = new Vector2(0, 0);

		public static readonly float MusicStartPositionX = 0;

		public static readonly float FirstGroundLeftX = -10;

		// LevelElements

		public static readonly float SuggestedChasmJumpTimingOffset = 0.2f;

		public static readonly float MaximumChasmJumpTimingOffset = 0.4f;

		public static readonly float MaximumJumpObstacleJumpTimingOffset = 0.1f;


		public static readonly float DuckingObstacleEnteringSafetyTime = 0.3f;
		public static readonly float DuckingObstacleLeavingSafetyTime = 0.3f;

		public static float GetDuckObstacleGapHeight()
		{
			return PhysicsValues.PlayerHitboxWidth + (PhysicsValues.PlayerHitboxHeight - PhysicsValues.PlayerHitboxWidth) * 0.5f;
		}

		public static float GetDuckObstacleHeight()
		{
			return PhysicsValues.PlayerHitboxWidth;
		}

		public static float GetHighCollectibleYOffset()
		{
			return PhysicsValues.GetPlainJumpHeight() + PhysicsValues.PlayerHitboxHeight * 0.8f;
		}

		public static float GetLowCollectibleYOffset()
		{
			return PhysicsValues.PlayerHitboxHeight * 0.6f;
		}

		public static float GetJumpObstacleWidth()
		{
			float minimumHorizontalDistanceRequired = 0;

			for (float time = 0; time < 100; time += 1.0f / GeneralValues.FPS)
			{
				if (GetYDifferenceAfterJump(time) > GetJumpObstacleHeight())
				{
					minimumHorizontalDistanceRequired = time * PhysicsValues.HorizontalPlayerVelocity;
					break;
				}
			}

			float halfJumpObstacleWidth = PhysicsValues.GetPlainJumpLength() / 2.0f;

			Console.WriteLine("safety: " + (MaximumJumpObstacleJumpTimingOffset * PhysicsValues.HorizontalPlayerVelocity));
			Console.WriteLine("minReq: " + minimumHorizontalDistanceRequired);
			Console.WriteLine("halfPl: " + PhysicsValues.GetHalfPlayerHitboxWidth());

			halfJumpObstacleWidth -= MaximumJumpObstacleJumpTimingOffset * PhysicsValues.HorizontalPlayerVelocity;
			halfJumpObstacleWidth -= minimumHorizontalDistanceRequired;
			halfJumpObstacleWidth -= PhysicsValues.GetHalfPlayerHitboxWidth();

			return halfJumpObstacleWidth * 2;
		}

		public static float GetJumpObstacleHeight()
		{
			return PhysicsValues.PlayerHitboxHeight * 0.3f;
		}


		public static float GetProjectileYOffset()
		{
			return PhysicsValues.PlayerHitboxHeight * 0.6f;
		}
		public static readonly float ProjectilePlayerHeightFactor = 0.6f;

		public static readonly float ProjectileSafetyTime = 1f;

		public static readonly float TimeBeforeFirstChasmCollectible = 0.25f;
		public static readonly float TimeAfterLastChasmCollectible = 0.25f;




		public static float GetPlainJumpDuration()
		{
			return PhysicsValues.GetPlainJumpDuration();
		}




		public static float GetPlayerVelocity()
		{
			return PhysicsValues.HorizontalPlayerVelocity;
		}

		public static float GetXPositionByTime(float time)
		{
			return MusicStartPositionX + time * GetPlayerVelocity();
		}

		public static float GetYDifferenceAfterJump(float timeAfterJump)
		{
			int iterations = (int)Math.Round(timeAfterJump * GeneralValues.FPS);

			float jumpDistance = iterations * PhysicsValues.JumpAcceleration;

			float gravityDistance = (iterations * (iterations + 1)) / 2 * PhysicsValues.GetGravityAccelerationPerFrame();

			return jumpDistance - gravityDistance;
		}

		public static float GetChasmXDifference(float hangTime)
		{
			return (hangTime - MaximumChasmJumpTimingOffset) * GetPlayerVelocity();
		}

		public static float GetChasmYDifference(float hangTime)
		{
			int iterations = (int)Math.Round(hangTime * GeneralValues.FPS);

			float jumpDistance = iterations * PhysicsValues.JumpAcceleration;

			float gravityDistance = (iterations * (iterations + 1)) / 2 * PhysicsValues.GetGravityAccelerationPerFrame();

			return jumpDistance - gravityDistance;
		}

	}
}

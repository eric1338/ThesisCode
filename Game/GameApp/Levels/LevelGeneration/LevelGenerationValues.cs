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

		public static readonly float TimeToJump = 1;


		// Applicability Thresholds

		public static readonly float HeldNoteApplicabilityThreshold = 0.2f;
		public static readonly float MultipleBeatsApplicabilityThreshold = 0.1f;
		public static readonly float SingleBeatsApplicabilityThreshold = 0.3f;
		public static readonly float FillUpElementsApplicabilityThreshold = 0.4f;

		// Rest

		public static readonly Vector2 PlayerStartPosition = new Vector2(0, 0);

		public static readonly float FirstGroundLeftX = -10;

		public static readonly float DuckingObstacleEnteringSafetyTime = 0.3f;
		public static readonly float DuckingObstacleLeavingSafetyTime = 0.3f;

		public static readonly float ProjectileSafetyTime = 1f;

		public static readonly float MaximumTimeMarginForMultipleBeats = 1f;




		public static float GetJumpDuration()
		{
			return 2;
			//throw new NotImplementedException();
		}



		public static readonly float AverageTiming = 0.2f;

		public static readonly float MaximumTimingTolerance = 0.4f;


		public static float GetPlayerVelocity()
		{
			return PhysicsValues.HorizontalPlayerVelocity;
		}

		public static float GetXPositionByTime(float time)
		{
			return PlayerStartPosition.X + time * GetPlayerVelocity();
		}

		public static float GetChasmXDifference(float hangTime)
		{
			return (hangTime - MaximumTimingTolerance) * GetPlayerVelocity();
		}

		public static float GetChasmYDifference(float hangTime)
		{
			int iterations = (int)Math.Round(hangTime * GeneralValues.FPS);

			float y = 0;
			float yAcceleration = PhysicsValues.JumpAcceleration;

			for (int i = 0; i < iterations; i++)
			{
				y += yAcceleration;

				yAcceleration -= PhysicsValues.GetGravityAccelerationPerFrame();
			}

			return y;
		}


	}
}

using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class LevelAnalysis
	{

		public static Ground GetGroundBelowVector(Level level, Vector2 vector)
		{
			foreach (Ground ground in level.Grounds)
			{
				if (vector.X > ground.LeftX && vector.X < ground.RightX)
				{
					return ground;
				}
			}

			return null;
		}

		public static Ground GetGroundLeftFromVector(Level level, Vector2 vector)
		{
			Ground bestGround = null;
			float bestDistance = 99999999.0f;

			foreach (Ground ground in level.Grounds)
			{
				if (ground.RightX > vector.X) continue;

				float distance = vector.X - ground.RightX;

				if (distance < bestDistance)
				{
					bestGround = ground;
					bestDistance = distance;
				}
			}

			return bestGround;
		}

		public static bool IsVectorOnGround(Level level, Vector2 vector)
		{
			Ground groundBelowVector = GetGroundBelowVector(level, vector);

			if (groundBelowVector == null) return false;

			return IsVectorOnGround(groundBelowVector, vector);
		}

		private static bool IsVectorOnGround(Ground ground, Vector2 vector)
		{
			return vector.Y == ground.TopY;
		}

	}
}

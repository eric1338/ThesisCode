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

		public static Ground GetGroundBelowPlayer(Level level, Vector2 playerPosition)
		{
			foreach (Ground ground in level.Grounds)
			{
				if (playerPosition.X > ground.LeftX && playerPosition.X < ground.RightX)
				{
					return ground;
				}
			}

			return null;
		}

		// TODO: Rumpf beider Methoden vereinigen

		public static Ground GetGroundLeftFromPlayer(Level level, Vector2 playerPosition)
		{
			Ground bestGround = null;
			float bestDistance = 99999999.0f;

			foreach (Ground ground in level.Grounds)
			{
				if (ground.RightX > playerPosition.X) continue;

				float distance = playerPosition.X - ground.RightX;

				if (distance < bestDistance)
				{
					bestGround = ground;
					bestDistance = distance;
				}
			}

			return bestGround;
		}

		public static Ground GetGroundRightFromPlayer(Level level, Vector2 playerPosition)
		{
			Ground bestGround = null;
			float bestDistance = 99999999.0f;

			foreach (Ground ground in level.Grounds)
			{
				if (ground.LeftX < playerPosition.X) continue;

				float distance = ground.LeftX - playerPosition.X;

				if (distance < bestDistance)
				{
					bestGround = ground;
					bestDistance = distance;
				}
			}

			return bestGround;
		}

		public static bool IsPlayerOnGround(Level level, Vector2 playerPosition)
		{
			Ground groundBelowPlayer = GetGroundBelowPlayer(level, playerPosition);

			if (groundBelowPlayer == null) return false;

			return IsPlayerOnGround(groundBelowPlayer, playerPosition);
		}

		public static bool IsPlayerOnGround(Ground ground, Vector2 playerPosition)
		{
			return playerPosition.Y == ground.TopY;
			//return playerPosition.Y < (ground.TopY - Utils.Constants.Epsilon);
		}

	}
}

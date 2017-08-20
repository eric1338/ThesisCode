using GameApp.Gameplay;
using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay.Physics
{
	class GamePhysics
	{

		private Level level;

		private float verticalVelocity = 0f;

		public GamePhysics(Level level)
		{
			this.level = level;
		}
		
		private bool IsPlayerOnGround(Vector2 playerPosition)
		{
			return LevelAnalysis.IsPlayerOnGround(level, playerPosition);
		}

		public void PerformJump(Vector2 playerPosition)
		{
			if (IsPlayerOnGround(playerPosition))
			{
				verticalVelocity = PhysicsValues.JumpAcceleration;
			}
		}

		public void ResetVerticalVelocity()
		{
			verticalVelocity = 0;
		}

		public Vector2 DoPlayerPhysics(Vector2 playerPosition)
		{
			if (!IsPlayerOnGround(playerPosition + new Vector2(0.2f, 0.0f)))
			{
				verticalVelocity -= PhysicsValues.GravityAcceleration;
			}

			return playerPosition + new Vector2(PhysicsValues.HorizontalPlayerVelocity, verticalVelocity);
		}

	}
}

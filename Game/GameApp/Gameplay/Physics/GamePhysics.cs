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
			Vector2 leftFoot = PhysicsValues.GetLeftFootPosition(playerPosition);
			Vector2 rightFoot = PhysicsValues.GetRightFootPosition(playerPosition);

			return LevelAnalysis.IsVectorOnGround(level, leftFoot) || LevelAnalysis.IsVectorOnGround(level, rightFoot);
		}

		public void PerformJump(Vector2 playerPosition)
		{
			if (IsPlayerOnGround(playerPosition))
			{
				verticalVelocity = PhysicsValues.GetJumpAcceleration();
			}
		}

		public void ResetVerticalVelocity()
		{
			verticalVelocity = 0;
		}

		public Vector2 DoPlayerPhysics(Vector2 playerPosition)
		{
			if (!IsPlayerOnGround(playerPosition))
			{
				verticalVelocity -= PhysicsValues.GetGravityAccelerationPerFrame();
			}

			return playerPosition + new Vector2(PhysicsValues.GetHorizontalPlayerVelocityPerFrame(), verticalVelocity);
		}

	}
}

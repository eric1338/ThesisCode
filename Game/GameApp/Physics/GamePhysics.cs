using GameApp.Gameplay;
using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Physics
{
	class GamePhysics
	{

		private float epsilon = 0.0001f;
		private float smallEpsilon = 0.00005f;

		private Level level;

		private float currentPlayerV = 1.0f;
		private float currentPlayerA = 0.0f;

		// TODO: horizontal Collision Detection (für Sprünge)

		public void PerformJump()
		{
			if (LevelAnalysis.IsPlayerOnGround(level, Vector2.Zero))
			{

			}
		}




		public Vector2 DoPlayerPhysics()
		{
			return Vector2.Zero;
		}


		private void CalculateGravity()
		{


			Vector2 newPosition = Vector2.Zero;
			Vector2 calculatedNewPosition = Vector2.Zero;

			Ground groundBelowNewPosition = LevelAnalysis.GetGroundBelowPlayer(level, calculatedNewPosition);

			if (groundBelowNewPosition == null)
			{
				newPosition = calculatedNewPosition;
				return;
			}

			if (LevelAnalysis.IsPlayerOnGround(groundBelowNewPosition, calculatedNewPosition))
			{
				newPosition = new Vector2(calculatedNewPosition.X, groundBelowNewPosition.TopY + smallEpsilon);
			}
		}


	}
}

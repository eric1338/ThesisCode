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

		private Level level;

		private Vector2 velocity = Vector2.Zero;
		private Vector2 acceleration = Vector2.Zero;

		// TODO: horizontal Collision Detection (für Sprünge)

		
		public GamePhysics(Level level)
		{
			this.level = level;
		}
		
		private bool IsPlayerOnGround(Vector2 playerPosition)
		{
			return LevelAnalysis.IsPlayerOnGround(level, playerPosition);
		}

		public void PerformJump()
		{
			if (LevelAnalysis.IsPlayerOnGround(level, Vector2.Zero))
			{
				acceleration += new Vector2(0, 0.01f);
			}
		}

		

		public Vector2 DoPlayerPhysics(Vector2 playerPosition)
		{
			if (IsPlayerOnGround(playerPosition))
			{
				velocity = new Vector2(0.002f, 0);
			}
			else
			{
				ApplyGravity(playerPosition);
			}


			velocity += acceleration;

			playerPosition += velocity;

			// TODO: check if in Ground

			acceleration = Vector2.Zero;

			return playerPosition + velocity;
		}


		private void ApplyGravity(Vector2 playerPosition)
		{
			Ground groundBelowNewPosition = LevelAnalysis.GetGroundBelowPlayer(level, playerPosition);

			if (groundBelowNewPosition == null || !IsPlayerOnGround(playerPosition))
			{
				acceleration += new Vector2(0, -0.0002f);
			}
		}


	}
}

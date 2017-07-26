using GameApp.Gameplay;
using GameApp.Levels;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Visual
{
	class LevelDrawer
	{

		private Level level;

		private Vector2 visualCenter = Vector2.Zero;

		private float zoomFactor = 2f;

		public LevelDrawer(Level level)
		{
			this.level = level;
		}


		public void DrawLevel(LevelProgression levelProgression)
		{
			CalculateVisualCenter(levelProgression);

			DrawGrounds();
			DrawCollectibles(levelProgression);
			DrawPlayer(levelProgression.CurrentPlayerPosition);

			GL.Color3(1.0f, 0.0f, 0.3f);

			//DrawOpenGLLine(new Vector2(-1, 0), new Vector2(1, 0));
			//DrawOpenGLLine(new Vector2(0, -1), new Vector2(0, 1));
		}

		private void CalculateVisualCenter(LevelProgression levelProgression)
		{
			// TODO: 0.5f -> stattdessen variabel (Spielergröße / Zoom)
			visualCenter = GetVisualCenter(levelProgression) + new Vector2(0.5f, 0.5f);
		}

		private Vector2 GetVisualCenter(LevelProgression levelProgression)
		{
			Vector2 playerPosition = levelProgression.CurrentPlayerPosition;

			Ground groundBelowPlayer = LevelAnalysis.GetGroundBelowPlayer(level, playerPosition);

			if (groundBelowPlayer != null)
			{
				return new Vector2(playerPosition.X, groundBelowPlayer.TopY);
			}

			Ground groundLeftFromPlayer = LevelAnalysis.GetGroundLeftFromPlayer(level, playerPosition);
			Ground groundRightFromPlayer = LevelAnalysis.GetGroundRightFromPlayer(level, playerPosition);

			if (groundLeftFromPlayer == null || groundRightFromPlayer == null)
			{
				return playerPosition;
			}

			float chasmWidth = groundRightFromPlayer.LeftX - groundLeftFromPlayer.RightX;

			float rightPercentage = (playerPosition.X - groundLeftFromPlayer.RightX) / chasmWidth;

			float y = (1 - rightPercentage) * groundLeftFromPlayer.TopY + rightPercentage * groundRightFromPlayer.TopY;

			return new Vector2(playerPosition.X, y);
		}

		// TODO: Auslagern + Berechnen
		private float GetAspectRatio()
		{
			return 1.77777777f;
		}

		private float GetScreenWidth()
		{
			return 2 * zoomFactor * GetAspectRatio();
		}

		private bool IsCoordOnScreen(float coordX)
		{
			float halfScreenWidth = GetScreenWidth() / 1.98f;

			float leftestScreenX = visualCenter.X - halfScreenWidth;
			float rightestScreenX = visualCenter.X + halfScreenWidth;

			return coordX > leftestScreenX && coordX < rightestScreenX;
		}

		private bool IsObjectOnScreen(float leftestObjectX, float rightestObjectX)
		{
			return IsCoordOnScreen(leftestObjectX) || IsCoordOnScreen(rightestObjectX);
		}


		private void DrawPlayer(Vector2 playerPosition)
		{
			GL.Color3(0.1f, 0.4f, 1.0f);

			float x1 = playerPosition.X - 0.2f;
			float x2 = playerPosition.X + 0.2f;
			float y1 = playerPosition.Y + 0.8f;

			Vector2 v1 = new Vector2(x1, y1);
			Vector2 v2 = new Vector2(x2, playerPosition.Y);

			DrawSquare(v1, v2);
		}


		private void DrawGrounds()
		{
			foreach (Ground ground in level.Grounds)
			{
				if (IsObjectOnScreen(ground.LeftX, ground.RightX)) DrawGround(ground);
			}
		}

		private void DrawGround(Ground ground)
		{
			GL.Color3(0.2f, 0.1f, 0.0f);

			Vector2 v1 = new Vector2(ground.LeftX, ground.TopY);
			Vector2 v2 = new Vector2(ground.RightX, -10.0f);

			DrawSquare(v1, v2);
		}

		private float GetCollectibleWidth()
		{
			return 0.2f;
		}

		private void DrawCollectibles(LevelProgression levelProgression)
		{
			foreach (Collectible collectible in levelProgression.RemainingCollectibles)
			{
				float halfCollectibleWidth = GetCollectibleWidth() / 1.98f;

				float leftX = collectible.Position.X - halfCollectibleWidth;
				float rightX = collectible.Position.X + halfCollectibleWidth;

				if (IsObjectOnScreen(leftX, rightX)) DrawCollectible(collectible);
			}
		}

		private void DrawCollectible(Collectible collectible)
		{
			GL.Color3(1.0f, 0.0f, 0.4f);

			float x = collectible.Position.X;
			float y = collectible.Position.Y;
			float h = GetCollectibleWidth() / 2.0f;

			Vector2 v1 = new Vector2(x - h, y + h);
			Vector2 v2 = new Vector2(x + h, y - h);

			DrawSquare(v1, v2);
		}


		private Vector2 GetTransformedVector(Vector2 vector)
		{
			Vector2 newVector = vector - visualCenter;

			newVector *= (1.0f / zoomFactor);

			return newVector;
		}


		private void DrawOpenGLLine(Vector2 point1, Vector2 point2)
		{
			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(point1);
			GL.Vertex2(point2);
			GL.End();
		}

		private void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{

			topLeft = GetTransformedVector(topLeft);
			bottomRight = GetTransformedVector(bottomRight);

			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.End();
		}



		public void Test(bool value)
		{
			if (value) GL.Color3(0.6f, 0.6f, 0.6f);
			else GL.Color3(0.4f, 0.4f, 0.4f);

			Vector2 topLeft = new Vector2(0.9f, 0.95f);
			Vector2 bottomRight = new Vector2(0.95f, 0.9f);

			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.End();
		}

	}
}

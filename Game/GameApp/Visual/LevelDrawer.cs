using GameApp.Levels;
using OpenTK;
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

		public LevelDrawer(Level level)
		{
			this.level = level;
		}


		public void DrawLevel(LevelProgression levelProgression)
		{
			CalculateVisualCenter(levelProgression);

			DrawBackground();

			DrawGrounds();
			DrawObstacles(levelProgression);
			DrawCollectibles(levelProgression);
			DrawPlayer(levelProgression);

			DrawDebugLines();

			BasicGraphics.SetColor(1.0f, 0.0f, 0.3f);

			//DrawOpenGLLine(new Vector2(-1, 0), new Vector2(1, 0));
			//DrawOpenGLLine(new Vector2(0, -1), new Vector2(0, 1));
		}

		private void CalculateVisualCenter(LevelProgression levelProgression)
		{
			visualCenter = GetVisualCenter(levelProgression) + VisualValues.ScreenCenterOffset;
		}

		private Vector2 GetVisualCenter(LevelProgression levelProgression)
		{
			Vector2 playerPosition = levelProgression.CurrentPlayerPosition;

			Ground groundBelowPlayer = LevelAnalysis.GetGroundBelowVector(level, playerPosition);

			if (groundBelowPlayer != null)
			{
				return new Vector2(playerPosition.X, groundBelowPlayer.TopY);
			}

			Ground groundLeftFromPlayer = LevelAnalysis.GetGroundLeftFromVector(level, playerPosition);
			Ground groundRightFromPlayer = LevelAnalysis.GetGroundRightFromVector(level, playerPosition);

			if (groundLeftFromPlayer == null || groundRightFromPlayer == null)
			{
				return playerPosition;
			}

			float chasmWidth = groundRightFromPlayer.LeftX - groundLeftFromPlayer.RightX;

			float rightPercentage = Utils.MyMath.Smoothstep(groundLeftFromPlayer.RightX, groundRightFromPlayer.LeftX, playerPosition.X);

			//float rightPercentage = (playerPosition.X - groundLeftFromPlayer.RightX) / chasmWidth;

			float y = (1 - rightPercentage) * groundLeftFromPlayer.TopY + rightPercentage * groundRightFromPlayer.TopY;

			return new Vector2(playerPosition.X, y);
		}

		private float GetScreenWidth()
		{
			return 2 * VisualValues.ZoomFactor * VisualValues.GetAspectRatio();
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
			return true;

			// TODO: evtl wieder (richtig) implementieren
			//return IsCoordOnScreen(leftestObjectX) || IsCoordOnScreen(rightestObjectX);
		}

		private void DrawBackground()
		{
			BasicGraphics.SetColor(0.4f, 0.8f, 1.0f);

			DrawSquare(new Vector2(-2.1f, 1.1f), new Vector2(2.1f, -1.1f), false);
		}

		private void DrawPlayer(LevelProgression levelProgression)
		{
			if (levelProgression.IsPlayerInGodmode()) BasicGraphics.SetColor(1.0f, 1.0f, 1.0f);
			else if (levelProgression.IsPlayerInHittingMode()) BasicGraphics.SetColor(0.2f, 0.6f, 0.35f);
			else BasicGraphics.SetColor(0.2f, 0.35f, 0.6f);

			Vector2 playerPosition = levelProgression.CurrentPlayerPosition;

			float halfPlayerWidth = VisualValues.PlayerWidth / 2;
			float halfPlayerHeight = VisualValues.PlayerHeight / 2;

			float x1 = playerPosition.X - halfPlayerWidth;
			float x2 = playerPosition.X + halfPlayerWidth;
			float y1 = playerPosition.Y + VisualValues.PlayerHeight;

			if (!levelProgression.IsPlayerStanding)
			{
				x1 = playerPosition.X - halfPlayerHeight;
				x2 = playerPosition.X + halfPlayerHeight;
				y1 = playerPosition.Y + VisualValues.PlayerWidth;
			}

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
			BasicGraphics.SetColor(0.7f, 0.2f, 0.3f);

			Vector2 v1 = new Vector2(ground.LeftX, ground.TopY);
			Vector2 v2 = new Vector2(ground.RightX, -10.0f);

			DrawSquare(v1, v2);
		}

		private void DrawObstacles(LevelProgression levelProgression)
		{
			foreach (Obstacle obstacle in level.SolidObstacles) DrawSolidObstacle(obstacle);

			foreach (Obstacle obstacle in level.DestructibleObstacles)
			{
				if (levelProgression.IsObstacleAlreadyDestructed(obstacle)) continue;

				DrawDestructibleObstacle(obstacle, levelProgression);
			}
		}

		private void DrawSolidObstacle(Obstacle solidObstacle)
		{
			BasicGraphics.SetColor(1.0f, 0.6f, 0.4f);

			DrawSquare(solidObstacle.TopLeftCorner, solidObstacle.BottomRightCorner);
		}

		private void DrawDestructibleObstacle(Obstacle destructibleObstacle, LevelProgression levelProgression)
		{
			BasicGraphics.SetColor(1.0f, 0.9f, 0.4f);

			DrawSquare(destructibleObstacle.TopLeftCorner, destructibleObstacle.BottomRightCorner);
		}

		private void DrawCollectibles(LevelProgression levelProgression)
		{
			foreach (Collectible collectible in level.Collectibles)
			{
				if (levelProgression.IsCollectibleAlreadyCollected(collectible)) continue;

				float halfCollectibleWidth = VisualValues.HalfCollectibleWidthHeight * 0.95f;

				float leftX = collectible.Position.X - halfCollectibleWidth;
				float rightX = collectible.Position.X + halfCollectibleWidth;

				if (IsObjectOnScreen(leftX, rightX)) DrawCollectible(collectible);
			}
		}

		private void DrawCollectible(Collectible collectible)
		{
			BasicGraphics.SetColor(1.0f, 0.0f, 0.4f);

			float x = collectible.Position.X;
			float y = collectible.Position.Y;
			float h = VisualValues.HalfCollectibleWidthHeight;

			Vector2 v1 = new Vector2(x - h, y + h);
			Vector2 v2 = new Vector2(x + h, y - h);

			DrawSquare(v1, v2);
		}



		private Vector2 GetTransformedVector(Vector2 vector)
		{
			Vector2 newVector = vector - visualCenter;

			newVector *= (1.0f / VisualValues.ZoomFactor);

			return newVector;
		}



		private void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			DrawSquare(topLeft, bottomRight, true);
		}

		private void DrawSquare(Vector2 topLeft, Vector2 bottomRight, bool transformVectors)
		{
			if (transformVectors)
			{
				topLeft = GetTransformedVector(topLeft);
				bottomRight = GetTransformedVector(bottomRight);
			}

			BasicGraphics.DrawSquare(topLeft, bottomRight);
		}


		
		class DebugLine
		{
			public Vector2 Vector1 { get; set; }
			public Vector2 Vector2 { get; set; }
			public Vector3 Color { get; set; }

			public DebugLine(Vector2 vector1, Vector2 vector2, Vector3 color)
			{
				Vector1 = vector1;
				Vector2 = vector2;
				Color = color;
			}
		}


		private static List<DebugLine> debugLines = new List<DebugLine>();

		public static void AddDebugLine(Vector2 vector1, Vector2 vector2, Vector3 color)
		{
			debugLines.Add(new DebugLine(vector1, vector2, color));
		}

		public static void AddDebugLines(List<Vector2> vectors, Vector3 color)
		{
			int vectorCount = vectors.Count;

			for (int i = 0; i < vectorCount - 1; i++)
			{
				AddDebugLine(vectors[i], vectors[i + 1], color);
			}

			AddDebugLine(vectors[vectorCount - 1], vectors[0], color);
		}

		public static void AddDebugSquare(Vector2 vector1, Vector2 vector2, Vector2 vector3, Vector2 vector4, Vector3 color)
		{
			AddDebugLine(vector1, vector2, color);
			AddDebugLine(vector2, vector3, color);
			AddDebugLine(vector3, vector4, color);
			AddDebugLine(vector4, vector1, color);
		}

		public void DrawDebugLines()
		{
			foreach (DebugLine debugLine in debugLines)
			{
				BasicGraphics.SetColor(debugLine.Color);
				
				BasicGraphics.DrawOpenGLLine(GetTransformedVector(debugLine.Vector1), GetTransformedVector(debugLine.Vector2));
			}

			debugLines.Clear();
		}

	}
}

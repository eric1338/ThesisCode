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

		private Vector3 DangerColor = new Vector3(1.0f, 0.0f, 0.2f);

		private Texture playerStandardTexture;
		private Texture playerDefendingTexture;
		private Texture playerGhostTexture;

		public Texture overlayGameComplete;
		public Texture overlayGamePaused;
		public Texture overlayPressX;

		public Texture ratingNone;
		public Texture ratingHalf;
		public Texture ratingFull;

		private Texture tutorialJumpTexture;
		private Texture tutorialDuckTexture;
		private Texture tutorialDeflectTexture;
		private Texture tutorialNextTutorial;

		public LevelDrawer(Level level)
		{
			Textures.Instance.LoadTextures();

			playerStandardTexture = Textures.Instance.PlayerStandardTexture;
			playerDefendingTexture = Textures.Instance.PlayerDefendingTexture;
			playerGhostTexture = Textures.Instance.PlayerGhostTexture;

			overlayGameComplete = Textures.Instance.OverlayGameComplete;
			overlayGamePaused = Textures.Instance.OverlayGamePaused;
			overlayPressX = Textures.Instance.OverlayPressX;

			ratingNone = Textures.Instance.RatingNone;
			ratingHalf = Textures.Instance.RatingHalf;
			ratingFull = Textures.Instance.RatingFull;

			tutorialJumpTexture = Textures.Instance.TutorialJumpTexture;
			tutorialDuckTexture = Textures.Instance.TutorialDuckTexture;
			tutorialDeflectTexture = Textures.Instance.TutorialDeflectTexture;
			tutorialNextTutorial = Textures.Instance.TutorialNextTutorial;

			this.level = level;
		}


		public void DrawLevel(LevelProgression levelProgression)
		{
			BasicGraphics.textureDrawAttempts = 0;

			CalculateVisualCenter(levelProgression);

			DrawBackground(levelProgression);

			DrawGrounds();
			DrawObstacles(levelProgression);
			DrawCollectibles(levelProgression);
			DrawProjectiles(levelProgression);
			DrawPlayer(levelProgression);

			//DrawDebugLines();

			if (levelProgression.IsLevelComplete) DrawLevelCompleteScreen(0.92f);

			if (level.IsTutorial) DrawTutorialInfoTexture(levelProgression);

			if (levelProgression.IsGamePaused) DrawPauseScreen();
		}

		private void CalculateVisualCenter(LevelProgression levelProgression)
		{
			visualCenter = GetVisualCenter(levelProgression) + VisualValues.ScreenCenterOffset;
		}

		private bool hasPlayerLeftCurrentGround = false;
		private float lastGroundTopY = 99999;

		private Vector2 GetVisualCenter(LevelProgression levelProgression)
		{
			Vector2 playerPosition = levelProgression.CurrentPlayerPosition;
			Ground groundBelowPlayer = LevelAnalysis.GetGroundBelowVector(level, playerPosition);

			if (hasPlayerLeftCurrentGround)
			{
				if (groundBelowPlayer != null)
				{
					if (Math.Abs(playerPosition.Y - groundBelowPlayer.TopY) < 0.001f) hasPlayerLeftCurrentGround = false;
				}

				return new Vector2(playerPosition.X, Math.Min(lastGroundTopY, playerPosition.Y));
			}

			if (groundBelowPlayer != null)
			{
				return new Vector2(playerPosition.X, groundBelowPlayer.TopY);
			}

			if (!hasPlayerLeftCurrentGround) hasPlayerLeftCurrentGround = true;

			Ground groundLeftFromPlayer = LevelAnalysis.GetGroundLeftFromVector(level, playerPosition);
			lastGroundTopY = groundLeftFromPlayer.TopY;

			if (groundLeftFromPlayer == null) return playerPosition;

			return new Vector2(playerPosition.X, Math.Min(groundLeftFromPlayer.TopY, playerPosition.Y));
		}

		private float GetScreenWidth()
		{
			return 2 * VisualValues.ZoomFactor * VisualValues.GetAspectRatio();
		}

		private bool IsObjectOnScreen(float leftestX, float rightestX)
		{
			return true;
		}

		private bool IsCoordOnScreen(float coordX)
		{
			float halfScreenWidth = GetScreenWidth() / 1.98f;

			float leftestScreenX = visualCenter.X - halfScreenWidth;
			float rightestScreenX = visualCenter.X + halfScreenWidth;

			return coordX > leftestScreenX && coordX < rightestScreenX;
		}

		private bool IsCoordLeftOfScreen(float coordX)
		{
			return coordX < (visualCenter.X - (GetScreenWidth() / 1.98f));
		}

		private bool IsCoordRightOfScreen(float coordX)
		{
			return coordX > (visualCenter.X + (GetScreenWidth() / 1.98f));
		}


		//
		// LEVELELEMENTS
		//

		private void DrawBackground(LevelProgression levelProgression)
		{
			BasicGraphics.SetColor(0.4f, 0.8f, 1.0f);

			DrawSquare(new Vector2(-2.1f, 1.1f), new Vector2(2.1f, -1.1f), false);

			//DrawClouds(levelProgression);
		}

		private void DrawClouds(LevelProgression levelProgression)
		{
			BasicGraphics.SetColor(0.9f, 0.9f, 1.0f);

			float time = levelProgression.GetSecondsPlayed();
			
			Vector2 cloud1TopLeftCorner = new Vector2(1, 0.8f) - new Vector2(time * 0.05f, 0);;
			Vector2 cloud1BottomRightCorner = cloud1TopLeftCorner + new Vector2(0.3f, -0.1f);

			BasicGraphics.DrawSquare(cloud1TopLeftCorner, cloud1BottomRightCorner);
		}

		private void DrawPlayer(LevelProgression levelProgression)
		{
			Texture playerTexture;

			if (levelProgression.IsPlayerInGodmode()) playerTexture = playerGhostTexture;
			else if (levelProgression.IsPlayerDefending) playerTexture = playerDefendingTexture;
			else playerTexture = playerStandardTexture;

			Vector2 playerPosition = levelProgression.CurrentPlayerPosition;
			
			float x1 = playerPosition.X - (VisualValues.PlayerWidth / 2.0f);
			float x2 = playerPosition.X + (VisualValues.PlayerWidth / 2.0f);
			float y1 = playerPosition.Y + VisualValues.PlayerHeight;

			if (!levelProgression.IsPlayerStanding)
			{
				x1 = playerPosition.X - (VisualValues.PlayerHeight / 2.0f);
				x2 = playerPosition.X + (VisualValues.PlayerHeight / 2.0f);
				y1 = playerPosition.Y + VisualValues.PlayerWidth;
			}

			Vector2 v1 = new Vector2(x1, y1);
			Vector2 v2 = new Vector2(x2, playerPosition.Y);

			if (levelProgression.IsPlayerStanding) DrawAdjustedTexture(playerTexture, v1, v2);
			else DrawAdjustedTextureVertically(playerTexture, v1, v2);
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
			BasicGraphics.SetColor(0.22f, 0.13f, 0.11f);

			Vector2 v1 = new Vector2(ground.LeftX, ground.TopY);
			Vector2 v2 = new Vector2(ground.RightX, ground.TopY - 99999f);

			//Vector2 v2 = new Vector2(ground.RightX, ground.TopY - 0.15f);
			//DrawMultipleHorizontalTextures(groundTopTexture, v1, v2);

			DrawSquare(v1, v2);

			DrawGround2(ground);
		}



		private void DrawGround2(Ground ground)
		{
			float groundPartWidth = 2.4f;
			float groundMinimumHeight = 0.1f;
			float triangleYOffset = 0.05f;

			float leftYOffset = triangleYOffset;
			float rightYOffset = 0;

			float topY = ground.TopY;

			float leftX = ground.LeftX;
			float maxRightX = ground.RightX;

			BasicGraphics.SetColor(0.2f, 0.74f, 0.33f);

			while (leftX < maxRightX)
			{
				if (IsCoordRightOfScreen(leftX)) break;

				leftYOffset = leftYOffset > 0 ? 0 : triangleYOffset;
				rightYOffset = rightYOffset > 0 ? 0 : triangleYOffset;

				float fullRightX = leftX + groundPartWidth;

				if (IsCoordLeftOfScreen(fullRightX))
				{
					leftX = fullRightX;
					continue;
				}

				Vector2 topLeftCorner = new Vector2(leftX, topY);
				Vector2 bottomLeftCorner = new Vector2(leftX, topY - groundMinimumHeight - leftYOffset);

				Vector2 topRightCorner;
				Vector2 bottomRightCorner;

				if (fullRightX < maxRightX)
				{
					topRightCorner = new Vector2(fullRightX, topY);
					bottomRightCorner = new Vector2(fullRightX, topY - groundMinimumHeight - rightYOffset);
				}
				else
				{
					topRightCorner = new Vector2(maxRightX, topY);

					//float totalRightOffset = ((maxRightX - leftX) / groundPartWidth) * (groundMinimumHeight + rightYOffset);

					bottomRightCorner = new Vector2(maxRightX, (topY - groundMinimumHeight - rightYOffset));
				}

				topLeftCorner = GetTransformedVector(topLeftCorner);
				topRightCorner = GetTransformedVector(topRightCorner);
				bottomLeftCorner = GetTransformedVector(bottomLeftCorner);
				bottomRightCorner = GetTransformedVector(bottomRightCorner);

				BasicGraphics.DrawQuad(topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner);

				leftX = fullRightX;
			}
		}




		private void DrawObstacles(LevelProgression levelProgression)
		{
			foreach (Obstacle obstacle in level.SolidObstacles) DrawObstacle(obstacle);
		}

		private void DrawObstacle(Obstacle obstacle)
		{
			BasicGraphics.SetColor(DangerColor);

			DrawSquare(obstacle.TopLeftCorner, obstacle.BottomRightCorner);
		}



		private int collectibleMaximumValue = 60;

		private Dictionary<int, int> collectiblesStateValues = new Dictionary<int, int>();

		private void DrawCollectibles(LevelProgression levelProgression)
		{
			foreach (KeyValuePair<int, int> csv in collectiblesStateValues.ToList())
			{
				collectiblesStateValues[csv.Key] = csv.Value - 1;
			}

			foreach (Collectible collectible in level.Collectibles)
			{
				int id = collectible.ID;
				int stateValue = collectibleMaximumValue;

				if (levelProgression.IsCollectibleAlreadyCollected(collectible))
				{
					if (!collectiblesStateValues.ContainsKey(id))
					{
						collectiblesStateValues.Add(id, 60);
					}

					stateValue = collectiblesStateValues[id];
				}

				float halfCollectibleWidth = VisualValues.HalfCollectibleWidthHeight * 0.95f;

				float leftX = collectible.Position.X - halfCollectibleWidth;
				float rightX = collectible.Position.X + halfCollectibleWidth;

				if (IsObjectOnScreen(leftX, rightX)) DrawCollectible(collectible, stateValue);
			}
		}




		private void DrawCollectible(Collectible collectible, int stateValue)
		{
			float alpha = Math.Max(stateValue / (float)collectibleMaximumValue, 0);
			float sizeFactor = 1 - alpha;

			BasicGraphics.SetColor4(1.0f, 0.9f, 0.4f, alpha);

			float x = collectible.Position.X;
			float y = collectible.Position.Y;
			float add = VisualValues.HalfCollectibleWidthHeight * sizeFactor * 1.5f;
			float h = VisualValues.HalfCollectibleWidthHeight + add;

			Vector2 v1 = new Vector2(x - h, y + h);
			Vector2 v2 = new Vector2(x + h, y - h);

			DrawSquare(v1, v2);
		}

		private void DrawProjectiles(LevelProgression levelProgression)
		{
			foreach (Projectile projectile in level.Projectiles)
			{
				float halfProjectileWidth = VisualValues.HalfProjectileWidthHeight * 0.95f;

				Vector2 projectilePosition = levelProgression.GetProjectilePosition(projectile);

				float leftX = projectilePosition.X - halfProjectileWidth;
				float rightX = projectilePosition.X + halfProjectileWidth;

				if (IsObjectOnScreen(leftX, rightX)) DrawProjectile(projectilePosition);
			}
		}

		private void DrawProjectile(Vector2 projectilePosition)
		{
			BasicGraphics.SetColor(DangerColor);

			float x = projectilePosition.X;
			float y = projectilePosition.Y;
			float h = VisualValues.HalfProjectileWidthHeight;

			Vector2 v1 = new Vector2(x - h, y + h);
			Vector2 v2 = new Vector2(x + h, y - h);

			DrawSquare(v1, v2);
		}


		//
		// UI / INFOTEXTS
		//

		private void DrawPauseScreen()
		{
			DrawOverlayScreen(false);
		}

		private void DrawLevelCompleteScreen(float rating)
		{
			DrawOverlayScreen(true, rating);
		}

		private void DrawOverlayScreen(bool isLevelCompleteScreen, float rating = 0)
		{
			BasicGraphics.SetColor4(0.0f, 0.0f, 0.0f, 0.85f);
			BasicGraphics.DrawSquare(new Vector2(-2, 2), new Vector2(2, -2));

			Texture headerTexture = isLevelCompleteScreen ? overlayGameComplete : overlayGamePaused;

			DrawTexture(headerTexture, new Vector2(-0.6f, 0.6f), new Vector2(0.6f, 0.36f));

			if (isLevelCompleteScreen) DrawRatings(rating);

			DrawTexture(overlayPressX, new Vector2(-0.6f, -0.4f), new Vector2(0.6f, -0.64f));
		}

		private void DrawRatings(float rating)
		{
			float ratingSymbolWidth = 0.25f;
			float ratingSymbolMargin = 0.03f;

			for (int i = 0; i < 5; i++)
			{
				float leftXWidthFactor = i - 2.5f;
				float rightXWidthFactor = i - 1.5f;

				float marginOffset = (i - 2) * ratingSymbolMargin;

				float leftX = leftXWidthFactor * ratingSymbolWidth + marginOffset;
				float rightX = rightXWidthFactor * ratingSymbolWidth + marginOffset;

				float topY = 0.105f;
				float bottomY = topY - ratingSymbolWidth;

				Texture ratingTexture;

				if (rating > i * 0.2f + 0.15f) ratingTexture = ratingFull;
				else if (rating > i * 0.2f + 0.05f) ratingTexture = ratingHalf;
				else ratingTexture = ratingNone;

				Vector2 topLeftCorner = new Vector2(leftX, topY);
				Vector2 bottomRightCorner = new Vector2(rightX, bottomY);

				DrawTexture(ratingTexture, topLeftCorner, bottomRightCorner);
			}
		}

		private void DrawTutorialInfoTexture(LevelProgression levelProgression)
		{
			Texture tutorialTexture;

			if (level.Name == "TutorialLevel1") tutorialTexture = tutorialJumpTexture;
			else if (level.Name == "TutorialLevel2") tutorialTexture = tutorialDuckTexture;
			else tutorialTexture = tutorialDeflectTexture;

			bool showNextTutorial = levelProgression.CurrentPlayerPosition.X > 15
				&& level.Name != "TutorialLevel3";

			Vector2 topLeftCorner = new Vector2(-0.6f, 0.8f);
			Vector2 bottomRightCorner = new Vector2(0.6f, 0.4f);

			if (showNextTutorial)
			{
				topLeftCorner = new Vector2(-1.3f, 0.8f);
				bottomRightCorner = new Vector2(-0.1f, 0.4f);
			}

			DrawTexture(tutorialTexture, topLeftCorner, bottomRightCorner);

			if (showNextTutorial)
			{
				Vector2 topLeftCorner2 = new Vector2(0.1f, 0.8f);
				Vector2 bottomRightCorner2 = new Vector2(1.3f, 0.4f);

				DrawTexture(tutorialNextTutorial, topLeftCorner2, bottomRightCorner2);
			}
		}





		//
		// BASICS
		//

		private Vector2 GetTransformedVector(Vector2 vector)
		{
			Vector2 newVector = vector - visualCenter;

			newVector *= (1.0f / VisualValues.ZoomFactor);

			return newVector;
		}

		private float GetTransformedXCoord(float xCoord)
		{
			return GetTransformedVector(new Vector2(xCoord, 0)).X;
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

		private void DrawAdjustedTexture(Texture texture, Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			topLeftCorner = GetTransformedVector(topLeftCorner);
			bottomRightCorner = GetTransformedVector(bottomRightCorner);

			BasicGraphics.DrawTextureWithUse(texture, topLeftCorner, bottomRightCorner);
		}

		private void DrawAdjustedTextureVertically(Texture texture, Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			topLeftCorner = GetTransformedVector(topLeftCorner);
			bottomRightCorner = GetTransformedVector(bottomRightCorner);

			BasicGraphics.DrawTextureVerticallyWithUse(texture, topLeftCorner, bottomRightCorner);
		}

		private void DrawTexture(Texture texture, Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			BasicGraphics.DrawTextureWithUse(texture, topLeftCorner, bottomRightCorner);
		}








		/*
		 


		


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





		private void DrawTextureMultipleTimes(Texture texture, Vector2 topLeftCorner,
			Vector2 bottomRightCorner, float textureWidth, float textureHeight)
		{
			Vector2 tlc = GetTransformedVector(topLeftCorner);
			Vector2 brc = GetTransformedVector(bottomRightCorner);


		}

		private void DrawMultipleHorizontalTextures(Texture texture,
			Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			float yDifference = topLeftCorner.Y - bottomRightCorner.Y;

			float topY = topLeftCorner.Y;
			float bottomY = bottomRightCorner.Y;

			float leftX = topLeftCorner.X;

			texture.BeginUse();

			while (leftX < bottomRightCorner.X)
			{
				if (IsCoordRightOfScreen(leftX)) break;

				float fullRightX = leftX + yDifference;
				
				if (IsCoordLeftOfScreen(fullRightX))
				{
					leftX += yDifference;
					continue;
				}

				if (fullRightX < bottomRightCorner.X)
				{
					Vector2 textureTopLeftCorner = GetTransformedVector(new Vector2(leftX, topY));
					Vector2 textureBottomRightCorner = GetTransformedVector(new Vector2(fullRightX, bottomY));
					
					BasicGraphics.DrawTexture(texture, textureTopLeftCorner, textureBottomRightCorner);
				}
				else
				{
					Vector2 textureTopLeftCorner = GetTransformedVector(new Vector2(leftX, topY));
					Vector2 textureBottomRightCorner = GetTransformedVector(new Vector2(bottomRightCorner.X, bottomY));

					float xPercentage = (bottomRightCorner.X - leftX) / yDifference;

					BasicGraphics.DrawPartialTexture(texture, textureTopLeftCorner, textureBottomRightCorner, xPercentage);
				}

				leftX += yDifference;
			}

			texture.EndUse();
		}

		*/


	}
}

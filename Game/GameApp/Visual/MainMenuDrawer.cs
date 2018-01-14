using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Visual
{
	class MainMenuDrawer
	{

		private Texture titleTexture;
		private Texture playTutorialTexture;
		private Texture importSongTexture;
		private Texture playSong1Texture;
		private Texture playSong2Texture;
		private Texture playSong3Texture;
		private Texture exitGameTexture;

		//private Vector2 menuTopLeftCorner = new Vector2(0.4f, -0.2f);
		private Vector2 menuTopLeftCorner = new Vector2(0.5f, 0.3f);

		private float menuItemHeight = 0.12f;
		private float menuItemYMargin = 0.05f;

		private float menuItemXPaddingFactor = 0.0014f;


		public MainMenuDrawer()
		{
			Textures.Instance.LoadTextures();

			titleTexture = Textures.Instance.TitleTexture;
			playTutorialTexture = Textures.Instance.PlayTutorialTexture;
			importSongTexture = Textures.Instance.ImportSongTexture;
			playSong1Texture = Textures.Instance.PlaySong1Texture;
			playSong2Texture = Textures.Instance.PlaySong2Texture;
			playSong3Texture = Textures.Instance.PlaySong3Texture;
			exitGameTexture = Textures.Instance.ExitGameTexture;
		}

		public void DrawMainMenu(int[] menuItemXPaddings)
		{
			float aspectRatio = VisualValues.GetAspectRatio();

			Vector2 titleTopLeftCorner = new Vector2(-aspectRatio, 1);
			Vector2 titleBottomRightCorner = new Vector2(aspectRatio, -1);

			BasicGraphics.DrawTextureWithUse(titleTexture, titleTopLeftCorner, titleBottomRightCorner);

			for (int i = 0; i < 6; i++)
			{
				DrawMenuItem(i, menuItemXPaddings[i]);
			}
		}

		public void DrawMenuItem(int index, int menuItemXPadding)
		{
			Texture texture;

			if (index == 0) texture = playTutorialTexture;
			else if (index == 1) texture = playSong1Texture;
			else if (index == 2) texture = playSong2Texture;
			else if (index == 3) texture = playSong3Texture;
			else if (index == 4) texture = importSongTexture;
			else texture = exitGameTexture;

			float xOffset = menuItemXPadding * menuItemXPaddingFactor;
			float yOffset = index * (menuItemHeight + menuItemYMargin);

			Vector2 topLeftCorner = menuTopLeftCorner + new Vector2(xOffset, -yOffset);
			Vector2 bottomRightCorner = topLeftCorner + new Vector2(menuItemHeight * 7, -menuItemHeight);

			BasicGraphics.DrawTextureWithUse(texture, topLeftCorner, bottomRightCorner);
		}

	}
}

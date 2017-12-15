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

		private Texture playTutorialTexture;
		private Texture playTestSongTexture;
		private Texture exitGameTexture;

		private Vector2 menuTopLeftCorner = new Vector2(0.3f, -0.2f);

		private float menuItemHeight = 0.12f;
		private float menuItemYMargin = 0.05f;

		private float menuItemXPaddingFactor = 0.0014f;


		public MainMenuDrawer()
		{
			Textures.Instance.LoadTextures();

			playTutorialTexture = Textures.Instance.PlayTutorialTexture;
			playTestSongTexture = Textures.Instance.PlayTestSongTexture;
			exitGameTexture = Textures.Instance.ExitGameTexture;
		}

		public void DrawMainMenu(int[] menuItemXPaddings)
		{
			for (int i = 0; i < 3; i++)
			{
				DrawMenuItem(i, menuItemXPaddings[i]);
			}
		}

		public void DrawMenuItem(int index, int menuItemXPadding)
		{
			Texture texture;

			if (index == 0) texture = playTutorialTexture;
			else if (index == 1) texture = playTestSongTexture;
			else texture = exitGameTexture;

			float xOffset = menuItemXPadding * menuItemXPaddingFactor;
			float yOffset = index * (menuItemHeight + menuItemYMargin);

			Vector2 topLeftCorner = menuTopLeftCorner + new Vector2(xOffset, -yOffset);
			Vector2 bottomRightCorner = topLeftCorner + new Vector2(menuItemHeight * 7, -menuItemHeight);

			BasicGraphics.DrawRectangularTexture(texture, topLeftCorner, bottomRightCorner);
		}

	}
}

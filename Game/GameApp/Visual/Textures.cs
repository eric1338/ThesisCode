using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Visual
{
	class Textures
	{

		public static Textures Instance = new Textures();

		private bool _texturesLoaded = false;

		public Texture PlayerTexture { get; set; }

		public Texture PlayTutorialTexture { get; set; }
		public Texture PlayTestSongTexture { get; set; }
		public Texture ExitGameTexture { get; set; }

		public void LoadTextures()
		{
			if (_texturesLoaded) return;

			PlayerTexture = TextureLoader.FromBitmap(Resources.playerTest);

			if (GeneralValues.UseEnglishLanguage)
			{
				PlayTutorialTexture = TextureLoader.FromBitmap(Resources.menuEnTutorial);
				PlayTestSongTexture = TextureLoader.FromBitmap(Resources.menuEnTestSong);
				ExitGameTexture = TextureLoader.FromBitmap(Resources.menuEnExit);
			}
			else
			{
				PlayTutorialTexture = TextureLoader.FromBitmap(Resources.menuDeTutorial);
				PlayTestSongTexture = TextureLoader.FromBitmap(Resources.menuDeTestSong);
				ExitGameTexture = TextureLoader.FromBitmap(Resources.menuDeExit);
			}

			_texturesLoaded = true;
		}
	}
}

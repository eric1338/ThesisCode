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

		public Texture TitleTexture { get; set; }

		public Texture PlayTutorialTexture { get; set; }
		public Texture PlayTestSongTexture { get; set; }
		public Texture ExitGameTexture { get; set; }

		public Texture OverlayGameComplete { get; set; }
		public Texture OverlayGamePaused { get; set; }
		public Texture OverlayPressX { get; set; }

		public Texture RatingNone { get; set; }
		public Texture RatingHalf { get; set; }
		public Texture RatingFull { get; set; }

		public Texture TutorialJumpTexture { get; set; }
		public Texture TutorialDuckTexture { get; set; }
		public Texture TutorialDeflectTexture { get; set; }

		public void LoadTextures()
		{
			if (_texturesLoaded) return;

			PlayerTexture = TextureLoader.FromBitmap(Resources.playerTest);

			TitleTexture = TextureLoader.FromBitmap(Resources.title);

			RatingNone = TextureLoader.FromBitmap(Resources.ratingNone);
			RatingHalf = TextureLoader.FromBitmap(Resources.ratingHalf);
			RatingFull = TextureLoader.FromBitmap(Resources.ratingFull);

			if (GeneralValues.UseEnglishLanguage)
			{
				PlayTutorialTexture = TextureLoader.FromBitmap(Resources.menuEnTutorial);
				PlayTestSongTexture = TextureLoader.FromBitmap(Resources.menuEnTestSong);
				ExitGameTexture = TextureLoader.FromBitmap(Resources.menuEnExit);

				OverlayGameComplete = TextureLoader.FromBitmap(Resources.overlayEnGameComplete);
				OverlayGamePaused = TextureLoader.FromBitmap(Resources.overlayEnGamePaused);
				OverlayPressX = TextureLoader.FromBitmap(Resources.overlayEnPressX);

				TutorialJumpTexture = TextureLoader.FromBitmap(Resources.tutorialEnJump);
				TutorialDuckTexture = TextureLoader.FromBitmap(Resources.tutorialEnDuck);
				TutorialDeflectTexture = TextureLoader.FromBitmap(Resources.tutorialEnDeflect);
			}
			else
			{
				PlayTutorialTexture = TextureLoader.FromBitmap(Resources.menuDeTutorial);
				PlayTestSongTexture = TextureLoader.FromBitmap(Resources.menuDeTestSong);
				ExitGameTexture = TextureLoader.FromBitmap(Resources.menuDeExit);

				OverlayGameComplete = TextureLoader.FromBitmap(Resources.overlayDeGameComplete);
				OverlayGamePaused = TextureLoader.FromBitmap(Resources.overlayDeGamePaused);
				OverlayPressX = TextureLoader.FromBitmap(Resources.overlayDePressX);

				TutorialJumpTexture = TextureLoader.FromBitmap(Resources.tutorialDeJump);
				TutorialDuckTexture = TextureLoader.FromBitmap(Resources.tutorialDeDuck);
				TutorialDeflectTexture = TextureLoader.FromBitmap(Resources.tutorialDeDeflect);
			}

			_texturesLoaded = true;
		}
	}
}

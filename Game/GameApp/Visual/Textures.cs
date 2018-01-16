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

		public Texture TitleTexture { get; set; }

		public Texture PlayerStandardTexture { get; set; }
		public Texture PlayerDefendingTexture { get; set; }
		public Texture PlayerGhostTexture { get; set; }

		public Texture PlayTutorialTexture { get; set; }
		public Texture ImportSongTexture { get; set; }
		public Texture PlaySong1Texture { get; set; }
		public Texture PlaySong2Texture { get; set; }
		public Texture PlaySong3Texture { get; set; }
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
		public Texture TutorialNextTutorial { get; set; }

		public void LoadTextures()
		{
			if (_texturesLoaded) return;

			TitleTexture = TextureLoader.FromBitmap(Resources.title);

			PlayerStandardTexture = TextureLoader.FromBitmap(Resources.playerStandard);
			PlayerDefendingTexture = TextureLoader.FromBitmap(Resources.playerDefending);
			PlayerGhostTexture = TextureLoader.FromBitmap(Resources.playerGhost);

			RatingNone = TextureLoader.FromBitmap(Resources.ratingNone);
			RatingHalf = TextureLoader.FromBitmap(Resources.ratingHalf);
			RatingFull = TextureLoader.FromBitmap(Resources.ratingFull);

			if (GeneralValues.UseEnglishLanguage)
			{
				PlayTutorialTexture = TextureLoader.FromBitmap(Resources.menuEnTutorial);
				ImportSongTexture = TextureLoader.FromBitmap(Resources.menuEnImportSong);
				PlaySong1Texture = TextureLoader.FromBitmap(Resources.menuEnPlaySong1);
				PlaySong2Texture = TextureLoader.FromBitmap(Resources.menuEnPlaySong2);
				PlaySong3Texture = TextureLoader.FromBitmap(Resources.menuEnPlaySong3);
				ExitGameTexture = TextureLoader.FromBitmap(Resources.menuEnExit);

				OverlayGameComplete = TextureLoader.FromBitmap(Resources.overlayEnGameComplete);
				OverlayGamePaused = TextureLoader.FromBitmap(Resources.overlayEnGamePaused);
				OverlayPressX = TextureLoader.FromBitmap(Resources.overlayEnPressX);

				TutorialJumpTexture = TextureLoader.FromBitmap(Resources.tutorialEnJump);
				TutorialDuckTexture = TextureLoader.FromBitmap(Resources.tutorialEnDuck);
				TutorialDeflectTexture = TextureLoader.FromBitmap(Resources.tutorialEnDeflect);
				TutorialNextTutorial = TextureLoader.FromBitmap(Resources.tutorialEnNextTutorial);
			}
			else
			{
				PlayTutorialTexture = TextureLoader.FromBitmap(Resources.menuDeTutorial);
				ImportSongTexture = TextureLoader.FromBitmap(Resources.menuDeImportSong);
				PlaySong1Texture = TextureLoader.FromBitmap(Resources.menuDePlaySong1);
				PlaySong2Texture = TextureLoader.FromBitmap(Resources.menuDePlaySong2);
				PlaySong3Texture = TextureLoader.FromBitmap(Resources.menuDePlaySong3);
				ExitGameTexture = TextureLoader.FromBitmap(Resources.menuDeExit);

				OverlayGameComplete = TextureLoader.FromBitmap(Resources.overlayDeGameComplete);
				OverlayGamePaused = TextureLoader.FromBitmap(Resources.overlayDeGamePaused);
				OverlayPressX = TextureLoader.FromBitmap(Resources.overlayDePressX);

				TutorialJumpTexture = TextureLoader.FromBitmap(Resources.tutorialDeJump);
				TutorialDuckTexture = TextureLoader.FromBitmap(Resources.tutorialDeDuck);
				TutorialDeflectTexture = TextureLoader.FromBitmap(Resources.tutorialDeDeflect);
				TutorialNextTutorial = TextureLoader.FromBitmap(Resources.tutorialDeNextTutorial);
			}

			_texturesLoaded = true;
		}
	}
}

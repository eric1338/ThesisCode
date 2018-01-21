using GameApp.Gameplay;
using GameApp.Screens.Input;
using GameApp.Levels;
using GameApp.Visual;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameApp.Audio;
using GameApp.Levels.LevelGeneration;

namespace GameApp.Screens
{
	class GameScreen : Screen
	{

		private LevelAttempt levelAttempt;

		private GameLogic gameLogic;
		private LevelDrawer levelDrawer;

		private MusicPlayer musicPlayer;

		private bool isGamePaused = false;

		private bool isPlayerAlwaysDefending = false;

		public GameScreen(MyGameWindow gameWindow, Level level, string fileDirectory = "") : base(gameWindow)
		{
			levelAttempt = new LevelAttempt(level);

			gameLogic = new GameLogic(levelAttempt);
			levelDrawer = new LevelDrawer(levelAttempt.Level);

			AddKeyToSingleUserActionMapping(Key.W, UserAction.Jump);
			AddKeyToSingleUserActionMapping(Key.Space, UserAction.Jump);
			
			AddKeyToProlongedUserActionMapping(Key.D, UserAction.Defend);
			AddKeyToProlongedUserActionMapping(Key.S, UserAction.Duck);
			AddKeyToProlongedUserActionMapping(Key.ControlLeft, UserAction.Duck);

			AddKeyToSingleUserActionMapping(Key.H, UserAction.ToggleAlwaysDefending);

			AddKeyToSingleUserActionMapping(Key.P, UserAction.TogglePauseGame);
			AddKeyToSingleUserActionMapping(Key.Escape, UserAction.TogglePauseGame);
			AddKeyToSingleUserActionMapping(Key.F10, UserAction.ResetLevel);
			AddKeyToSingleUserActionMapping(Key.N, UserAction.JumpToNextLevel);
			AddKeyToSingleUserActionMapping(Key.X, UserAction.ReturnToMainMenu);

			AddSingleUserActionToFunctionMapping(UserAction.Jump, Jump);
			AddSingleUserActionToFunctionMapping(UserAction.Hit, Hit);
			AddProlongedUserActionToFunctionMapping(UserAction.Duck, Duck);
			AddProlongedUserActionToFunctionMapping(UserAction.Defend, Defend);

			AddSingleUserActionToFunctionMapping(UserAction.ToggleAlwaysDefending, ToggleAlwaysDefending);

			AddSingleUserActionToFunctionMapping(UserAction.ResetLevel, ResetLevel);
			AddSingleUserActionToFunctionMapping(UserAction.TogglePauseGame, TogglePauseGame);
			AddSingleUserActionToFunctionMapping(UserAction.JumpToNextLevel, JumpToNextLevel);
			AddSingleUserActionToFunctionMapping(UserAction.ReturnToMainMenu, ReturnToMainMenu);

			if (fileDirectory.Length > 0)
			{
				musicPlayer = new MusicPlayer(fileDirectory);
				musicPlayer.InitAudio();
			}

			Utils.Logger.StartNewLog(levelAttempt.Level.Name);
		}

		public static GameScreen CreateTutorialGameScreen(MyGameWindow gameWindow, int tutorialNumber)
		{
			LevelGenerator levelGenerator = new LevelGenerator();

			Level level;

			if (tutorialNumber == 1) level = levelGenerator.GenerateFirstTutorialLevel();
			else if (tutorialNumber == 2) level = levelGenerator.GenerateSecondTutorialLevel();
			else level = levelGenerator.GenerateThirdTutorialLevel();

			GameScreen gameScreen = new GameScreen(gameWindow, level);

			return gameScreen;
		}

		public static GameScreen CreateTestSongGameScreen(MyGameWindow gameWindow, int songNumber)
		{
			string fileDirectory = @"C:\ForVS\demoSong" + songNumber + ".mp3";

			if (!System.IO.Directory.Exists(@"C:\ForVS"))
			{
				fileDirectory = "demoSong" + songNumber + ".mp3";
			}

			return CreateImportSongGameScreen(gameWindow, fileDirectory, songNumber == 2, songNumber == 1,
				"TestLevel" + songNumber);
		}

		public static GameScreen CreateImportSongGameScreen(MyGameWindow gameWindow, string fileDirectory,
			bool removeSingleBeats = false, bool removeHeldNotes = false, string levelName = "")
		{
			if (!System.IO.File.Exists(fileDirectory))
			{
				Console.WriteLine("FILE NOT FOUND");
				return null;
			}

			SongElements songElements = AudioAnalyzer.GetSongElements(fileDirectory);

			if (removeSingleBeats) songElements.SingleBeats.Clear();
			if (removeHeldNotes) songElements.HeldNotes.Clear();

			Level level = LevelGenerator.GenerateLevel(songElements);

			level.Name = levelName;

			GameScreen gameScreen = new GameScreen(gameWindow, level, fileDirectory);

			return gameScreen;
		}

		private LevelProgression GetLevelProgression()
		{
			return levelAttempt.LevelProgression;
		}

		private void Jump()
		{
			if (isGamePaused) return;

			gameLogic.PerformPlayerJump();
		}

		private void Hit()
		{
			if (isGamePaused) return;

			gameLogic.PerformPlayerHit();
		}

		private void Duck(bool value)
		{
			if (isGamePaused) return;

			gameLogic.SetPlayerIsStanding(!value);
		}

		private void Defend(bool value)
		{
			if (isGamePaused) return;

			gameLogic.SetPlayerIsDefending(value || isPlayerAlwaysDefending);
		}

		private void ToggleAlwaysDefending()
		{
			isPlayerAlwaysDefending = !isPlayerAlwaysDefending;

			gameLogic.SetPlayerIsDefending(isPlayerAlwaysDefending);
		}

		private void ResetLevel()
		{
			GetLevelProgression().Reset();
		}

		private void JumpToNextLevel()
		{
			string levelName = levelAttempt.Level.Name;

			int tutorialNumber = -1;

			if (levelName == "TutorialLevel1") tutorialNumber = 2;
			if (levelName == "TutorialLevel2") tutorialNumber = 3;

			if (tutorialNumber > 0)
			{
				SwitchToScreenAndDispose(CreateTutorialGameScreen(gameWindow, tutorialNumber));
			}
		}

		private void ReturnToMainMenu()
		{
			if (isGamePaused || GetLevelProgression().IsLevelComplete)
			{
				Utils.Logger.FinishLog();

				SwitchToScreenAndDispose(new MainMenuScreen(gameWindow));
			}
		}

		private void SwitchToScreenAndDispose(Screen screen)
		{
			musicPlayer.Dispose();
			SwitchToScreen(screen);
		}

		private void TogglePauseGame()
		{
			isGamePaused = !isGamePaused;

			GetLevelProgression().IsGamePaused = isGamePaused;

			if (musicPlayer != null)
			{
				if (isGamePaused) musicPlayer.PauseTrack();
				else musicPlayer.PlayTrack();
			}
		}

		public override void DoLogic()
		{
			CheckMusicPlayerStart();

			ProcessUserActions();

			if (isGamePaused) return;

			gameLogic.DoLogic();
		}

		bool musicStarted = false;

		private void CheckMusicPlayerStart()
		{
			if (!musicStarted &&
				GetLevelProgression().CurrentPlayerPosition.X >= GeneralValues.MusicStartPositionX)
			{
				if (musicPlayer != null) musicPlayer.PlayTrack();
				musicStarted = true;
			}
		}

		public override void Draw()
		{
			levelDrawer.DrawLevel(levelAttempt.LevelProgression);
		}


	}
}

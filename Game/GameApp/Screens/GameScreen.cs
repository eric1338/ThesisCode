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

		public GameScreen(MyGameWindow gameWindow, bool isTutorial = false) : base(gameWindow)
		{
			//Level level = Level.CreateTestLevel();

			LevelGenerator levelGenerator = new LevelGenerator();

			Level level;

			if (isTutorial) level = levelGenerator.GenerateTutorialLevel();
			else level = levelGenerator.GenerateTestLevel();

			levelAttempt = new LevelAttempt(level);

			gameLogic = new GameLogic(levelAttempt);
			levelDrawer = new LevelDrawer(levelAttempt.Level);

			musicPlayer = new MusicPlayer(@"C:\ForVS\taylor.mp3");

			AddKeyToSingleUserActionMapping(Key.W, UserAction.Jump);
			AddKeyToSingleUserActionMapping(Key.Space, UserAction.Jump);

			AddKeyToSingleUserActionMapping(Key.A, UserAction.Hit);
			AddKeyToProlongedUserActionMapping(Key.D, UserAction.Defend);
			AddKeyToProlongedUserActionMapping(Key.S, UserAction.Duck);
			AddKeyToProlongedUserActionMapping(Key.ControlLeft, UserAction.Duck);

			AddKeyToSingleUserActionMapping(Key.P, UserAction.TogglePauseGame);
			AddKeyToSingleUserActionMapping(Key.Escape, UserAction.TogglePauseGame);
			AddKeyToSingleUserActionMapping(Key.F10, UserAction.ResetLevel);
			AddKeyToSingleUserActionMapping(Key.X, UserAction.ReturnToMainMenu);

			AddSingleUserActionToFunctionMapping(UserAction.Jump, Jump);
			AddSingleUserActionToFunctionMapping(UserAction.Hit, Hit);
			AddProlongedUserActionToFunctionMapping(UserAction.Duck, Duck);
			AddProlongedUserActionToFunctionMapping(UserAction.Defend, Defend);

			AddSingleUserActionToFunctionMapping(UserAction.ResetLevel, ResetLevel);
			AddSingleUserActionToFunctionMapping(UserAction.TogglePauseGame, TogglePauseGame);
			AddSingleUserActionToFunctionMapping(UserAction.ReturnToMainMenu, ReturnToMainMenu);
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

			gameLogic.SetPlayerIsDefending(value);
		}

		private void ResetLevel()
		{
			GetLevelProgression().Reset();
		}

		private void ReturnToMainMenu()
		{
			if (isGamePaused || GetLevelProgression().IsLevelComplete)
			{
				SwitchToScreen(new MainMenuScreen(gameWindow));
			}
		}

		private void TogglePauseGame()
		{
			isGamePaused = !isGamePaused;

			GetLevelProgression().IsGamePaused = isGamePaused;

			if (isGamePaused) musicPlayer.PauseTrack();
			else musicPlayer.PlayTrack();
		}

		public override void DoLogic()
		{
			ProcessUserActions();

			if (isGamePaused) return;

			gameLogic.DoLogic();
		}

		public override void Draw()
		{
			levelDrawer.DrawLevel(levelAttempt.LevelProgression);
		}


	}
}

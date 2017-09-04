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

namespace GameApp.Screens
{
	class GameScreen : Screen
	{

		private LevelAttempt levelAttempt;

		private GameLogic gameLogic;
		private LevelDrawer levelDrawer;

		private MusicPlayer musicPlayer;

		private bool isGamePaused = false;

		public GameScreen(MyGameWindow gameWindow) : base(gameWindow)
		{
			levelAttempt = new LevelAttempt(Level.CreateTestLevel());

			gameLogic = new GameLogic(levelAttempt);
			levelDrawer = new LevelDrawer(levelAttempt.Level);

			musicPlayer = new MusicPlayer(@"C:\ForVS\taylor.mp3");

			AddKeyToSingleUserActionMapping(Key.W, UserAction.Jump);
			AddKeyToSingleUserActionMapping(Key.Space, UserAction.Jump);

			AddKeyToSingleUserActionMapping(Key.R, UserAction.ResetLevel);
			AddKeyToSingleUserActionMapping(Key.P, UserAction.TogglePauseGame);

			AddKeyToProlongedUserActionMapping(Key.D, UserAction.Duck);

			AddSingleUserActionToFunctionMapping(UserAction.Jump, Jump);
			AddProlongedUserActionToFunctionMapping(UserAction.Duck, Duck);

			AddSingleUserActionToFunctionMapping(UserAction.ResetLevel, ResetLevel);
			AddSingleUserActionToFunctionMapping(UserAction.TogglePauseGame, TogglePauseGame);
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

		private void Duck(bool value)
		{
			if (isGamePaused) return;

			gameLogic.SetPlayerIsStanding(!value);
		}

		private void ResetLevel()
		{
			GetLevelProgression().Reset();
		}

		private void TogglePauseGame()
		{
			isGamePaused = !isGamePaused;

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

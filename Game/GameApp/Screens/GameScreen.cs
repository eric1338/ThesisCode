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

namespace GameApp.Screens
{
	class GameScreen : Screen
	{

		private LevelAttempt levelAttempt;

		private GameLogic gameLogic;
		private LevelDrawer levelDrawer;

		private bool isGamePaused = false;

		public GameScreen()
		{
			levelAttempt = new LevelAttempt(Level.CreateTestLevel());

			gameLogic = new GameLogic(levelAttempt);
			levelDrawer = new LevelDrawer(levelAttempt.Level);


			AddKeyToSingleUserActionMapping(Key.W, UserAction.Jump);
			AddKeyToSingleUserActionMapping(Key.Space, UserAction.Jump);

			AddKeyToSingleUserActionMapping(Key.P, UserAction.TogglePauseGame);

			AddKeyToProlongedUserActionMapping(Key.D, UserAction.Duck);

			AddSingleUserActionToFunctionMapping(UserAction.Jump, Jump);

			AddSingleUserActionToFunctionMapping(UserAction.TogglePauseGame, TogglePauseGame);

			AddProlongedUserActionToFunctionMapping(UserAction.Duck, Duck);
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
		}

		private void TogglePauseGame()
		{
			isGamePaused = !isGamePaused;
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

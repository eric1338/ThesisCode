using GameApp.Gameplay;
using GameApp.Input;
using GameApp.Levels;
using GameApp.Physics;
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

		public GameScreen()
		{
			levelAttempt = new LevelAttempt(Level.CreateTestLevel());

			gameLogic = new GameLogic(levelAttempt);
			levelDrawer = new LevelDrawer(levelAttempt.Level);


			AddKeyToSingleUserActionMapping(Key.W, UserAction.Jump);
			AddKeyToSingleUserActionMapping(Key.Space, UserAction.Jump);

			AddKeyToProlongedUserActionMapping(Key.D, UserAction.Duck);

			AddSingleUserActionToFunctionMapping(UserAction.Jump, Jump);
			AddProlongedUserActionToFunctionMapping(UserAction.Duck, DuckTest);
		}

		private LevelProgression GetLevelProgression()
		{
			return levelAttempt.LevelProgression;
		}

		private void Jump()
		{
			gameLogic.PerformPlayerJump();
		}

		private bool testVar = false;

		private void DuckTest(bool value)
		{
			testVar = value;
		}

		public override void DoLogic()
		{
			ProcessUserActions();

			gameLogic.DoLogic();
		}

		public override void Draw()
		{
			levelDrawer.Test(testVar);

			levelDrawer.DrawLevel(levelAttempt.LevelProgression);
		}


	}
}

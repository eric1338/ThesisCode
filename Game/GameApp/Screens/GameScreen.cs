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
		private LevelDrawer levelDrawer;

		private GamePhysics gamePhysics;

		public GameScreen()
		{
			levelAttempt = new LevelAttempt(Level.CreateTestLevel());
			levelDrawer = new LevelDrawer(levelAttempt.Level);
			gamePhysics = new GamePhysics(levelAttempt.Level);


			AddKeyToSingleUserActionMapping(Key.W, UserAction.Jump);
			AddKeyToSingleUserActionMapping(Key.Space, UserAction.Jump);

			AddKeyToProlongedUserActionMapping(Key.D, UserAction.Duck);

			AddSingleUserActionToFunctionMapping(UserAction.Jump, Jump);
			AddProlongedUserActionToFunctionMapping(UserAction.Duck, DuckTest);
		}

		private Level GetLevel()
		{
			return levelAttempt.Level;
		}

		private LevelProgression GetLevelProgression()
		{
			return levelAttempt.LevelProgression;
		}

		private void Jump()
		{
			gamePhysics.PerformJump();
		}

		private bool testVar = false;

		private void DuckTest(bool value)
		{
			testVar = value;
		}

		public override void DoLogic()
		{
			ProcessUserActions();

			//GetLevelProgression().CurrentPlayerPosition += new Vector2(0.0166666f, 0.0f);

			Vector2 oldPosition = GetLevelProgression().CurrentPlayerPosition;
			Vector2 newPosition = gamePhysics.DoPlayerPhysics(oldPosition);

			GetLevelProgression().CurrentPlayerPosition = newPosition;

			// Input-Logic + Springen

			//Vector2 newPlayerPosition = gamePhysics.DoPlayerPhysics();

			//GetLevelProgression().CurrentPlayerPosition = newPlayerPosition;
		}

		public override void Draw()
		{
			levelDrawer.Test(testVar);

			levelDrawer.DrawLevel(GetLevelProgression());
		}


	}
}

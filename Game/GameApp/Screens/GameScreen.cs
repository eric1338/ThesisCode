using GameApp.Gameplay;
using GameApp.Levels;
using GameApp.Physics;
using GameApp.Visual;
using OpenTK;
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

			gamePhysics = new GamePhysics();
		}

		private Level GetLevel()
		{
			return levelAttempt.Level;
		}

		private LevelProgression GetLevelProgression()
		{
			return levelAttempt.LevelProgression;
		}

		public override void DoLogic()
		{
			// Input-Logic + Springen

			//Vector2 newPlayerPosition = gamePhysics.DoPlayerPhysics();

			//GetLevelProgression().CurrentPlayerPosition = newPlayerPosition;
		}

		public override void Draw()
		{
			levelDrawer.DrawLevel(GetLevelProgression());
		}


	}
}

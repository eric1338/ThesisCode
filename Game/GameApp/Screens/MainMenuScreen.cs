using GameApp.Screens.Input;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Screens
{
	class MainMenuScreen : Screen
	{

		public MainMenuScreen(MyGameWindow gameWindow) : base(gameWindow)
		{
			AddKeyToSingleUserActionMapping(Key.Enter, UserAction.ChooseCurrentMenuItem);

			AddSingleUserActionToFunctionMapping(UserAction.ChooseCurrentMenuItem, StartGame);
		}

		public void StartGame()
		{
			GameScreen gameScreen = new GameScreen(gameWindow);

			SwitchToScreen(gameScreen);
		}

		public override void DoLogic()
		{
			ProcessUserActions();
		}

		public override void Draw()
		{

		}
	}
}

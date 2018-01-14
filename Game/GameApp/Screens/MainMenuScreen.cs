using GameApp.Screens.Input;
using GameApp.Visual;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameApp.Screens
{
	class MainMenuScreen : Screen
	{

		private MainMenuDrawer mainMenuDrawer;

		private int currentIndex = 0;
		private int numberOfMenuItems = 6;

		private int maximumMenuItemMargin = 30;

		private int[] menuItemXPaddings;


		private int switchFrameCount = 0;

		public MainMenuScreen(MyGameWindow gameWindow) : base(gameWindow)
		{
			mainMenuDrawer = new MainMenuDrawer();

			AddKeyToSingleUserActionMapping(Key.Up, UserAction.GoUpInMenu);
			AddKeyToSingleUserActionMapping(Key.W, UserAction.GoUpInMenu);
			AddKeyToSingleUserActionMapping(Key.Down, UserAction.GoDownInMenu);
			AddKeyToSingleUserActionMapping(Key.S, UserAction.GoDownInMenu);
			AddKeyToSingleUserActionMapping(Key.Enter, UserAction.SelectCurrentMenuItem);
			AddKeyToSingleUserActionMapping(Key.N, UserAction.ImportSong);

			AddSingleUserActionToFunctionMapping(UserAction.GoUpInMenu, GoUpInMenu);
			AddSingleUserActionToFunctionMapping(UserAction.GoDownInMenu, GoDownInMenu);
			AddSingleUserActionToFunctionMapping(UserAction.SelectCurrentMenuItem, SelectCurrentMenuItem);
			AddSingleUserActionToFunctionMapping(UserAction.ImportSong, ImportSong);

			menuItemXPaddings = new int[numberOfMenuItems];

			menuItemXPaddings[0] = 1;

			for (int i = 1; i < numberOfMenuItems; i++) menuItemXPaddings[i] = 0;
		}

		public void GoUpInMenu()
		{
			if (currentIndex <= 0) return;

			currentIndex--;

			SetMenuItemXPaddings(currentIndex);
		}

		public void GoDownInMenu()
		{
			if (currentIndex + 1 >= numberOfMenuItems) return;

			currentIndex++;

			SetMenuItemXPaddings(currentIndex);
		}

		private void SetMenuItemXPaddings(int selectedIndex)
		{
			for (int i = 0; i < numberOfMenuItems; i++)
			{
				menuItemXPaddings[i] = i == selectedIndex ? 1 : 0;
			}
		}

		private void SelectCurrentMenuItem()
		{
			if (currentIndex == 0) StartTutorial();
			else if (currentIndex == 1) PlayTestSong(1);
			else if (currentIndex == 2) PlayTestSong(2);
			else if (currentIndex == 3) PlayTestSong(3);
			else if (currentIndex == 4) ImportSong();
			else if (currentIndex == 5) gameWindow.Exit();
		}

		private void StartTutorial()
		{
			GameScreen gameScreen = new GameScreen(gameWindow, true);

			StartGame(gameScreen);
		}

		private void ImportSong()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (openFileDialog.ShowDialog() != DialogResult.OK) return;

			string fileDirectory = openFileDialog.FileName;

			Audio.SongElements songElements = Audio.AudioAnalyzer.GetSongElements(fileDirectory);

			Levels.Level level = Levels.LevelGeneration.LevelGenerator.GenerateLevel(songElements);

			GameScreen gameScreen = new GameScreen(gameWindow, false, level, fileDirectory);

			StartGame(gameScreen);
		}

		private void PlayTestSong(int number)
		{
			GameScreen gameScreen = new GameScreen(gameWindow);

			StartGame(gameScreen);
		}

		public override void DoLogic()
		{
			if (switchFrameCount > 0) switchFrameCount++;

			if (switchFrameCount > 800)
			{
				switchFrameCount = 0;
				SwitchToScreen(upcomingGameScreen);
				return;
			}

			ProcessUserActions();

			for (int i = 0; i < numberOfMenuItems; i++)
			{
				if (menuItemXPaddings[i] > 0 && menuItemXPaddings[i] < maximumMenuItemMargin)
				{
					menuItemXPaddings[i]++;
				}
			}
		}

		private GameScreen upcomingGameScreen;

		private void StartGame(GameScreen gameScreen)
		{
			switchFrameCount = 1;

			upcomingGameScreen = gameScreen;
		}

		public override void Draw()
		{
			mainMenuDrawer.DrawMainMenu(menuItemXPaddings);

			if (switchFrameCount > 0)
			{
				BasicGraphics.SetColor(0, 1.0f, 0.8f);
				BasicGraphics.DrawSquare(new OpenTK.Vector2(-0.4f, 0.4f), new OpenTK.Vector2(0.4f, -0.4f));
			}
		}
	}
}

﻿using GameApp.Screens.Input;
using GameApp.Visual;
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

		private MainMenuDrawer mainMenuDrawer;

		private int currentIndex = 0;
		private int numberOfMenuItems = 3;

		private int maximumMenuItemMargin = 30;

		private int[] menuItemXPaddings;

		public MainMenuScreen(MyGameWindow gameWindow) : base(gameWindow)
		{
			mainMenuDrawer = new MainMenuDrawer();

			AddKeyToSingleUserActionMapping(Key.Up, UserAction.GoUpInMenu);
			AddKeyToSingleUserActionMapping(Key.W, UserAction.GoUpInMenu);
			AddKeyToSingleUserActionMapping(Key.Down, UserAction.GoDownInMenu);
			AddKeyToSingleUserActionMapping(Key.S, UserAction.GoDownInMenu);
			AddKeyToSingleUserActionMapping(Key.Enter, UserAction.SelectCurrentMenuItem);

			AddSingleUserActionToFunctionMapping(UserAction.GoUpInMenu, GoUpInMenu);
			AddSingleUserActionToFunctionMapping(UserAction.GoDownInMenu, GoDownInMenu);
			AddSingleUserActionToFunctionMapping(UserAction.SelectCurrentMenuItem, SelectCurrentMenuItem);

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


		public void SelectCurrentMenuItem()
		{
			if (currentIndex == 0) Console.WriteLine("2do :)");
			if (currentIndex == 1) StartGame();
			if (currentIndex == 2) gameWindow.Exit();
		}

		private void StartGame()
		{
			GameScreen gameScreen = new GameScreen(gameWindow);

			SwitchToScreen(gameScreen);
		}

		public override void DoLogic()
		{
			ProcessUserActions();

			for (int i = 0; i < numberOfMenuItems; i++)
			{
				if (menuItemXPaddings[i] > 0 && menuItemXPaddings[i] < maximumMenuItemMargin)
				{
					menuItemXPaddings[i]++;
				}
			}
		}

		public override void Draw()
		{
			mainMenuDrawer.DrawMainMenu(menuItemXPaddings);
		}
	}
}

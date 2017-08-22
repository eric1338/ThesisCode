using GameApp.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
	class Program
	{
		static void Main(string[] args)
		{
			int width = 1366;
			int height = 768;

			VisualValues.ScreenWidth = width;
			VisualValues.ScreenHeight = height;

			MyGameWindow gameWindow = new MyGameWindow(width, height);

			gameWindow.Run(GeneralValues.FPS);

		}
	}
}

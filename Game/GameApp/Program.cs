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

			MyGameWindow gameWindow = new MyGameWindow(VisualValues.ScreenWidth, VisualValues.ScreenHeight);

			gameWindow.Run(GeneralValues.FPS);

		}
	}
}

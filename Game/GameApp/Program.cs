﻿using System;
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

			MyGameWindow gameWindow = new MyGameWindow();

			gameWindow.Run(60.0);

		}
	}
}

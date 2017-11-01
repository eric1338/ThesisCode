using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStatisticsApp
{
	class MyGameWindow : GameWindow
	{

		public MyGameWindow() : base(640, 480)
		{
			KeyUp += MyGameWindow_KeyUp;
			KeyDown += MyGameWindow_KeyDown;
		}

		private void MyGameWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Console.WriteLine("presed " + e.KeyChar);
		}

		private void MyGameWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
		{
			ProcessKeyAction(e.Key, true);
		}

		private void MyGameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			ProcessKeyAction(e.Key, false);
		}

		private void ProcessKeyAction(Key key, bool keyUp)
		{
			string keyName = key.ToString();
			DateTime now = DateTime.Now;

			Console.WriteLine(keyName + " - " + keyUp + " - " + now.ToString());
		}
	}
}

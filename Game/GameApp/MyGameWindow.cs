using GameApp.Screens;
using GameApp.Visual;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
	class MyGameWindow : GameWindow
	{

		private Screen currentScreen;

		public MyGameWindow(int width, int height) : base(width, height)
		{
			Title = "Thesis";

			float aspect = width / (float)height;

			GL.Viewport(0, 0, width, height);
			GL.MatrixMode(MatrixMode.Projection);
			GL.Ortho(-aspect, aspect, -1, 1, -1, 1);

			//GL.MatrixMode(MatrixMode.Modelview);
			//GL.LoadIdentity();


			RenderFrame += MyGameWindow_RenderFrame;
			UpdateFrame += MyGameWindow_UpdateFrame;

			currentScreen = new GameScreen();
		}

		private void MyGameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			currentScreen.DoLogic();
		}

		private void MyGameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			currentScreen.Draw();

			SwapBuffers();
		}
	}
}

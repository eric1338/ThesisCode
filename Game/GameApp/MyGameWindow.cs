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

		private VisualManager visualManager = new VisualManager();

		private Screen currentScreen;

		public MyGameWindow() : base(1366, 768)
		{
			RenderFrame += MyGameWindow_RenderFrame;
			UpdateFrame += MyGameWindow_UpdateFrame;
		}

		private void MyGameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			currentScreen.DoLogic();
		}

		private void MyGameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			visualManager.DrawLevel();

			SwapBuffers();
		}
	}
}

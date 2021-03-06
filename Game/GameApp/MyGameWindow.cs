﻿using GameApp.Screens;
using GameApp.Visual;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
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

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			//GL.MatrixMode(MatrixMode.Modelview);
			//GL.LoadIdentity();

			RenderFrame += MyGameWindow_RenderFrame;
			UpdateFrame += MyGameWindow_UpdateFrame;

			KeyUp += MyGameWindow_KeyUp;
			KeyDown += MyGameWindow_KeyDown;

			currentScreen = new MainMenuScreen(this);
		}

		public void SetScreen(Screen newScreen)
		{
			currentScreen = newScreen;
		}

		private void MyGameWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
		{
			Utils.Logger.AddKeyPress(DateTime.Now, e.Key, true);

			currentScreen.ProcessKeyUp(e.Key);
		}

		private void MyGameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			Utils.Logger.AddKeyPress(DateTime.Now, e.Key, false);

			currentScreen.ProcessKeyDown(e.Key);
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

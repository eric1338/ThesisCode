using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace GameApp.Visual
{
	class VisualManager : IVisual
	{

		public void DrawLevel()
		{
			GL.Color3(1.0f, 0.0f, 0.4f);

			Vector2 v1 = new Vector2(0.25f, 0.5f);
			Vector2 v2 = new Vector2(0.5f, 0.25f);

			DrawSquare(v1, v2);
		}


		private static void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.End();
		}

	}
}

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Visual
{
	class BasicGraphics
	{


		public static void SetColor(float r, float g, float b)
		{
			GL.Color3(r, g, b);
		}

		public static void SetColor(Vector3 color)
		{
			GL.Color3(color.X, color.Y, color.Z);
		}

		public static void DrawOpenGLLine(Vector2 point1, Vector2 point2)
		{
			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(point1);
			GL.Vertex2(point2);
			GL.End();
		}

		public static void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.End();
		}

		public static void DrawRectangularTexture(Texture texture, Vector2 topLeft, Vector2 bottomRight, float alpha = 1.0f)
		{
			Vector2 bottomLeft = new Vector2(topLeft.X, bottomRight.Y);
			Vector2 topRight = new Vector2(bottomRight.X, topLeft.Y);

			DrawTexture(texture, bottomLeft, bottomRight, topRight, topLeft, alpha);
		}

		public static void DrawTexture(Texture texture, Vector2 bottomLeft, Vector2 bottomRight, Vector2 topRight, Vector2 topLeft, float alpha = 1.0f)
		{
			texture.BeginUse();

			GL.Color3(alpha, alpha, alpha);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(bottomLeft);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(bottomRight);
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(topRight);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(topLeft);
			GL.End();

			texture.EndUse();
		}


	}
}

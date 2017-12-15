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

		public static void DrawSquare(Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeftCorner.X, bottomRightCorner.Y);
			GL.Vertex2(topLeftCorner.X, topLeftCorner.Y);
			GL.Vertex2(bottomRightCorner.X, topLeftCorner.Y);
			GL.Vertex2(bottomRightCorner.X, bottomRightCorner.Y);
			GL.End();
		}

		public static void DrawRectangularTexture(Texture texture, Vector2 topLeftCorner,
			Vector2 bottomRightCorner, float alpha = 1.0f)
		{
			Vector2 bottomLeft = new Vector2(topLeftCorner.X, bottomRightCorner.Y);
			Vector2 topRight = new Vector2(bottomRightCorner.X, topLeftCorner.Y);

			DrawTexture(texture, bottomLeft, bottomRightCorner, topRight, topLeftCorner, alpha);
		}

		public static void DrawTexture(Texture texture, Vector2 bottomLeftCorner,
			Vector2 bottomRightCorner, Vector2 topRightCorner, Vector2 topLeftCorner, float alpha = 1.0f)
		{
			texture.BeginUse();

			GL.Color3(alpha, alpha, alpha);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(bottomLeftCorner);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(bottomRightCorner);
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(topRightCorner);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(topLeftCorner);
			GL.End();

			texture.EndUse();
		}


	}
}

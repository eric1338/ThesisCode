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

		public static void SetColor4(float r, float g, float b, float a)
		{
			GL.Color4(r, g, b, a);
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

		public static void DrawTextureWithFadeIn(Texture texture, Vector2 topLeftCorner,
			Vector2 bottomRightCorner, float percentage)
		{
			if (percentage <= 0) return;

			Vector2 bottomLeftCorner = new Vector2(topLeftCorner.X, bottomRightCorner.Y);
			Vector2 topRightCorner = new Vector2(bottomRightCorner.X, topLeftCorner.Y);

			topRightCorner.X *= percentage;
			bottomRightCorner.X *= percentage;

			texture.BeginUse();

			GL.Color3(1, 1, 1);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(bottomLeftCorner);
			GL.TexCoord2(percentage, 0.0f);
			GL.Vertex2(bottomRightCorner);
			GL.TexCoord2(percentage, 1.0f);
			GL.Vertex2(topRightCorner);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(topLeftCorner);
			GL.End();

			texture.EndUse();
		}

		public static void DrawTexture(Texture texture, Vector2 topLeftCorner,
			Vector2 bottomRightCorner, float alpha = 1)
		{
			Vector2 bottomLeftCorner = new Vector2(topLeftCorner.X, bottomRightCorner.Y);
			Vector2 topRightCorner = new Vector2(bottomRightCorner.X, topLeftCorner.Y);

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
		}

		public static void DrawTextureWithUse(Texture texture, Vector2 topLeftCorner,
			Vector2 bottomRightCorner, float alpha = 1)
		{
			Vector2 bottomLeftCorner = new Vector2(topLeftCorner.X, bottomRightCorner.Y);
			Vector2 topRightCorner = new Vector2(bottomRightCorner.X, topLeftCorner.Y);

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

		public static void DrawPartialTexture(Texture texture, Vector2 topLeftCorner,
			Vector2 bottomRightCorner, float xPercentage, float alpha = 1)
		{
			Vector2 bottomLeftCorner = new Vector2(topLeftCorner.X, bottomRightCorner.Y);
			Vector2 topRightCorner = new Vector2(bottomRightCorner.X, topLeftCorner.Y);

			GL.Color3(alpha, alpha, alpha);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(bottomLeftCorner);
			GL.TexCoord2(xPercentage, 0.0f);
			GL.Vertex2(bottomRightCorner);
			GL.TexCoord2(xPercentage, 1.0f);
			GL.Vertex2(topRightCorner);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(topLeftCorner);
			GL.End();
		}

	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GameApp.Visual
{
	public static class TextureLoader
	{

		public static Texture FromBitmap(Bitmap bitmap)
		{
			Texture texture = new Texture();
			texture.BeginUse();
			texture.FilterTrilinear();
			//todo: 16bit channels
			using (Bitmap bmp = new Bitmap(bitmap))
			{
				bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
				BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
				PixelInternalFormat internalFormat = selectInternalPixelFormat(bmp.PixelFormat);
				OpenTK.Graphics.OpenGL.PixelFormat inputPixelFormat = selectInputPixelFormat(bmp.PixelFormat);
				texture.LoadPixels(bmpData.Scan0, bmpData.Width, bmpData.Height, internalFormat, inputPixelFormat, PixelType.UnsignedByte);
				bmp.UnlockBits(bmpData);
			}
			texture.EndUse();
			return texture;
		}

		public static Texture FromFile(string fileName)
		{
			if (String.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(fileName);
			}
			if (!File.Exists(fileName))
			{
				throw new FileLoadException(fileName);
			}
			return FromBitmap(new Bitmap(fileName));
		}

		public static void SaveToFile(Texture texture, string fileName)
		{
			var format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
			texture.BeginUse();
			using (Bitmap bmp = new Bitmap(texture.Width, texture.Height))
			{
				BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, format);
				GL.GetTexImage(TextureTarget.Texture2D, 0, selectInputPixelFormat(format), PixelType.UnsignedByte, data.Scan0);
				bmp.UnlockBits(data);
				texture.EndUse();
				bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
				bmp.Save(fileName);
			}
		}

		private static OpenTK.Graphics.OpenGL.PixelFormat selectInputPixelFormat(System.Drawing.Imaging.PixelFormat pixelFormat)
		{
			switch (pixelFormat)
			{
				case System.Drawing.Imaging.PixelFormat.Format8bppIndexed: return OpenTK.Graphics.OpenGL.PixelFormat.Red;
				case System.Drawing.Imaging.PixelFormat.Format24bppRgb: return OpenTK.Graphics.OpenGL.PixelFormat.Bgr;
				case System.Drawing.Imaging.PixelFormat.Format32bppArgb: return OpenTK.Graphics.OpenGL.PixelFormat.Bgra;
				default: throw new FileLoadException("Wrong pixel format " + pixelFormat.ToString());
			}
		}

		private static PixelInternalFormat selectInternalPixelFormat(System.Drawing.Imaging.PixelFormat pixelFormat)
		{
			switch (pixelFormat)
			{
				case System.Drawing.Imaging.PixelFormat.Format8bppIndexed: return PixelInternalFormat.Luminance;
				case System.Drawing.Imaging.PixelFormat.Format24bppRgb: return PixelInternalFormat.Rgb;
				case System.Drawing.Imaging.PixelFormat.Format32bppArgb: return PixelInternalFormat.Rgba;
				default: throw new FileLoadException("Wrong pixel format " + pixelFormat.ToString());
			}
		}

	}
}

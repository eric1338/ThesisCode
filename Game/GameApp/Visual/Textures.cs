using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Visual
{
	class Textures
	{

		public static Textures Instance = new Textures();

		private bool _texturesLoaded = false;

		public Texture PlayerTexture { get; set; }

		public void LoadTextures()
		{
			if (_texturesLoaded) return;

			PlayerTexture = TextureLoader.FromBitmap(Resources.playerTest);

			_texturesLoaded = true;
		}
	}
}

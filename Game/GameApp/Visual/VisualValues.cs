using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Visual
{
	class VisualValues
	{

		public static readonly int ScreenWidth = 1280;
		public static readonly int ScreenHeight = 720;

		public static float GetAspectRatio()
		{
			return ScreenWidth / (float) ScreenHeight;
		}

		public static readonly Vector2 ScreenCenterOffset = new Vector2(0.65f, 0.35f);

		public static readonly float ZoomFactor = 0.75f;

		// temp
		public static readonly float HalfCollectibleWidthHeight = 0.05f;
		public static readonly float HalfProjectileWidthHeight = 0.05f;

		public static readonly float PlayerWidth = 0.16f;
		public static readonly float PlayerHeight = 0.32f;

	}
}

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

		public static int ScreenWidth;
		public static int ScreenHeight;

		public static readonly float ZoomFactor = 0.75f;

		public static float GetAspectRatio()
		{
			return ScreenWidth / (float) ScreenHeight;
		}

		public static readonly Vector2 ScreenCenterOffset = new Vector2(0.65f, 0.35f);

		// temp
		public static readonly float HalfCollectibleWidthHeight = 0.05f;

		public static readonly float PlayerWidth = 0.16f;
		public static readonly float PlayerHeight = 0.32f;

	}
}

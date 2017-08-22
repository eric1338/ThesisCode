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

		public static readonly float ZoomFactor = 2f;

		public static float GetAspectRatio()
		{
			return ScreenWidth / (float) ScreenHeight;
		}


		// temp
		public static readonly float HalfCollectibleWidthHeight = 0.1f;

		public static readonly float PlayerWidth = 0.2f;
		public static readonly float PlayerHeight = 0.8f;

	}
}

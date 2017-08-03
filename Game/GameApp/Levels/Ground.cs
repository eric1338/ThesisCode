using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class Ground
	{

		public float LeftX { get; set; } = 0.0f;
		public float RightX { get; set; } = 0.0f;
		public float TopY { get; set; } = 0.0f;

		public Vector2 TopLeftCorner;
		public Vector2 TopRightCorner;

		public Ground(float leftX, float rightX, float topY)
		{
			LeftX = leftX;
			RightX = rightX;
			TopY = topY;

			TopLeftCorner = new Vector2(leftX, topY);
			TopRightCorner = new Vector2(rightX, topY);
		}


	}
}

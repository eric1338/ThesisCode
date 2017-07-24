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


		public Ground(float leftX, float rightX, float topY)
		{
			LeftX = leftX;
			RightX = rightX;
			TopY = topY;
		}


	}
}

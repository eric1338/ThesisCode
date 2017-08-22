using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class Obstacle
	{

		public Vector2 TopLeftCorner { get; set; }
		public Vector2 BottomRightCorner { get; set; }


		public Obstacle(Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			TopLeftCorner = topLeftCorner;
			BottomRightCorner = bottomRightCorner;
		}

	}
}

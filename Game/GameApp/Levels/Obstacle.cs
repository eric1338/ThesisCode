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

		public int ID { get; set; }

		public Vector2 TopLeftCorner { get; set; }
		public Vector2 BottomRightCorner { get; set; }

		public Obstacle(Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			ID = -1;
			TopLeftCorner = topLeftCorner;
			BottomRightCorner = bottomRightCorner;
		}

		public Obstacle(int id, Vector2 topLeftCorner, Vector2 bottomRightCorner)
		{
			ID = id;
			TopLeftCorner = topLeftCorner;
			BottomRightCorner = bottomRightCorner;
		}

	}
}

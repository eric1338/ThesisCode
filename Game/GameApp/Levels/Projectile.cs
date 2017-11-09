using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class Projectile
	{

		public int ID { get; set; }

		public Vector2 StartingPosition { get; set; }


		public Projectile(int id, Vector2 startingPosition)
		{
			ID = id;
			StartingPosition = startingPosition;
		}

	}
}

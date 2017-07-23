using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Level
{
	class Collectible
	{

		public int ID { get; set; }

		public Vector2 Position { get; set; }


		public Collectible(int id, Vector2 position)
		{
			ID = id;
			Position = position;
		}

	}
}

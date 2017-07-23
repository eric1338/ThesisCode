using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Level
{
	class Level
	{

		private List<Ground> grounds = new List<Ground>();
		private List<Collectible> collectibles = new List<Collectible>();

		public Vector2 PlayerStartingPosition { get; set; }

		public Level()
		{

		}

		public void AddGround(Ground ground)
		{
			grounds.Add(ground);
		}

		public void AddCollectible(Collectible collectible)
		{
			collectibles.Add(collectible);
		}

	}
}

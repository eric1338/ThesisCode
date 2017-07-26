using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class Level
	{

		public List<Ground> Grounds { get; set; }
		public List<Collectible> Collectibles { get; set; }

		public Vector2 PlayerStartingPosition { get; set; }

		public Level()
		{
			Grounds = new List<Ground>();
			Collectibles = new List<Collectible>();
		}

		public static Level CreateTestLevel()
		{
			Level level = new Level();

			Ground g1 = new Ground(-1, 6, 1);
			Ground g2 = new Ground(7, 13, 0.5f);
			Ground g3 = new Ground(13.5f, 20, 0.6f);

			level.AddGround(g1);
			level.AddGround(g2);
			level.AddGround(g3);

			Collectible c1 = new Collectible(0, new Vector2(4f, 1.5f));
			Collectible c2 = new Collectible(1, new Vector2(6.5f, 1f));
			Collectible c3 = new Collectible(2, new Vector2(10f, 1f));

			level.AddCollectible(c1);
			level.AddCollectible(c2);
			level.AddCollectible(c3);

			level.PlayerStartingPosition = new Vector2(0, 1);

			return level;
		}

		public void AddGround(Ground ground)
		{
			Grounds.Add(ground);
		}

		public void AddCollectible(Collectible collectible)
		{
			Collectibles.Add(collectible);
		}

	}
}

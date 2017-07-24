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

			Ground g1 = new Ground(0.0f, 0.5f, 0.1f);
			Ground g2 = new Ground(0.6f, 0.9f, -0.05f);

			level.AddGround(g1);
			level.AddGround(g2);

			Collectible c1 = new Collectible(0, new Vector2(0.3f, 0.2f));
			Collectible c2 = new Collectible(0, new Vector2(0.55f, 0.2f));
			Collectible c3 = new Collectible(0, new Vector2(0.8f, 0.05f));

			level.AddCollectible(c1);
			level.AddCollectible(c2);
			level.AddCollectible(c3);

			level.PlayerStartingPosition = new Vector2(0.05f, 0.1f);

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

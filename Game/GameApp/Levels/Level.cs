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

		public string Name { get; set; }

		public List<Ground> Grounds { get; set; }
		public List<Obstacle> Obstacles { get; set; }
		public List<Collectible> Collectibles { get; set; }
		public List<Projectile> Projectiles { get; set; }

		public Vector2 PlayerStartingPosition { get; set; }

		public float GoalLineX { get; set; }

		public bool IsTutorial { get; set; }

		private int currentCollectibleID = 0;
		private int currentProjectileID = 0;

		public Level(string name = "", bool isTutorial = false)
		{
			Name = name;

			Grounds = new List<Ground>();
			Obstacles = new List<Obstacle>();
			Collectibles = new List<Collectible>();
			Projectiles = new List<Projectile>();

			IsTutorial = isTutorial;
		}

		public void AddGround(Ground ground)
		{
			Grounds.Add(ground);
		}

		public void AddObstacle(Obstacle obstacle)
		{
			Obstacles.Add(obstacle);
		}

		public void AddCollectibleByPosition(Vector2 collectiblePosition)
		{
			Collectibles.Add(new Collectible(currentCollectibleID, collectiblePosition));

			currentCollectibleID++;
		}

		public void AddProjectileByPosition(Vector2 projectilePosition)
		{
			Projectiles.Add(new Projectile(currentProjectileID, projectilePosition));

			currentProjectileID++;
		}

	}
}

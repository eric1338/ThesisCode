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
		public List<Obstacle> SolidObstacles { get; set; }
		public List<Obstacle> DestructibleObstacles { get; set; }
		public List<Collectible> Collectibles { get; set; }
		public List<Projectile> Projectiles { get; set; }

		public Vector2 PlayerStartingPosition { get; set; }

		private int currentCollectibleID = 0;
		private int currentProjectileID = 0;

		public Level()
		{
			Grounds = new List<Ground>();
			SolidObstacles = new List<Obstacle>();
			DestructibleObstacles = new List<Obstacle>();
			Collectibles = new List<Collectible>();
			Projectiles = new List<Projectile>();
		}

		public static Level CreateTutorialLevel()
		{
			Level level = new Level();

			level.PlayerStartingPosition = new Vector2(0, 1);
			
			level.AddGround(new Ground(-2, 40, 1));



			return level;
		}

		public static Level CreateTestLevel()
		{
			Level level = new Level();

			Ground g1 = new Ground(-0.15f, 6, 1);
			Ground g2 = new Ground(7, 13, 0.5f);
			Ground g3 = new Ground(13.5f, 20, 0.6f);

			level.AddGround(g1);
			level.AddGround(g2);
			level.AddGround(g3);

			Obstacle destructibleObstacle1 = new Obstacle(new Vector2(3.05f, 1.45f), new Vector2(3.15f, 0.95f));
			level.AddDestructibleObstacle(destructibleObstacle1);

			Obstacle solidObstacle1 = new Obstacle(new Vector2(3.05f, 1.05f), new Vector2(3.25f, 0.95f));
			Obstacle solidObstacle2 = new Obstacle(new Vector2(8.4f, 0.8f), new Vector2(8.6f, 0.7f));

			//level.AddSolidObstacle(solidObstacle1);
			level.AddSolidObstacle(solidObstacle2);

			Collectible c1 = new Collectible(0, new Vector2(4f, 1.5f));
			Collectible c2 = new Collectible(1, new Vector2(6.5f, 1f));
			Collectible c3 = new Collectible(2, new Vector2(10f, 1f));

			//level.AddCollectible(c1);
			//level.AddCollectible(c2);
			//level.AddCollectible(c3);

			level.PlayerStartingPosition = new Vector2(0, 1);

			return level;
		}

		public void AddGround(Ground ground)
		{
			Grounds.Add(ground);
		}

		public void AddSolidObstacle(Obstacle solidObstacle)
		{
			SolidObstacles.Add(solidObstacle);
		}

		public void AddDestructibleObstacle(Obstacle destructibleObstacle)
		{
			DestructibleObstacles.Add(destructibleObstacle);
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



		// ungenutzt + ungetestet + muss noch refactored werden

		private Dictionary<int, List<Ground>> optimizedGrounds = new Dictionary<int, List<Ground>>();


		// private -> public (falls es genutzt werden sollte)
		private List<Ground> GetGrounds(float playerPositionX)
		{
			return optimizedGrounds[GetIndex(playerPositionX)];
		}


		private int GetIndex(float x)
		{
			return (int) Math.Floor(x / GeneralValues.OptimizationX);
		}

		private float GetLeftX(int index)
		{
			return Math.Max(0, index - 1) * GeneralValues.OptimizationX;
		}

		private float GetRightX(int index)
		{
			return (index + 2) * GeneralValues.OptimizationX;
		}
		
		private bool IsVectorInRange(float vectorX, float leftX, float rightX)
		{
			return vectorX > leftX || vectorX < rightX;
		}

		private bool IsObjectInRange(float leftestObjectX, float rightestObjectX, float leftX, float rightX)
		{
			return IsVectorInRange(leftestObjectX, leftX, rightX) || IsVectorInRange(rightestObjectX, leftX, rightX);
		}


		private void OptimizeGrounds()
		{
			float rightestX = -1;

			foreach (Ground ground in Grounds)
			{
				rightestX = Math.Max(rightestX, ground.RightX);
			}

			int lastIndex = GetIndex(rightestX) + 2;

			for (int i = 0; i <= lastIndex; i++)
			{
				CreateGroundSchachtel(Grounds, i);
			}
		}


		private void CreateGroundSchachtel(List<Ground> allGrounds, int index)
		{
			List<Ground> schachtel = new List<Ground>();

			float leftX = GetLeftX(index);
			float rightX = GetRightX(index);

			foreach (Ground ground in allGrounds)
			{
				if (IsObjectInRange(ground.LeftX, ground.RightX, leftX, rightX))
				{
					schachtel.Add(ground);
				}
			}
			
			optimizedGrounds[index] = schachtel;
		}



	}
}

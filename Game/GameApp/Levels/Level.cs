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
		public List<Obstacle> SolidObstacles { get; set; }
		public List<Obstacle> DestructibleObstacles { get; set; }
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
			SolidObstacles = new List<Obstacle>();
			DestructibleObstacles = new List<Obstacle>();
			Collectibles = new List<Collectible>();
			Projectiles = new List<Projectile>();

			IsTutorial = isTutorial;
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

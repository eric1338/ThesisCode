using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Gameplay
{
	class LevelProgression
	{

		public Vector2 CurrentPlayerPosition { get; set; }
		public List<int> CollectedCollectibleIDs { get; set; }

		public List<Collectible> RemainingCollectibles { get; set; }

		public LevelProgression(Level level)
		{
			RemainingCollectibles = new List<Collectible>(level.Collectibles);

			CurrentPlayerPosition = level.PlayerStartingPosition;
		}

		public void CollectCollectible(Collectible collectible)
		{
			Console.WriteLine("points :)");

			if (RemainingCollectibles.Contains(collectible))
			{
				RemainingCollectibles.Remove(collectible);
			}
		}

	}
}

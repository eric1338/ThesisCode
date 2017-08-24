using GameApp.Levels;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels
{
	class LevelProgression
	{

		public Vector2 CurrentPlayerPosition { get; set; }
		public List<int> CollectedCollectibleIDs { get; set; }

		public List<Collectible> RemainingCollectibles { get; set; }

		public bool IsPlayerStanding { get; set; }

		private float secondsPlayed = 0;
		private float secondsOfGodmodeLeft = 0;

		public LevelProgression(Level level)
		{
			RemainingCollectibles = new List<Collectible>(level.Collectibles);

			CurrentPlayerPosition = level.PlayerStartingPosition;

			IsPlayerStanding = true;
		}

		public void UpdateTime(float timeSinceLastUpdate)
		{
			secondsPlayed += timeSinceLastUpdate;

			if (secondsOfGodmodeLeft > 0) secondsOfGodmodeLeft -= timeSinceLastUpdate;
		}

		public void ActivateGodmode(float secondsOfGodmode)
		{
			secondsOfGodmodeLeft = secondsOfGodmode;
		}

		public bool IsPlayerInGodmode()
		{
			return secondsOfGodmodeLeft > 0;
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

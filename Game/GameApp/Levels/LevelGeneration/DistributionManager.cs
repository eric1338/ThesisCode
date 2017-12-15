using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class DistributionManager
	{

		private Random rng;

		private Dictionary<LevelElementType, int> singleBeatLevelElementDistributions;
		private Dictionary<LevelElementType, int> multipleBeatsLevelElementDistributions;
		private Dictionary<LevelElementType, int> heldNoteLevelElementDistributions;

		public DistributionManager(int rngSeed)
		{
			rng = new Random(rngSeed);
			
			singleBeatLevelElementDistributions = new Dictionary<LevelElementType, int>();
			multipleBeatsLevelElementDistributions = new Dictionary<LevelElementType, int>();
			heldNoteLevelElementDistributions = new Dictionary<LevelElementType, int>();

			Init();
		}

		private void Init()
		{
			singleBeatLevelElementDistributions.Add(LevelElementType.JumpObstacle, 0);
			singleBeatLevelElementDistributions.Add(LevelElementType.HighCollectible, 0);
			singleBeatLevelElementDistributions.Add(LevelElementType.SingleProjectile, 0);

			multipleBeatsLevelElementDistributions.Add(LevelElementType.ChasmWithCollectibles, 0);
			multipleBeatsLevelElementDistributions.Add(LevelElementType.MultipleProjectiles, 0);

			heldNoteLevelElementDistributions.Add(LevelElementType.Chasm, 0);
			heldNoteLevelElementDistributions.Add(LevelElementType.DuckObstacle, 0);
		}

		public List<LevelElementType> GetPossibleSingleBeatLevelElementTypes()
		{
			return GetPossibleLevelElements(singleBeatLevelElementDistributions);
		}

		public void AddSingleBeatLevelElementUse(LevelElementType levelElementType)
		{
			singleBeatLevelElementDistributions[levelElementType] =
				singleBeatLevelElementDistributions[levelElementType] + 1;
		}

		public List<LevelElementType> GetPossibleMultipleBeatsLevelElementTypes()
		{
			return GetPossibleLevelElements(multipleBeatsLevelElementDistributions);
		}

		public void AddMultipleBeatsLevelElementUse(LevelElementType levelElementType)
		{
			multipleBeatsLevelElementDistributions[levelElementType] =
				multipleBeatsLevelElementDistributions[levelElementType] + 1;
		}

		public List<LevelElementType> GetPossibleHeldNoteLevelElementTypes()
		{
			return GetPossibleLevelElements(heldNoteLevelElementDistributions);
		}

		public void AddHeldNoteLevelElementUse(LevelElementType levelElementType)
		{
			heldNoteLevelElementDistributions[levelElementType] =
				heldNoteLevelElementDistributions[levelElementType] + 1;
		}

		private List<LevelElementType> GetPossibleLevelElements(Dictionary<LevelElementType, int> distributions)
		{
			List<LevelElementType> possibleLevelElements = new List<LevelElementType>();

			int lowestOccurenceValue = 9999;

			foreach (KeyValuePair<LevelElementType, int> distribution in distributions.ToList())
			{
				LevelElementType levelElement = distribution.Key;
				int numberOfOccurences = distribution.Value;

				if (numberOfOccurences < lowestOccurenceValue)
				{
					possibleLevelElements.Clear();
					possibleLevelElements.Add(levelElement);

					lowestOccurenceValue = numberOfOccurences;
				}
				else if (numberOfOccurences == lowestOccurenceValue)
				{
					possibleLevelElements.Add(levelElement);
				}
			}

			if (possibleLevelElements.Count < 2) return possibleLevelElements;

			List<LevelElementType> shuffledPossibleLevelElements = new List<LevelElementType>();

			for (int i = possibleLevelElements.Count; i >= 1; i--)
			{
				int randomIndex = rng.Next(i);

				LevelElementType levelElement = possibleLevelElements[randomIndex];

				shuffledPossibleLevelElements.Add(levelElement);
				possibleLevelElements.Remove(levelElement);
			}

			return shuffledPossibleLevelElements;
		}
		

	}
}

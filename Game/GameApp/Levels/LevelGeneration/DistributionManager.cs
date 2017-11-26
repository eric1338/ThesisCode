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

		private LevelElementType lastSingleBeatLevelElement;
		private LevelElementType lastMultipleBeatsLevelElement;
		private LevelElementType lastHeldNoteLevelElement;

		private Dictionary<LevelElementType, int> singleBeatLevelElementDistributions;
		private Dictionary<LevelElementType, int> multipleBeatsLevelElementDistributions;
		private Dictionary<LevelElementType, int> heldNoteLevelElementDistributions;

		public DistributionManager(int rngSeed)
		{
			rng = new Random(rngSeed);

			lastSingleBeatLevelElement = LevelElementType.None;
			lastMultipleBeatsLevelElement = LevelElementType.None;
			lastHeldNoteLevelElement = LevelElementType.None;

			singleBeatLevelElementDistributions = new Dictionary<LevelElementType, int>();
			multipleBeatsLevelElementDistributions = new Dictionary<LevelElementType, int>();
			heldNoteLevelElementDistributions = new Dictionary<LevelElementType, int>();

			Init();
		}

		private void Init()
		{
			singleBeatLevelElementDistributions.Add(LevelElementType.Chasm, 0);
			singleBeatLevelElementDistributions.Add(LevelElementType.JumpObstacle, 0);
			singleBeatLevelElementDistributions.Add(LevelElementType.DuckObstacle, 0);
		}

		public LevelElementType GetNextSingleBeatLevelElementType()
		{
			LevelElementType levelElement = GetNextLevelElement(singleBeatLevelElementDistributions, lastSingleBeatLevelElement);

			lastSingleBeatLevelElement = levelElement;

			return levelElement;
		}

		public LevelElementType GetNextMultipleBeatsLevelElementType()
		{
			LevelElementType levelElement = GetNextLevelElement(multipleBeatsLevelElementDistributions, lastMultipleBeatsLevelElement);

			lastMultipleBeatsLevelElement = levelElement;

			return levelElement;
		}

		public LevelElementType GetNextHeldNoteLevelElementType()
		{
			LevelElementType levelElement = GetNextLevelElement(heldNoteLevelElementDistributions, lastHeldNoteLevelElement);

			lastHeldNoteLevelElement = levelElement;

			return levelElement;
		}

		private LevelElementType GetNextLevelElement(Dictionary<LevelElementType, int> distributions, LevelElementType lastLevelElement)
		{
			List<LevelElementType> possibleLevelElements = new List<LevelElementType>();

			int lowestOccurenceValue = 9999;

			foreach (KeyValuePair<LevelElementType, int> distribution in distributions.ToList())
			{
				LevelElementType levelElement = distribution.Key;
				int numberOfOccurences = distribution.Value;

				if (levelElement == lastLevelElement) continue;

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

			int randomIndex = rng.Next(possibleLevelElements.Count);

			LevelElementType chosenLevelElement = possibleLevelElements[randomIndex];

			distributions[chosenLevelElement] = distributions[chosenLevelElement] + 1;

			return chosenLevelElement;
		}



	}
}

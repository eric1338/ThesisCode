using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class DistributionManager
	{

		private class LevelElementDistribution
		{

			public LevelElementType Type { get; set; }
			public int Occurrences { get; set; }
			public int FavorValue { get; set; }

			public LevelElementDistribution(LevelElementType type, int favorValue)
			{
				Type = type;
				Occurrences = 0;
				FavorValue = favorValue;
			}

			public void AddOccurrence()
			{
				Occurrences++;
			}

		}

		private class LevelElementDistributions
		{

			private List<LevelElementDistribution> distributions;

			public LevelElementDistributions()
			{
				distributions = new List<LevelElementDistribution>();
			}

			public void AddLevelElementType(LevelElementType type, int favorValue = -1)
			{
				distributions.Add(new LevelElementDistribution(type, favorValue));
			}

			public void AddLevelElementOccurence(LevelElementType type)
			{
				foreach (LevelElementDistribution distribution in distributions)
				{
					if (distribution.Type == type) distribution.AddOccurrence();
				}
			}

			public List<LevelElementType> GetOrderedLevelElementTypes()
			{
				List<LevelElementType> types = new List<LevelElementType>();

				distributions.Sort(delegate (LevelElementDistribution d1, LevelElementDistribution d2)
				{
					if (d1.Occurrences != d2.Occurrences) return d1.Occurrences.CompareTo(d2.Occurrences);

					return d1.FavorValue.CompareTo(d2.FavorValue);
				});

				foreach (LevelElementDistribution distribution in distributions)
				{
					types.Add(distribution.Type);
				}

				return types;
			}

		}
		

		private LevelElementDistributions singleBeatDistributions;
		private LevelElementDistributions multipleBeatsDistributions;
		private LevelElementDistributions heldNoteDistributions;

		public DistributionManager()
		{
			singleBeatDistributions = new LevelElementDistributions();
			multipleBeatsDistributions = new LevelElementDistributions();
			heldNoteDistributions = new LevelElementDistributions();

			Init();
		}

		private void Init()
		{
			singleBeatDistributions.AddLevelElementType(LevelElementType.JumpObstacle, 1);
			singleBeatDistributions.AddLevelElementType(LevelElementType.HighCollectible, 2);
			singleBeatDistributions.AddLevelElementType(LevelElementType.SingleProjectile, 3);

			multipleBeatsDistributions.AddLevelElementType(LevelElementType.MultipleProjectiles, 1);
			multipleBeatsDistributions.AddLevelElementType(LevelElementType.ChasmWithCollectibles, 2);

			heldNoteDistributions.AddLevelElementType(LevelElementType.Chasm, 1);
			heldNoteDistributions.AddLevelElementType(LevelElementType.DuckObstacle, 2);
		}

		public void AddLevelElementUse(LevelElementType type)
		{
			singleBeatDistributions.AddLevelElementType(type);
			multipleBeatsDistributions.AddLevelElementType(type);
			heldNoteDistributions.AddLevelElementType(type);
		}

		public List<LevelElementType> GetOrderedSingleBeatLevelElementTypes()
		{
			return singleBeatDistributions.GetOrderedLevelElementTypes();
		}

		public List<LevelElementType> GetOrderedMultipleBeatsLevelElementTypes()
		{
			return multipleBeatsDistributions.GetOrderedLevelElementTypes();
		}

		public List<LevelElementType> GetOrderedHeldNoteLevelElementTypes()
		{
			return heldNoteDistributions.GetOrderedLevelElementTypes();
		}

	}
}

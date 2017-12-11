using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class LevelPlanCreator
	{

		enum SynchronisationType
		{
			SingleBeat,
			MultipleBeats,
			HeldNote
		}



		private SongElements songElements;
		private DistributionManager distributionManager;

		private List<Tuple<float, float>> reservedTimeRanges;

		private float songTimeRange = -1;

		private float timeUsedForHeldNoteLevelElements = 0;
		private float timeUsedForMultipleBeatsLevelElements = 0;

		public LevelPlan LevelPlan { get; set; }

		public LevelPlanCreator(SongElements songElements)
		{
			LevelPlan = new LevelPlan();

			this.songElements = songElements;

			distributionManager = new DistributionManager(GetRandomSeed(songElements));

			reservedTimeRanges = new List<Tuple<float, float>>();
		}


		private int GetRandomSeed(SongElements songElements)
		{
			int seed = songElements.SingleBeats.Count * 3;
			seed += songElements.HeldNotes.Count * 7;

			return seed;
		}

		public void CreateLevelPlan()
		{
			songElements.SortByApplicability();

			CalculateSongTimeMargin();

			AddHeldNotes();
			AddMultipleBeats();
			AddSingleBeats();
			FillWithLowCollectibles();
		}

		private void CalculateSongTimeMargin()
		{
			float earliestTime = 9999;
			float latestTime = -1;

			foreach (SingleBeat singleBeat in songElements.SingleBeats)
			{
				earliestTime = Math.Min(earliestTime, singleBeat.Time);
				latestTime = Math.Max(latestTime, singleBeat.Time);
			}

			foreach (HeldNote heldNote in songElements.HeldNotes)
			{
				earliestTime = Math.Min(earliestTime, heldNote.StartTime);
				latestTime = Math.Max(latestTime, heldNote.EndTime);
			}

			songTimeRange = latestTime - earliestTime;
		}

		private void AddHeldNotes()
		{
			foreach (HeldNote heldNote in songElements.HeldNotes)
			{
				if (heldNote.Applicability < LevelGenerationValues.HeldNoteApplicabilityThreshold) continue;

				if (timeUsedForHeldNoteLevelElements > songTimeRange * 0.5f) continue;

				List<LevelElementType> types = distributionManager.GetPossibleHeldNoteLevelElementTypes();

				foreach (LevelElementType type in types)
				{
					if (TryToAddHeldNoteLevelElement(heldNote, type)) break;
				}
			}
		}

		

		private List<MultipleBeats> CreateMultipleBeatsList()
		{
			List<MultipleBeats> multipleBeatsList = new List<MultipleBeats>();

			List<SingleBeat> tempList = new List<SingleBeat>();

			SingleBeat lastElement = null;

			foreach (SingleBeat singleBeat in songElements.SingleBeats)
			{
				if (lastElement != null)
				{
					float timeDifference = singleBeat.Time - lastElement.Time;

					if (timeDifference < LevelGenerationValues.MaximumTimeMarginForMultipleBeats)
					{
						if (tempList.Count <= 0) tempList.Add(lastElement);

						tempList.Add(singleBeat);
					}
					else if (tempList.Count > 1)
					{
						MultipleBeats multipleBeats = new MultipleBeats();
						multipleBeats.Beats = new List<SingleBeat>(tempList);

						multipleBeatsList.Add(multipleBeats);
						tempList.Clear();
					}
				}

				lastElement = singleBeat;
			}

			multipleBeatsList.Sort(delegate (MultipleBeats c1, MultipleBeats c2)
			{
				return c1.GetAverageApplicabilityValue().CompareTo(c2.GetAverageApplicabilityValue());
			});

			multipleBeatsList.Reverse();

			return multipleBeatsList;
		}

		private void AddMultipleBeats()
		{
			List<MultipleBeats> multipleBeatsList = CreateMultipleBeatsList();

			Console.WriteLine("MultipleBeats");

			foreach (MultipleBeats multipleBeats in multipleBeatsList)
			{
				Console.WriteLine("Applicability: " + multipleBeats.GetAverageApplicabilityValue());

				if (multipleBeats.GetAverageApplicabilityValue() < LevelGenerationValues.HeldNoteApplicabilityThreshold)
				{
					continue;
				}

				if (timeUsedForMultipleBeatsLevelElements > songTimeRange * 0.25f) continue;

				List<LevelElementType> types = distributionManager.GetPossibleMultipleBeatsLevelElementTypes();

				foreach (LevelElementType type in types)
				{
					if (TryToAddMultipleBeatsLevelElement(multipleBeats, type)) break;
				}
			}

			Console.WriteLine(" ");
		}

		private void AddSingleBeats()
		{
			foreach (SingleBeat singleBeat in songElements.SingleBeats)
			{
				if (singleBeat.Applicability < LevelGenerationValues.SingleBeatsApplicabilityThreshold)
				{
					continue;
				}

				List<LevelElementType> types = distributionManager.GetPossibleSingleBeatLevelElementTypes();

				foreach (LevelElementType type in types)
				{
					if (TryToAddSingleBeatLevelElement(singleBeat, type)) break;
				}

			}
		}

		private void FillWithLowCollectibles()
		{
			foreach (SingleBeat singleBeat in songElements.SingleBeats)
			{
				if (singleBeat.Applicability < LevelGenerationValues.FillUpElementsApplicabilityThreshold)
				{
					continue;
				}

				TryToAddSingleBeatLevelElement(singleBeat, LevelElementType.LowCollectible);
			}
		}

		private bool TryToAddHeldNoteLevelElement(HeldNote heldNote, LevelElementType type)
		{
			LevelElementPlacement placement = LevelElementPlacement.CreateProlongedSynchro(type,
				heldNote.StartTime, heldNote.EndTime);

			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(placement);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(placement);

			if (IsTimeRangeFree(leftTimeMargin, rightTimeMargin))
			{
				AddLevelElementPlacement(placement);

				timeUsedForHeldNoteLevelElements += (rightTimeMargin - leftTimeMargin);

				return true;
			}

			return false;
		}

		private bool TryToAddMultipleBeatsLevelElement(MultipleBeats multipleBeats, LevelElementType type)
		{
			LevelElementPlacement placement = LevelElementPlacement.CreateMultipleSynchro(type,
				multipleBeats.GetBeatTimes());

			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(placement);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(placement);

			if (IsTimeRangeFree(leftTimeMargin, rightTimeMargin))
			{
				AddLevelElementPlacement(placement);

				timeUsedForMultipleBeatsLevelElements += (rightTimeMargin - leftTimeMargin);

				return true;
			}

			return false;
		}

		private bool TryToAddSingleBeatLevelElement(SingleBeat singleBeat, LevelElementType type)
		{
			LevelElementPlacement placement = LevelElementPlacement.CreateSingleSynchro(type,
				singleBeat.Time);

			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(placement);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(placement);

			if (IsTimeRangeFree(leftTimeMargin, rightTimeMargin))
			{
				AddLevelElementPlacement(placement);

				return true;
			}

			return false;
		}

		private float GetLeftTimeMarginOfLevelElement(LevelElementPlacement placement)
		{
			return placement.LevelElementStartTime - LevelGenerationValues.TimeBeforeLevelElement;
		}

		private float GetRightTimeMarginOfLevelElement(LevelElementPlacement placement)
		{
			return placement.LevelElementEndTime;
		}

		private bool IsTimeRangeFree(float startTime, float endTime)
		{
			foreach (Tuple<float, float> reservedTime in reservedTimeRanges)
			{
				if (startTime >= reservedTime.Item1 && startTime <= reservedTime.Item2) return false;

				if (endTime >= reservedTime.Item1 && endTime <= reservedTime.Item2) return false;
			}

			return true;
		}

		private void AddLevelElementPlacement(LevelElementPlacement placement)
		{
			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(placement);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(placement);

			ReserveTimeRange(leftTimeMargin, rightTimeMargin);

			LevelPlan.AddLevelElementPlacement(placement);
		}

		private void ReserveTimeRange(float startTime, float endTime)
		{
			reservedTimeRanges.Add(new Tuple<float, float>(startTime, endTime));
		}



	}
}

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

			foreach (SongElements.SingleBeat singleBeat in songElements.SingleBeats)
			{
				earliestTime = Math.Min(earliestTime, singleBeat.Time);
				latestTime = Math.Max(latestTime, singleBeat.Time);
			}

			foreach (SongElements.HeldNote heldNote in songElements.HeldNotes)
			{
				earliestTime = Math.Min(earliestTime, heldNote.StartTime);
				latestTime = Math.Max(latestTime, heldNote.EndTime);
			}

			songTimeRange = latestTime - earliestTime;
		}

		private void AddHeldNotes()
		{
			foreach (SongElements.HeldNote heldNote in songElements.HeldNotes)
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

		private void AddMultipleBeats()
		{

		}

		private void AddSingleBeats()
		{

		}

		private void FillWithLowCollectibles()
		{
			foreach (SongElements.SingleBeat singleBeat in songElements.SingleBeats)
			{
				if (singleBeat.Applicability < LevelGenerationValues.FillUpElementsApplicabilityThreshold)
				{
					continue;
				}

				TryToAddSingleBeatLevelElement(singleBeat, LevelElementType.LowCollectible);
			}
		}

		private bool TryToAddHeldNoteLevelElement(SongElements.HeldNote heldNote, LevelElementType type)
		{
			LevelElementDestination destination = LevelElementDestination.CreateProlongedSynchro(type,
				heldNote.StartTime, heldNote.EndTime);

			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(destination);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(destination);

			if (IsTimeRangeFree(leftTimeMargin, rightTimeMargin))
			{
				AddLevelElementDestination(destination);

				timeUsedForHeldNoteLevelElements += (rightTimeMargin - leftTimeMargin);

				return true;
			}

			return false;
		}

		private bool TryToAddSingleBeatLevelElement(SongElements.SingleBeat singleBeat, LevelElementType type)
		{
			LevelElementDestination destination = LevelElementDestination.CreateSingleSynchro(type,
				singleBeat.Time);

			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(destination);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(destination);

			if (IsTimeRangeFree(leftTimeMargin, rightTimeMargin))
			{
				AddLevelElementDestination(destination);

				return true;
			}

			return false;
		}

		private float GetLeftTimeMarginOfLevelElement(LevelElementDestination destination)
		{
			return destination.LevelElementStartTime - LevelGenerationValues.TimeBeforeLevelElement;
		}

		private float GetRightTimeMarginOfLevelElement(LevelElementDestination destination)
		{
			return destination.LevelElementEndTime;
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

		private void AddLevelElementDestination(LevelElementDestination destination)
		{
			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(destination);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(destination);

			ReserveTimeRange(leftTimeMargin, rightTimeMargin);

			LevelPlan.AddLevelElementDestination(destination);
		}

		private void ReserveTimeRange(float startTime, float endTime)
		{
			reservedTimeRanges.Add(new Tuple<float, float>(startTime, endTime));
		}



	}
}

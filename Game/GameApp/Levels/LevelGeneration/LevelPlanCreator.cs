using GameApp.Audio;
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

		private List<MultipleBeats> multipleBeatsList = new List<MultipleBeats>();

		private List<Tuple<float, float>> reservedTimeRanges;

		public LevelPlan LevelPlan { get; set; }

		public LevelPlanCreator(SongElements songElements)
		{
			LevelPlan = new LevelPlan();

			this.songElements = songElements;

			distributionManager = new DistributionManager();

			reservedTimeRanges = new List<Tuple<float, float>>();
		}

		public void CreateLevelPlan()
		{
			FillMultipleBeatsList();
			CalculateSingleBeatIsolationValues();

			songElements.SortByApplicability();

			AddHeldNotes();
			AddMultipleBeats();
			AddSingleBeats();
			FillWithLowCollectibles();

			LevelPlan.SortByTime();
		}

		private void FillMultipleBeatsList()
		{
			songElements.SortByTime();

			multipleBeatsList.Clear();

			MultipleBeatsCreator multipleBeatsCreator = new MultipleBeatsCreator();

			multipleBeatsList = multipleBeatsCreator.GetMultipleBeats(songElements.SingleBeats);

			multipleBeatsList.Sort(delegate (MultipleBeats c1, MultipleBeats c2)
			{
				return c1.Applicability.CompareTo(c2.Applicability);
			});

			multipleBeatsList.Reverse();
		}

		private void CalculateSingleBeatIsolationValues()
		{
			songElements.SortByTime();

			List<SingleBeat> singleBeats = songElements.SingleBeats;

			for (int i = 1; i < singleBeats.Count - 1; i++)
			{
				float freeTimeBefore = singleBeats[i].Time - singleBeats[i - 1].Time;
				float freeTimeAfter = singleBeats[i + 1].Time - singleBeats[i].Time;

				singleBeats[i].IsolationValue = freeTimeBefore * freeTimeAfter;
			}
		}







		

		private void AddHeldNotes()
		{
			foreach (HeldNote heldNote in songElements.HeldNotes)
			{
				if (heldNote.Applicability < LevelGenerationValues.HeldNoteApplicabilityThreshold) continue;

				List<LevelElementType> types = distributionManager.GetOrderedHeldNoteLevelElementTypes();

				foreach (LevelElementType type in types)
				{
					if (TryToAddHeldNoteLevelElement(heldNote, type)) break;
				}
			}
		}
		

		private void AddMultipleBeats()
		{
			foreach (MultipleBeats multipleBeats in multipleBeatsList)
			{
				if (multipleBeats.Applicability < LevelGenerationValues.HeldNoteApplicabilityThreshold)
				{
					continue;
				}

				List<LevelElementType> types = distributionManager.GetOrderedMultipleBeatsLevelElementTypes();

				foreach (LevelElementType type in types)
				{
					if (TryToAddMultipleBeatsLevelElement(multipleBeats, type)) break;
				}
			}
		}

		private void AddSingleBeats()
		{
			foreach (SingleBeat singleBeat in songElements.SingleBeats)
			{
				if (singleBeat.Applicability < LevelGenerationValues.SingleBeatsApplicabilityThreshold)
				{
					continue;
				}

				List<LevelElementType> types = distributionManager.GetOrderedSingleBeatLevelElementTypes();

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

				LevelElementPlacement placement = LevelElementPlacement.CreateSingleSynchro(
					LevelElementType.LowCollectible, singleBeat.Time);

				if (IsTimeRangeFree(placement.LevelElementStartTime, placement.LevelElementEndTime))
				{
					AddLevelElementPlacement(placement);
				}
			}
		}


		private bool TryToAddHeldNoteLevelElement(HeldNote heldNote, LevelElementType type)
		{
			LevelElementPlacement placement = LevelElementPlacement.CreateProlongedSynchro(
				type, heldNote.StartTime, heldNote.EndTime);

			return TryToAddLevelElement(placement);
		}

		private bool TryToAddMultipleBeatsLevelElement(MultipleBeats multipleBeats, LevelElementType type)
		{
			LevelElementPlacement placement = LevelElementPlacement.CreateMultipleSynchro(
				type, multipleBeats.GetBeatTimes());

			return TryToAddLevelElement(placement);
		}

		private bool TryToAddSingleBeatLevelElement(SingleBeat singleBeat, LevelElementType type)
		{
			LevelElementPlacement placement = LevelElementPlacement.CreateSingleSynchro(
				type, singleBeat.Time);

			return TryToAddLevelElement(placement);
		}

		private bool TryToAddLevelElement(LevelElementPlacement placement)
		{
			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(placement);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(placement);

			if (IsTimeRangeFree(leftTimeMargin, rightTimeMargin))
			{
				AddLevelElementPlacement(placement);

				distributionManager.AddLevelElementUse(placement.Type);

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

				if (startTime <= reservedTime.Item1 && endTime >= reservedTime.Item2) return false;
			}

			return true;
		}

		private void AddLevelElementPlacement(LevelElementPlacement placement)
		{
			float leftTimeMargin = GetLeftTimeMarginOfLevelElement(placement);
			float rightTimeMargin = GetRightTimeMarginOfLevelElement(placement);

			reservedTimeRanges.Add(new Tuple<float, float>(leftTimeMargin, rightTimeMargin));

			LevelPlan.AddLevelElementPlacement(placement);
		}

	}
}

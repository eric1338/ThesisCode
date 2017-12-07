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

		public class Synchronisations
		{

			public List<SingleBeatSynchronisation> SingleBeatSynchronisations { get; set; }
			public List<MultipleBeatsSynchronisation> MultipleBeatsSynchronisations { get; set; }
			public List<HeldNoteSynchronisation> HeldNoteSynchronisations { get; set; }

			public Synchronisations()
			{
				SingleBeatSynchronisations = new List<SingleBeatSynchronisation>();
				MultipleBeatsSynchronisations = new List<MultipleBeatsSynchronisation>();
				HeldNoteSynchronisations = new List<HeldNoteSynchronisation>();
			}

		}


		public class SingleBeatSynchronisation
		{
			public float SynchronisationTime { get; set; }

			public SingleBeatSynchronisation(float synchronisationTime)
			{
				SynchronisationTime = synchronisationTime;
			}

		}

		public class MultipleBeatsSynchronisation
		{
			public List<float> SynchronisationTimes { get; set; }

			public MultipleBeatsSynchronisation(List<float> synchronisationTimes)
			{
				SynchronisationTimes = synchronisationTimes;
			}

		}

		public class HeldNoteSynchronisation
		{
			public float SynchronisationStartingTime { get; set; }
			public float SynchronisationEndTime { get; set; }

			public HeldNoteSynchronisation(float synchronisationStartingTime, float synchronisationEndTime)
			{
				SynchronisationStartingTime = synchronisationStartingTime;
				SynchronisationEndTime = synchronisationEndTime;
			}

		}



		private Synchronisations synchronisations;
		private DistributionManager distributionManager;

		public LevelPlanCreator(Synchronisations synchronisations)
		{
			this.synchronisations = synchronisations;

			distributionManager = new DistributionManager(GetRandomSeed(synchronisations));
		}

		public void CreateLevelPlan()
		{
			LevelPlan levelPlan = new LevelPlan();

			// TODO: evtl Synchronisations nach Zeit sortieren

			foreach (SingleBeatSynchronisation singleBeatSynchronisation in synchronisations.SingleBeatSynchronisations)
			{
				LevelElementType levelElementType = distributionManager.GetNextSingleBeatLevelElementType();


			}

		}





		private List<Tuple<float, float>> reservedTimeRanges;

		private void ReserveTimeRange(float startTime, float endTime)
		{
			reservedTimeRanges.Add(new Tuple<float, float>(startTime, endTime));
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

		public void Test(SongElements songElements)
		{
			reservedTimeRanges = new List<Tuple<float, float>>();

			songElements.SortByApplicability();


			LevelElementDestination destination = LevelElementDestination.CreateChasm(-1, -1);

			float startTime = destination.LevelElementStartTime - LevelGenerationValues.TimeBeforeLevelElement;
			float endTime = destination.LevelElementEndTime;

			if (IsTimeRangeFree(startTime, endTime))
			{

			}
		}

		private void Add


		

		private void GetA(SongElements songElements)
		{

		}



		private void TryToAddLevelElement()
		{

		}



		private int GetRandomSeed(Synchronisations synchronisations)
		{
			int seed = synchronisations.SingleBeatSynchronisations.Count * 3;
			seed += synchronisations.MultipleBeatsSynchronisations.Count * 7;
			seed += synchronisations.HeldNoteSynchronisations.Count * 11;

			return seed;
		}



	}
}

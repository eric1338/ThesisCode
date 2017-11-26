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

		private int GetRandomSeed(Synchronisations synchronisations)
		{
			int seed = synchronisations.SingleBeatSynchronisations.Count * 3;
			seed += synchronisations.MultipleBeatsSynchronisations.Count * 7;
			seed += synchronisations.HeldNoteSynchronisations.Count * 11;

			return seed;
		}



	}
}

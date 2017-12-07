using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class SongElements
	{

		public class SingleBeat
		{
			public float Time { get; set; }
			public float Applicability { get; set; }

			public SingleBeat(float time, float applicability)
			{
				Time = time;
				Applicability = applicability;
			}

		}

		public class HeldNote
		{
			public float StartTime { get; set; }
			public float EndTime { get; set; }
			public float Applicability { get; set; }

			public List<SingleBeat> ParallelBeats { get; set; }

			public HeldNote(float startingTime, float endTime, float applicability)
			{
				StartTime = startingTime;
				EndTime = endTime;
				Applicability = applicability;

				ParallelBeats = new List<SingleBeat>();
			}

			public void AddParallelBeat(SingleBeat singleBeat)
			{
				ParallelBeats.Add(singleBeat);
			}

			public float GetParallelBeatsApplicabilitySum(float threshold)
			{
				float sum = 0;

				foreach (SingleBeat singleBeat in ParallelBeats)
				{
					if (singleBeat.Applicability > threshold) sum += singleBeat.Applicability;
				}

				return sum;
			}
		}

		public List<SingleBeat> SingleBeats { get; set; }

		public List<HeldNote> HeldNotes { get; set; }


		public SongElements()
		{
			SingleBeats = new List<SingleBeat>();
			HeldNotes = new List<HeldNote>();
		}

		public void AddSingleBeat(float time, float applicability)
		{
			SingleBeats.Add(new SingleBeat(time, applicability));
		}

		public void AddHeldNote(float startingTime, float endTime, float applicability)
		{
			HeldNotes.Add(new HeldNote(startingTime, endTime, applicability));
		}

		public void SortByApplicability()
		{
			SingleBeats.Sort(delegate (SingleBeat s1, SingleBeat s2)
			{
				return s1.Applicability.CompareTo(s2.Applicability);
			});

			HeldNotes.Sort(delegate(HeldNote h1, HeldNote h2)
			{
				return h1.Applicability.CompareTo(h2.Applicability);
			});
		}

		public void DetermineParallelBeatsOfHeldNotes()
		{
			foreach (HeldNote heldNote in HeldNotes)
			{
				foreach (SingleBeat singleBeat in SingleBeats)
				{
					if (singleBeat.Time > heldNote.StartTime && singleBeat.Time < heldNote.EndTime)
					{
						heldNote.AddParallelBeat(singleBeat);
					}
				}
			}
		}



	}
}

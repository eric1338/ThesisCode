using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class SongElements
	{

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

			SingleBeats.Reverse();

			HeldNotes.Sort(delegate(HeldNote h1, HeldNote h2)
			{
				return h1.Applicability.CompareTo(h2.Applicability);
			});

			HeldNotes.Reverse();
		}



	}
}

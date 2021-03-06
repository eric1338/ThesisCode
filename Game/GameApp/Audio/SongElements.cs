﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
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

		public void SortByTime()
		{
			SingleBeats.Sort(delegate (SingleBeat s1, SingleBeat s2)
			{
				return s1.Time.CompareTo(s2.Time);
			});

			HeldNotes.Sort(delegate (HeldNote h1, HeldNote h2)
			{
				return h1.StartTime.CompareTo(h2.StartTime);
			});
		}

		public void SortByApplicabilityAndIsolationValue()
		{
			SingleBeats.Sort(delegate (SingleBeat s1, SingleBeat s2)
			{
				return s1.IsolationValue.CompareTo(s2.IsolationValue);
			});

			SingleBeats.Reverse();

			HeldNotes.Sort(delegate (HeldNote h1, HeldNote h2)
			{
				return h1.Applicability.CompareTo(h2.Applicability);
			});

			HeldNotes.Reverse();
		}

	}
}

using GameApp.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class MultipleBeats
	{

		public List<SingleBeat> Beats { get; set; }

		public float Applicability { get; set; }

		public MultipleBeats()
		{
			Beats = new List<SingleBeat>();
		}


		public void AddBeat(SingleBeat beat)
		{
			Beats.Add(beat);
		}

		public void CalculateApplicability()
		{
			float applicabilitySum = 0;

			foreach (SingleBeat beat in Beats)
			{
				applicabilitySum += beat.Applicability;
			}

			float averageTimeDelta = (Beats[Beats.Count - 1].Time - Beats[0].Time) / (Beats.Count - 1);

			Applicability = (applicabilitySum / Beats.Count) * 0.5f + (1 - averageTimeDelta) * 0.5f;
		}

		public List<float> GetBeatTimes()
		{
			List<float> beatTimes = new List<float>();

			foreach (SingleBeat beat in Beats)
			{
				beatTimes.Add(beat.Time);
			}

			return beatTimes;
		}

		/*
		public float GetAverageApplicabilityValue()
		{
			if (Beats.Count <= 0) return 0;

			float applicabilitySum = 0;

			foreach (SingleBeat singleBeat in Beats)
			{
				applicabilitySum += singleBeat.Applicability;
			}

			return applicabilitySum / Beats.Count;
		}
		*/
	}
}

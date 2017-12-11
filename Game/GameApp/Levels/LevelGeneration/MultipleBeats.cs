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

		public MultipleBeats()
		{
			Beats = new List<SingleBeat>();
		}

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

		public List<float> GetBeatTimes()
		{
			List<float> beatTimes = new List<float>();

			foreach (SingleBeat beat in Beats)
			{
				beatTimes.Add(beat.Time);
			}

			return beatTimes;
		}
	}
}

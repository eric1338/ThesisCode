using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class SingleBeat
	{
		public float Time { get; set; }
		public float Applicability { get; set; }

		public float IsolationValue { get; set; }

		public SingleBeat(float time, float applicability)
		{
			Time = time;
			Applicability = applicability;

			IsolationValue = -1;
		}
	}
}

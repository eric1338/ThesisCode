using GameApp.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Levels.LevelGeneration
{
	class MultipleBeatsCreator
	{



		public List<MultipleBeats> GetMultipleBeats(List<SingleBeat> singleBeats)
		{

			List<List<SingleBeat>> chains = new List<List<SingleBeat>>();

			SingleBeat lastBeat = null;
			List<SingleBeat> tempBeatList = new List<SingleBeat>();

			foreach (SingleBeat beat in singleBeats)
			{
				if (lastBeat == null)
				{
					lastBeat = beat;
					tempBeatList.Add(beat);
					continue;
				}

				float timeDelta = beat.Time - lastBeat.Time;

				if (timeDelta <= LevelGenerationValues.MaximumMultipleBeatsTimeDelta)
				{
					tempBeatList.Add(beat);
				}
				else
				{
					if (tempBeatList.Count > LevelGenerationValues.MininumBeatsForMultipleBeats)
					{
						chains.Add(new List<SingleBeat>(tempBeatList));
					}

					tempBeatList.Clear();
				}

				lastBeat = beat;
			}

			List<MultipleBeats> multipleBeats = new List<MultipleBeats>();

			foreach (List<SingleBeat> chain in chains)
			{
				for (int i = 0; i < chain.Count; i++)
				{
					if (i + LevelGenerationValues.MininumBeatsForMultipleBeats > chain.Count) continue;

					MultipleBeats beatChain = new MultipleBeats();

					SingleBeat firstBeat = chain[i];

					beatChain.AddBeat(firstBeat);

					for (int j = i + 1; j < chain.Count; j++)
					{
						SingleBeat nextBeat = chain[j];

						if (nextBeat.Time - firstBeat.Time >
							LevelGenerationValues.MaximumMultipleBeatsTotalTimeMargin) break;

						beatChain.AddBeat(nextBeat);
					}

					beatChain.CalculateApplicability();

					multipleBeats.Add(beatChain);
				}
			}

			return multipleBeats;
		}

	}
}

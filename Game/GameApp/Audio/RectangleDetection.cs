using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class RectangleDetection
	{

		private class PossibleHeldNote
		{
			public float StartTime { get; set; }
			public float EndTime { get; set; }
			public float AmplitudeMeanValue { get; set; }
			public float Applicability { get; set; }

			public float GetDuration()
			{
				return EndTime - StartTime;
			}
		}

		private class FinalHeldNotes
		{
			public List<PossibleHeldNote> HeldNotes { get; set; }

			public FinalHeldNotes()
			{
				HeldNotes = new List<PossibleHeldNote>();
			}

			public void SortByTime()
			{
				HeldNotes.Sort(delegate (PossibleHeldNote h1, PossibleHeldNote h2)
				{
					return h1.StartTime.CompareTo(h2.StartTime);
				});
			}

			public void TryToAddHeldNote(PossibleHeldNote heldNote)
			{
				if (!IsInOtherHeldNote(heldNote)) HeldNotes.Add(heldNote);
			}

			private bool IsInOtherHeldNote(PossibleHeldNote newHeldNote)
			{
				foreach (PossibleHeldNote heldNote in HeldNotes)
				{
					if (IsInBetween(heldNote, newHeldNote.StartTime) ||
						IsInBetween(heldNote, newHeldNote.EndTime))
					{
						return true;
					}
					if (IsInBetween(newHeldNote, heldNote.StartTime) ||
						IsInBetween(newHeldNote, heldNote.EndTime))
					{
						return true;
					}
				}

				return false;
			}

			private bool IsInBetween(PossibleHeldNote heldNote, float value)
			{
				return heldNote.StartTime <= value && heldNote.EndTime >= value;
			}

		}


		public float MinimumTimeWidth { get; set; }

		public float ValueThreshold { get; set; }
		public float MaximumSquaredError { get; set; }

		public RectangleDetection()
		{
			MinimumTimeWidth = 0.9f;

			ValueThreshold = 0.0001f;
			MaximumSquaredError = 0.00005f;
		}


		public void AddHeldNotesFromFrequencyBands(SongElements songElements, List<FrequencyBand> frequencyBands)
		{
			List<PossibleHeldNote> possibleHeldNotes = new List<PossibleHeldNote>();

			foreach (FrequencyBand frequencyBand in frequencyBands)
			{
				possibleHeldNotes.AddRange(GetHeldNotesFromFrequencyBand(frequencyBand));
			}

			possibleHeldNotes.Sort(delegate (PossibleHeldNote h1, PossibleHeldNote h2)
			{
				return h1.AmplitudeMeanValue.CompareTo(h2.AmplitudeMeanValue);
			});
			possibleHeldNotes.Reverse();

			FinalHeldNotes finalHeldNotes = new FinalHeldNotes();

			foreach (PossibleHeldNote heldNote in possibleHeldNotes)
			{
				finalHeldNotes.TryToAddHeldNote(heldNote);
			}

			finalHeldNotes.SortByTime();

			foreach (PossibleHeldNote heldNote in finalHeldNotes.HeldNotes)
			{
				songElements.AddHeldNote(heldNote.StartTime, heldNote.EndTime, heldNote.Applicability);
			}
		}

		private List<PossibleHeldNote> GetHeldNotesFromFrequencyBand(FrequencyBand newFrequencyBand)
		{
			List<PossibleHeldNote> heldNotes = new List<PossibleHeldNote>();

			FrequencyBand.FBPoint lastValidPoint = null;

			float amplitudeSum = 0;
			int amplitudesN = 0;

			float timeDelta = newFrequencyBand.FBPoints[1].Time - newFrequencyBand.FBPoints[0].Time;

			foreach (FrequencyBand.FBPoint fbPoint in newFrequencyBand.FBPoints)
			{
				if (fbPoint.MinimumSquaredErrorToPrevious < MaximumSquaredError)
				{
					if (lastValidPoint == null) lastValidPoint = fbPoint;

					amplitudeSum += fbPoint.Amplitude;
					amplitudesN++;
				}
				else
				{
					if (lastValidPoint != null)
					{
						float timeDifference = fbPoint.Time - lastValidPoint.Time;

						if (timeDifference > MinimumTimeWidth)
						{
							PossibleHeldNote heldNote = new PossibleHeldNote();

							heldNote.StartTime = lastValidPoint.Time;
							heldNote.EndTime = fbPoint.Time - timeDelta;

							float amplitudeMeanValue = amplitudeSum / amplitudesN;

							heldNote.AmplitudeMeanValue = amplitudeMeanValue;
							heldNote.Applicability = amplitudeMeanValue;

							heldNotes.Add(heldNote);
						}

						lastValidPoint = null;
					}

					amplitudeSum = 0;
					amplitudesN = 0;
				}
			}

			return heldNotes;
		}

	}
}

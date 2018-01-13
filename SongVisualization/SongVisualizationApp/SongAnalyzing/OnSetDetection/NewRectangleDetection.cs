using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class NewRectangleDetection
	{

		public class HeldNote : IComparable
		{
			public float StartTime { get; set; }
			public float EndTime { get; set; }
			public float AmplitudeMeanValue { get; set; }
			public float ConfidenceValue { get; set; }

			public float GetDuration()
			{
				return EndTime - StartTime;
			}

			public int CompareTo(object obj)
			{
				if (obj is HeldNote)
				{
					HeldNote otherHeldNote = obj as HeldNote;

					return GetDuration().CompareTo(otherHeldNote.GetDuration());
				}

				return -1;
			}
		}

		public float ValueThreshold { get; set; }

		public float MinimumTimeWidth { get; set; }


		public static float TEST_VALUE_THRESHOLD = 0.1f;
		public static float TEST_RECDETEC_MINIMUM_TIME_WIDTH = 0.9f;

		public NewRectangleDetection()
		{
			ValueThreshold = TEST_VALUE_THRESHOLD;
			MinimumTimeWidth = TEST_RECDETEC_MINIMUM_TIME_WIDTH;
		}

		private class HeldNotes
		{
			public List<HeldNote> HeldNotesList { get; set; }

			public HeldNotes()
			{
				HeldNotesList = new List<HeldNote>();
			}

			public void SortByTime()
			{
				HeldNotesList.Sort(delegate (HeldNote h1, HeldNote h2)
				{
					return h1.StartTime.CompareTo(h2.StartTime);
				});
			}

			public void TryToAddHeldNote(HeldNote heldNote)
			{
				if (!IsInOtherHeldNote(heldNote)) HeldNotesList.Add(heldNote);
			}

			private bool IsInOtherHeldNote(HeldNote newHeldNote)
			{
				foreach (HeldNote heldNote in HeldNotesList)
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

			private bool IsInBetween(HeldNote heldNote, float value)
			{
				return heldNote.StartTime <= value && heldNote.EndTime >= value;
			}

		}

		public SongPropertyValues GetHeldNotes(List<NewFrequencyBand> frequencyBands)
		{
			List<HeldNote> allHeldNotes = new List<HeldNote>();

			foreach (NewFrequencyBand frequencyBand in frequencyBands)
			{
				allHeldNotes.AddRange(GetHeldNotesFromFrequencyBand(frequencyBand));
			}

			/*
			allHeldNotes.Sort(delegate (HeldNote h1, HeldNote h2)
			{
				return h1.GetDuration().CompareTo(h2.GetDuration());
			});
			allHeldNotes.Reverse();
			*/

			allHeldNotes.Sort(delegate (HeldNote h1, HeldNote h2)
			{
				return h1.AmplitudeMeanValue.CompareTo(h2.AmplitudeMeanValue);
			});
			allHeldNotes.Reverse();

			//allHeldNotes.Sort(delegate (HeldNote h1, HeldNote h2)
			//{
			//	return h1.ConfidenceValue.CompareTo(h2.ConfidenceValue);
			//});
			//allHeldNotes.Reverse();

			HeldNotes heldNotesObj = new HeldNotes();

			float highestConfidenceValue = -1;
			float lowestConfidenceValue = 9999;

			foreach (HeldNote heldNote in allHeldNotes)
			{
				heldNotesObj.TryToAddHeldNote(heldNote);

				highestConfidenceValue = Math.Max(highestConfidenceValue, heldNote.ConfidenceValue);
				lowestConfidenceValue = Math.Min(lowestConfidenceValue, heldNote.ConfidenceValue);
			}

			heldNotesObj.SortByTime();



			List<MyPoint> testPoints = new List<MyPoint>();

			testPoints.Add(new MyPoint(0, 0));

			float span = highestConfidenceValue - lowestConfidenceValue;

			foreach (HeldNote heldNote in heldNotesObj.HeldNotesList)
			{
				float val = (heldNote.ConfidenceValue - lowestConfidenceValue) / span;

				testPoints.Add(new MyPoint(heldNote.StartTime - 0.03f, 0));
				testPoints.Add(new MyPoint(heldNote.StartTime, val));
				testPoints.Add(new MyPoint(heldNote.EndTime, val));
				testPoints.Add(new MyPoint(heldNote.EndTime + 0.03f, 0));
			}

			SongPropertyValues heldNoteValues = new SongPropertyValues("Held Notes " + DateTime.Now.Millisecond);

			heldNoteValues.Points = testPoints;

			return heldNoteValues;
		}


		public static float TEST_MAXIMUM_SQUARED_ERROR = 0.0001f;
		

		private List<HeldNote> GetHeldNotesFromFrequencyBand(NewFrequencyBand newFrequencyBand)
		{
			List<HeldNote> heldNotes = new List<HeldNote>();

			MyPoint lastValidPoint1 = null;

			float highestMinimumSquaredError = -1;
			float squaredErrorSum = 0;
			int squaredErrorsN = 0;

			foreach (MyPoint minimumSquaredErrorToPrevious in newFrequencyBand.MinimumSquaredErrorsToPrevious)
			{
				float minimumSquaredError = minimumSquaredErrorToPrevious.Y;

				if (minimumSquaredError < TEST_MAXIMUM_SQUARED_ERROR)
				{
					if (lastValidPoint1 == null) lastValidPoint1 = minimumSquaredErrorToPrevious;

					highestMinimumSquaredError = Math.Max(highestMinimumSquaredError, minimumSquaredError);

					squaredErrorSum += minimumSquaredError;
					squaredErrorsN++;
				}
				else
				{
					if (lastValidPoint1 != null)
					{
						float timeDifference = minimumSquaredErrorToPrevious.X - lastValidPoint1.X;

						if (timeDifference > MinimumTimeWidth)
						{
							HeldNote heldNote = new HeldNote();

							heldNote.StartTime = lastValidPoint1.X;
							heldNote.EndTime = minimumSquaredErrorToPrevious.X;
							heldNote.AmplitudeMeanValue = GetMeanAmplitude(newFrequencyBand, lastValidPoint1.X,
								minimumSquaredErrorToPrevious.X);
							heldNote.ConfidenceValue = heldNote.AmplitudeMeanValue;
							//heldNote.ConfidenceValue = (1 - highestMinimumSquaredError);
							//heldNote.ConfidenceValue = squaredErrorSum / squaredErrorsN;

							heldNotes.Add(heldNote);
						}

						lastValidPoint1 = null;
						highestMinimumSquaredError = -1;

						squaredErrorSum = 0;
						squaredErrorsN = 0;
					}

				}
			}

			return heldNotes;
		}

		private float GetMeanAmplitude(NewFrequencyBand newFrequencyBand, float startTime, float endTime)
		{
			float amplitudeSum = 0;
			int amplitudeN = 0;

			foreach (MyPoint amplitudePoint in newFrequencyBand.MaxAmplitudes)
			{
				if (amplitudePoint.X < startTime) continue;
				if (amplitudePoint.X > endTime) break;

				amplitudeSum += amplitudePoint.Y;
				amplitudeN++;
			}

			return amplitudeSum / amplitudeN;
		}
		

	}
}

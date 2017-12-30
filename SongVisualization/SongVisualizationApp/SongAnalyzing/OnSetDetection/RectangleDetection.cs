using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.SongAnalyzing.OnSetDetection
{
	class RectangleDetection
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

		public RectangleDetection()
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

		public SongPropertyValues GetHeldNotes(List<FrequencyBand> frequencyBands)
		{
			List<HeldNote> allHeldNotes = new List<HeldNote>();

			foreach (FrequencyBand frequencyBand in frequencyBands)
			{
				allHeldNotes.AddRange(GetHeldNotesFromFrequencyBand(frequencyBand.Points));
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

			HeldNotes heldNotesObj = new HeldNotes();

			foreach (HeldNote heldNote in allHeldNotes) heldNotesObj.TryToAddHeldNote(heldNote);

			heldNotesObj.SortByTime();

			List<MyPoint> testPoints = new List<MyPoint>();

			testPoints.Add(new MyPoint(0, 0));

			foreach (HeldNote heldNote in heldNotesObj.HeldNotesList)
			{
				testPoints.Add(new MyPoint(heldNote.StartTime - 0.03f, 0));
				testPoints.Add(new MyPoint(heldNote.StartTime, heldNote.AmplitudeMeanValue));
				testPoints.Add(new MyPoint(heldNote.EndTime, heldNote.AmplitudeMeanValue));
				testPoints.Add(new MyPoint(heldNote.EndTime + 0.03f, 0));
			}

			SongPropertyValues heldNoteValues = new SongPropertyValues("Held Notes " + DateTime.Now.Millisecond);

			heldNoteValues.Points = testPoints;

			return heldNoteValues;
		}


		public static float TEST_MAXIMUM_SQUARED_ERROR = 0.0244f;


		private class Pocket
		{

			public List<MyPoint> Points { get; set; }

			public float Mean { get; set; }
			public float ErrorSum { get; set; }
			
			public Pocket(List<MyPoint> points)
			{
				Points = points;

				Mean = -1;
				ErrorSum = 0;
			}

			public bool CalculateError()
			{
				CalculateMean();

				foreach (MyPoint point in Points)
				{
					float squaredError = (float)Math.Pow(point.Y - Mean, 2);

					if (squaredError > TEST_MAXIMUM_SQUARED_ERROR) return false;

					ErrorSum += squaredError;
				}

				return true;
			}

			private void CalculateMean()
			{
				float sum = 0;

				foreach (MyPoint point in Points) sum += point.Y;

				Mean = sum / Points.Count;
			}

		}


		private List<HeldNote> GetHeldNotesFromFrequencyBand(List<MyPoint> frequencyBandPoints)
		{
			List<HeldNote> heldNotes = new List<HeldNote>();

			float pocketTimeWidth = MinimumTimeWidth / 2.0f;

			int elementsPerPocket = 0;

			float startTime = frequencyBandPoints[0].X;

			foreach (MyPoint point in frequencyBandPoints)
			{
				if (point.X - startTime > pocketTimeWidth) break;
				elementsPerPocket++;
			}

			List<Pocket> pockets = new List<Pocket>();

			int maxIndex = (int)Math.Floor(frequencyBandPoints.Count / (float)elementsPerPocket) * elementsPerPocket;

			for (int i = 0; i < maxIndex; i += elementsPerPocket)
			{
				List<MyPoint> pocketPoints = new List<MyPoint>();
				bool fullPocket = true;

				for (int j = i; j < i + elementsPerPocket; j++)
				{
					MyPoint point = frequencyBandPoints[j];

					if (point.Y < ValueThreshold)
					{
						fullPocket = false;
						break;
					}

					pocketPoints.Add(point);
				}

				if (fullPocket)
				{
					Pocket pocket = new Pocket(pocketPoints);

					bool isValid = pocket.CalculateError();

					if (isValid) pockets.Add(pocket);
				}
			}

			pockets.Sort(delegate(Pocket p1, Pocket p2)
			{
				return p1.ErrorSum.CompareTo(p2.ErrorSum);
			});

			
			foreach (Pocket pocket in pockets)
			{
				HeldNote heldNote = GetHeldNoteTest(frequencyBandPoints, pocket);

				if (heldNote != null) heldNotes.Add(heldNote);
			}

			return heldNotes;
		}


		private HeldNote GetHeldNoteTest(List<MyPoint> frequencyBandPoints, Pocket pocket)
		{
			List<MyPoint> pocketPoints = pocket.Points;

			int leftestIndex = frequencyBandPoints.IndexOf(pocketPoints[0]);
			int rightestIndex = frequencyBandPoints.IndexOf(pocketPoints[pocketPoints.Count - 1]);


			for (int i = leftestIndex - 1; i >= 0; i--)
			{
				if (!IsValueValid(frequencyBandPoints[i].Y, pocket.Mean)) break;

				leftestIndex = i;
			}

			for (int i = rightestIndex + 1; i < frequencyBandPoints.Count; i++)
			{
				if (!IsValueValid(frequencyBandPoints[i].Y, pocket.Mean)) break;

				rightestIndex = i;
			}

			float startTime = frequencyBandPoints[leftestIndex].X;
			float endTime = frequencyBandPoints[rightestIndex].X;

			if ((endTime - startTime) < MinimumTimeWidth) return null;

			HeldNote heldNote = new HeldNote();

			heldNote.StartTime = startTime;
			heldNote.EndTime = endTime;

			heldNote.AmplitudeMeanValue = pocket.Mean;
			heldNote.ConfidenceValue = 1;

			return heldNote;
		}

		private bool IsValueValid(float value, float mean)
		{
			if (value < ValueThreshold) return false;

			if ((float)Math.Pow(value - mean, 2) > TEST_MAXIMUM_SQUARED_ERROR) return false;

			return true;
		}

	}
}

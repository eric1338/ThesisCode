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

		public RectangleDetection()
		{
			ValueThreshold = 0.15f;
			MinimumTimeWidth = 0.5f;
		}

		public void RectangulateSongPropertyValues(SongPropertyValues values)
		{
			List<MyPoint> recPoints = GetRectanglePoints(values.Points);

			values.Points = recPoints;
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

			allHeldNotes.Sort(delegate (HeldNote h1, HeldNote h2)
			{
				return h1.GetDuration().CompareTo(h2.GetDuration());
			});
			allHeldNotes.Reverse();

			HeldNotes heldNotesObj = new HeldNotes();

			foreach (HeldNote heldNote in allHeldNotes) heldNotesObj.TryToAddHeldNote(heldNote);

			heldNotesObj.SortByTime();

			Console.WriteLine("------");

			foreach (HeldNote heldNote in heldNotesObj.HeldNotesList)
			{
				Console.WriteLine(Math.Round(heldNote.StartTime, 1) + " - " + Math.Round(heldNote.EndTime, 1));
			}

			Console.WriteLine("------");

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

		private List<HeldNote> GetHeldNotesFromFrequencyBand(List<MyPoint> curve)
		{
			List<HeldNote> heldNotes = new List<HeldNote>();

			float rectangleStartTime = -1;
			List<MyPoint> possibleRectanglePoints = new List<MyPoint>();

			foreach (MyPoint point in curve)
			{
				float pointTime = point.X;
				float pointValue = point.Y;

				if (pointValue < ValueThreshold && possibleRectanglePoints.Count > 0)
				{
					if ((pointTime - rectangleStartTime) >= MinimumTimeWidth)
					{
						heldNotes.Add(GetHeldNote(possibleRectanglePoints));
					}

					rectangleStartTime = -1;
					possibleRectanglePoints.Clear();
				}
				else
				{
					if (rectangleStartTime < 0) rectangleStartTime = pointTime;

					possibleRectanglePoints.Add(point);
				}
			}

			return heldNotes;
		}

		private float DeviationTolerance = 0.2f;


		private HeldNote GetHeldNote(List<MyPoint> rectanglePoints)
		{
			HeldNote heldNote = new HeldNote();

			float sum = 0;

			foreach (MyPoint point in rectanglePoints)
			{
				sum += point.Y;
			}

			float mean = sum / rectanglePoints.Count;

			heldNote.AmplitudeMeanValue = mean;

			float confidenceSum = 0;

			foreach (MyPoint point in rectanglePoints)
			{
				confidenceSum += 1 - Math.Max(0, Math.Abs(point.Y - mean) - DeviationTolerance);
			}

			heldNote.ConfidenceValue = confidenceSum / rectanglePoints.Count;

			heldNote.StartTime = rectanglePoints[0].X;
			heldNote.EndTime = rectanglePoints[rectanglePoints.Count - 1].X;

			return heldNote;
		}








		private List<MyPoint> GetRectanglePoints(List<MyPoint> curve)
		{
			List<MyPoint> points = new List<MyPoint>();

			float rectangleStartTime = -1;
			List<MyPoint> possibleRectanglePoints = new List<MyPoint>();

			foreach (MyPoint point in curve)
			{
				float pointTime = point.X;
				float pointValue = point.Y;

				if (pointValue < ValueThreshold && possibleRectanglePoints.Count <= 0)
				{
					points.Add(new MyPoint(pointTime, 0));
				}
				if (pointValue < ValueThreshold)
				{

					if ((pointTime - rectangleStartTime) < MinimumTimeWidth)
					{
						foreach (MyPoint newPoint in possibleRectanglePoints)
						{
							points.Add(new MyPoint(newPoint.X, 0));
						}
					}
					else
					{
						points.AddRange(GetRectangleWithConfidence(possibleRectanglePoints));
					}

					points.Add(new MyPoint(pointTime, 0));

					rectangleStartTime = -1;
					possibleRectanglePoints.Clear();
				}
				else
				{
					if (rectangleStartTime < 0) rectangleStartTime = pointTime;

					possibleRectanglePoints.Add(point);
				}
			}

			return points;
		}


		private List<MyPoint> GetRectangleWithConfidence(List<MyPoint> rectanglePoints)
		{
			List<MyPoint> pointsWithConfidence = new List<MyPoint>();

			float sum = 0;

			foreach (MyPoint point in rectanglePoints)
			{
				sum += point.Y;
			}

			float mean = sum / rectanglePoints.Count;


			foreach (MyPoint point in rectanglePoints)
			{
				float confidence = 1 - Math.Max(0, Math.Abs(point.Y - mean) - DeviationTolerance);

				pointsWithConfidence.Add(new MyPoint(point.X, confidence));
			}

			return pointsWithConfidence;
		}



	}
}

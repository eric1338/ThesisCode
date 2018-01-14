using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

using SongVisualizationApp.Util;
using SongVisualizationApp.FileReader;
using NAudio.Dsp;
using SongVisualizationApp.SongAnalyzing.OnSetDetection;

namespace SongVisualizationApp.SongAnalyzing
{
	public class SongAnalyzer
	{

		private IVisualFacade visualFacade;

		public void SetVisualFacade(IVisualFacade visualFacade)
		{
			this.visualFacade = visualFacade;
		}

		public void AnalyzeSong(string fileDirectory)
		{
			visualFacade.SetProgress("Loading Song...", 0.0f);

			SongFile songFile = MyAudioFileReader.ReadAudioFile(fileDirectory);

			visualFacade.SetSongFile(songFile);

			List<SongPropertyValues> songPropertyValuesList = new List<SongPropertyValues>();

			visualFacade.SetProgress("Analyze...", 0.1f);

			AudioAnalyzer audioAnalyzer = new AudioAnalyzer();

			audioAnalyzer.LoadAudioFromFile(fileDirectory);

			audioAnalyzer.Analyze();

			songPropertyValuesList.Add(GetSingleBeatsSPV(audioAnalyzer.SongElements));
			songPropertyValuesList.Add(GetHeldNoteSPV(audioAnalyzer.SongElements));

			visualFacade.PlotSongPropertyValues(songPropertyValuesList);

			visualFacade.SetProgress("done :)", 1);
		}

		private static SongPropertyValues GetSingleBeatsSPV(SongElements songElements)
		{
			songElements.SortByTime();

			List<MyPoint> points = new List<MyPoint>();

			points.Add(new MyPoint(0, 0));

			foreach (SongElements.SingleBeat singleBeat in songElements.SingleBeats)
			{
				points.Add(new MyPoint(singleBeat.Time - 0.03f, 0));
				points.Add(new MyPoint(singleBeat.Time, singleBeat.Applicability));
				points.Add(new MyPoint(singleBeat.Time + 0.03f, 0));
			}

			SongPropertyValues onsetValues = new SongPropertyValues("SingleBeats");

			onsetValues.Points = points;

			return onsetValues;
		}

		private static SongPropertyValues GetHeldNoteSPV(SongElements songElements)
		{
			songElements.SortByTime();

			float highestApplicability = -1;
			float lowestApplicability = 9999;

			foreach (SongElements.HeldNote heldNote in songElements.HeldNotes)
			{
				highestApplicability = Math.Max(highestApplicability, heldNote.Applicability);
				lowestApplicability = Math.Min(lowestApplicability, heldNote.Applicability);
			}

			List<MyPoint> points = new List<MyPoint>();

			points.Add(new MyPoint(0, 0));

			foreach (SongElements.HeldNote heldNote in songElements.HeldNotes)
			{
				points.Add(new MyPoint(heldNote.StartTime - 0.03f, 0));
				points.Add(new MyPoint(heldNote.StartTime, heldNote.Applicability));
				points.Add(new MyPoint(heldNote.EndTime, heldNote.Applicability));
				points.Add(new MyPoint(heldNote.EndTime + 0.03f, 0));
			}

			SongPropertyValues heldNoteValues = new SongPropertyValues("Held Notes " + DateTime.Now.Millisecond);

			heldNoteValues.Points = points;

			return heldNoteValues;
		}



		public static SongPropertyValues GetSongPropertyValues(string fileDirectory, float vth, float msqe)
		{
			//RectangleDetection.TEST_MAXIMUM_SQUARED_ERROR = msqe;
			FrequencyBand.ValueThreshold = vth;

			Console.WriteLine(vth + " / " + msqe);

			AudioAnalyzer audioAnalyzer = new AudioAnalyzer();
			audioAnalyzer.LoadAudioFromFile(fileDirectory);
			audioAnalyzer.Analyze();

			SongPropertyValues spv = GetHeldNoteSPV(audioAnalyzer.SongElements);

			spv.Normalize();

			return spv;
		}

	}
}

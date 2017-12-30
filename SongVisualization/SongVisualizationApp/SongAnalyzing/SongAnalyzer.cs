using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

using SongVisualizationApp.Util;
using SongVisualizationApp.FileReader;
using NAudio.Dsp;
using SongVisualizationApp.SongAnalyzing.SongPropertyAnalyzers;
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


		public void AnalyzeSong4(string fileDirectory)
		{
			visualFacade.SetProgress("Loading Song...", 0.0f);

			SongFile songFile = MyAudioFileReader.ReadAudioFile(fileDirectory);

			visualFacade.SetSongFile(songFile);

			List<SongPropertyValues> songPropertyValuesList = new List<SongPropertyValues>();

			visualFacade.SetProgress("Analyze...", 0.1f);

			SongPropertyValues goal = new SongPropertyValues("Perfect");

			InitHeldNoteTest();

			for (float i = 0; i < 80; i += 0.05f)
			{
				goal.AddPoint(i, heldNoteTest.GetValue(i));
			}

			songPropertyValuesList.Add(goal);

			//gut
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 22, 0.1f, 0.3f));

			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 9, 0.0001f, 0.06f));
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 20, 0.0001f, 0.1f));
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 16, 0.0001f, 0.06f));
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 11, 0.001f, 0.006f));

			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 22, 0.1f, 0.3f));
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 16, 0.01f, 0.06f));
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 20, 0.15f, 0.1f));

			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 9, 0.025f, 0.06f));
			//songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 2, 0.01f, 0.04f));

			songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 40, 18, 8, 0.001f, 0.2f));
			songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 36, 18, 8, 0.025f, 0.2f));
			songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 30, 50, 16, 0.001f, 0.1f));

			visualFacade.PlotSongPropertyValues(songPropertyValuesList);

			visualFacade.SetProgress("done :)", 1);
		}

		float f = 0.15f;

		private SongPropertyValues GetSongPropertyValues(string fileDirectory, int fbw, float vth, float msqe)
		{
			return GetSongPropertyValues(fileDirectory, 0, 72, fbw, vth, msqe);
		}

		private SongPropertyValues GetSongPropertyValues(string fileDirectory, int sfi, int nf, int fbw, float vth, float msqe)
		{
			HeldNoteDetection.TEST_STARTING_FREQUENCY_INDEX = sfi;
			HeldNoteDetection.TEST_NUMBER_OF_FREQUENCIES = nf;
			HeldNoteDetection.TEST_FREQUENCY_BAND_WIDTH = fbw;
			RectangleDetection.TEST_VALUE_THRESHOLD = vth;
			RectangleDetection.TEST_MAXIMUM_SQUARED_ERROR = msqe;

			AudioAnalyzer audioAnalyzer = new AudioAnalyzer();
			audioAnalyzer.LoadAudioFromFile(fileDirectory);
			audioAnalyzer.Analyze();

			SongPropertyValues spv = audioAnalyzer.GetHeldNoteThingy();

			foreach (MyPoint point in spv.Points)
			{
				if (point.Y > 0) point.Y = f;
			}

			f += 0.2f;

			return spv;
		}

		public void AnalyzeSong(string fileDirectory)
		{
			int n = 1;

			//int[] freqBandWidthVals = { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
			//float[] valueThresholdVals = { 0.0001f, 0.0005f, 0.001f, 0.01f, 0.025f, 0.06f, 0.1f, 0.15f, 0.2f, 0.3f, 0.4f };
			//float[] maxSquaredErrorVals = { 0.00001f, 0.0001f, 0.001f, 0.003f, 0.006f, 0.01f, 0.02f, 0.03f, 0.04f, 0.05f, 0.06f, 0.1f, 0.2f, 0.3f };

			/*
			 * 
			 * 
			songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 40, 18, 8, 0.001f, 0.2f));
			songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 36, 18, 8, 0.025f, 0.2f));
			songPropertyValuesList.Add(GetSongPropertyValues(fileDirectory, 30, 50, 16, 0.001f, 0.1f));
			 * */

			int[] freqBandWidthVals = { 2, 3, 4, 5, 6, 7, 8, 9, 12, 14, 16 };
			float[] valueThresholdVals = { 0.001f, 0.01f, 0.025f, 0.06f, 0.1f, 0.2f };
			float[] maxSquaredErrorVals = { 0.0001f, 0.001f, 0.005f, 0.01f, 0.03f, 0.06f, 0.1f, 0.2f };

			int[] startingFrequencyVals = { 0, 30 };
			int[] numberOfFrequenciesVals = { 72 };

			/*
			int[] freqBandWidthVals = { 8, 16 };
			float[] valueThresholdVals = { 0.001f, 0.025f };
			float[] maxSquaredErrorVals = { 0.1f, 0.2f };
			
			int[] startingFrequencyVals = { 40, 36, 30 };
			int[] numberOfFrequenciesVals = { 50, 18 };
			*/


			InitHeldNoteTest();

			RectangleDetection.TEST_RECDETEC_MINIMUM_TIME_WIDTH = 0.9f;


			SongPropertyValues goal = new SongPropertyValues("Goal");

			for (float i = 0; i < 80; i += 0.025f)
			{

				goal.AddPoint(i, heldNoteTest.GetValue(i));
			}

			List<string> outputs = new List<string>();

			foreach (int freqBandWidthVal in freqBandWidthVals)
			{
				foreach (float valueThresholdVal in valueThresholdVals)
				{
					foreach (float maxSquaredErrorVal in maxSquaredErrorVals)
					{
						foreach (int startingFrequencyVal in startingFrequencyVals)
						{
							foreach (int numberOfFrequenciesVal in numberOfFrequenciesVals)
							{

								visualFacade.SetProgress("TestNr " + n, 0.1f);

								HeldNoteDetection.TEST_FREQUENCY_BAND_WIDTH = freqBandWidthVal;
								RectangleDetection.TEST_VALUE_THRESHOLD = valueThresholdVal;
								RectangleDetection.TEST_MAXIMUM_SQUARED_ERROR = maxSquaredErrorVal;

								HeldNoteDetection.TEST_NUMBER_OF_FREQUENCIES = numberOfFrequenciesVal;
								HeldNoteDetection.TEST_STARTING_FREQUENCY_INDEX = startingFrequencyVal;


								AudioAnalyzer audioAnalyzer = new AudioAnalyzer();
								audioAnalyzer.LoadAudioFromFile(fileDirectory);
								audioAnalyzer.Analyze();

								SongPropertyValues heldNoteThingy = audioAnalyzer.GetHeldNoteThingy();

								SongPropertyValues newHeldNoteThingy = new SongPropertyValues("HeldNoteThingy " + n);

								for (float i = 0; i < 80; i += 0.025f)
								{
									newHeldNoteThingy.AddPoint(i, GetValueEfficient(heldNoteThingy.Points, i));
								}

								float testResults = heldNoteTest.GetTestResult(newHeldNoteThingy.Points);

								string output = testResults + " | [TestNr=" + n + "; ";
								output += "StartFreqIndex=" + startingFrequencyVal + "; ";
								output += "NumberOfFreqs=" + numberOfFrequenciesVal + "; ";
								output += "FreqBandWidth=" + freqBandWidthVal + "; ";
								output += "ValueThreshold=" + valueThresholdVal + "; ";
								output += "MaxSquaredError=" + maxSquaredErrorVal + "];";

								outputs.Add(output);

								Console.WriteLine(output);

								n++;

								if (n % 1000 == 0)
								{
									WriteToTxt(outputs, n);
								}
							}
						}
					}
				}
			}

			WriteToTxt(outputs, 999999);
		}

		/*
		 * 
		 * 
			List<string> outputs = new List<string>();

			foreach (int freqBandWidthVal in freqBandWidthVals)
			{
				foreach (float valueThresholdVal in valueThresholdVals)
				{
					foreach (float maxSquaredErrorVal in maxSquaredErrorVals)
					{
						visualFacade.SetProgress("TestNr " + n, 0.1f);

						HeldNoteDetection.TEST_FREQUENCY_BAND_WIDTH = freqBandWidthVal;
						RectangleDetection.TEST_VALUE_THRESHOLD = valueThresholdVal;
						RectangleDetection.TEST_MAXIMUM_SQUARED_ERROR = maxSquaredErrorVal;

						AudioAnalyzer audioAnalyzer = new AudioAnalyzer();
						audioAnalyzer.LoadAudioFromFile(fileDirectory);
						audioAnalyzer.Analyze();

						SongPropertyValues heldNoteThingy = audioAnalyzer.GetHeldNoteThingy();

						SongPropertyValues newHeldNoteThingy = new SongPropertyValues("HeldNoteThingy " + n);

						for (float i = 0; i < 80; i += 0.025f)
						{
							newHeldNoteThingy.AddPoint(i, GetValueEfficient(heldNoteThingy.Points, i));
						}

						float testResults = heldNoteTest.GetTestResult(newHeldNoteThingy.Points);

						string output = testResults + " | [TestNr=" + n + "; ";
						output += "FreqBandWidth=" + freqBandWidthVal + "; ";
						output += "ValueThreshold=" + valueThresholdVal + "; ";
						output += "MaxSquaredError=" + maxSquaredErrorVal + "];";

						outputs.Add(output);

						Console.WriteLine(output);

						n++;

						if (n % 1000 == 0)
						{
							WriteToTxt(outputs, n);
						}
					}
				}
		 * 
		 * 
		 * */

		private void WriteToTxt(List<string> lines, int n)
		{
			List<string> newLines = new List<string>(lines);

			newLines.Sort();

			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(@"C:\ForVS\HN-Test4-Unsorted-" + n + ".txt"))
			{
				foreach (string line in lines)
				{
					file.WriteLine(line);
				}
			}

			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(@"C:\ForVS\HN-Test4-Sorted-" + n + ".txt"))
			{
				foreach (string line in newLines)
				{
					file.WriteLine(line);
				}
			}
		}

		public void AnalyzeSongAlt(string fileDirectory)
		{


			visualFacade.SetProgress("Loading Song...", 0.0f);

			SongFile songFile = MyAudioFileReader.ReadAudioFile(fileDirectory);

			visualFacade.SetSongFile(songFile);

			List<SongPropertyValues> songPropertyValuesList = new List<SongPropertyValues>();


			visualFacade.SetProgress("Analyze...", 0.1f);

			AudioAnalyzer audioAnalyzer = new AudioAnalyzer();
			audioAnalyzer.LoadAudioFromFile(fileDirectory);

			audioAnalyzer.Analyze();





			/*
			songPropertyValuesList.Add(audioAnalyzer.GetSuperOnsetThingy());

			AudioAnalyzer audioAnalyzer2 = new AudioAnalyzer(20, 20);
			audioAnalyzer2.LoadAudioFromFile(fileDirectory);

			audioAnalyzer2.Analyze();

			songPropertyValuesList.Add(audioAnalyzer2.GetSuperOnsetThingy());

			AudioAnalyzer audioAnalyzer3 = new AudioAnalyzer(10, 10);
			audioAnalyzer3.LoadAudioFromFile(fileDirectory);

			audioAnalyzer3.Analyze();

			songPropertyValuesList.Add(audioAnalyzer3.GetSuperOnsetThingy());
			*/


			//songPropertyValuesList.Add(audioAnalyzer.GetOnsetThingy());

			//songPropertyValuesList.Add(audioAnalyzer.GetHeldNoteThingy());

			//songPropertyValuesList.Add(audioAnalyzer.GetAmplitudeThingy());

			SongPropertyValues heldNoteThingy = audioAnalyzer.GetHeldNoteThingy();

			//songPropertyValuesList.Add(heldNoteThingy);

			SongPropertyValues newHeldNoteThingy = new SongPropertyValues("NewHeldNote");

			SongPropertyValues perfect = new SongPropertyValues("Perfect");

			InitHeldNoteTest();

			for (float i = 0; i < 80; i += 0.05f)
			{
				newHeldNoteThingy.AddPoint(i, GetValueEfficient(heldNoteThingy.Points, i));

				perfect.AddPoint(i, heldNoteTest.GetValue(i));
			}

			songPropertyValuesList.Add(perfect);
			songPropertyValuesList.Add(newHeldNoteThingy);

			Console.WriteLine("Test-Result: " + heldNoteTest.GetTestResult(newHeldNoteThingy.Points));

			songPropertyValuesList.Add(heldNoteTest.GetSPVTest(newHeldNoteThingy.Points));




			visualFacade.SetProgress("Plotting...", 0.95f);

			visualFacade.PlotSongPropertyValues(songPropertyValuesList);

			visualFacade.SetProgress("done :)", 1);
		}



		private SongPropertyValues AnalyzeSongProperty(SongFile songFile, SongPropertyAnalyzer analyzer, float relativeProgressBefore)
		{
			visualFacade.SetProgress(analyzer.ProgressText, relativeProgressBefore);

			return analyzer.Analyze(songFile);
		}



		private float GetValueEfficient(List<MyPoint> points, float time)
		{
			if (time <= points[0].X) return points[0].Y;

			int startIndex = 0;

			for (int i = 0; i < points.Count; i += 10)
			{
				if (points[i].X < time) startIndex = i;
			}

			MyPoint leftPoint = points[startIndex];

			for (int i = startIndex + 1; i < points.Count; i++)
			{
				MyPoint point = points[i];

				if (point.X > time)
				{
					float leftPointTimeDifference = Math.Abs(time - leftPoint.X);
					float rightPointTimeDifference = Math.Abs(point.X - time);

					if (leftPointTimeDifference < rightPointTimeDifference) return leftPoint.Y;
					return point.Y;
				}
				else
				{
					leftPoint = point;
				}
			}

			return points[points.Count - 1].Y;
		}



		class HeldNoteTest
		{

			private List<MyPoint> points = new List<MyPoint>();

			public HeldNoteTest()
			{

			}

			public void AddPoint(float time, float value)
			{
				points.Add(new MyPoint(time, value));
			}

			public float GetValue(float time)
			{
				if (time <= points[0].X) return 0;

				int startIndex = 0;

				for (int i = 0; i < points.Count; i += 10)
				{
					if (points[i].X < time) startIndex = i;
				}

				float leftValue = 0;

				for (int i = startIndex; i < points.Count; i++)
				{
					MyPoint point = points[i];

					if (point.X > time) return point.Y * leftValue;
					else leftValue = point.Y;
				}

				return 0;
			}

			public float GetTestResult(List<MyPoint> heldNotePoints)
			{
				int tries = 0;
				int successes = 0;

				foreach (MyPoint heldNotePoint in heldNotePoints)
				{
					float requiredValue = GetValue(heldNotePoint.X);

					int heldNoteValue = heldNotePoint.Y > 0.02f ? 1 : 0;

					if (Math.Abs(requiredValue - heldNoteValue) > 0.02f) successes++;

					tries++;
				}

				return successes / (float)tries;
			}



			public SongPropertyValues GetSPVTest(List<MyPoint> heldNotePoints)
			{
				SongPropertyValues spv = new SongPropertyValues("Successes");

				foreach (MyPoint heldNotePoint in heldNotePoints)
				{
					float requiredValue = GetValue(heldNotePoint.X);

					int heldNoteValue = heldNotePoint.Y > 0.05f ? 1 : 0;

					float value = Math.Abs(requiredValue - heldNoteValue) < 0.001f ? 0.92f : 0;

					spv.AddPoint(heldNotePoint.X, value);
				}

				return spv;
			}


		}

		HeldNoteTest heldNoteTest = new HeldNoteTest();
		

		private void InitHeldNoteTest()
		{
			heldNoteTest.AddPoint(12.069f, 0);
			heldNoteTest.AddPoint(12.099f, 1);
			heldNoteTest.AddPoint(13.255f, 1);
			heldNoteTest.AddPoint(13.285f, 0);
			heldNoteTest.AddPoint(15.849f, 0);
			heldNoteTest.AddPoint(15.879f, 1);
			heldNoteTest.AddPoint(16.754f, 1);
			heldNoteTest.AddPoint(16.784f, 0);
			heldNoteTest.AddPoint(18.473f, 0);
			heldNoteTest.AddPoint(18.503f, 1);
			heldNoteTest.AddPoint(20.112f, 1);
			heldNoteTest.AddPoint(20.142f, 0);
			heldNoteTest.AddPoint(21.439f, 0);
			heldNoteTest.AddPoint(21.469f, 1);
			heldNoteTest.AddPoint(22.825f, 1);
			heldNoteTest.AddPoint(22.855f, 0);
			heldNoteTest.AddPoint(25.095f, 0);
			heldNoteTest.AddPoint(25.125f, 1);
			heldNoteTest.AddPoint(26.195f, 1);
			heldNoteTest.AddPoint(26.225f, 0);
			heldNoteTest.AddPoint(27.739f, 0);
			heldNoteTest.AddPoint(27.769f, 1);
			heldNoteTest.AddPoint(29.256f, 1);
			heldNoteTest.AddPoint(29.286f, 0);
			heldNoteTest.AddPoint(30.246f, 0);
			heldNoteTest.AddPoint(30.276f, 1);
			heldNoteTest.AddPoint(32.011f, 1);
			heldNoteTest.AddPoint(32.041f, 0);
			heldNoteTest.AddPoint(32.556f, 0);
			heldNoteTest.AddPoint(32.586f, 1);
			heldNoteTest.AddPoint(33.982f, 1);
			heldNoteTest.AddPoint(34.012f, 0);
			heldNoteTest.AddPoint(37.056f, 0);
			heldNoteTest.AddPoint(37.086f, 1);
			heldNoteTest.AddPoint(38.761f, 1);
			heldNoteTest.AddPoint(38.791f, 0);
			heldNoteTest.AddPoint(40.065f, 0);
			heldNoteTest.AddPoint(40.095f, 1);
			heldNoteTest.AddPoint(41.462f, 1);
			heldNoteTest.AddPoint(41.492f, 0);
			heldNoteTest.AddPoint(46.342f, 0);
			heldNoteTest.AddPoint(46.372f, 1);
			heldNoteTest.AddPoint(48.025f, 1);
			heldNoteTest.AddPoint(48.055f, 0);
			heldNoteTest.AddPoint(48.815f, 0);
			heldNoteTest.AddPoint(48.845f, 1);
			heldNoteTest.AddPoint(51.525f, 1);
			heldNoteTest.AddPoint(51.555f, 0);
			heldNoteTest.AddPoint(51.736f, 0);
			heldNoteTest.AddPoint(51.766f, 1);
			heldNoteTest.AddPoint(54.193f, 1);
			heldNoteTest.AddPoint(54.223f, 0);
			heldNoteTest.AddPoint(55.19f, 0);
			heldNoteTest.AddPoint(55.22f, 1);
			heldNoteTest.AddPoint(59.23f, 1);
			heldNoteTest.AddPoint(59.26f, 0);
			heldNoteTest.AddPoint(60.006f, 0);
			heldNoteTest.AddPoint(60.036f, 1);
			heldNoteTest.AddPoint(62.638f, 1);
			heldNoteTest.AddPoint(62.668f, 0);
			heldNoteTest.AddPoint(62.902f, 0);
			heldNoteTest.AddPoint(62.932f, 1);
			heldNoteTest.AddPoint(65.328f, 1);
			heldNoteTest.AddPoint(65.358f, 0);
			heldNoteTest.AddPoint(66.337f, 0);
			heldNoteTest.AddPoint(66.367f, 1);
			heldNoteTest.AddPoint(70.972f, 1);
			heldNoteTest.AddPoint(71.002f, 0);
		}




	}
}

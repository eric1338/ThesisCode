using SongVisualizationApp.Audio;
using SongVisualizationApp.FileReader;
using SongVisualizationApp.SongAnalyzing;
using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SongVisualizationApp
{
	public partial class Form1 : Form
	{

		private VisualValues visualValues;
		private ISongAnalyzingFacade songAnalyzingFacade;

		private AudioPlayer audioPlayer;

		private bool isSongPlaying = false;

		private SongFile songFile;

		private List<MyPoint> songPoints;
		private List<MyPoint> fftPoints;

		public Form1()
		{
			InitializeComponent();

			audioPlayer = new AudioPlayer();
		}

		public void SetVisualValues(VisualValues visualValues)
		{
			this.visualValues = visualValues;
		}

		public void SetSongAnalyzingFacade(ISongAnalyzingFacade songAnalyzingFacade)
		{
			this.songAnalyzingFacade = songAnalyzingFacade;
		}


		public void SetProgress(string progressText, float relativeProgress)
		{
			progressLabel.Text = progressText;

			progressLabel.ForeColor = relativeProgress < 1 ? Color.MediumVioletRed : Color.DodgerBlue;

			Update();
		}

		public void SetSongFile(SongFile songFile)
		{
			this.songFile = songFile;

			visualValues.SetSongDuration(songFile.SongDuration);

			songNameLabel.Text = songFile.SongName;
			songDurationLabel.Text = visualValues.GetSongDurationString();

			audioPlayer.Dispose();
			audioPlayer.InitAudio(songFile);

			songWaveViewer.SetSongFile(songFile);

			togglePlaybackButton.Enabled = true;
		}

		public void PlotSongPropertyValues(List<SongPropertyValues> songPropertyValuesList)
		{
			
			foreach (SongPropertyValues songPropertyValues in songPropertyValuesList)
			{
				Series series = new Series();

				series.Name = songPropertyValues.PropertyName;
				
				foreach (MyPoint point in songPropertyValues.Points)
				{
					series.Points.AddXY(point.X, point.Y);
				}

				series.ChartArea = "ChartArea1";

				series.ChartType = SeriesChartType.FastLine;

				songValueChart.Series.Add(series);
			}

		}

		public void UpdateSongVisualization()
		{
			visualValues.UpdateTimeMargins();

			songWaveViewer.SetTimeMargins(visualValues.LeftTimeMargin, visualValues.RightTimeMargin);

			songValueChart.ChartAreas["ChartArea1"].AxisX.Minimum = visualValues.LeftTimeMargin;
			songValueChart.ChartAreas["ChartArea1"].AxisX.Maximum = visualValues.RightTimeMargin;

			leftTimeMarginLabel.Text = visualValues.GetLeftTimeMarginString();
			rightTimeMarginLabel.Text = visualValues.GetRightTimeMarginString();

			Update();
		}


		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void selectSongButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openSongDialog = new OpenFileDialog();

			//openSongDialog.Filter = "mp3 File (*.mp3)|*.mp3|Wave File (*.wav)|*.wav;";

			if (openSongDialog.ShowDialog() != DialogResult.OK) return;

			songAnalyzingFacade.AnalyzeSong(openSongDialog.FileName);
		}

		private void togglePlaybackButton_Click(object sender, EventArgs e)
		{
			isSongPlaying = !isSongPlaying;

			togglePlaybackButton.Text = isSongPlaying ? "Stop" : "Play";

			if (isSongPlaying)
			{
				double time = Math.Max(songWaveViewer.MarkerTime, 0);

				Console.WriteLine("time:" + time);

				audioPlayer.PlayAudioFromTime(time);
			}
			else
			{
				audioPlayer.StopAudio();
			}
		}

		private void songSecondsDisplayedTrackBar_Scroll(object sender, EventArgs e)
		{
			visualValues.SetSecondsDisplayed(songSecondsDisplayedTrackBar.Value);

			UpdateSongVisualization();
		}

		private void songScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			double scrollBarValue = songScrollBar.Value;

			scrollBarValue = Math.Max(0, scrollBarValue * 1.25 - 10);

			double percentage = scrollBarValue / songScrollBar.Maximum;

			visualValues.SetTimeCenter(percentage);

			UpdateSongVisualization();
		}

		private void debugButton1_Click(object sender, EventArgs e)
		{
			double startingTime = Convert.ToDouble(debugTextBox1.Text);
			double endTime = Convert.ToDouble(debugTextBox2.Text);

			List<MyPoint> fftPoints;

			if (debugCheckBox1.Checked)
			{
				Console.WriteLine("Calculus");
				//fftPoints = SongAnalyzer.GetFFTValues(songFile, startingTime, endTime);
			}
			else
			{
				Console.WriteLine("NAudio");
				//fftPoints = SongAnalyzer.DoFFT3(songFile, startingTime, endTime);
			}

			


			//if (startingTime > 1) PlotFFT(SongAnalyzer.prob1.Points);
			//if (startingTime < 1) PlotFFT(SongAnalyzer.prob2.Points);

			//PlotFFT(fftPoints);
		}

		
		public void PlotAmplitude()
		{
			/*
			songValueChart.Series["Amplitude"].Points.Clear();
			songValueChart.Series["AmplitudeNormalized"].Points.Clear();
			songValueChart.Series["AmplitudeThreshold"].Points.Clear();

			foreach (MyPoint point in SongAnalyzer.prob1.Points)
			{
				songValueChart.Series["Amplitude"].Points.AddXY(point.X, point.Y);
			}
			foreach (MyPoint point in SongAnalyzer.prob2.Points)
			{
				songValueChart.Series["AmplitudeNormalized"].Points.AddXY(point.X, point.Y);
			}
			foreach (MyPoint point in SongAnalyzer.prob3.Points)
			{
				songValueChart.Series["AmplitudeThreshold"].Points.AddXY(point.X, point.Y);
			}
			*/
		}


		public void PlotFFT(List<MyPoint> points)
		{
			fftPoints = points;

			fftChart.Series["Frequency"].Points.Clear();

			foreach (MyPoint point in points)
			{
				fftChart.Series["Frequency"].Points.AddXY(point.X, point.Y);
			}

			Update();
		}
	}
}

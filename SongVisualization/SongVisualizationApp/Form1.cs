﻿using SongVisualizationApp.Audio;
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

				series.BorderWidth = 2;

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

				timerSongStartTime = time;
				playbackButtonHitTime = DateTime.Now;
				updateTimer = new Timer();
				updateTimer.Tick += new EventHandler(UpdateTimeCenter);
				updateTimer.Interval = 100;
				updateTimer.Start();

				audioPlayer.PlayAudioFromTime(time);
			}
			else
			{
				updateTimer.Stop();
				audioPlayer.StopAudio();
			}
		}

		private Timer updateTimer;
		private double timerSongStartTime;
		private DateTime playbackButtonHitTime;

		private void UpdateTimeCenter(object sender, EventArgs e)
		{
			double timePassed = (DateTime.Now - playbackButtonHitTime).TotalSeconds;

			visualValues.SetTimeCenter(timerSongStartTime + timePassed);

			UpdateSongVisualization();
		}


		private void songSecondsDisplayedTrackBar_Scroll(object sender, EventArgs e)
		{
			visualValues.SetSecondsDisplayed(songSecondsDisplayedTrackBar.Value);

			UpdateSongVisualization();
		}

		private void songScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			double scrollBarValue = Math.Max(0, songScrollBar.Value * 1.25 - 10);

			double percentage = scrollBarValue / songScrollBar.Maximum;

			visualValues.SetTimeCenterRelative(percentage);

			UpdateSongVisualization();
		}

		

		private void debugButton1_Click(object sender, EventArgs e)
		{
			string fileDirectory = songFile.FileDirectory;
			
			float valueThreshold = (float)Convert.ToDouble(debugTextBox4.Text);
			float maximumSquaredError = (float)Convert.ToDouble(debugTextBox5.Text);

			SongPropertyValues spv = SongAnalyzer.GetSongPropertyValues(fileDirectory,
				valueThreshold, maximumSquaredError);

			if (lastSeries != null) songValueChart.Series.Remove(lastSeries);

			PlotSPVTest(spv);

			Update();
		}


		private Series lastSeries = null;

		private void PlotSPVTest(SongPropertyValues songPropertyValues)
		{
			Series series = new Series();

			series.Name = songPropertyValues.PropertyName + DateTime.Now.Millisecond;

			foreach (MyPoint point in songPropertyValues.Points)
			{
				series.Points.AddXY(point.X, point.Y);
			}

			series.ChartArea = "ChartArea1";

			series.ChartType = SeriesChartType.FastLine;

			series.BorderWidth = 2;

			songValueChart.Series.Add(series);

			lastSeries = series;
		}

		private void songValueChart_Click(object sender, EventArgs e)
		{

		}
	}
}

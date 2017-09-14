namespace SongVisualizationApp
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.selectSongButton = new System.Windows.Forms.Button();
			this.fftChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.songScrollBar = new System.Windows.Forms.HScrollBar();
			this.songNameLabel = new System.Windows.Forms.Label();
			this.leftTimeMarginLabel = new System.Windows.Forms.Label();
			this.rightTimeMarginLabel = new System.Windows.Forms.Label();
			this.songChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.songDurationLabel = new System.Windows.Forms.Label();
			this.togglePlaybackButton = new System.Windows.Forms.Button();
			this.songWaveViewer = new SongVisualizationApp.MyWaveViewer();
			this.debugTextBox1 = new System.Windows.Forms.TextBox();
			this.debugTextBox2 = new System.Windows.Forms.TextBox();
			this.debugButton1 = new System.Windows.Forms.Button();
			this.songSecondsDisplayedTrackBar = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.fftChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.songChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.songSecondsDisplayedTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// selectSongButton
			// 
			this.selectSongButton.Location = new System.Drawing.Point(926, 127);
			this.selectSongButton.Name = "selectSongButton";
			this.selectSongButton.Size = new System.Drawing.Size(75, 23);
			this.selectSongButton.TabIndex = 1;
			this.selectSongButton.Text = "Select Song";
			this.selectSongButton.UseVisualStyleBackColor = true;
			this.selectSongButton.Click += new System.EventHandler(this.selectSongButton_Click);
			// 
			// fftChart
			// 
			chartArea1.Name = "ChartArea1";
			this.fftChart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.fftChart.Legends.Add(legend1);
			this.fftChart.Location = new System.Drawing.Point(140, 330);
			this.fftChart.Name = "fftChart";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Frequency";
			this.fftChart.Series.Add(series1);
			this.fftChart.Size = new System.Drawing.Size(572, 215);
			this.fftChart.TabIndex = 2;
			this.fftChart.Text = "chart1";
			// 
			// songScrollBar
			// 
			this.songScrollBar.Location = new System.Drawing.Point(140, 260);
			this.songScrollBar.Name = "songScrollBar";
			this.songScrollBar.Size = new System.Drawing.Size(572, 17);
			this.songScrollBar.TabIndex = 4;
			this.songScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.songScrollBar_Scroll);
			// 
			// songNameLabel
			// 
			this.songNameLabel.AutoSize = true;
			this.songNameLabel.Location = new System.Drawing.Point(923, 63);
			this.songNameLabel.Name = "songNameLabel";
			this.songNameLabel.Size = new System.Drawing.Size(88, 13);
			this.songNameLabel.TabIndex = 5;
			this.songNameLabel.Text = "no song selected";
			// 
			// leftTimeMarginLabel
			// 
			this.leftTimeMarginLabel.AutoSize = true;
			this.leftTimeMarginLabel.Location = new System.Drawing.Point(137, 233);
			this.leftTimeMarginLabel.Name = "leftTimeMarginLabel";
			this.leftTimeMarginLabel.Size = new System.Drawing.Size(28, 13);
			this.leftTimeMarginLabel.TabIndex = 6;
			this.leftTimeMarginLabel.Text = "0:00";
			// 
			// rightTimeMarginLabel
			// 
			this.rightTimeMarginLabel.AutoSize = true;
			this.rightTimeMarginLabel.Location = new System.Drawing.Point(684, 233);
			this.rightTimeMarginLabel.Name = "rightTimeMarginLabel";
			this.rightTimeMarginLabel.Size = new System.Drawing.Size(28, 13);
			this.rightTimeMarginLabel.TabIndex = 7;
			this.rightTimeMarginLabel.Text = "0:00";
			// 
			// songChart
			// 
			chartArea2.BorderColor = System.Drawing.Color.Gray;
			chartArea2.Name = "ChartArea1";
			this.songChart.ChartAreas.Add(chartArea2);
			legend2.Enabled = false;
			legend2.Name = "Legend1";
			this.songChart.Legends.Add(legend2);
			this.songChart.Location = new System.Drawing.Point(999, 330);
			this.songChart.Name = "songChart";
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series2.Legend = "Legend1";
			series2.Name = "Waveform";
			this.songChart.Series.Add(series2);
			this.songChart.Size = new System.Drawing.Size(204, 143);
			this.songChart.TabIndex = 8;
			this.songChart.Text = "chart1";
			// 
			// songDurationLabel
			// 
			this.songDurationLabel.AutoSize = true;
			this.songDurationLabel.Location = new System.Drawing.Point(926, 93);
			this.songDurationLabel.Name = "songDurationLabel";
			this.songDurationLabel.Size = new System.Drawing.Size(28, 13);
			this.songDurationLabel.TabIndex = 9;
			this.songDurationLabel.Text = "0:00";
			// 
			// togglePlaybackButton
			// 
			this.togglePlaybackButton.Enabled = false;
			this.togglePlaybackButton.Location = new System.Drawing.Point(814, 206);
			this.togglePlaybackButton.Name = "togglePlaybackButton";
			this.togglePlaybackButton.Size = new System.Drawing.Size(75, 23);
			this.togglePlaybackButton.TabIndex = 10;
			this.togglePlaybackButton.Text = "play";
			this.togglePlaybackButton.UseVisualStyleBackColor = true;
			this.togglePlaybackButton.Click += new System.EventHandler(this.togglePlaybackButton_Click);
			// 
			// songWaveViewer
			// 
			this.songWaveViewer.Location = new System.Drawing.Point(138, 90);
			this.songWaveViewer.Name = "songWaveViewer";
			this.songWaveViewer.SamplesPerPixel = 128;
			this.songWaveViewer.Size = new System.Drawing.Size(572, 130);
			this.songWaveViewer.StartPosition = ((long)(0));
			this.songWaveViewer.TabIndex = 11;
			this.songWaveViewer.WaveStream = null;
			// 
			// debugTextBox1
			// 
			this.debugTextBox1.Location = new System.Drawing.Point(789, 406);
			this.debugTextBox1.Name = "debugTextBox1";
			this.debugTextBox1.Size = new System.Drawing.Size(100, 20);
			this.debugTextBox1.TabIndex = 12;
			// 
			// debugTextBox2
			// 
			this.debugTextBox2.Location = new System.Drawing.Point(789, 453);
			this.debugTextBox2.Name = "debugTextBox2";
			this.debugTextBox2.Size = new System.Drawing.Size(100, 20);
			this.debugTextBox2.TabIndex = 13;
			// 
			// debugButton1
			// 
			this.debugButton1.Location = new System.Drawing.Point(801, 491);
			this.debugButton1.Name = "debugButton1";
			this.debugButton1.Size = new System.Drawing.Size(75, 23);
			this.debugButton1.TabIndex = 14;
			this.debugButton1.Text = "button1";
			this.debugButton1.UseVisualStyleBackColor = true;
			this.debugButton1.Click += new System.EventHandler(this.debugButton1_Click);
			// 
			// songSecondsDisplayedTrackBar
			// 
			this.songSecondsDisplayedTrackBar.Location = new System.Drawing.Point(999, 184);
			this.songSecondsDisplayedTrackBar.Maximum = 8;
			this.songSecondsDisplayedTrackBar.Name = "songSecondsDisplayedTrackBar";
			this.songSecondsDisplayedTrackBar.Size = new System.Drawing.Size(104, 45);
			this.songSecondsDisplayedTrackBar.TabIndex = 15;
			this.songSecondsDisplayedTrackBar.Scroll += new System.EventHandler(this.songSecondsDisplayedTrackBar_Scroll);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1228, 665);
			this.Controls.Add(this.songSecondsDisplayedTrackBar);
			this.Controls.Add(this.debugButton1);
			this.Controls.Add(this.debugTextBox2);
			this.Controls.Add(this.debugTextBox1);
			this.Controls.Add(this.songWaveViewer);
			this.Controls.Add(this.togglePlaybackButton);
			this.Controls.Add(this.songDurationLabel);
			this.Controls.Add(this.songChart);
			this.Controls.Add(this.rightTimeMarginLabel);
			this.Controls.Add(this.leftTimeMarginLabel);
			this.Controls.Add(this.songNameLabel);
			this.Controls.Add(this.songScrollBar);
			this.Controls.Add(this.fftChart);
			this.Controls.Add(this.selectSongButton);
			this.Name = "Form1";
			this.Text = "SongVisualization";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.fftChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.songChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.songSecondsDisplayedTrackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button selectSongButton;
		private System.Windows.Forms.DataVisualization.Charting.Chart fftChart;
		private System.Windows.Forms.HScrollBar songScrollBar;
		private System.Windows.Forms.Label songNameLabel;
		private System.Windows.Forms.Label leftTimeMarginLabel;
		private System.Windows.Forms.Label rightTimeMarginLabel;
		private System.Windows.Forms.DataVisualization.Charting.Chart songChart;
		private System.Windows.Forms.Label songDurationLabel;
		private System.Windows.Forms.Button togglePlaybackButton;
		private SongVisualizationApp.MyWaveViewer songWaveViewer;
		private System.Windows.Forms.TextBox debugTextBox1;
		private System.Windows.Forms.TextBox debugTextBox2;
		private System.Windows.Forms.Button debugButton1;
		private System.Windows.Forms.TrackBar songSecondsDisplayedTrackBar;
	}
}


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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.selectSongButton = new System.Windows.Forms.Button();
			this.songScrollBar = new System.Windows.Forms.HScrollBar();
			this.songNameLabel = new System.Windows.Forms.Label();
			this.leftTimeMarginLabel = new System.Windows.Forms.Label();
			this.rightTimeMarginLabel = new System.Windows.Forms.Label();
			this.songDurationLabel = new System.Windows.Forms.Label();
			this.togglePlaybackButton = new System.Windows.Forms.Button();
			this.debugTextBox1 = new System.Windows.Forms.TextBox();
			this.debugTextBox2 = new System.Windows.Forms.TextBox();
			this.debugButton1 = new System.Windows.Forms.Button();
			this.songSecondsDisplayedTrackBar = new System.Windows.Forms.TrackBar();
			this.songValueChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.debugCheckBox1 = new System.Windows.Forms.CheckBox();
			this.progressLabel = new System.Windows.Forms.Label();
			this.songWaveViewer = new SongVisualizationApp.MyWaveViewer();
			this.debugChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.songSecondsDisplayedTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.songValueChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.debugChart1)).BeginInit();
			this.SuspendLayout();
			// 
			// selectSongButton
			// 
			this.selectSongButton.Location = new System.Drawing.Point(1108, 107);
			this.selectSongButton.Name = "selectSongButton";
			this.selectSongButton.Size = new System.Drawing.Size(75, 23);
			this.selectSongButton.TabIndex = 1;
			this.selectSongButton.Text = "Select Song";
			this.selectSongButton.UseVisualStyleBackColor = true;
			this.selectSongButton.Click += new System.EventHandler(this.selectSongButton_Click);
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
			this.songNameLabel.Location = new System.Drawing.Point(1115, 39);
			this.songNameLabel.Name = "songNameLabel";
			this.songNameLabel.Size = new System.Drawing.Size(60, 13);
			this.songNameLabel.TabIndex = 5;
			this.songNameLabel.Text = "<fileName>";
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
			// songDurationLabel
			// 
			this.songDurationLabel.AutoSize = true;
			this.songDurationLabel.Location = new System.Drawing.Point(1155, 73);
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
			// debugTextBox1
			// 
			this.debugTextBox1.Location = new System.Drawing.Point(1262, 564);
			this.debugTextBox1.Name = "debugTextBox1";
			this.debugTextBox1.Size = new System.Drawing.Size(100, 20);
			this.debugTextBox1.TabIndex = 12;
			// 
			// debugTextBox2
			// 
			this.debugTextBox2.Location = new System.Drawing.Point(1262, 590);
			this.debugTextBox2.Name = "debugTextBox2";
			this.debugTextBox2.Size = new System.Drawing.Size(100, 20);
			this.debugTextBox2.TabIndex = 13;
			// 
			// debugButton1
			// 
			this.debugButton1.Location = new System.Drawing.Point(1287, 616);
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
			this.songSecondsDisplayedTrackBar.Maximum = 7;
			this.songSecondsDisplayedTrackBar.Name = "songSecondsDisplayedTrackBar";
			this.songSecondsDisplayedTrackBar.Size = new System.Drawing.Size(104, 45);
			this.songSecondsDisplayedTrackBar.TabIndex = 15;
			this.songSecondsDisplayedTrackBar.Scroll += new System.EventHandler(this.songSecondsDisplayedTrackBar_Scroll);
			// 
			// songValueChart
			// 
			chartArea1.Name = "ChartArea1";
			this.songValueChart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			legend1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.songValueChart.Legends.Add(legend1);
			this.songValueChart.Location = new System.Drawing.Point(26, 362);
			this.songValueChart.Name = "songValueChart";
			this.songValueChart.Size = new System.Drawing.Size(281, 277);
			this.songValueChart.TabIndex = 16;
			this.songValueChart.Text = "chart1";
			// 
			// debugCheckBox1
			// 
			this.debugCheckBox1.AutoSize = true;
			this.debugCheckBox1.Location = new System.Drawing.Point(1287, 531);
			this.debugCheckBox1.Name = "debugCheckBox1";
			this.debugCheckBox1.Size = new System.Drawing.Size(56, 17);
			this.debugCheckBox1.TabIndex = 17;
			this.debugCheckBox1.Text = "debug";
			this.debugCheckBox1.UseVisualStyleBackColor = true;
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.Location = new System.Drawing.Point(1274, 39);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(88, 13);
			this.progressLabel.TabIndex = 18;
			this.progressLabel.Text = "no song selected";
			// 
			// songWaveViewer
			// 
			this.songWaveViewer.Location = new System.Drawing.Point(140, 73);
			this.songWaveViewer.Name = "songWaveViewer";
			this.songWaveViewer.SamplesPerPixel = 128;
			this.songWaveViewer.Size = new System.Drawing.Size(572, 147);
			this.songWaveViewer.StartPosition = ((long)(0));
			this.songWaveViewer.TabIndex = 11;
			this.songWaveViewer.WaveStream = null;
			// 
			// debugChart1
			// 
			chartArea2.Name = "ChartArea1";
			this.debugChart1.ChartAreas.Add(chartArea2);
			this.debugChart1.Location = new System.Drawing.Point(343, 326);
			this.debugChart1.Name = "debugChart1";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series1.Name = "Series1";
			this.debugChart1.Series.Add(series1);
			this.debugChart1.Size = new System.Drawing.Size(889, 336);
			this.debugChart1.TabIndex = 19;
			this.debugChart1.Text = "chart1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1440, 694);
			this.Controls.Add(this.debugChart1);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.debugCheckBox1);
			this.Controls.Add(this.songValueChart);
			this.Controls.Add(this.songSecondsDisplayedTrackBar);
			this.Controls.Add(this.debugButton1);
			this.Controls.Add(this.debugTextBox2);
			this.Controls.Add(this.debugTextBox1);
			this.Controls.Add(this.songWaveViewer);
			this.Controls.Add(this.togglePlaybackButton);
			this.Controls.Add(this.songDurationLabel);
			this.Controls.Add(this.rightTimeMarginLabel);
			this.Controls.Add(this.leftTimeMarginLabel);
			this.Controls.Add(this.songNameLabel);
			this.Controls.Add(this.songScrollBar);
			this.Controls.Add(this.selectSongButton);
			this.Name = "Form1";
			this.Text = "SongVisualization";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.songSecondsDisplayedTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.songValueChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.debugChart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button selectSongButton;
		private System.Windows.Forms.HScrollBar songScrollBar;
		private System.Windows.Forms.Label songNameLabel;
		private System.Windows.Forms.Label leftTimeMarginLabel;
		private System.Windows.Forms.Label rightTimeMarginLabel;
		private System.Windows.Forms.Label songDurationLabel;
		private System.Windows.Forms.Button togglePlaybackButton;
		private SongVisualizationApp.MyWaveViewer songWaveViewer;
		private System.Windows.Forms.TextBox debugTextBox1;
		private System.Windows.Forms.TextBox debugTextBox2;
		private System.Windows.Forms.Button debugButton1;
		private System.Windows.Forms.TrackBar songSecondsDisplayedTrackBar;
		private System.Windows.Forms.DataVisualization.Charting.Chart songValueChart;
		private System.Windows.Forms.CheckBox debugCheckBox1;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.DataVisualization.Charting.Chart debugChart1;
	}
}


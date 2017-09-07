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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.selectSongButton = new System.Windows.Forms.Button();
			this.fftChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.secondsDisplayedComboBox = new System.Windows.Forms.ComboBox();
			this.songScrollBar = new System.Windows.Forms.HScrollBar();
			this.songNameLabel = new System.Windows.Forms.Label();
			this.leftTimeMarginLabel = new System.Windows.Forms.Label();
			this.rightTimeMarginLabel = new System.Windows.Forms.Label();
			this.songChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.songDurationLabel = new System.Windows.Forms.Label();
			this.togglePlaybackButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.fftChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.songChart)).BeginInit();
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
			chartArea3.Name = "ChartArea1";
			this.fftChart.ChartAreas.Add(chartArea3);
			legend3.Name = "Legend1";
			this.fftChart.Legends.Add(legend3);
			this.fftChart.Location = new System.Drawing.Point(140, 330);
			this.fftChart.Name = "fftChart";
			series3.ChartArea = "ChartArea1";
			series3.Legend = "Legend1";
			series3.Name = "Frequency";
			this.fftChart.Series.Add(series3);
			this.fftChart.Size = new System.Drawing.Size(572, 215);
			this.fftChart.TabIndex = 2;
			this.fftChart.Text = "chart1";
			// 
			// secondsDisplayedComboBox
			// 
			this.secondsDisplayedComboBox.FormattingEnabled = true;
			this.secondsDisplayedComboBox.Items.AddRange(new object[] {
            "all",
            "5 s",
            "10 s",
            "20 s",
            "60 s"});
			this.secondsDisplayedComboBox.Location = new System.Drawing.Point(906, 256);
			this.secondsDisplayedComboBox.Name = "secondsDisplayedComboBox";
			this.secondsDisplayedComboBox.Size = new System.Drawing.Size(121, 21);
			this.secondsDisplayedComboBox.TabIndex = 3;
			this.secondsDisplayedComboBox.SelectedIndexChanged += new System.EventHandler(this.secondsDisplayedComboBox_SelectedIndexChanged);
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
			chartArea4.BorderColor = System.Drawing.Color.Gray;
			chartArea4.Name = "ChartArea1";
			this.songChart.ChartAreas.Add(chartArea4);
			legend4.Enabled = false;
			legend4.Name = "Legend1";
			this.songChart.Legends.Add(legend4);
			this.songChart.Location = new System.Drawing.Point(140, 50);
			this.songChart.Name = "songChart";
			series4.ChartArea = "ChartArea1";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series4.Legend = "Legend1";
			series4.Name = "Waveform";
			this.songChart.Series.Add(series4);
			this.songChart.Size = new System.Drawing.Size(572, 180);
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1137, 580);
			this.Controls.Add(this.togglePlaybackButton);
			this.Controls.Add(this.songDurationLabel);
			this.Controls.Add(this.songChart);
			this.Controls.Add(this.rightTimeMarginLabel);
			this.Controls.Add(this.leftTimeMarginLabel);
			this.Controls.Add(this.songNameLabel);
			this.Controls.Add(this.songScrollBar);
			this.Controls.Add(this.secondsDisplayedComboBox);
			this.Controls.Add(this.fftChart);
			this.Controls.Add(this.selectSongButton);
			this.Name = "Form1";
			this.Text = "SongVisualization";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.fftChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.songChart)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button selectSongButton;
		private System.Windows.Forms.DataVisualization.Charting.Chart fftChart;
		private System.Windows.Forms.ComboBox secondsDisplayedComboBox;
		private System.Windows.Forms.HScrollBar songScrollBar;
		private System.Windows.Forms.Label songNameLabel;
		private System.Windows.Forms.Label leftTimeMarginLabel;
		private System.Windows.Forms.Label rightTimeMarginLabel;
		private System.Windows.Forms.DataVisualization.Charting.Chart songChart;
		private System.Windows.Forms.Label songDurationLabel;
		private System.Windows.Forms.Button togglePlaybackButton;
	}
}


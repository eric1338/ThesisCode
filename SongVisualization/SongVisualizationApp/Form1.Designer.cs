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
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.debugChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.songWaveViewer = new SongVisualizationApp.MyWaveViewer();
            this.debugTextBox3 = new System.Windows.Forms.TextBox();
            this.debugTextBox4 = new System.Windows.Forms.TextBox();
            this.debugTextBox5 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.songSecondsDisplayedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.songValueChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.debugChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectSongButton
            // 
            this.selectSongButton.Location = new System.Drawing.Point(882, 165);
            this.selectSongButton.Margin = new System.Windows.Forms.Padding(4);
            this.selectSongButton.Name = "selectSongButton";
            this.selectSongButton.Size = new System.Drawing.Size(100, 28);
            this.selectSongButton.TabIndex = 1;
            this.selectSongButton.Text = "Select Song";
            this.selectSongButton.UseVisualStyleBackColor = true;
            this.selectSongButton.Click += new System.EventHandler(this.selectSongButton_Click);
            // 
            // songScrollBar
            // 
            this.songScrollBar.Location = new System.Drawing.Point(23, 176);
            this.songScrollBar.Name = "songScrollBar";
            this.songScrollBar.Size = new System.Drawing.Size(525, 17);
            this.songScrollBar.TabIndex = 4;
            this.songScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.songScrollBar_Scroll);
            // 
            // songNameLabel
            // 
            this.songNameLabel.AutoSize = true;
            this.songNameLabel.Location = new System.Drawing.Point(903, 35);
            this.songNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.songNameLabel.Name = "songNameLabel";
            this.songNameLabel.Size = new System.Drawing.Size(79, 17);
            this.songNameLabel.TabIndex = 5;
            this.songNameLabel.Text = "<fileName>";
            // 
            // leftTimeMarginLabel
            // 
            this.leftTimeMarginLabel.AutoSize = true;
            this.leftTimeMarginLabel.Location = new System.Drawing.Point(20, 149);
            this.leftTimeMarginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.leftTimeMarginLabel.Name = "leftTimeMarginLabel";
            this.leftTimeMarginLabel.Size = new System.Drawing.Size(36, 17);
            this.leftTimeMarginLabel.TabIndex = 6;
            this.leftTimeMarginLabel.Text = "0:00";
            // 
            // rightTimeMarginLabel
            // 
            this.rightTimeMarginLabel.AutoSize = true;
            this.rightTimeMarginLabel.Location = new System.Drawing.Point(512, 149);
            this.rightTimeMarginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rightTimeMarginLabel.Name = "rightTimeMarginLabel";
            this.rightTimeMarginLabel.Size = new System.Drawing.Size(36, 17);
            this.rightTimeMarginLabel.TabIndex = 7;
            this.rightTimeMarginLabel.Text = "0:00";
            // 
            // songDurationLabel
            // 
            this.songDurationLabel.AutoSize = true;
            this.songDurationLabel.Location = new System.Drawing.Point(946, 84);
            this.songDurationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.songDurationLabel.Name = "songDurationLabel";
            this.songDurationLabel.Size = new System.Drawing.Size(36, 17);
            this.songDurationLabel.TabIndex = 9;
            this.songDurationLabel.Text = "0:00";
            // 
            // togglePlaybackButton
            // 
            this.togglePlaybackButton.Enabled = false;
            this.togglePlaybackButton.Location = new System.Drawing.Point(668, 165);
            this.togglePlaybackButton.Margin = new System.Windows.Forms.Padding(4);
            this.togglePlaybackButton.Name = "togglePlaybackButton";
            this.togglePlaybackButton.Size = new System.Drawing.Size(100, 28);
            this.togglePlaybackButton.TabIndex = 10;
            this.togglePlaybackButton.Text = "play";
            this.togglePlaybackButton.UseVisualStyleBackColor = true;
            this.togglePlaybackButton.Click += new System.EventHandler(this.togglePlaybackButton_Click);
            // 
            // debugTextBox1
            // 
            this.debugTextBox1.Location = new System.Drawing.Point(1064, 60);
            this.debugTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.debugTextBox1.Name = "debugTextBox1";
            this.debugTextBox1.Size = new System.Drawing.Size(100, 22);
            this.debugTextBox1.TabIndex = 12;
            // 
            // debugTextBox2
            // 
            this.debugTextBox2.Location = new System.Drawing.Point(1064, 99);
            this.debugTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.debugTextBox2.Name = "debugTextBox2";
            this.debugTextBox2.Size = new System.Drawing.Size(100, 22);
            this.debugTextBox2.TabIndex = 13;
            // 
            // debugButton1
            // 
            this.debugButton1.Location = new System.Drawing.Point(1064, 135);
            this.debugButton1.Margin = new System.Windows.Forms.Padding(4);
            this.debugButton1.Name = "debugButton1";
            this.debugButton1.Size = new System.Drawing.Size(100, 28);
            this.debugButton1.TabIndex = 14;
            this.debugButton1.Text = "button1";
            this.debugButton1.UseVisualStyleBackColor = true;
            this.debugButton1.Click += new System.EventHandler(this.debugButton1_Click);
            // 
            // songSecondsDisplayedTrackBar
            // 
            this.songSecondsDisplayedTrackBar.Location = new System.Drawing.Point(642, 101);
            this.songSecondsDisplayedTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.songSecondsDisplayedTrackBar.Maximum = 7;
            this.songSecondsDisplayedTrackBar.Name = "songSecondsDisplayedTrackBar";
            this.songSecondsDisplayedTrackBar.Size = new System.Drawing.Size(139, 56);
            this.songSecondsDisplayedTrackBar.TabIndex = 15;
            this.songSecondsDisplayedTrackBar.Scroll += new System.EventHandler(this.songSecondsDisplayedTrackBar_Scroll);
            // 
            // songValueChart
            // 
            this.songValueChart.BorderlineWidth = 2;
            chartArea3.Name = "ChartArea1";
            this.songValueChart.ChartAreas.Add(chartArea3);
            legend2.Name = "Legend1";
            legend2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songValueChart.Legends.Add(legend2);
            this.songValueChart.Location = new System.Drawing.Point(23, 244);
            this.songValueChart.Margin = new System.Windows.Forms.Padding(4);
            this.songValueChart.Name = "songValueChart";
            this.songValueChart.Size = new System.Drawing.Size(1128, 338);
            this.songValueChart.TabIndex = 16;
            this.songValueChart.Text = "chart1";
            this.songValueChart.Click += new System.EventHandler(this.songValueChart_Click);
            // 
            // debugCheckBox1
            // 
            this.debugCheckBox1.AutoSize = true;
            this.debugCheckBox1.Location = new System.Drawing.Point(1716, 654);
            this.debugCheckBox1.Margin = new System.Windows.Forms.Padding(4);
            this.debugCheckBox1.Name = "debugCheckBox1";
            this.debugCheckBox1.Size = new System.Drawing.Size(70, 21);
            this.debugCheckBox1.TabIndex = 17;
            this.debugCheckBox1.Text = "debug";
            this.debugCheckBox1.UseVisualStyleBackColor = true;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(665, 35);
            this.progressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(116, 17);
            this.progressLabel.TabIndex = 18;
            this.progressLabel.Text = "no song selected";
            // 
            // debugChart1
            // 
            chartArea4.Name = "ChartArea1";
            this.debugChart1.ChartAreas.Add(chartArea4);
            this.debugChart1.Location = new System.Drawing.Point(1385, 401);
            this.debugChart1.Margin = new System.Windows.Forms.Padding(4);
            this.debugChart1.Name = "debugChart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Name = "Series1";
            this.debugChart1.Series.Add(series2);
            this.debugChart1.Size = new System.Drawing.Size(257, 414);
            this.debugChart1.TabIndex = 19;
            this.debugChart1.Text = "chart1";
            // 
            // songWaveViewer
            // 
            this.songWaveViewer.Location = new System.Drawing.Point(23, 23);
            this.songWaveViewer.Margin = new System.Windows.Forms.Padding(4);
            this.songWaveViewer.Name = "songWaveViewer";
            this.songWaveViewer.SamplesPerPixel = 128;
            this.songWaveViewer.Size = new System.Drawing.Size(525, 109);
            this.songWaveViewer.StartPosition = ((long)(0));
            this.songWaveViewer.TabIndex = 11;
            this.songWaveViewer.WaveStream = null;
            // 
            // debugTextBox3
            // 
            this.debugTextBox3.Location = new System.Drawing.Point(1190, 60);
            this.debugTextBox3.Name = "debugTextBox3";
            this.debugTextBox3.Size = new System.Drawing.Size(100, 22);
            this.debugTextBox3.TabIndex = 20;
            // 
            // debugTextBox4
            // 
            this.debugTextBox4.Location = new System.Drawing.Point(1190, 99);
            this.debugTextBox4.Name = "debugTextBox4";
            this.debugTextBox4.Size = new System.Drawing.Size(100, 22);
            this.debugTextBox4.TabIndex = 21;
            // 
            // debugTextBox5
            // 
            this.debugTextBox5.Location = new System.Drawing.Point(1190, 135);
            this.debugTextBox5.Name = "debugTextBox5";
            this.debugTextBox5.Size = new System.Drawing.Size(100, 22);
            this.debugTextBox5.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1357, 626);
            this.Controls.Add(this.debugTextBox5);
            this.Controls.Add(this.debugTextBox4);
            this.Controls.Add(this.debugTextBox3);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.TextBox debugTextBox3;
        private System.Windows.Forms.TextBox debugTextBox4;
        private System.Windows.Forms.TextBox debugTextBox5;
    }
}


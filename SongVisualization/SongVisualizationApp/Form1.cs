using SongVisualizationApp.SongAnalyzing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongVisualizationApp
{
	public partial class Form1 : Form
	{

		private VisualValues visualValues;
		private ISongAnalyzingFacade songAnalyzingFacade;

		public Form1()
		{
			InitializeComponent();
		}

		public void SetVisualValues(VisualValues visualValues)
		{
			this.visualValues = visualValues;
		}

		public void SetSongAnalyzingFacade(ISongAnalyzingFacade songAnalyzingFacade)
		{
			this.songAnalyzingFacade = songAnalyzingFacade;
		}

		public void UpdateEverything()
		{
			visualValues.UpdateTimeMargins();

			songNameLabel.Text = visualValues.SongName;
			songDurationLabel.Text = visualValues.GetSongDurationString();

			leftTimeMarginLabel.Text = visualValues.GetLeftTimeMarginString();
			rightTimeMarginLabel.Text = visualValues.GetRightTimeMarginString();

			//songScrollBar.LargeChange = (int) Math.Floor(visualValues.GetZoomRatio() * songScrollBar.Maximum);

			Update();
		}



		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void selectSongButton_Click(object sender, EventArgs e)
		{
			selectSongDialog.ShowDialog();
		}

		private void selectSongDialog_FileOk(object sender, CancelEventArgs e)
		{
			songAnalyzingFacade.AnalyzeSong(selectSongDialog.FileName);
		}

		private void fftChart_Click(object sender, EventArgs e)
		{

		}

		private void secondsDisplayedComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			visualValues.SetSecondsDisplayed(secondsDisplayedComboBox.Text);

			UpdateEverything();
		}

		private void songScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			double scrollBarValue = songScrollBar.Value;

			scrollBarValue = Math.Max(0, scrollBarValue * 1.2 - 10);

			double percentage = scrollBarValue / (double) songScrollBar.Maximum;

			visualValues.SetTimeCenter(percentage);

			UpdateEverything();
		}
	}
}

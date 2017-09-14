using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using NAudio.Wave;
using SongVisualizationApp.FileReader;

namespace SongVisualizationApp
{

	// modification of NAudio.Gui.WaveViewer


	/// <summary>
	/// Control for viewing waveforms
	/// </summary>
	public class MyWaveViewer : System.Windows.Forms.UserControl
	{

		private SongFile songFile;

		private double leftTimeMargin = 0;
		private double rightTimeMargin = 10;

		private double secondsDisplayed = 10;

		private bool markerLineDrawn = false;
		private int oldMarkerXPosition = -1;

		public double CursorTime { get; private set; }
		public double MarkerTime { get; private set; }


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private WaveStream waveStream;
		private int samplesPerPixel = 128;
		private int bytesPerSample;
		/// <summary>
		/// Creates a new WaveViewer control
		/// </summary>
		public MyWaveViewer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.DoubleBuffered = true;

			CursorTime = -1;
			MarkerTime = -1;
		}
		
		public WaveStream WaveStream
		{
			get
			{
				return waveStream;
			}
			set
			{
				waveStream = value;
				if (waveStream != null)
				{
					bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * waveStream.WaveFormat.Channels;
				}
				this.Invalidate();
			}
		}
		
		public int SamplesPerPixel
		{
			get
			{
				return samplesPerPixel;
			}
			set
			{
				samplesPerPixel = value;
				this.Invalidate();
			}
		}
		
		public long StartPosition { get; set; }

		
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			if (waveStream != null)
			{
				waveStream.Position = 0;
				int bytesRead;
				byte[] waveData = new byte[samplesPerPixel * bytesPerSample];
				waveStream.Position = StartPosition + (e.ClipRectangle.Left * bytesPerSample * samplesPerPixel);

				for (float x = e.ClipRectangle.X; x < e.ClipRectangle.Right; x += 1)
				{
					short low = 0;
					short high = 0;
					bytesRead = waveStream.Read(waveData, 0, samplesPerPixel * bytesPerSample);
					if (bytesRead == 0)
						break;
					for (int n = 0; n < bytesRead; n += 2)
					{
						short sample = BitConverter.ToInt16(waveData, n);
						if (sample < low) low = sample;
						if (sample > high) high = sample;
					}
					float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
					float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);
					e.Graphics.DrawLine(Pens.HotPink, x, this.Height * lowPercent, x, this.Height * highPercent);
				}
			}

			base.OnPaint(e);
		}


		public void SetSongFile(SongFile songFile)
		{
			this.songFile = songFile;

			WaveStream = songFile.WaveStream;
		}

		public void SetTimeMargins(double leftTimeMargin, double rightTimeMargin)
		{
			RemovePreviousLine();

			this.leftTimeMargin = leftTimeMargin;
			this.rightTimeMargin = rightTimeMargin;

			secondsDisplayed = rightTimeMargin - leftTimeMargin;

			if (songFile == null) return;

			int samplesDisplayed = songFile.SampleRate * (int) secondsDisplayed;

			SamplesPerPixel = samplesDisplayed / Width;

			StartPosition = (long) leftTimeMargin * songFile.SampleRate;
		}


		protected override void OnMouseDown(MouseEventArgs e)
		{
			RemovePreviousLine();

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				DrawVerticalLine(e.X);

				markerLineDrawn = true;
				oldMarkerXPosition = e.X;

				MarkerTime = GetTimeByXPosition(e.X);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			CursorTime = GetTimeByXPosition(e.Location.X);
		}

		private double GetTimeByXPosition(int xPosition)
		{
			double relativeXPosition = xPosition / (double)Width;

			return leftTimeMargin + relativeXPosition * secondsDisplayed;
		}

		private void RemovePreviousLine()
		{
			if (oldMarkerXPosition > 0 && markerLineDrawn) DrawVerticalLine(oldMarkerXPosition);

			markerLineDrawn = false;
		}

		private void DrawVerticalLine(int x)
		{
			Point lineStartPoint = PointToScreen(new Point(x, 0));
			Point lineEndPoint = PointToScreen(new Point(x, Height));

			ControlPaint.DrawReversibleLine(lineStartPoint, lineEndPoint, Color.Black);
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Audio
{
	class MusicPlayer
	{
		
		private WaveOutEvent waveOutEvent;
		private WaveStream waveStream;

		private string fileDirectory;

		public MusicPlayer(string fileDirectory)
		{
			waveOutEvent = new WaveOutEvent();

			this.fileDirectory = fileDirectory;
		}

		public void InitAudio()
		{
			waveStream = new Mp3FileReader(fileDirectory);

			waveOutEvent.Init(waveStream);
		}

		public void PlayTrack()
		{
			waveOutEvent.Play();
		}

		public void PauseTrack()
		{
			waveOutEvent.Stop();
		}

		public void Dispose()
		{
			if (waveOutEvent != null) waveOutEvent.Dispose();
			if (waveStream != null) waveStream.Dispose();
		}

	}
}

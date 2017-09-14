using NAudio.Wave;
using SongVisualizationApp.FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.Audio
{
	class AudioPlayer
	{

		private SongFile songFile;

		private WaveOutEvent waveOutEvent;
		private WaveStream waveStream;

		public AudioPlayer()
		{
			waveOutEvent = new WaveOutEvent();
		}

		public void InitAudio(SongFile songFile)
		{
			this.songFile = songFile;

			string fileDirectory = songFile.FileDirectory;

			if (songFile.FileType == FileType.Mp3) waveStream = new Mp3FileReader(fileDirectory);
			else waveStream = new WaveFileReader(fileDirectory);

			waveOutEvent.Init(waveStream);
		}

		public void PlayAudio()
		{
			waveOutEvent.Play();
		}

		public void PlayAudioFromTime(double time)
		{
			waveStream.Position = (long) time * songFile.SampleRate * 4;

			PlayAudio();
		}

		public void StopAudio()
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace GameApp.Audio
{
	class MusicPlayer
	{

		private string musicFileURL;

		private WindowsMediaPlayer windowsMediaPlayer = new WindowsMediaPlayer();

		public MusicPlayer(string musicFileURL)
		{
			this.musicFileURL = musicFileURL;

			windowsMediaPlayer.URL = musicFileURL;

			windowsMediaPlayer.controls.stop();
		}

		public void PlayTrack()
		{
			windowsMediaPlayer.controls.play();
		}

		public void PauseTrack()
		{
			windowsMediaPlayer.controls.pause();
		}

	}
}

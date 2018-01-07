using GameApp.Screens.Input;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Utils
{

	public class KeyPress
	{
		public DateTime Time { get; set; }
		public Key Key { get; set; }
		public bool IsKeyUp { get; set; }

		public KeyPress(DateTime time, Key key, bool isKeyUp)
		{
			Time = time;
			Key = key;
			IsKeyUp = isKeyUp;
		}

	}

	class PlaySessionLog
	{

		public string FileName { get; set; }

		public string LevelName { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public List<KeyPress> KeyPresses { get; set; }

		public PlaySessionLog(string levelName)
		{
			LevelName = levelName;

			KeyPresses = new List<KeyPress>();

			SetFileName();
		}

		public void StartSession()
		{
			StartTime = DateTime.Now;
		}

		public void FinishSession()
		{
			EndTime = DateTime.Now;
		}

		private void SetFileName()
		{
			DateTime now = DateTime.Now;

			FileName = "playSession_" + now.Year + "-" + now.Month + "-" + now.Day + "_" +
				now.Hour + "-" + now.Minute + "-" + now.Second + "_" + now.Millisecond;
		}

		public void AddKeyPress(DateTime time, Key key, bool isKeyUp)
		{
			KeyPresses.Add(new KeyPress(time, key, isKeyUp));
		}
	}
}

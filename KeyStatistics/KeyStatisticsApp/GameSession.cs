using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStatisticsApp
{
	class GameSession
	{

		private class KeyAction
		{
			public string KeyName { get; set; }
			public float TimePressed { get; set; }
			public bool WasKeyUp { get; set; }

			public KeyAction(string keyName, float timePressed, bool wasKeyUp)
			{
				KeyName = keyName;
				TimePressed = timePressed;
				WasKeyUp = wasKeyUp;
			}
		}

		private string name;
		private DateTime startingTime;

		public GameSession()
		{
			DateTime now = DateTime.Now;

			startingTime = now;

			name = "GS-" + now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute + "-" + now.Second;
		}

		public void AddKey(string keyName, DateTime timePressed, bool wasKeyUp)
		{

		}

		public void Finish()
		{

		}


	}
}

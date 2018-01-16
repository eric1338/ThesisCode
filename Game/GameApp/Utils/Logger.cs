using GameApp.Screens.Input;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Utils
{
	class Logger
	{

		private static PlaySessionLog currentPlaySessionLog;

		public static void StartNewLog(string levelName)
		{
			currentPlaySessionLog = new PlaySessionLog(levelName);

			currentPlaySessionLog.StartSession();
		}

		public static void FinishLog()
		{
			currentPlaySessionLog.FinishSession();

			WriteCurrentPlaySessionLogToFile();
		}

		public static void AddKeyPress(DateTime time, Key key, bool isKeyUp)
		{
			if (currentPlaySessionLog != null) currentPlaySessionLog.AddKeyPress(time, key, isKeyUp);
		}

		private static void WriteCurrentPlaySessionLogToFile()
		{
			string fileDirectory = @"C:\ForVS\" + currentPlaySessionLog.FileName + ".txt";

			if (!Directory.Exists(@"C:\ForVS"))
			{
				fileDirectory = currentPlaySessionLog.FileName + ".txt";
			}

			using (StreamWriter file = new StreamWriter(fileDirectory))
			{
				file.WriteLine("LevelName=" + currentPlaySessionLog.LevelName);
				file.WriteLine("StartTime=" + GetDateString(currentPlaySessionLog.StartTime, true));
				file.WriteLine("EndTime=" + GetDateString(currentPlaySessionLog.EndTime, true));

				file.WriteLine("-KeyPresses-");

				foreach (KeyPress keyPress in currentPlaySessionLog.KeyPresses)
				{
					string line = GetDateString(keyPress.Time, false);
					line += "; " + keyPress.Key.ToString() + "; isKeyUp=" + keyPress.IsKeyUp;

					file.WriteLine(line);
				}
			}
		}

		private static string GetDateString(DateTime dateTime, bool withDMY)
		{
			string dateString = "";

			if (withDMY) dateString += dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + " (DMY) - ";

			dateString += dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + "," + dateTime.Millisecond;

			return dateString;
		}



	}
}

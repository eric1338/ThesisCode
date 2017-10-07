using NAudio.Wave;
using SongVisualizationApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongVisualizationApp.FileReader
{
	class MyAudioFileReader
	{
		
		public static SongFile ReadAudioFile(string fileDirectory)
		{
			SongFile songFile = new SongFile(fileDirectory);

			FileType fileType = GetFileType(fileDirectory);
			songFile.FileType = fileType;

			songFile.SongName = GetSongName(fileDirectory);

			WaveStream waveStream;

			if (fileType == FileType.Mp3) waveStream = new Mp3FileReader(fileDirectory);
			else waveStream = new WaveFileReader(fileDirectory);

			songFile.WaveStream = waveStream;

			WaveChannel32 waveChannel = new WaveChannel32(waveStream);

			WaveFormat waveFormat = waveChannel.WaveFormat;

			songFile.BitDepth = waveFormat.BitsPerSample;
			songFile.SampleRate = waveFormat.SampleRate;
			songFile.NumberOfChannels = waveFormat.Channels;

			songFile.SongDuration = waveChannel.TotalTime.TotalSeconds;

			songFile.Samples = GetSamples(waveChannel, waveFormat);

			//waveStream.Dispose();
			//waveChannel.Dispose();

			return songFile;
		}

		private static FileType GetFileType(string fileDirectory)
		{
			string[] pieces = fileDirectory.Split('.');

			if (pieces.Length < 2) return FileType.Unknown;

			string ending = pieces[pieces.Length - 1];

			if (ending == "mp3" || ending == "MP3") return FileType.Mp3;

			return FileType.Wav;
		}

		private static string GetSongName(string fileDirectory)
		{
			string[] pieces = fileDirectory.Split('\\');

			if (pieces.Length < 2) return fileDirectory;

			return pieces[pieces.Length - 1];
		}

		private static List<MyPoint> GetSamples(WaveChannel32 waveChannel, WaveFormat waveFormat)
		{
			int byteDepth = waveFormat.BitsPerSample / 8;
			float frequency = 1.0f / waveFormat.SampleRate;

			int bufferSize = 8192;

			byte[] buffer = new byte[bufferSize];
			int read = 0;

			int sampleNumber = 0;

			List<MyPoint> samples = new List<MyPoint>();

			while (waveChannel.Position < waveChannel.Length)
			{
				read = waveChannel.Read(buffer, 0, bufferSize);

				for (int i = 0; i < read / byteDepth; i++)
				{
					float amplitude;

					if (byteDepth == 4) amplitude = BitConverter.ToSingle(buffer, i * 4);
					else amplitude = (float) BitConverter.ToDouble(buffer, i * 8);

					float time = sampleNumber * frequency * (1.0f / waveFormat.Channels);
					
					samples.Add(new MyPoint(time, amplitude));

					sampleNumber++;
				}
			}

			return samples;
		}

	}
}

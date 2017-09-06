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

			WaveChannel32 waveChannel = new WaveChannel32(new WaveFileReader(fileDirectory));

			WaveFormat waveFormat = waveChannel.WaveFormat;

			songFile.BitDepth = waveFormat.BitsPerSample;
			songFile.SampleRate = waveFormat.SampleRate;
			songFile.SongDuration = waveChannel.TotalTime.TotalSeconds;

			int byteDepth = waveFormat.BitsPerSample / 8;
			double frequency = 1.0 / waveFormat.SampleRate;

			byte[] buffer = new byte[16384];
			int read = 0;

			int sampleNumber = 0;

			Console.WriteLine("wave.Length: " + waveChannel.Length);

			List<MyPoint> samplePoints = new List<MyPoint>();

			while (waveChannel.Position < waveChannel.Length)
			{
				read = waveChannel.Read(buffer, 0, 16384);

				for (int i = 0; i < read / byteDepth; i++)
				{
					double amplitude;

					if (byteDepth == 4) amplitude = BitConverter.ToSingle(buffer, i * 4);
					else amplitude = BitConverter.ToDouble(buffer, i * 8);

					double time = sampleNumber * frequency;

					samplePoints.Add(new MyPoint(time, amplitude));

					sampleNumber++;
				}
			}

			Console.WriteLine("sampleNumber: " + sampleNumber);

			songFile.Samples = samplePoints;

			Console.WriteLine("samplePoints.Count: " + samplePoints.Count);

			return songFile;
		}

		private static void ReadMp3File(string fileDirectory)
		{

		}

	}
}

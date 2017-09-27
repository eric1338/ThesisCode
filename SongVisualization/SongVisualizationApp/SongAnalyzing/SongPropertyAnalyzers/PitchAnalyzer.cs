using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.IntegralTransforms;

using SongVisualizationApp.FileReader;
using SongVisualizationApp.Util;

namespace SongVisualizationApp.SongAnalyzing.SongPropertyAnalyzers
{
	class PitchAnalyzer : SongPropertyAnalyzer
	{

		public PitchAnalyzer()
		{
			PropertyName = "Pitch";
			ProgressText = "Determining Pitch...";
		}

		public override SongPropertyValues Analyze(SongFile songFile)
		{
			return DoMultipleFFTs(songFile, 0, 80, 1f);
		}


		public static SongPropertyValues DoMultipleFFTs(SongFile songFile, float startingTime, float endTime, float sampleWidthTime)
		{
			SongPropertyValues values = new SongPropertyValues("FFT-Ari " + sampleWidthTime);

			float sampleDeltaX = sampleWidthTime * 0.5f;

			for (float i = startingTime; i < endTime; i += sampleDeltaX)
			{

				float time = i + (sampleDeltaX / 2.0f);

				List<MyPoint> fftValues = GetFFTValues(songFile, i, i + sampleWidthTime);

				float max = GetMax(fftValues);

				List<MyPoint> testPoints = new List<MyPoint>();

				foreach (MyPoint point in fftValues)
				{
					if (point.Y * 4 > max) testPoints.Add(point);
				}

				float y = GetAriMeanX(testPoints);

				values.AddPoint(time * 0.5f, y);
			}

			values.Normalize();

			return values;
		}

		public static float GetMax(List<MyPoint> points)
		{
			float max = -1;

			foreach (MyPoint point in points)
			{
				if (point.Y > max) max = point.Y;
			}

			return max;
		}

		public static float GetAriMeanX(List<MyPoint> points)
		{
			float sum = 0;

			if (points.Count <= 0) return 0;

			foreach (MyPoint point in points)
			{
				sum += point.X;
			}

			return sum / points.Count;
		}

		public static List<MyPoint> GetFFTValues(SongFile songFile, double startingTime, double endTime)
		{
			List<MyPoint> songSamples = songFile.GetSamples(startingTime, endTime);

			int nSamples = songSamples.Count;

			System.Numerics.Complex[] samplesForFFT = new System.Numerics.Complex[nSamples];

			for (int i = 0; i < nSamples; i++)
			{
				samplesForFFT[i] = new System.Numerics.Complex(songSamples[i].Y, 0);
			}

			if (nSamples <= 0) return new List<MyPoint>();

			Fourier.Forward(samplesForFFT, FourierOptions.NoScaling);

			List<MyPoint> fftPoints = new List<MyPoint>();

			float hzPerSample = songFile.SampleRate / (float)nSamples;

			int maxFrequency = Math.Min(samplesForFFT.Length / 2, 5000);

			for (int i = 1; i < maxFrequency; i++)
			{
				float mag = (2.0f / nSamples) * Utils.GetRealValue(samplesForFFT[i].Real, samplesForFFT[i].Imaginary);

				fftPoints.Add(new MyPoint(hzPerSample * i, mag));
			}

			return fftPoints;
		}
	}
}

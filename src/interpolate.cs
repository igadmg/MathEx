using System;

namespace MathEx
{
	public static class CubicHermiteSpline
	{
		private static float hermite_30(float t) { return 1 - 3 * t * t + 2 * t * t * t; }
		private static float hermite_31(float t) { return t - 2 * t * t + t * t * t; }
		private static float hermite_32(float t) { return -t * t + t * t * t; }
		private static float hermite_33(float t) { return 3 * t * t - 2 * t * t * t; }

		public static float Interpolate(float p0, float v0, float p1, float v1, float t)
		{
			return hermite_30(t) * p0 + hermite_31(t) * v0 + hermite_32(t) * v1 + hermite_33(t) * p1;
		}
	}

	public class MonotonicCubicHermiteSpline
	{
		private float[] x;
		private float[] y;

		private float[] c1s;
		private float[] c2s;
		private float[] c3s;

		public MonotonicCubicHermiteSpline(Tuple<float, float>[] points)
		{
			x = new float[points.Length];
			y = new float[points.Length];

			float[] ks = new float[points.Length - 1];
			float[] ms = new float[points.Length];

			for (int i = 0; i < points.Length; i++)
			{
				x[i] = points[i].Item1;
				y[i] = points[i].Item2;

				int k = i - 1;
				if (k >= 0)
				{
					ks[k] = (x[k+1] - x[k]) / (y[k+1] - y[k]);

					if (k > 0)
					{
						ms[k] = (ks[k - 1] + ks[k]) / 2.0f;
					}
				}
			}
			ms[0] = ks[0];
			ms[ms.Length - 1] = ks[ks.Length - 1];

			c1s = new float[points.Length];
			c2s = new float[points.Length];
			c3s = new float[points.Length];

			for (int i = 0; i < points.Length; i++)
			{

			}
		}
	}

	public static class QuinticHermiteSpline
	{
		private static float hermite_50(float t) { return 1 - 10 * t * t * t + 15 * t * t * t * t - 6 * t * t * t * t * t; }
		private static float hermite_51(float t) { return t - 6 * t * t * t + 8 * t * t * t * t - 3 * t * t * t * t * t; }
		private static float hermite_52(float t) { return 0.5f * t * t - 1.5f * t * t * t + 1.5f * t * t * t * t - 0.5f * t * t * t * t * t; }
		private static float hermite_53(float t) { return 0.5f * t * t * t - t * t * t * t + 0.5f * t * t * t * t * t; }
		private static float hermite_54(float t) { return -4 * t * t * t + 7 * t * t * t * t - 3 * t * t * t * t * t; }
		private static float hermite_55(float t) { return 10 * t * t * t - 15 * t * t * t * t + 6 * t * t * t * t * t; }



		private static float Interpolate(float p0, float v0, float a0, float p1, float v1, float a1, float t)
		{
			return hermite_50(t) * p0 + hermite_51(t) * v0 + hermite_52(t) * a0 + hermite_53(t) * a1 + hermite_54(t) * v1 + hermite_55(t) * p1;
		}
	}
}

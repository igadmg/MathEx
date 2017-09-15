using System;

namespace MathEx
{
	public abstract class Interpolator<T>
	{
		protected static MathTypeTag<T> mtt = MathTypeTag<T>.Get();

		public abstract int size { get; }

		public abstract T integral(float t, params T[] p);
		public abstract T value(float t, params T[] p);
		public abstract T derivative(float t, params T[] p);

		public abstract T integral(float t, T[] p, int i);
		public abstract T value(float t, T[] p, int i);
		public abstract T derivative(float t, T[] p, int i);
	}

	// based on https://www.rose-hulman.edu/~finn/CCLI/Notes/day09.pdf
	public class CubicHermiteSpline<T> : Interpolator<T>
	{
		private static float hermite_p30(float t) { return  1f/2f * t*t*t*t - 1f/2f * t*t*t               + t; }
		private static float hermite_p31(float t) { return  1f/4f * t*t*t*t - 2f/3f * t*t*t + 1f/2f * t*t; }
		private static float hermite_p32(float t) { return  1f/4f * t*t*t*t - 1f/3f * t*t*t; }
		private static float hermite_p33(float t) { return -1f/2f * t*t*t*t +         t*t*t; }

		private static float hermite_30(float t) { return  2 * t*t*t - 3 * t*t     + 1; }
		private static float hermite_31(float t) { return      t*t*t - 2 * t*t + t; }
		private static float hermite_32(float t) { return      t*t*t -     t*t; }
		private static float hermite_33(float t) { return -2 * t*t*t + 3 * t*t; }

		private static float hermite_d30(float t) { return  6 * t*t - 6 * t; }
		private static float hermite_d31(float t) { return  3 * t*t - 4 * t + 1; }
		private static float hermite_d32(float t) { return  3 * t*t - 2 * t; }
		private static float hermite_d33(float t) { return -6 * t*t + 6 * t; }


		public static int Size = 4;

		public static T Integral(T p0, T v0, T p1, T v1, float t)
		{
			return mtt.sum(
				mtt.mul(hermite_p30(t), p0)
				, mtt.mul(hermite_p31(t), v0)
				, mtt.mul(hermite_p32(t), v1)
				, mtt.mul(hermite_p33(t), p1));
		}

		public static T Value(T p0, T v0, T p1, T v1, float t)
		{
			return mtt.sum(
				mtt.mul(hermite_30(t), p0)
				, mtt.mul(hermite_31(t), v0)
				, mtt.mul(hermite_32(t), v1)
				, mtt.mul(hermite_33(t), p1));
		}

		public static T Derivative(T p0, T v0, T p1, T v1, float t)
		{
			return mtt.sum(
				mtt.mul(hermite_d30(t), p0)
				, mtt.mul(hermite_d31(t), v0)
				, mtt.mul(hermite_d32(t), v1)
				, mtt.mul(hermite_d33(t), p1));
		}

		public override int size { get { return Size; } }

		public override T integral(float t, params T[] p)
		{
			return Integral(p[0], p[1], p[3], p[2], t);
		}

		public override T value(float t, params T[] p)
		{
			return Value(p[0], p[1], p[3], p[2], t);
		}

		public override T derivative(float t, params T[] p)
		{
			return Derivative(p[0], p[1], p[3], p[2], t);
		}

		public override T integral(float t, T[] p, int i)
		{
			return integral(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3]);
		}

		public override T value(float t, T[] p, int i)
		{
			return value(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3]);
		}

		public override T derivative(float t, T[] p, int i)
		{
			return derivative(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3]);
		}
	}

	public class QuinticHermiteSpline<T> : Interpolator<T>
	{
		private static float hermite_50(float t) { return    -6f * t*t*t*t*t +   15f * t*t*t*t -   10f * t*t*t                   + 1; }
		private static float hermite_51(float t) { return    -3f * t*t*t*t*t +    8f * t*t*t*t -    6f * t*t*t               + t; }
		private static float hermite_52(float t) { return -1f/2f * t*t*t*t*t + 3f/2f * t*t*t*t - 3f/2f * t*t*t + 1f/2f * t*t; }
		private static float hermite_53(float t) { return  1f/2f * t*t*t*t*t -         t*t*t*t + 1f/2f * t*t*t; }
		private static float hermite_54(float t) { return    -3f * t*t*t*t*t +    7f * t*t*t*t -    4f * t*t*t; }
		private static float hermite_55(float t) { return     6f * t*t*t*t*t -   15f * t*t*t*t +   10f * t*t*t; }



		public static int Size = 6;

		private static T Value(T p0, T v0, T a0, T p1, T v1, T a1, float t)
		{
			return mtt.sum(
					mtt.mul(hermite_50(t), p0)
					, mtt.mul(hermite_51(t), v0)
					, mtt.mul(hermite_52(t), a0)
					, mtt.mul(hermite_53(t), a1)
					, mtt.mul(hermite_54(t), v1)
					, mtt.mul(hermite_55(t), p1));
		}

		public override int size { get { return Size; } }

		public override T integral(float t, params T[] p)
		{
			throw new Exception(); // Integral(p[0], p[1], p[2], p[5], p[4], p[3], t);
		}

		public override T value(float t, params T[] p)
		{
			return Value(p[0], p[1], p[2], p[5], p[4], p[3], t);
		}

		public override T derivative(float t, params T[] p)
		{
			throw new Exception(); // Derivative(p[0], p[1], p[2], p[5], p[4], p[3], t);
		}

		public override T integral(float t, T[] p, int i)
		{
			return integral(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3], p[i + 4], p[i + 5]);
		}

		public override T value(float t, T[] p, int i)
		{
			return value(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3], p[i + 4], p[i + 5]);
		}

		public override T derivative(float t, T[] p, int i)
		{
			return derivative(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3], p[i + 4], p[i + 5]);
		}
	}

	public class QuadricBezierSpline<T> : Interpolator<T>
	{
		private static float bezier_p20(float t) { return  1f/3f * t*t*t - t*t + t; }
		private static float bezier_p21(float t) { return -2f/3f * t*t*t + t*t; }
		private static float bezier_p22(float t) { return  1f/3f * t*t*t; }
		
		private static float bezier_20(float t) { return       t*t - 2f * t + 1f; }  // (1-t)^2
		private static float bezier_21(float t) { return -2f * t*t + 2f * t; }       // 2 * t * (1-t)
		private static float bezier_22(float t) { return       t*t; }                // t^2
		
		private static float bezier_d20(float t) { return  2f * t - 2f; }
		private static float bezier_d21(float t) { return -4f * t + 2f; }
		private static float bezier_d22(float t) { return  2f * t; }
		

		public static int Size = 3;

		public static T Integral(T p0, T p1, T p2, float t)
		{
			return mtt.sum(
				mtt.mul(bezier_p20(t), p0)
				, mtt.mul(bezier_p21(t), p1)
				, mtt.mul(bezier_p22(t), p2));
		}

		public static T Value(T p0, T p1, T p2, float t)
		{
			return mtt.sum(
				mtt.mul(bezier_20(t), p0)
				, mtt.mul(bezier_21(t), p1)
				, mtt.mul(bezier_22(t), p2));
		}

		public static T Derivative(T p0, T p1, T p2, float t)
		{
			return mtt.sum(
				mtt.mul(bezier_d20(t), p0)
				, mtt.mul(bezier_d21(t), p1)
				, mtt.mul(bezier_d22(t), p2));
		}

		public override int size { get { return Size; } }

		public override T integral(float t, params T[] p)
		{
			return Integral(p[0], p[1], p[2], t);
		}

		public override T value(float t, params T[] p)
		{
			return Value(p[0], p[1], p[2], t);
		}

		public override T derivative(float t, params T[] p)
		{
			return Derivative(p[0], p[1], p[2], t);
		}

		public override T integral(float t, T[] p, int i)
		{
			return integral(t, p[i + 0], p[i + 1], p[i + 2]);
		}

		public override T value(float t, T[] p, int i)
		{
			return value(t, p[i + 0], p[i + 1], p[i + 2]);
		}

		public override T derivative(float t, T[] p, int i)
		{
			return derivative(t, p[i + 0], p[i + 1], p[i + 2]);
		}
	}

	public class CubicBezierSpline<T> : Interpolator<T>
	{
		private static float bezier_p30(float t) { return -1f/4f * t*t*t*t +      t*t*t - 3f/2f * t*t + t; }
		private static float bezier_p31(float t) { return  3f/4f * t*t*t*t - 2f * t*t*t + 3f/2f * t*t; }
		private static float bezier_p32(float t) { return -3f/4f * t*t*t*t +      t*t*t; }
		private static float bezier_p33(float t) { return  1f/4f * t*t*t*t; }

		private static float bezier_30(float t) { return -1f * t*t*t + 3f * t*t - 3f * t + 1f; } // (1-t)^3
		private static float bezier_31(float t) { return  3f * t*t*t - 6f * t*t + 3f * t; }      // 3 * t * (1-t)^2
		private static float bezier_32(float t) { return -3f * t*t*t + 3f * t*t; }               // 3 * t^2 * (1-t)
		private static float bezier_33(float t) { return       t*t*t; }                          // t^3

		private static float bezier_d30(float t) { return -3f * t*t +  6f * t - 3f; }
		private static float bezier_d31(float t) { return  9f * t*t - 12f * t + 3f; }
		private static float bezier_d32(float t) { return -9f * t*t +  6f * t; }
		private static float bezier_d33(float t) { return  3f * t*t; }


		public static int Size = 4;

		public static T Integral(T p0, T p1, T p2, T p3, float t)
		{
			return mtt.sum(
				mtt.mul(bezier_p30(t), p0)
				, mtt.mul(bezier_p31(t), p1)
				, mtt.mul(bezier_p32(t), p2)
				, mtt.mul(bezier_p33(t), p3));
		}

		public static T Value(T p0, T p1, T p2, T p3, float t)
		{
			return mtt.sum(
				mtt.mul(bezier_30(t), p0)
				, mtt.mul(bezier_31(t), p1)
				, mtt.mul(bezier_32(t), p2)
				, mtt.mul(bezier_33(t), p3));
		}

		public static T Derivative(T p0, T p1, T p2, T p3, float t)
		{
			return mtt.sum(
				mtt.mul(bezier_d30(t), p0)
				, mtt.mul(bezier_d31(t), p1)
				, mtt.mul(bezier_d32(t), p2)
				, mtt.mul(bezier_d33(t), p3));
		}

		public override int size { get { return Size; } }

		public override T integral(float t, params T[] p)
		{
			return Integral(p[0], p[1], p[2], p[3], t);
		}

		public override T value(float t, params T[] p)
		{
			return Value(p[0], p[1], p[2], p[3], t);
		}

		public override T derivative(float t, params T[] p)
		{
			return Derivative(p[0], p[1], p[2], p[3], t);
		}

		public override T integral(float t, T[] p, int i)
		{
			return integral(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3]);
		}

		public override T value(float t, T[] p, int i)
		{
			return value(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3]);
		}

		public override T derivative(float t, T[] p, int i)
		{
			return derivative(t, p[i + 0], p[i + 1], p[i + 2], p[i + 3]);
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
}

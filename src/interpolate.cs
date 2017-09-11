using System;

namespace MathEx
{
	public abstract class Interpolator
	{
		public abstract int size { get; }

		public abstract float integral(float t, params float[] p);
		public abstract float value(float t, params float[] p);
		public abstract float derivative(float t, params float[] p);
	}

	// based on https://www.rose-hulman.edu/~finn/CCLI/Notes/day09.pdf
	public class CubicHermiteSpline : Interpolator
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

		public static float Integral(float p0, float v0, float p1, float v1, float t)
		{
			return hermite_p30(t) * p0 + hermite_p31(t) * v0 + hermite_p32(t) * v1 + hermite_p33(t) * p1;
		}

		public static float Value(float p0, float v0, float p1, float v1, float t)
		{
			return hermite_30(t) * p0 + hermite_31(t) * v0 + hermite_32(t) * v1 + hermite_33(t) * p1;
		}

		public static float Derivative(float p0, float v0, float p1, float v1, float t)
		{
			return hermite_d30(t) * p0 + hermite_d31(t) * v0 + hermite_d32(t) * v1 + hermite_d33(t) * p1;
		}

		public override int size { get { return Size; } }

		public override float integral(float t, params float[] p)
		{
			return Integral(p[0], p[1], p[3], p[2], t);
		}

		public override float value(float t, params float[] p)
		{
			return Value(p[0], p[1], p[3], p[2], t);
		}

		public override float derivative(float t, params float[] p)
		{
			return Derivative(p[0], p[1], p[3], p[2], t);
		}
	}

	public class QuinticHermiteSpline : Interpolator
	{
		private static float hermite_50(float t) { return    -6f * t*t*t*t*t +   15f * t*t*t*t -   10f * t*t*t                   + 1; }
		private static float hermite_51(float t) { return    -3f * t*t*t*t*t +    8f * t*t*t*t -    6f * t*t*t               + t; }
		private static float hermite_52(float t) { return -1f/2f * t*t*t*t*t + 3f/2f * t*t*t*t - 3f/2f * t*t*t + 1f/2f * t*t; }
		private static float hermite_53(float t) { return  1f/2f * t*t*t*t*t -         t*t*t*t + 1f/2f * t*t*t; }
		private static float hermite_54(float t) { return    -3f * t*t*t*t*t +    7f * t*t*t*t -    4f * t*t*t; }
		private static float hermite_55(float t) { return     6f * t*t*t*t*t -   15f * t*t*t*t +   10f * t*t*t; }



		public static int Size = 6;

		private static float Value(float p0, float v0, float a0, float p1, float v1, float a1, float t)
		{
			return hermite_50(t) * p0 + hermite_51(t) * v0 + hermite_52(t) * a0 + hermite_53(t) * a1 + hermite_54(t) * v1 + hermite_55(t) * p1;
		}

		public override int size { get { return Size; } }

		public override float integral(float t, params float[] p)
		{
			return 0;// Integral(p[0], p[1], p[2], p[5], p[4], p[3], t);
		}

		public override float value(float t, params float[] p)
		{
			return Value(p[0], p[1], p[2], p[5], p[4], p[3], t);
		}

		public override float derivative(float t, params float[] p)
		{
			return 0;// Derivative(p[0], p[1], p[2], p[5], p[4], p[3], t);
		}
	}

	public class QuadricBezierSpline : Interpolator
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

		public static float Integral(float p0, float p1, float p2, float t)
		{
			return bezier_p20(t) * p0 + bezier_p21(t) * p1 + bezier_p22(t) * p2;
		}

		public static float Value(float p0, float p1, float p2, float t)
		{
			return bezier_20(t) * p0 + bezier_21(t) * p1 + bezier_22(t) * p2;
		}

		public static float Derivative(float p0, float p1, float p2, float t)
		{
			return bezier_d20(t) * p0 + bezier_d21(t) * p1 + bezier_d22(t) * p2;
		}

		public override int size { get { return Size; } }

		public override float integral(float t, params float[] p)
		{
			return Integral(p[0], p[1], p[2], t);
		}

		public override float value(float t, params float[] p)
		{
			return Value(p[0], p[1], p[2], t);
		}

		public override float derivative(float t, params float[] p)
		{
			return Derivative(p[0], p[1], p[2], t);
		}
	}

	public class CubicBezierSpline : Interpolator
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

		public static float Integral(float p0, float p1, float p2, float p3, float t)
		{
			return bezier_p30(t) * p0 + bezier_p31(t) * p1 + bezier_p32(t) * p2 + bezier_p33(t) * p3;
		}

		public static float Value(float p0, float p1, float p2, float p3, float t)
		{
			return bezier_30(t) * p0 + bezier_31(t) * p1 + bezier_32(t) * p2 + bezier_33(t) * p3;
		}

		public static float Derivative(float p0, float p1, float p2, float p3, float t)
		{
			return bezier_d30(t) * p0 + bezier_d31(t) * p1 + bezier_d32(t) * p2 + bezier_d33(t) * p3;
		}

		public override int size { get { return Size; } }

		public override float integral(float t, params float[] p)
		{
			return Integral(p[0], p[1], p[2], p[3], t);
		}

		public override float value(float t, params float[] p)
		{
			return Value(p[0], p[1], p[2], p[3], t);
		}

		public override float derivative(float t, params float[] p)
		{
			return Derivative(p[0], p[1], p[2], p[3], t);
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

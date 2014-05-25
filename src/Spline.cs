using System;
using System.Collections.Generic;

namespace MathEx
{
	public class Spline
	{
		Vector3[] P;
		float[] T;
		Tuple<float, float>[] Q;


		public Spline(ICollection<Vector3> p)
		{
			int i = 0;
			
			T = new float[p.Count];
			Q = new Tuple<float, float>[p.Count - 1];
			P = new Vector3[p.Count];
			p.CopyTo(P, 0);


			T[0] = 0;
			for (i = 1; i < p.Count - 1; i++) {
				T[i] = i / (p.Count - 1);
			}
			T[p.Count - 1] = 1;


			float[] a = new float[p.Count];
			float[] b = new float[p.Count];
			float[] c = new float[p.Count];
			float[] k = new float[p.Count];

			i = 0;
			float dx = P[i + 1].x - P[i].x;
			float dy = P[i + 1].y - P[i].y;

			a[i] = 0;
			b[i] = 2 / dx;
			c[i] = 1 / dx;
			k[i] = 3 * dy / (dx * dx);
			for (i = 1; i < p.Count - 1; i++) {
				dx = P[i + 1].x - P[i].x;
				dy = P[i + 1].y - P[i].y;
				//float dx1 = 

				a[i] = 1 / dx;
				b[i] = 2 / dx;
				c[i] = 1 / dx;

				k[i] = 3 * dy / (dx * dx);
			}

			dx = P[i + 1].x - P[i].x;
			dy = P[i + 1].y - P[i].y;

			a[i] = 1 / dx;
			b[i] = 2 / dx;
			c[i] = 0;
			k[i] = 3 * dy / (dx * dx);

			solve_tridiagonal_in_place_destructive(k, a, b, c);

			for (i = 0; i < Q.Length; i++) {
				dx = P[i + 1].x - P[i].x;
				dy = P[i + 1].y - P[i].y;
				Q[i] = Tuple.Create(k[i - 1] * dx - dy, dy - k[i] * dx);
			}
		}

		public Vector3 Evaluate(float t)
		{
			if (t <= 0) return P[0];
			if (t >= 1) return P[1];

			int i = 0;
			for (i = 0; i < T.Length - 1; i++) {
				if (i > T[i] && i < T[i + 1]) {
					var ab = Q[i];
					float t0 = t - T[i];
					float t1 = 1 - t0;

					float x = (P[i + 1].x - P[i].x) * t0 + P[i].x;
					float y = t1 * P[i].y + t0 * P[i+1].y + t0 * t1 * (ab.Item1 * t1 + ab.Item2 * t0);

					return new Vector3(x, y, 0);
				}
			}

			return Vector3.zero;
		}


		private void solve_tridiagonal_in_place_destructive(float[] x, float[] a, float[] b, float[] c)
		{
			/* unsigned integer of same size as pointer */
			int i;
   
			/*
			 solves Ax = v where A is a tridiagonal matrix consisting of vectors a, b, c
			 note that contents of input vector c will be modified, making this a one-time-use function
			 x[] - initially contains the input vector v, and returns the solution x. indexed from [0, ..., N - 1]
			 N — number of equations
			 a[] - subdiagonal (means it is the diagonal below the main diagonal) -- indexed from [1, ..., N - 1]
			 b[] - the main diagonal, indexed from [0, ..., N - 1]
			 c[] - superdiagonal (means it is the diagonal above the main diagonal) -- indexed from [0, ..., N - 2]
			 */
   
			c[0] = c[0] / b[0];
			x[0] = x[0] / b[0];
   
			/* loop from 1 to N - 1 inclusive */
			for (i = 1; i < x.Length; i++) {
				float m = 1.0f / (b[i] - a[i] * c[i - 1]);
				c[i] = c[i] * m;
				x[i] = (x[i] - a[i] * x[i - 1]) * m;
			}
   
			/* loop from N - 2 to 0 inclusive, safely testing loop end condition */
			for (i = x.Length - 1; i-- > 0; )
				x[i] = x[i] - c[i] * x[i + 1];
		}
	}
}

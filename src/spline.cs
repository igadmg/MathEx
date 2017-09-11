using System;
using System.Collections.Generic;

namespace MathEx
{
	public class spline_curve<I> where I : Interpolator, new()
	{
		static I interpolator = new I();

		public bool loop = false;
		public vec3[] p;


		// return length of spline curve in segments.
		public int length
		{
			get { return p.Length / (interpolator.size - 1); }
		}

		protected int calculateT(ref float t)
		{
			int i;

			if (t >= 1f)
			{
				t = 1f;
				i = p.Length - 4;
			}
			else
			{
				t = MathEx.Clamp(t, 0, 1) * length;
				i = (int)t;
				t -= i;
				i *= 3;
			}

			return i;
		}

		public vec3 Evaluate(float t)
		{
			int i = calculateT(ref t);

			return bezier.Evaluate(p, i, t);
		}

		public vec3 Derivative(float t)
		{
			int i = calculateT(ref t);

			return bezier.Derivative(p, i, t);
		}
	}

	public class spline
	{
		public vec3[] ps;
		float[] ts;
		Tuple<float, float>[] q;


		public spline(ICollection<vec3> p)
		{
			ts = new float[p.Count];
			q = new Tuple<float, float>[p.Count - 1];
			ps = new vec3[p.Count];
			p.CopyTo(ps, 0);

			init();
		}

		protected void init()
		{
			int i = 0;

			ts[0] = 0;
			for (i = 1; i < ps.Length - 1; i++) {
				ts[i] = i / (ps.Length - 1);
			}
			ts[ps.Length - 1] = 1;


			float[] a = new float[ps.Length];
			float[] b = new float[ps.Length];
			float[] c = new float[ps.Length];
			float[] k = new float[ps.Length];

			i = 0;
			float dx = ps[i + 1].x - ps[i].x;
			float dy = ps[i + 1].y - ps[i].y;

			a[i] = 0;
			b[i] = 2 / dx;
			c[i] = 1 / dx;
			k[i] = 3 * dy / (dx * dx);
			for (i = 1; i < ps.Length - 1; i++) {
				dx = ps[i + 1].x - ps[i].x;
				dy = ps[i + 1].y - ps[i].y;
				//float dx1 = 

				a[i] = 1 / dx;
				b[i] = 2 / dx;
				c[i] = 1 / dx;

				k[i] = 3 * dy / (dx * dx);
			}

			dx = ps[i + 1].x - ps[i].x;
			dy = ps[i + 1].y - ps[i].y;

			a[i] = 1 / dx;
			b[i] = 2 / dx;
			c[i] = 0;
			k[i] = 3 * dy / (dx * dx);

			solve_tridiagonal_in_place_destructive(k, a, b, c);

			for (i = 0; i < q.Length; i++) {
				dx = ps[i + 1].x - ps[i].x;
				dy = ps[i + 1].y - ps[i].y;
				q[i] = Tuple.Create(k[i - 1] * dx - dy, dy - k[i] * dx);
			}
		}

		public vec3 Evaluate(float t)
		{
			if (t <= 0) return ps[0];
			if (t >= 1) return ps[1];

			int i = 0;
			for (i = 0; i < ts.Length - 1; i++) {
				if (i > ts[i] && i < ts[i + 1]) {
					var ab = q[i];
					float t0 = t - ts[i];
					float t1 = 1 - t0;

					float x = (ps[i + 1].x - ps[i].x) * t0 + ps[i].x;
					float y = t1 * ps[i].y + t0 * ps[i + 1].y + t0 * t1 * (ab.Item1 * t1 + ab.Item2 * t0);

					return new vec3(x, y, 0);
				}
			}

			return vec3.zero;
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

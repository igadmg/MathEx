using System;
using System.Collections.Generic;
using SystemEx;

namespace MathEx
{
	[Serializable]
	public abstract class curve
	{
		public abstract Type type { get; }
		public abstract int numberOfNodes { get; }
		public abstract int chunkSize { get; }
		public abstract float length { get; }
	}

	public abstract class curve<T> : curve
	{
		[Serializable] public class distance_type : curve_distance<curve<T>, T> { public distance_type(curve<T> c) : base(c) { } }
		[Serializable] public class controller_type : curve_controller<curve<T>, T> { public controller_type(curve<T> c) : base(c) { } }

		public override Type type { get { return typeof(T); } }
		public distance_type distance { get { return new distance_type(this); } }
		public controller_type controller { get { return new controller_type(this); } }

		public abstract T value(float t);
		public abstract T velocity(float t);
		public abstract T value(int i, float t);
		public abstract T velocity(int i, float t);

		public abstract int insert(int nodeIndex);
		public abstract int remove(int nodeIndex);
	}

	public class spline_curve<T, I> : curve<T>
		where I : Interpolator<T>, new()
	{
		protected static MathTypeTag<T> mtt = MathTypeTag<T>.Get();
		protected static I interpolator = new I();

		public bool loop = false;
		public T[] p = null;


		public spline_curve()
		{
		}

		public spline_curve(int length)
		{
			p = new T[length * (interpolator.size - 1) + 1];
		}

		public spline_curve(int length, bool loop)
		{
			this.loop = loop;
			p = new T[length * (interpolator.size - 1) + 1];
		}

		// return length of spline curve in segments.
		public override int numberOfNodes
		{
			get { return 1 + p.Length / (interpolator.size - 1); }
		}

		public override int chunkSize
		{
			get { return interpolator.size; }
		}

		public override float length
		{
			get
			{
				float result = 0;
				for (int i = 0; i < numberOfNodes - 1; i++)
				{
					int i0 = i * (interpolator.size - 1);
					int i1 = (i + 1) * (interpolator.size - 1);

					T p0 = p[i0];
					T p1 = p[i1];

					float chunkLength = mtt.distance(p0, p1);
					for (int j = i0; j < i1 - 1; j++)
					{
						chunkLength += mtt.distance(p[j], p[j + 1]);
					}

					result += chunkLength / 2;
				}

				return result;
			}
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
				t = MathEx.Clamp(t, 0, 1) * (numberOfNodes - 1);
				i = (int)t;
				t -= i;
				i *= 3;
			}

			return i;
		}

		public override int insert(int nodeIndex)
		{
			int i = nodeIndex * interpolator.size - 1;
			List<T> pl = new List<T>(p);

			if (i <= 0)
			{
				pl.InsertRange(0, new T[interpolator.size - 1]);
			}
			else if (i >= p.Length)
			{
				pl.InsertRange(p.Length, new T[interpolator.size - 1]);
			}
			else
			{
				pl.InsertRange(i - (interpolator.size - 1) / 2, new T[interpolator.size - 1]);
			}

			p = pl.ToArray();

			return nodeIndex;
		}

		public override int remove(int nodeIndex)
		{
			int i = nodeIndex * interpolator.size - 1;
			List<T> pl = new List<T>(p);

			if (i < 0)
			{
				pl.RemoveRange(0, interpolator.size - 1);
			}
			else if (i >= p.Length)
			{
				pl.RemoveRange(p.Length - (interpolator.size - 1), interpolator.size - 1);
			}
			else
			{
				pl.RemoveRange(i - (interpolator.size - 1) / 2, interpolator.size - 1);
			}

			p = pl.ToArray();

			return nodeIndex;
		}

		public override T value(float t)
		{
			if (p == null)
				return mtt.zero();

			int i = calculateT(ref t);

			return interpolator.value(t, p, i);
		}

		public override T velocity(float t)
		{
			if (p == null)
				return mtt.zero();

			int i = calculateT(ref t);

			return interpolator.derivative(t, p, i);
		}

		public override T value(int i, float t)
		{
			if (p == null || i >= numberOfNodes - 1)
				return mtt.zero();

			return interpolator.value(t, p, i);
		}

		public override T velocity(int i, float t)
		{
			if (p == null || i >= numberOfNodes - 1)
				return mtt.zero();

			return interpolator.derivative(t, p, i);
		}
	}

	[Serializable]
	public class curve_distance<C, T>
		where C : curve<T>
	{
		protected static MathTypeTag<T> mtt = MathTypeTag<T>.Get();

		public C c;
		Tuple<float, float>[] d;

		public curve_distance(C c)
		{
			this.c = c;

			update();
		}

		public float length
		{
			get { return d[d.Length - 1].Item2; }
		}

		public void update()
		{
			List<Tuple<float, float>> dl = new List<Tuple<float, float>>();

			T prevValue = c.value(0);
			float prevDistance = 0;
			foreach (var i in c.Iterate())
			{
				float dd = mtt.distance(prevValue, i.value);
				dl.Add(Tuple.Create(i.t, prevDistance + dd));
				prevDistance += dd;
				prevValue = i.value;
			}

			d = dl.ToArray();
		}

		public float distance(float t)
		{
			int i = Array.BinarySearch(d, Tuple.Create(t, 0f)
				, LambdaComparer.Create((Tuple<float, float> a, Tuple<float, float> b) => Comparer<float>.Default.Compare(a.Item1, b.Item1)));

			if (i >= 0)
				return d[i].Item2;

			i = -i - 1;
			return t.InvLerp(d[i - 1].Item1, d[i].Item1).Lerp(d[i - 1].Item2, d[i].Item2);
		}

		public float time(float d)
		{
			int i = Array.BinarySearch(this.d, Tuple.Create(0f, d)
				, LambdaComparer.Create((Tuple<float, float> a, Tuple<float, float> b) => Comparer<float>.Default.Compare(a.Item2, b.Item2)));

			if (i >= 0)
				return this.d[i].Item1;

			i = -i - 1;
			return d.InvLerp(this.d[i - 1].Item2, this.d[i].Item2).Lerp(this.d[i - 1].Item1, this.d[i].Item1);
		}
	}

	[Serializable]
	public class curve_controller<C, T>
		where C : curve<T>
	{
		[Flags]
		public enum CurveMode
		{
			Auto,
			Broken,
			Curve,
			CurveSymmetric,
		}

		public C c;
		public CurveMode[] modes;

		public curve_controller(C c)
		{
			this.c = c;
			modes = new CurveMode[c.numberOfNodes];
		}

		public int insert(int nodeIndex)
		{
			nodeIndex = c.insert(nodeIndex);

			List<CurveMode> ml = new List<CurveMode>(modes);
			ml.Insert(nodeIndex, CurveMode.Auto);
			modes = ml.ToArray();

			return nodeIndex;
		}

		public int remove(int nodeIndex)
		{
			nodeIndex = c.remove(nodeIndex);

			List<CurveMode> ml = new List<CurveMode>(modes);
			ml.RemoveAt(nodeIndex);
			modes = ml.ToArray();

			return nodeIndex;
		}
	}

#if false
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
#endif
}

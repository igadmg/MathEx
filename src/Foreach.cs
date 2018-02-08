using System;
using SystemEx;
using System.Collections.Generic;



namespace MathEx
{
	public static class Foreach
	{
		public static IEnumerable<float> Fit(this float distance, int steps, float minDistance)
		{
			if (steps == 0)
			{
				yield break;
			}

			if (steps == 1)
			{
				yield return distance * 0.5f;
			}
			else
			{
				float d = minDistance.Min(distance / (steps - 1));
				float start = (distance - (d * (steps - 1))) * 0.5f;
				for (int i = 0; i < steps; i++)
				{
					yield return start + i * d;
				}
			}

			yield break;
		}

		public static IEnumerable<aabb2> Cell(vec2i s)
		{
			vec2 sp = -((vec2)s).Div(2.0f);
			for (int y = 0; y < s.y; y++)
				for (int x = 0; x < s.x; x++)
					yield return new aabb2(sp + new vec2(x, y), sp + new vec2(x + 1, y + 1));

			yield break;
		}

		public static IEnumerable<vec2> Cell(vec2 d, vec2i s)
		{
			vec2 sp = -d.Mul(s).Div(2.0f).Sub(d.Div(2));
			vec2 p = sp;
			for (int y = 0; y < s.y; y++, p = p.Add(0, d.y).X(sp.x))
				for (int x = 0; x < s.x; x++, p = p.Add(d.x, 0))
					yield return p;

			yield break;
		}

		public static IEnumerable<vec2> Cell(vec2 d, aabb2i s)
		{
			for (int y = s.a.y; y < s.b.y; y++)
				for (int x = s.a.x; x < s.b.x; x++)
					yield return d.Mul(x, y);

			yield break;
		}

		private static Func<vec2i, vec2i>[] octTransforms = new Func<vec2i,vec2i>[] {
			(a) => new vec2i( a.x, a.y),
			(a) => new vec2i( a.y, a.x),
			(a) => new vec2i(-a.y, a.x),
			(a) => new vec2i(-a.x, a.y),
			(a) => new vec2i(-a.x,-a.y),
			(a) => new vec2i(-a.y,-a.x),
			(a) => new vec2i( a.y,-a.x),
			(a) => new vec2i( a.x,-a.y),
		};

		public static IEnumerable<vec2i> Line(this vec2i a, vec2i b)
		{
			vec2i d = (b - a);

			if (d.isZero) {
				yield break;
			}
			if (d.x == 0) {
				int s = d.y.Sign();
				for (int i = a.y; i != b.y; i += s)
					yield return new vec2i(a.x, i);
				yield break;
			}
			if (d.y == 0) {
				int s = d.x.Sign();
				for (int i = a.x; i != b.x; i += s)
					yield return new vec2i(i, a.y);
				yield break;
			}
			if (d.x.meq(d.y)) {
				int sx = d.x.Sign();
				int sy = d.y.Sign();
				for (int xi = a.x, yi = a.y; xi != b.x; xi += sx, yi += sy)
					yield return new vec2i(xi, yi);
				yield break;
			}

			var ot = octTransforms[d.oct];
			b = ot(b);
			d = ot(d);

			int D = 2*d.y - d.x;
			int y = a.y;			

			yield return a;
			for (int x = a.x + 1; x < b.x; x++) {
				if (D > 0) {
					y++;
					D += 2*d.y - 2*d.x;
				}
				else
					D += 2*d.y;
				yield return ot(new vec2i(x, y));
			}
			
			yield break;
		}

		public static IEnumerable<vec3i> Line(this vec3i a, vec3i b)
		{
			vec3i d = (b - a);

			if (d.isZero) {
				yield break;
			}
			if (d.x == 0) {
				foreach (var p in a.yz().Line(b.yz())) {
					yield return p.zxy(a.x);
				}
				yield break;
			}
			if (d.y == 0) {
				foreach (var p in a.zx().Line(b.zx())) {
					yield return p.yzx(a.y);
				}
				yield break;
			}
			if (d.z == 0) {
				foreach (var p in a.xy().Line(b.xy())) {
					yield return p.xyz(a.z);
				}
				yield break;
			}
			if (d.x.meq(d.y) && d.x.meq(d.z)) {
				int sx = d.x.Sign();
				int sy = d.y.Sign();
				int sz = d.z.Sign();
				for (int xi = a.x, yi = a.y, zi = a.z; xi != b.x; xi += sx, yi += sy, zi += sz)
					yield return new vec3i(xi, yi, zi);
				yield break;
			}

			/*
			var ot = octTransforms[d.oct];
			int D = 2 * d.y - d.x;
			int y = a.y;
			b = ot(b);

			yield return a;
			for (int x = a.x + 1; x < b.x; x++) {
				if (D > 0) {
					y++;
					D += 2 * d.y - 2 * d.x;
				}
				else
					D += 2 * d.y;
				yield return ot(new vec2i(x, y));
			}
			 */

			yield break;
		}

		public struct CurveIterator<T>
		{
			public float t;
			public T value;
			public T velocity;
		}

		public static IEnumerable<CurveIterator<vec2>> Iterate(this circle c, int sectors)
		{
			if (c.isEmpty)
				yield break;

			float dA0 = 0;
			float dA = 2f * MathEx.PI / sectors;
			for (int i = 0; i <= sectors; i++) {
				Foreach.CurveIterator<vec2> ci = new Foreach.CurveIterator<vec2>();
				float a = dA0 + i * dA;
				ci.t = ((float)i) / sectors;
				ci.value = c.o + c.r * (new vec2(MathEx.Cos(a), MathEx.Sin(a)));
				yield return ci;
			}
			yield break;
		}

		public static IEnumerable<CurveIterator<T>> Iterate<T>(this curve<T> c)
		{
			return c.Iterate(1f);
		}

		public static IEnumerable<CurveIterator<T>> Iterate<T>(this curve<T> c, float stepMultiplier)
		{
			float sl = c.length;
			float islsl = 1 / (sl * sl);

			float t = 0;
			while (true)
			{
				CurveIterator<T> i;
				i.t = t;
				i.value = c.value(t);
				i.velocity = c.velocity(t);
				yield return i;

				if (t == 1)
					yield break;

				float dt = MathEx.Clamp(MathTypeTag<T>.Get().scalar(i.velocity) * islsl * stepMultiplier, islsl, 1);
				t = MathEx.Clamp01(t + dt);
			}
		}

		public static IEnumerable<CurveIterator<T>> IterateEquidistant<T>(this curve<T> c, float fraction)
		{
			var d = c.distance;
			float length = d.length;
			float delta = d.length * fraction;

			float cd = 0;
			while (true)
			{
				CurveIterator<T> i;
				i.t = d.time(cd);
				i.value = c.value(i.t);
				i.velocity = c.velocity(i.t);
				yield return i;

				if (cd == length)
					yield break;

				cd = MathEx.Clamp(cd + delta, 0, length);
			}
		}
	}
}

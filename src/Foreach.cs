using System;
using SystemEx;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public static class Foreach
	{
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
	}
}

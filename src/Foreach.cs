using System;
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
	}
}

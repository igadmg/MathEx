using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public static class Foreach
	{
		public static IEnumerable<vec2> Cell(vec2 d, vec2i s)
		{
			vec2 sp = -d.Mul(s).Div(2.0f).Sub(d.Div(2));
			vec2 p = sp;
			for (int y = 0; y < s.y; y++, p = p.Add(0, d.y).X(sp.x))
				for (int x = 0; x < s.x; x++, p = p.Add(d.x, 0))
					yield return p;

			yield break;
		}

		public static IEnumerable<vec2> Cell(vec2 d, aabb2 s)
		{
			yield break;
		}
	}
}

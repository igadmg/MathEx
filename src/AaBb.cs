using System;
using System.Collections.Generic;

namespace MathEx
{
	public class aabb2
	{
		public static readonly aabb2 empty = new aabb2(vec2.empty, vec2.empty);
		public static readonly aabb2 zero = new aabb2(vec2.zero, vec2.zero);
		public static readonly aabb2 one = new aabb2(vec2.zero, vec2.one);

		public vec2 a;
		public vec2 b;

		public vec2 size { get { return b - a; } }
		
		public aabb2(vec2 a, vec2 b)
		{
			this.a = a;
			this.b = b;
		}

		public bool IsEmpty { get { return a.IsEmpty || b.IsEmpty; } }

		public aabb2 Extend(vec2 p)
		{
			if (IsEmpty)
				return new aabb2(p, p);

			var min = vec2.Min(a, p);
			var max = vec2.Max(b, p);

			return new aabb2(min, max);
		}

		public vec2[] ToArray()
		{
			return new vec2[4] {
				a, new vec2(a.x, b.y),
				b, new vec2(b.x, a.y)
			};
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})", a, b);
		}

		public string ToString(string f)
		{
			return string.Format("({0}, {1})", a.ToString(f), b.ToString(f));
		}
	}

	public class AaBb3
	{
		public vec3 a;
		public vec3 b;
	}
}

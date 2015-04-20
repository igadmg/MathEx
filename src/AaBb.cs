using System;
using System.Collections.Generic;

namespace MathEx
{
	public struct aabb2
	{
		public static readonly aabb2 empty = new aabb2(vec2.empty, vec2.empty);
		public static readonly aabb2 zero = new aabb2(vec2.zero, vec2.zero);
		public static readonly aabb2 one = new aabb2(vec2.zero, vec2.one);

		public vec2 a;
		public vec2 b;

        public float x { get { return b.x - a.x; } }
        public float y { get { return b.y - a.y; } }
		public vec2 size { get { return b - a; } }
		public vec2[] vertices { get { return new vec2[] { a, new vec2(a.x, b.y), b, new vec2(b.x, a.y) }; } }
		
		public aabb2(vec2 a, vec2 b)
		{
			this.a = a;
			this.b = b;
		}

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }


		//
		// Operators
		//
		public static aabb2 operator +(aabb2 a, vec2 v) { return new aabb2(a.a + v, a.b + v); }
		public static aabb2 operator -(aabb2 a, vec2 v) { return new aabb2(a.a - v, a.b - v); }
        public static aabb2 operator *(aabb2 a, vec2 v) { return new aabb2(a.a.Mul(v), a.b.Mul(v)); }


		public aabb2 Extend(vec2 p)
		{
			if (isEmpty)
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

	public struct aabb3
	{
		public vec3 a;
		public vec3 b;

		public float x { get { return b.x - a.x; } }
		public float y { get { return b.y - a.y; } }
		public float z { get { return b.z - a.z; } }
		public vec3 size { get { return b - a; } }

		public aabb3(vec3 a, vec3 b)
		{
			this.a = a;
			this.b = b;
		}

		public aabb3(vec3 r)
		{
			this.a = -r;
			this.b = r;
		}

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }


		//
		// Operators
		//
		public static aabb3 operator +(aabb3 a, vec3 v) { return new aabb3(a.a + v, a.b + v); }
		public static aabb3 operator -(aabb3 a, vec3 v) { return new aabb3(a.a - v, a.b - v); }
		public static aabb3 operator *(aabb3 a, vec3 v) { return new aabb3(a.a.Mul(v), a.b.Mul(v)); }
	}
}

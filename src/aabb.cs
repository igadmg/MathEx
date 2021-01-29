using System;
using System.Runtime.InteropServices;

namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct aabb2
	{
		public static readonly aabb2 empty = new aabb2(vec2.empty, vec2.empty);
		public static readonly aabb2 zero = new aabb2(vec2.zero, vec2.zero);
		public static readonly aabb2 one = new aabb2(vec2.zero, vec2.one);

		public vec2 a;
		public vec2 b;

		public vec2 size {
			get => b - a;
			set => b = a + value;
		}


		public float x => a.x;
		public float y => a.y;
		public float width => size.x;
		public float height => size.y;
		public vec2 o => (a + b) / 2;
		public vec2[] vertices => new vec2[] { a, new vec2(a.x, a.y + size.y), b, new vec2(a.x + size.x, a.y) };


		public aabb2(vec2 a, vec2 b)
		{
			this.a = a;
			this.b = b;
		}

		public static aabb2 xywh(float x, float y, float w, float h)
			=> new aabb2(new vec2(x, y), new vec2(x + w, y + h));
		public static aabb2 xywh(vec2 a, vec2 s)
			=> new aabb2(a, a + s);

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }


		//
		// Operators
		//
		public static aabb2 operator +(aabb2 a, vec2 v) { return new aabb2(a.a + v, a.b + v); }
		public static aabb2 operator -(aabb2 a, vec2 v) { return new aabb2(a.a - v, a.b - v); }
		public static aabb2 operator *(aabb2 a, vec2 v) { return new aabb2(a.a.Mul(v), a.b.Mul(v)); }


		public int Position(vec2 v)
		{
			int res = 0;

			if (v.x < a.x)
				res |= 0x01;
			else if (v.x > b.x)
				res |= 0x02;

			if (v.y < a.y)
				res |= 0x04;
			else if (v.y > b.y)
				res |= 0x08;

			return res;
		}

		public aabb2 Extend(vec2 p)
		{
			if (isEmpty)
				return new aabb2(p, p);

			var min = vec2.Min(a, p);
			var max = vec2.Max(b, p);

			return new aabb2(min, max);
		}

		public vec2[] ToArray() => vertices;

		public override string ToString()
		{
			return string.Format("({0}, {1})", a, b);
		}

		public string ToString(string f)
		{
			return string.Format("({0}, {1})", a.ToString(f), b.ToString(f));
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
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
			a = -r;
			b = r;
		}

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }


		//
		// Operators
		//
		public static aabb3 operator +(aabb3 a, vec3 v) { return new aabb3(a.a + v, a.b + v); }
		public static aabb3 operator -(aabb3 a, vec3 v) { return new aabb3(a.a - v, a.b - v); }
		public static aabb3 operator *(aabb3 a, vec3 v) { return new aabb3(a.a.Mul(v), a.b.Mul(v)); }


		public int Position(vec3 v)
		{
			int res = 0;

			if (v.x < a.x)
				res |= 0x01;
			else if (v.x > b.x)
				res |= 0x02;

			if (v.y < a.y)
				res |= 0x04;
			else if (v.y > b.y)
				res |= 0x08;

			if (v.z < a.z)
				res |= 0x10;
			else if (v.z > b.z)
				res |= 0x12;

			return res;
		}
	}
}

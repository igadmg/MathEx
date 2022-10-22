using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SystemEx;

namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class aabb2i
	{
		public static readonly aabb2i empty = new aabb2i(vec2i.empty, vec2i.empty);
		public static readonly aabb2i zero = new aabb2i(vec2i.zero, vec2i.zero);
		public static readonly aabb2i one = new aabb2i(vec2i.zero, vec2i.one);

		public vec2i a;
		public vec2i b;

		public vec2i size {
			get => b - a;
			set => b = a + value;
		}

		public int x { get { return b.x - a.x; } }
		public int y { get { return b.y - a.y; } }
		public int width => size.x;
		public int height => size.y;

		public static aabb2i ab(vec2i a, vec2i b) => new aabb2i(a, b);
		public static aabb2i xywh(vec2i xy, vec2i wh) => new aabb2i(xy, xy + wh);
		public static aabb2i wh(vec2i wh) => xywh(vec2i.zero, wh);

		public aabb2i(vec2i a, vec2i b)
		{
			this.a = a;
			this.b = b;
		}

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }


		//
		// Operators
		//
		public static aabb2i operator +(aabb2i a, vec2i v) { return new aabb2i(a.a + v, a.b + v); }
		public static aabb2i operator -(aabb2i a, vec2i v) { return new aabb2i(a.a - v, a.b - v); }
		public static aabb2i operator *(aabb2i a, vec2i v) { return new aabb2i(a.a * v, a.b * v); }
		public static aabb2i operator *(aabb2i a, int v) => xywh(a.a * v, a.size * v);
		public static aabb2i operator *(aabb2i a, float v) => xywh((vec2i)(a.a * v), (vec2i)(a.size * v));


		public static bool operator <=(vec2i p, aabb2i a)
			=> (p.x >= a.a.x && p.y >= a.a.y)
			&& (p.x <= a.b.x && p.y <= a.b.y);
		public static bool operator >=(vec2i p, aabb2i a)
			=> (p.x <= a.a.x || p.y <= a.a.y)
			|| (p.x >= a.b.x || p.y >= a.b.y);

		public bool Contain(vec2i p)
			=> !(p < a) && !(p > b);

		public vec2i Projection(vec2i v) => v - a;

		public aabb2i Extend(vec2i p)
		{
			if (isEmpty)
				return new aabb2i(p, p);

			var min = vec2i.Min(a, p);
			var max = vec2i.Max(b, p);

			return new aabb2i(min, max);
		}

		public aabb2i u(aabb2i v)
			=> ab(a.Clamp(v.a, v.b), b.Clamp(v.a, v.b));

		public vec2i[] ToArray()
		{
			return new vec2i[4] {
				a, new vec2i(a.x, b.y),
				b, new vec2i(b.x, a.y)
			};
		}

		public static implicit operator aabb2i(ValueTuple<vec2i, vec2i> v) => aabb2i.ab(v.Item1, v.Item2);
		public void Deconstruct(out vec2i a, out vec2i b)
		{
			a = this.a; b = this.b;
		}

		public override string ToString() => "{0}, {1}".format(CultureInfo.InvariantCulture, a, b);
		public string ToString(string f) => "{0}, {1}".format(CultureInfo.InvariantCulture, a.ToString(f), b.ToString(f));
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class aabb3i
	{
		public vec3i a;
		public vec3i b;

		public int x { get { return b.x - a.x; } }
		public int y { get { return b.y - a.y; } }
		public int z { get { return b.z - a.z; } }
		public vec3i size { get { return b - a; } }

		public aabb3i(vec3i a, vec3i b)
		{
			this.a = a;
			this.b = b;
		}

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }
	}
}

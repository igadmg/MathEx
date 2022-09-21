using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SystemEx;

namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct aabb2t<T>
		where T : IFloatingPoint<T>
	{
		public static readonly aabb2t<T> empty = ab(vec2t<T>.empty, vec2t<T>.empty);
		public static readonly aabb2t<T> zero = ab(vec2t<T>.zero, vec2t<T>.zero);
		public static readonly aabb2t<T> one = ab(vec2t<T>.zero, vec2t<T>.one);

		public vec2t<T> a;
		public vec2t<T> b;

		public vec2t<T> size {
			get => b - a;
			set => b = a + value;
		}


		public T x => a.x;
		public T y => a.y;
		public T width => size.x;
		public T height => size.y;
		public vec2t<T> o => (a + b) / (T.One + T.One);
		public vec2t<T>[] vertices => new vec2t<T>[] { a, new vec2t<T>(a.x, a.y + size.y), b, new vec2t<T>(a.x + size.x, a.y) };


		public aabb2t(vec2t<T> a, vec2t<T> b)
		{
			this.a = a;
			this.b = b;
		}

		public static aabb2t<T> ab(vec2t<T> a, vec2t<T> b) => new aabb2t<T>(a, b);
		public static aabb2t<T> xywh(T x, T y, T w, T h)
			=> new aabb2t<T>(new vec2t<T>(x, y), new vec2t<T>(x + w, y + h));
		public static aabb2t<T> xywh(vec2t<T> a, vec2t<T> s)
			=> new aabb2t<T>(a, a + s);

		public bool isEmpty => a.isEmpty || b.isEmpty;


		//
		// Operators
		//
		public static aabb2t<T> operator +(aabb2t<T> a, vec2t<T> v) { return new aabb2t<T>(a.a + v, a.b + v); }
		public static aabb2t<T> operator -(aabb2t<T> a, vec2t<T> v) { return new aabb2t<T>(a.a - v, a.b - v); }
		public static aabb2t<T> operator *(aabb2t<T> a, vec2t<T> v) { return new aabb2t<T>(a.a * v, a.b * v); }


		public int Position(vec2t<T> v)
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

		public aabb2t<T> Extend(vec2t<T> p)
		{
			if (isEmpty)
				return new aabb2t<T>(p, p);

			var min = vec2t<T>.Min(a, p);
			var max = vec2t<T>.Max(b, p);

			return new aabb2t<T>(min, max);
		}

		public vec2t<T>[] ToArray() => vertices;

		public override string ToString() => "{0}, {1}".format(CultureInfo.InvariantCulture, a, b);
		public string ToString(string f) => "{0}, {1}".format(CultureInfo.InvariantCulture, a.ToString(f), b.ToString(f));
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

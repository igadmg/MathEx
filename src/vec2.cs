using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SystemEx;

namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct vec2t<T>
		where T : IFloatingPoint<T>
	{
		//
		// Fields
		//
		public T x;
		public T y;

		public struct Dto
		{
			public T x;
			public T y;
		}

		public vec2t(Dto dto)
		{
			x = dto.x;
			y = dto.y;
		}

		public Dto ToDto() => new Dto { x = x, y = y };

		//
		// Static Properties
		//
		public static readonly vec2t<T> zero = xy(T.Zero, T.Zero);
		public static readonly vec2t<T> empty = xy(T.NaN, T.NaN);
		public static readonly vec2t<T> one = xy(T.One, T.One);
		public static readonly vec2t<T> right = xy(T.One, T.Zero);
		public static readonly vec2t<T> left = xy(-T.One, T.Zero);
		public static readonly vec2t<T> up = xy(T.Zero, T.One);
		public static readonly vec2t<T> down = xy(T.Zero, -T.One);


		public bool isEmpty => T.IsNaN(x) || T.IsNaN(y);
		public bool isZero => x == T.Zero && y == T.Zero;

		public T magnitude { get { return x * x + y * y; } }
		public vec2t<T> normalized { get { return isZero ? this : this / length; } }
		public T length { get { return T.Sqrt(magnitude); } set { T l = value / length; x *= l; y *= l; } }

		public int quad {
			get {
				if (x > T.Zero)
					if (y > T.Zero)
						return 0;
					else
						return 1;
				else
					if (y < T.Zero)
					return 2;
				else
					return 3;
			}
		}

		public T middle => (x + y) / (T.One + T.One);

		//
		// Operators
		//
		public static bool operator ==(vec2t<T> a, vec2t<T> b) => a.x == b.x && a.y == b.y;
		public static bool operator !=(vec2t<T> a, vec2t<T> b) => a.x != b.x || a.y != b.y;
		public bool Equals(vec2t<T> obj) => obj == this;
		public override bool Equals(object obj) => obj is vec2t<T> v ? Equals(v) : false;
		public override int GetHashCode() => ObjectEx.GetHashCode(x.GetHashCode(), y.GetHashCode());


		public static vec2t<T> operator *(vec2t<T> a, T d) => xy(a.x * d, a.y * d);
		public static vec2t<T> operator /(vec2t<T> a, T d) => xy(a.x / d, a.y / d);
		public static vec2t<T> operator *(T d, vec2t<T> a) => xy(a.x * d, a.y * d);
		public static vec2t<T> operator /(T d, vec2t<T> a) => xy(a.x / d, a.y / d);
		public static vec2t<T> operator *(vec2t<T> a, vec2t<T> b) => xy(a.x * b.x, a.y * b.y);
		public static vec2t<T> operator /(vec2t<T> a, vec2t<T> b) => xy(a.x / b.x, a.y / b.y);


		public static T operator ^(vec2t<T> a, vec2t<T> b) => Dot(a, b);
		public static T operator %(vec2t<T> a, T t) { return a.x * (T.One - t) + a.y * t; }
		//public static vec2t<T> operator %(vec2t<T> a, vec2t<T> b) { return vec2t<T>.xy(b.x.Lerp(0, a.x), b.y.Lerp(0, a.y)); }

		public static bool operator <(vec2t<T> a, vec2t<T> b)
			=> a.y < b.y
			|| (a.x < b.x && !(a.y > b.x));

		public static bool operator >(vec2t<T> a, vec2t<T> b)
			=> a.y < b.y || a.x < b.x
			|| !(a.y > b.y);


		public static vec2t<T> operator -(vec2t<T> a) => xy(-a.x, -a.y);
		public static vec2t<T> operator +(vec2t<T> a, vec2t<T> b) => xy(a.x + b.x, a.y + b.y);
		public static vec2t<T> operator -(vec2t<T> a, vec2t<T> b) => xy(a.x - b.x, a.y - b.y);


		public vec2t<T> this[T a, T b] => xy(T.Clamp(x, a, b), T.Clamp(y, a, b));
		public vec2t<T> this[vec2t<T> ab] => xy(T.Clamp(x, ab.x, ab.y), T.Clamp(y, ab.x, ab.y));


		public static vec2t<T> xy(T xy) => new vec2t<T>(xy, xy);
		public static vec2t<T> xy(T x, T y) => new vec2t<T>(x, y);

		public vec2t(T x, T y)
		{
			this.x = x;
			this.y = y;
		}

		//public static implicit operator vec2t<T>(ValueTuple<float, float> v) => vec2t<T>.xy(v.Item1, v.Item2);
		//public static explicit operator vec2t<T>(vec2t<int> v) => vec2t<T>.xy(v.x, v.y);

		public static vec2t<T> Min(vec2t<T> a, vec2t<T> b)
			=> new vec2t<T>(T.Min(a.x, b.x), T.Min(a.y, b.y));

		public static vec2t<T> Max(vec2t<T> a, vec2t<T> b)
			=> new vec2t<T>(T.Max(a.x, b.x), T.Max(a.y, b.y));

		public static T Dot(vec2t<T> l, vec2t<T> r) => l.x * r.x + l.y * r.y;

		//public static float Angle(vec2t<T> a, vec2t<T> b)
		//{
		//	T cos = Dot(a, b);
		//	float sin = -vec3.Cross(a.xyz(0), b.xyz(0)).z;
		//
		//	return cos.Acos() * sin.Sign();
		//}


		public override string ToString() => "{0}, {1}".format(CultureInfo.InvariantCulture, x, y);
		public string ToString(string f) => "{0}, {1}".format(x.ToString(f, CultureInfo.InvariantCulture), y.ToString(f, CultureInfo.InvariantCulture));

		//public vec2t<T> Tovec2t<T>i() => new vec2t<T>i((int)x, (int)y);


#if UNITY || UNITY_5_3_OR_NEWER
		public static implicit operator UnityEngine.Vector2(vec2t<T> v)
		{
			return new UnityEngine.Vector2(v.x, v.y);
		}

		public static implicit operator vec2t<T>(UnityEngine.Vector2 v)
		{
			return new vec2t<T>(v.x, v.y);
		}

		public static vec2t<T> operator *(UnityEngine.Vector2 a, vec2t<T> b) => new vec2t<T>(a.x * b.x, a.y * b.y);
		public static vec2t<T> operator /(UnityEngine.Vector2 a, vec2t<T> b) => new vec2t<T>(a.x / b.x, a.y / b.y);
#endif
	}
}


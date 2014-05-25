using System;

namespace MathEx
{
	public struct Vector2
	{
		//
		// Fields
		//
		public float x;
		public float y;

		//
		// Static Properties
		//
		public static readonly Vector2 zero = new Vector2(0, 0);
		public static readonly Vector2 empty = new Vector2(float.NaN, float.NaN);
		public static readonly Vector2 one = new Vector2(1, 1);
		public static readonly Vector2 right = new Vector2(1, 0);
		public static readonly Vector2 up = new Vector2(0, 1);


		public bool IsEmpty { get { return float.IsNaN(x) || float.IsNaN(y); } }


		public int quad
		{
			get {
				if (x > 0)
					if (y > 0)
						return 0;
					else
						return 1;
				else
					if (y < 0)
						return 2;
					else
						return 3;
			}
		}

		//
		// Operators
		//

		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		public static Vector2 operator -(Vector2 a) { return new Vector2(-a.x, -a.y); }
		public static Vector2 operator +(Vector2 a, Vector2 b) { return new Vector2(a.x + b.x, a.y + b.y); }
		public static Vector2 operator -(Vector2 a, Vector2 b) { return new Vector2(a.x - b.x, a.y - b.y); }


		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public static Vector2 Min(Vector2 a, Vector2 b)
		{
			return new Vector2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		public static Vector2 Max(Vector2 a, Vector2 b)
		{
			return new Vector2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		internal object ToString(string f)
		{
			return string.Format("({0},{1})", x.ToString(f), y.ToString(f));
		}

		public int Clamp(int f)
		{
			return MathEx.Clamp(f, (int)x, (int)y);
		}

		public float Clamp(float f)
		{
			return MathEx.Clamp(f, x, y);
		}

		public Vector2 Clamp(Vector2 min, Vector2 max)
		{
			return MathEx.Clamp(this, min, max);
		}
	}

	public static class Vector2Ex
	{
		public static Vector2 Div(this Vector2 l, Vector2 r)
		{
			return new Vector2(l.x / r.x, l.y / r.y);
		}
	}
}


using System;

namespace MathEx
{
	public struct Vector2i
	{
		//
		// Fields
		//
		public int x;
		public int y;

		//
		// Static Properties
		//
		public static Vector2i zero     { get { return new Vector2i(0, 0); } }
		public static Vector2i one      { get { return new Vector2i(1, 1); } }
		public static Vector2i right    { get { return new Vector2i(1, 0); } }
		public static Vector2i up       { get { return new Vector2i(0, 1); } }


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

		public static Vector2i operator *(Vector2i a, int d)
		{
			return new Vector2i(a.x * d, a.y * d);
		}

		public Vector2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


		public int Clamp(int f)
		{
			return MathEx.Clamp(f, x, y);
		}

		public Vector2i Clamp(Vector2i min, Vector2i max)
		{
			return MathEx.Clamp(this, min, max);
		}
	}

	public static class Vector2iEx
	{
		public static Vector2 Div(this Vector2 l, Vector2i r)
		{
			return new Vector2(l.x / r.x, l.y / r.y);
		}
	}
}


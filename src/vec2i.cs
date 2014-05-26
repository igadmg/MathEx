using System;

namespace MathEx
{
	public struct vec2i
	{
		//
		// Fields
		//
		public int x;
		public int y;

		//
		// Static Properties
		//
		public static vec2i zero     { get { return new vec2i(0, 0); } }
		public static vec2i one      { get { return new vec2i(1, 1); } }
		public static vec2i right    { get { return new vec2i(1, 0); } }
		public static vec2i up       { get { return new vec2i(0, 1); } }


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

		public static vec2i operator *(vec2i a, int d) { return new vec2i(a.x * d, a.y * d); }
		public static vec2i operator /(vec2i a, int d) { return new vec2i(a.x / d, a.y / d); }


		public vec2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString() { return string.Format("({0},{1})", x, y); }
		public string ToString(string f) { return string.Format("({0},{1})", x.ToString(f), y.ToString(f)); }
	}
}


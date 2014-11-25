using System;
using System.Text.RegularExpressions;
using SystemEx;

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
		public static readonly vec2i zero = new vec2i(0, 0);
		public static readonly vec2i empty = new vec2i(int.MinValue, int.MinValue);
		public static readonly vec2i one = new vec2i(1, 1);
		public static readonly vec2i right = new vec2i(1, 0);
		public static readonly vec2i left = new vec2i(-1, 0);
		public static readonly vec2i up = new vec2i(0, 1);
		public static readonly vec2i down = new vec2i(0, -1);


		public bool isEmpty { get { return x == int.MinValue && y == int.MinValue; } }
		public bool isZero { get { return x == 0 && y == 0; } }

		public int product { get { return x * y; } }
		public float length { get { return MathEx.Sqrt(magnitude); } }
		public float magnitude { get { return x * x + y * y; } }
		//public vec2 normalized { get { return this / length; } }

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
		public static bool operator ==(vec2i a, vec2i b) { return a.x == b.x && a.y == b.y; }
		public static bool operator !=(vec2i a, vec2i b) { return a.x != b.x && a.y != b.y; }


		public static vec2i operator *(vec2i a, int d) { return new vec2i(a.x * d, a.y * d); }
		public static vec2i operator /(vec2i a, int d) { return new vec2i(a.x / d, a.y / d); }
		public static vec2i operator *(int d, vec2i a) { return new vec2i(a.x * d, a.y * d); }
		public static vec2i operator /(int d, vec2i a) { return new vec2i(a.x / d, a.y / d); }
		public static vec2i operator *(vec2i a, vec2i b) { return new vec2i(a.x * b.x, a.y * b.y); }
		public static vec2i operator /(vec2i a, vec2i b) { return new vec2i(a.x / b.x, a.y / b.y); }

		public static vec2i operator -(vec2i a) { return new vec2i(-a.x, -a.y); }
		public static vec2i operator +(vec2i a, vec2i b) { return new vec2i(a.x + b.x, a.y + b.y); }
		public static vec2i operator -(vec2i a, vec2i b) { return new vec2i(a.x - b.x, a.y - b.y); }


		public vec2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static vec2i Min(vec2i a, vec2i b)
		{
			return new vec2i(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		public static vec2i Max(vec2i a, vec2i b)
		{
			return new vec2i(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		public override string ToString() { return "({0},{1})".format(x, y); }
		public string ToString(string f) { return "({0},{1})".format(x.ToString(f), y.ToString(f)); }

		public static vec2i Parse(string s)
		{
			var match = new Regex(@"\((\d+),(\d+)\)", RegexOptions.IgnoreCase).Match(s);
			if (match.Success) {
				return new vec2i(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
			}
			return vec2i.empty;
		}
		public static vec2i Parse(string s, out int length)
		{
			var match = new Regex(@"\((\d+),(\d+)\)", RegexOptions.IgnoreCase).Match(s);
			if (match.Success) {
				length = match.Length;
				return new vec2i(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
			}

			length = 0;
			return vec2i.empty;
		}
	}
}


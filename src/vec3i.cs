using System;
using System.Text.RegularExpressions;
using SystemEx;

namespace MathEx
{
	[Serializable]
	public struct vec3i
	{
		//
		// Fields
		//
		public int x;
		public int y;
		public int z;

		//
		// Static Properties
		//
		public static readonly vec3i zero = new vec3i(0, 0, 0);
		public static readonly vec3i empty = new vec3i(int.MinValue, int.MinValue, int.MinValue);
		public static readonly vec3i one = new vec3i(1, 1, 1);
		public static readonly vec3i right = new vec3i(1, 0, 0);
		public static readonly vec3i left = new vec3i(-1, 0, 0);
		public static readonly vec3i up = new vec3i(0, 1, 0);
		public static readonly vec3i down = new vec3i(0, -1, 0);


		public bool isEmpty { get { return x == int.MinValue && y == int.MinValue && z == int.MinValue; } }
		public bool isZero { get { return x == 0 && y == 0 && z == 0; } }

		public int product { get { return x * y * z; } }
		public float length { get { return MathEx.Sqrt(magnitude); } }
		public float magnitude { get { return x * x + y * y + z * z; } }
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
		public static bool operator ==(vec3i a, vec3i b) { return a.x == b.x && a.y == b.y && a.z == b.z; }
		public static bool operator !=(vec3i a, vec3i b) { return a.x != b.x && a.y != b.y && a.z != b.z; }
		public bool Equals(vec3i obj) { return obj == this; }
		public override bool Equals(object obj) { return obj is vec3i ? Equals((vec3i)obj) : false; }
		public override int GetHashCode() { return x.GetHashCode() ^ y.GetHashCode(); }


		public static vec3i operator *(vec3i a, int d) { return new vec3i(a.x * d, a.y * d, a.z * d); }
		public static vec3i operator /(vec3i a, int d) { return new vec3i(a.x / d, a.y / d, a.z / d); }
		public static vec3i operator *(int d, vec3i a) { return new vec3i(a.x * d, a.y * d, a.z * d); }
		public static vec3i operator /(int d, vec3i a) { return new vec3i(a.x / d, a.y / d, a.z / d); }
		public static vec3i operator *(vec3i a, vec3i b) { return new vec3i(a.x * b.x, a.y * b.y, a.z * b.z); }
		public static vec3i operator /(vec3i a, vec3i b) { return new vec3i(a.x / b.x, a.y / b.y, a.z / b.z); }

		public static vec3i operator -(vec3i a) { return new vec3i(-a.x, -a.y, -a.z); }
		public static vec3i operator +(vec3i a, vec3i b) { return new vec3i(a.x + b.x, a.y + b.y, a.z + b.z); }
		public static vec3i operator -(vec3i a, vec3i b) { return new vec3i(a.x - b.x, a.y - b.y, a.z - b.z); }


		public vec3i(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static vec3i Min(vec3i a, vec3i b)
		{
			return new vec3i(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
		}

		public static vec3i Max(vec3i a, vec3i b)
		{
			return new vec3i(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Min(a.z, b.z));
		}

		public override string ToString() { return "({0},{1},{2})".format(x, y, z); }
		public string ToString(string f) { return "({0},{1},{2})".format(x.ToString(f), y.ToString(f), z.ToString(f)); }

		public static vec3i Parse(string s)
		{
			var match = new Regex(@"\((\d+),(\d+)\)", RegexOptions.IgnoreCase).Match(s);
			if (match.Success) {
				return new vec3i(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
			}
			return vec3i.empty;
		}
		public static vec3i Parse(string s, out int length)
		{
			var match = new Regex(@"\((\d+),(\d+)\)", RegexOptions.IgnoreCase).Match(s);
			if (match.Success) {
				length = match.Length;
				return new vec3i(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
			}

			length = 0;
			return vec3i.empty;
		}
	}
}


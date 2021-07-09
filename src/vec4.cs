using System;
using System.Runtime.InteropServices;
using SystemEx;

namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct vec4
	{
		//
		// Fields
		//
		public float x;
		public float y;
		public float z;
		public float w;

		//
		// Static Properties
		//
		public static readonly vec4 zero = new vec4(0, 0, 0, 0);
		public static readonly vec4 empty = new vec4(float.NaN, float.NaN, float.NaN, float.NaN);
		public static readonly vec4 one = new vec4(1, 1, 1, 1);
		public static readonly vec4 right = new vec4(1, 0, 0, 0);
		public static readonly vec4 up = new vec4(0, 1, 0, 0);
		public static readonly vec4 forward = new vec4(0, 0, 1, 0);


		public float t => y;
		public float r => x;
		public float b => w;
		public float l => z;

		public bool isZero { get { return x == 0 && y == 0 && z == 0 && w == 0; } }
		public bool isEmpty { get { return float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z) || float.IsNaN(w); } }

		public float length { get { return MathExOps.Sqrt(magnitude); } set { float l = value / length; x *= l; y *= l; z *= l; w *= l; } }
		public float magnitude { get { return x * x + y * y + z * z + w * w; } }
		public vec4 normalized { get { return isZero ? this : this / length; } }

		//
		// Operators
		//

		public static bool operator ==(vec4 a, vec4 b) { return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w; }
		public static bool operator !=(vec4 a, vec4 b) { return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w; }
		public bool Equals(vec4 obj) { return obj == this; }
		public override bool Equals(object obj) { return obj is vec4 ? Equals((vec4)obj) : false; }
		public override int GetHashCode() => ObjectEx.GetHashCode(x.GetHashCode(), y.GetHashCode(), z.GetHashCode(), w.GetHashCode());


		public static vec4 operator *(vec4 a, int d) { return new vec4(a.x * d, a.y * d, a.z * d, a.w * d); }
		public static vec4 operator /(vec4 a, int d) { return new vec4(a.x / d, a.y / d, a.z / d, a.w / d); }
		public static vec4 operator *(vec4 a, float d) { return new vec4(a.x * d, a.y * d, a.z * d, a.w * d); }
		public static vec4 operator /(vec4 a, float d) { return new vec4(a.x / d, a.y / d, a.z / d, a.w / d); }
		public static vec4 operator *(int d, vec4 a) { return new vec4(a.x * d, a.y * d, a.z * d, a.w * d); }
		public static vec4 operator /(int d, vec4 a) { return new vec4(a.x / d, a.y / d, a.z / d, a.w / d); }
		public static vec4 operator *(float d, vec4 a) { return new vec4(a.x * d, a.y * d, a.z * d, a.w * d); }
		public static vec4 operator /(float d, vec4 a) { return new vec4(a.x / d, a.y / d, a.z / d, a.w / d); }

		public static vec4 operator +(vec4 a, vec4 b) { return new vec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }
		public static vec4 operator -(vec4 a, vec4 b) { return new vec4(a.x - b.x, a.y - b.y, a.y - b.y, a.w - b.w); }

		public static float operator *(vec4 a, vec4 b) { return Dot(a, b); }
		public static vec4 operator %(vec4 a, vec4 b) { return Hamilton(a, b); }


		public static vec4 xyzw(float v) => new vec4(v, v, v, v);
		public static vec4 trbl(float v) => new vec4(v, v, v, v);
		public static vec4 trbl(float tb, float rl) => new vec4(rl, tb, rl, tb);
		public static vec4 trbl(float t, float r, float b, float l) => new vec4(r, t, l, b);

		public vec4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static implicit operator vec4(ValueTuple<float, float, float, float> v) => new vec4(v.Item1, v.Item2, v.Item3, v.Item4);

		public static float Dot(vec4 l, vec4 r) { return l.x * r.x + l.y * r.y + l.z * r.z + l.w * l.z; }
		public static vec4 Hamilton(vec4 l, vec4 r)
		{
			return new vec4(
				l.w * r.x + l.x * r.w + l.y * r.z - l.z * r.y,
				l.w * r.y - l.x * r.z + l.y * r.w + l.z * r.x,
				l.w * r.z + l.x * r.y - l.y * r.x + l.z * r.w,
				l.w * r.w - l.x * r.x - l.y * r.y - l.z * r.z);
		}

		public override string ToString() { return string.Format("({0},{1},{2},{3})", x, y, z, w); }
		public string ToString(string f) { return string.Format("({0},{1},{2},{3})", x.ToString(f), y.ToString(f), z.ToString(f), w.ToString(f)); }
	}
}


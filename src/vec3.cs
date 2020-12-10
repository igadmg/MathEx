using System;

namespace MathEx
{
	[Serializable]
	public struct vec3 : IComparable<vec3>, IFormattable
	{
		//
		// Fields
		//
		public float x;
		public float y;
		public float z;

		//
		// Static Properties
		//
		public static readonly vec3 zero = new vec3(0, 0, 0);
		public static readonly vec3 empty = new vec3(float.NaN, float.NaN, float.NaN);
		public static readonly vec3 one = new vec3(1, 1, 1);
		public static readonly vec3 right = new vec3(1, 0, 0);
		public static readonly vec3 left = new vec3(-1, 0, 0);
		public static readonly vec3 up = new vec3(0, 1, 0);
		public static readonly vec3 down = new vec3(0, -1, 0);
		public static readonly vec3 forward = new vec3(0, 0, 1);
		public static readonly vec3 backward = new vec3(0, 0, -1);


		public bool isZero { get { return x == 0 && y == 0 && z == 0; } }
		public bool isEmpty { get { return float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z); } }

		public float length { get { return MathExOps.Sqrt(magnitude); } set { float l = value / length; x *= l; y *= l; z *= l; } }
		public float magnitude { get { return x * x + y * y + z * z; } }
		public vec3 normalized { get { return isZero ? this : this / length; } }

		public vec4 toPoint { get { return new vec4(x, y, z, 1); } }
		public vec4 toVector { get { return new vec4(x, y, z, 0); } }

		//
		// Operators
		//


		public bool Equals(vec3 other, float eps) => MathTypeTagFloat.eq(x, other.x, eps) && MathTypeTagFloat.eq(y, other.y, eps) && MathTypeTagFloat.eq(z, other.z, eps);
		public static bool operator ==(vec3 a, vec3 b) { return a.x == b.x && a.y == b.y && a.z == b.z; }
		public static bool operator !=(vec3 a, vec3 b) { return a.x != b.x || a.y != b.y | a.z != b.z; }
		public bool Equals(vec3 obj) { return obj == this; }
		public override bool Equals(object obj) { return obj is vec3 ? Equals((vec3)obj) : false; }
		public override int GetHashCode() { return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode(); }
		public int CompareTo(vec3 other)
		{
			return x > other.x ? 1
				: x == other.x ? y > other.y ? 1
					: y == other.y ? z > other.z ? 1
						: z == other.z ? 0 : -1
					: -1
				: -1;
		}
		public int CompareTo(vec3 other, float eps)
		{
			return MathTypeTagFloat.gt(x, other.x, eps) ? 1
				: MathTypeTagFloat.eq(x, other.x, eps) ? MathTypeTagFloat.gt(y, other.y, eps) ? 1
					: MathTypeTagFloat.eq(y, other.y, eps) ? MathTypeTagFloat.gt(z, other.z, eps) ? 1
						: MathTypeTagFloat.eq(z, other.z, eps) ? 0 : -1
					: -1
				: -1;
		}

		public static vec3 operator *(vec3 a, int d) { return new vec3(a.x * d, a.y * d, a.z * d); }
		public static vec3 operator /(vec3 a, int d) { return new vec3(a.x / d, a.y / d, a.z / d); }
		public static vec3 operator *(vec3 a, float d) { return new vec3(a.x * d, a.y * d, a.z * d); }
		public static vec3 operator /(vec3 a, float d) { return new vec3(a.x / d, a.y / d, a.z / d); }
		public static vec3 operator *(int d, vec3 a) { return new vec3(a.x * d, a.y * d, a.z * d); }
		public static vec3 operator /(int d, vec3 a) { return new vec3(a.x / d, a.y / d, a.z / d); }
		public static vec3 operator *(float d, vec3 a) { return new vec3(a.x * d, a.y * d, a.z * d); }
		public static vec3 operator /(float d, vec3 a) { return new vec3(a.x / d, a.y / d, a.z / d); }

		public static vec3 operator -(vec3 a) { return new vec3(-a.x, -a.y, -a.z); }
		public static vec3 operator +(vec3 a, vec3 b) { return new vec3(a.x + b.x, a.y + b.y, a.z + b.z); }
		public static vec3 operator -(vec3 a, vec3 b) { return new vec3(a.x - b.x, a.y - b.y, a.z - b.z); }
		public static vec3 operator *(vec3 a, vec3i b) { return new vec3(a.x * b.x, a.y * b.y, a.z * b.z); }
		public static vec3 operator *(vec3i a, vec3 b) { return new vec3(a.x * b.x, a.y * b.y, a.z * b.z); }
		public static float operator *(vec3 a, vec3 b) { return Dot(a, b); }
		public static vec3 operator %(vec3 a, vec3 b) { return Cross(a, b); }

		public vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static float Dot(vec3 l, vec3 r) { return l.x * r.x + l.y * r.y + l.z * r.z; }
		public static vec3 Cross(vec3 l, vec3 r) { return new vec3(l.y * r.z - l.z * r.y, l.z * r.x - l.x * r.z, l.x * r.y - l.y * r.x); }

		public static float Angle(vec3 a, vec3 b)
		{
			vec3 c = Cross(a, b);
			float cos = Dot(a, b);
			float sin = Dot(c, c.s(1, -1, 1));

			return cos.Acos() * sin.Sign();
		}


		public override string ToString() { return string.Format("({0},{1},{2})", x, y, z); }
		public string ToString(string f) { return string.Format("({0},{1},{2})", x.ToString(f), y.ToString(f), z.ToString(f)); }
		public string ToString(string f, IFormatProvider p) { return string.Format("({0},{1},{2})", x.ToString(f, p), y.ToString(f, p), z.ToString(f, p)); }

#if UNITY || UNITY_5_3_OR_NEWER
		public static implicit operator UnityEngine.Vector3(vec3 v)
		{
			return new UnityEngine.Vector3(v.x, v.y, v.z);
		}

		public static implicit operator vec3(UnityEngine.Vector3 v)
		{
			return new vec3(v.x, v.y, v.z);
		}
#endif
	}
}


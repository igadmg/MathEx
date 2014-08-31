using System;

namespace MathEx
{
	public struct vec3
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
		public static readonly vec3 up = new vec3(0, 1, 0);
		public static readonly vec3 forward = new vec3(0, 0, 1);


		public bool IsEmpty { get { return float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z); } }

		public float length { get { return MathEx.Sqrt(magnitude); } }
		public float magnitude { get { return x*x + y*y + z*z; } }
		public vec3 normalized { get { return IsZero ? this : this / length; } }

		//
		// Operators
		//

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

		public vec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static vec3 Cross(vec3 l, vec3 r) { return new vec3(l.y * r.z - l.z * r.y, l.z * r.x - l.x * r.z, l.x * r.y - l.y * r.x); }


		public override string ToString() { return string.Format("({0},{1},{2})", x, y, z); }
		public string ToString(string f) { return string.Format("({0},{1},{2})", x.ToString(f), y.ToString(f), z.ToString(f)); }
	}                                                                 
}


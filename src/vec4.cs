using System;

namespace MathEx
{
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


		public bool IsEmpty { get { return float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z) || float.IsNaN(w); } }

		public float length { get { return MathEx.Sqrt(magnitude); } }
		public float magnitude { get { return x*x + y*y + z*z + w*w; } }
		public vec4 normalized { get { return IsZero ? this : this / length; } }

		//
		// Operators
		//

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

		public vec4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public override string ToString() { return string.Format("({0},{1},{2},{3})", x, y, z, w); }
		public string ToString(string f) { return string.Format("({0},{1},{2},{3})", x.ToString(f), y.ToString(f), z.ToString(f), w.ToString(f)); }
	}
}


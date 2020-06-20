using System;



namespace MathEx
{
	[Serializable]
	public struct quaternion
	{
		public float x;
		public float y;
		public float z;
		public float w;

		public static readonly quaternion identity = new quaternion(0, 0, 0, 1.0f);
		public static readonly quaternion empty = new quaternion(float.NaN, float.NaN, float.NaN, float.NaN);

		public bool isEmpty { get { return float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z) || float.IsNaN(w); } }
		public bool isZero { get { return x == 0 && y == 0 && z == 0 && w == 0; } }
		public bool isScalar { get { return x == 0 && y == 0 && z == 0; } }

		public float magnitude { get { return x * x + y * y + z * z + w * w; } }
		public float length { get { return magnitude.Sqrt(); } }
		public quaternion normalized { get { return isZero ? this : this / length; } }
		public quaternion conjugated { get { return new quaternion(-x, -y, -z, w); } }
		public quaternion inversed { get { return conjugated / magnitude; } }

		public quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public quaternion(vec3 axis, float angle)
		{
			float ha = angle / 2.0f;
			float sa = ha.Sin();

			x = axis.x * sa;
			y = axis.y * sa;
			z = axis.z * sa;
			w = ha.Cos();

			float l = length;
			x /= l;
			y /= l;
			z /= l;
			w /= l;
		}

		public static quaternion AngleAxis(float angle, vec3 axis)
		{
			return new quaternion(axis, MathExOps.Deg2Rad * angle);
		}

		public static quaternion operator *(quaternion a, float d) { return new quaternion(a.x * d, a.y * d, a.z * d, a.w * d); }
		public static quaternion operator /(quaternion a, float d) { return new quaternion(a.x / d, a.y / d, a.z / d, a.w / d); }

		public static vec4 operator ^(quaternion a, vec4 b) { return ((vec4)a) % b % ((vec4)a.inversed); }

		public static explicit operator vec4(quaternion q)
		{
			return new vec4(q.x, q.y, q.z, q.w);
		}

		public static explicit operator quaternion(vec4 v)
		{
			return new quaternion(v.x, v.y, v.z, v.w);
		}
	};
}

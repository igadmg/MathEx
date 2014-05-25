using System;

namespace MathEx
{
	public struct Vector3
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
		public static readonly Vector3 zero = new Vector3(0, 0, 0);
		public static readonly Vector3 empty = new Vector3(float.NaN, float.NaN, float.NaN);
		public static readonly Vector3 one = new Vector3(1, 1, 1);
		public static readonly Vector3 right = new Vector3(1, 0, 0);
		public static readonly Vector3 up = new Vector3(0, 1, 0);
		public static readonly Vector3 forward = new Vector3(0, 0, 1);


		public bool IsEmpty { get { return float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z); } }


		//
		// Operators
		//

		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); }
		public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3(a.x - b.x, a.y - b.y, a.y - b.y); }

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	public static class Vector3Ex
	{
		public static Vector3 Div(this Vector3 l, Vector3 r)
		{
			return new Vector3(l.x / r.x, l.y / r.y, l.z / r.z);
		}
	}
}


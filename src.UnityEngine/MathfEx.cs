using UnityEngine;

namespace MathEx
{
	public static class MathfEx
	{
		/// <summary>
		/// Repeat value in range [0..length] similar to mod operator. Works for negative values.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static int Repeat(int t, int length)
		{
			while (t < 0)
				t += length;

			return t % length;
		}

		/// <summary>
		/// Repeat value in range [0..length] similar to mod operator. Works for negative values.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static float Repeat(float t, float length)
		{
			while (t < 0)
				t += length;

			return Mathf.Repeat(t, length);
		}

		public static float Cbrt(float v)
		{
			if (v >= 0) {
				return Mathf.Pow(v, 1/3.0f);
			}
			else {
				return -Mathf.Pow(-v, 1/3.0f);
			}
		}

		public static Vector2 Clamp01(Vector2 value)
		{
			return new Vector2(
				Mathf.Clamp(value.x, 0, 1),
				Mathf.Clamp(value.y, 0, 1)
				);
		}

		public static Vector2 Clamp11(Vector2 value)
		{
			return new Vector2(
				Mathf.Clamp(value.x, -1, 1),
				Mathf.Clamp(value.y, -1, 1)
				);
		}

		public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
		{
			return new Vector2(
				Mathf.Clamp(value.x, min.x, max.x),
				Mathf.Clamp(value.y, min.y, max.y)
				);
		}

		public static Vector3 Clamp01(Vector3 value)
		{
			return new Vector3(
				Mathf.Clamp(value.x, 0, 1),
				Mathf.Clamp(value.y, 0, 1),
				Mathf.Clamp(value.z, 0, 1)
				);
		}

		public static Vector3 Clamp11(Vector3 value)
		{
			return new Vector3(
				Mathf.Clamp(value.x, -1, 1),
				Mathf.Clamp(value.y, -1, 1),
				Mathf.Clamp(value.z, -1, 1)
				);
		}

		public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
		{
			return new Vector3(
				Mathf.Clamp(value.x, min.x, max.x),
				Mathf.Clamp(value.y, min.y, max.y),
				Mathf.Clamp(value.z, min.z, max.z)
				);
		}

		public static int Round(float f)
		{
			return (int)(f + 0.5f);
		}

		public static Vector3 GetCenteredEulerAngles(this Vector3 v)
		{
			return new Vector3(
				v.x > 180 ? -(360 - v.x) : v.x,
				v.y > 180 ? -(360 - v.y) : v.y,
				v.z > 180 ? -(360 - v.z) : v.z
				);
		}

		public static Quaternion GetRotation(this Matrix4x4 m)
		{
			Quaternion q = new Quaternion();
			q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
			q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
			q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
			q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
			q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
			q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
			q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
			return q;
		}
	}
}

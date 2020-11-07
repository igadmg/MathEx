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

		public static bool SameSign(float a, float b)
		{
			return (Mathf.Sign(a) + Mathf.Sign(b)) != 0;
		}

		public static float Cbrt(this float v)
		{
			if (v >= 0)
			{
				return Mathf.Pow(v, 1 / 3.0f);
			}
			else
			{
				return -Mathf.Pow(-v, 1 / 3.0f);
			}
		}

		public static bool InRange(this float v, float min, float max)
		{
			return v >= min && v <= max;
		}

		public static bool InRange(this float v, Vector2 range)
		{
			return v >= range.x && v <= range.y;
		}

		public static float Lerp(this float t, Vector2 ab)
		{
			return Mathf.Lerp(ab.x, ab.y, t);
		}

		public static Rect Lerp(this float t, Rect a, Rect b)
		{
			return new Rect(t.Lerp(a.position, b.position), t.Lerp(a.size, b.size));
		}

		public static float InvLerp(this float v, Vector2 ab)
		{
			return (v - ab.x) / (ab.y - ab.x);
		}

		public static Vector2 Clamp01(this Vector2 value)
		{
			return new Vector2(
				Mathf.Clamp(value.x, 0, 1),
				Mathf.Clamp(value.y, 0, 1)
				);
		}

		public static Vector2 Clamp11(this Vector2 value)
		{
			return new Vector2(
				Mathf.Clamp(value.x, -1, 1),
				Mathf.Clamp(value.y, -1, 1)
				);
		}

		public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max)
		{
			return new Vector2(
				Mathf.Clamp(value.x, min.x, max.x),
				Mathf.Clamp(value.y, min.y, max.y)
				);
		}

		public static Vector3 Clamp01(this Vector3 value)
		{
			return new Vector3(
				Mathf.Clamp(value.x, 0, 1),
				Mathf.Clamp(value.y, 0, 1),
				Mathf.Clamp(value.z, 0, 1)
				);
		}

		public static Vector3 Clamp11(this Vector3 value)
		{
			return new Vector3(
				Mathf.Clamp(value.x, -1, 1),
				Mathf.Clamp(value.y, -1, 1),
				Mathf.Clamp(value.z, -1, 1)
				);
		}

		public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
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
	}
}

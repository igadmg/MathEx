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

		public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
		{
			return new Vector2(
				Mathf.Clamp(value.x, min.x, max.x),
				Mathf.Clamp(value.y, min.y, max.y)
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
	}
}

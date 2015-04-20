using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public static class MathEx
	{
		public static int Sign(this int v) { return v > 0 ? 1 : v < 0 ? -1 : 0; }

		public static float Abs(this float v) { return v > 0 ? v : -v; }
		public static float Sign(this float v) { return v > 0 ? 1.0f : v < 0 ? -1.0f : 0; }

		public static int Clamp(int f, int p1, int p2)
		{
			return (int)UnityEngine.Mathf.Clamp(f, p1, p2);
		}

		public static float Clamp(float f, float p1, float p2)
		{
			return UnityEngine.Mathf.Clamp(f, p1, p2);
		}

		public static vec2 Clamp(vec2 value, vec2 min, vec2 max)
		{
			return new vec2(
				Clamp(value.x, min.x, max.x),
				Clamp(value.y, min.y, max.y)
				);
		}

		public static vec2i Clamp(vec2i value, vec2i min, vec2i max)
		{
			return new vec2i(
				Clamp(value.x, min.x, max.x),
				Clamp(value.y, min.y, max.y)
				);
		}

		public static vec3 Clamp(vec3 value, vec3 min, vec3 max)
		{
			return new vec3(
				Clamp(value.x, min.x, max.x),
				Clamp(value.y, min.y, max.y),
				Clamp(value.z, min.z, max.z)
				);
		}

		public static float Lerp(this float t, float a, float b)
		{
			return a + (b - a) * t;
		}

		public static color_hsv Lerp(this float t, color_hsv a, color_hsv b)
		{
			return new color_hsv(t.Lerp(a.h, b.h), t.Lerp(a.s, b.s), t.Lerp(a.v, b.v), t.Lerp(a.a, b.a));
		}

		public static color_hsl Lerp(this float t, color_hsl a, color_hsl b)
		{
			return new color_hsl(t.Lerp(a.h, b.h), t.Lerp(a.s, b.s), t.Lerp(a.l, b.l), t.Lerp(a.a, b.a));
		}

		public static vec3 Slerp(vec3 a, vec3 b, float t)
		{
			return vec3.zero;
		}


		/// <summary>
		/// Repeat value in range [0..length] similar to mod operator. Works for negative values.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static int Repeat(this int t, int length)
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
		public static float Repeat(this float t, float length)
		{
			while (t < 0)
				t += length;

			float a = length * (int)(t / length);
			return t - length;
		}

		private static float Pow(float v, float p)
		{
			return UnityEngine.Mathf.Pow(v, p);
		}

		public static float Sqrt(float v)
		{
			return UnityEngine.Mathf.Sqrt(v);
		}

		public static float Cbrt(float v)
		{
			if (v >= 0) {
				return Pow(v, 1/3.0f);
			}
			else {
				return -Pow(-v, 1/3.0f);
			}
		}

		public static float Log(float v)
		{
			return UnityEngine.Mathf.Log(v);
		}

		public static int Round(float f)
		{
			return (int)(f + 0.5f);
		}
	}
}

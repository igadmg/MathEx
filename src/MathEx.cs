using System;
using System.Collections.Generic;

namespace MathEx
{
	public static class MathExOps
	{
		public const float PI = 3.1415927f;
		public const float _2PI = 2.0f * PI;
		public const float Deg2Rad = 0.0174532924F;
		public const float Rad2Deg = 57.29578F;

		public static int ToInt(this bool v) { return v ? 1 : 0; }
		public static int ToInt(this bool v, int t, int f) { return v ? t : f; }

		public static int Sign(this int v) { return v > 0 ? 1 : v < 0 ? -1 : 0; }
		public static int Signi(this float v) { return v > 0 ? 1 : v < 0 ? -1 : 0; }

		public static float Abs(this float v) { return v > 0 ? v : -v; }
		public static float Sign(this float v) { return v > 0 ? 1.0f : v < 0 ? -1.0f : 0; }

		public static float Min(this float a, float b) { return UnityEngine.Mathf.Min(a, b); }
		public static float Max(this float a, float b) { return UnityEngine.Mathf.Max(a, b); }

		public static int Clamp(this int f, int p1, int p2)
		{
			return UnityEngine.Mathf.Clamp(f, p1, p2);
		}

		public static float Clamp(this float f, float p1, float p2)
		{
			return UnityEngine.Mathf.Clamp(f, p1, p2);
		}

		public static float Clamp01(this float f)
		{
			return UnityEngine.Mathf.Clamp01(f);
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
			return a * (1 - t) + b * t;
		}

		public static float Lerp(this float t, vec3 abc)
		{
			return t.Lerp(abc.x, abc.y, abc.z);
		}

#if UNITY || UNITY_5_3_OR_NEWER
		public static float Lerp(this float t, UnityEngine.Vector3 abc)
		{
			return t.Lerp(abc.x, abc.y, abc.z);
		}
#endif

		public static float Lerp(this float t, float a, float b, params float[] o)
		{
			int count = 1 + o.Length;
			t = t * count;

			if (t <= 1)
				return t.Lerp(a, b);
			if (t <= 2)
				return (t - 1).Lerp(b, o[0]);

			float floor = (float)System.Math.Floor(t);
			if (t == floor)
				return o[(int)floor - 2];

			return (t - floor).Lerp(o[(int)floor - 2], o[(int)floor - 1]);
		}

		public static float InvLerp(this float v, float a, float b)
		{
			return (v - a) / (b - a);
		}

		public static float InvLerp(this float v, params float[] o)
		{
			if (o == null || o.Length < 2)
				return float.NaN;

			for (int i = 0; i < o.Length; i++)
			{
				if (o.Length == i + 1 || v <= o[i + 1])
					return (i + v.InvLerp(o[i], o[i + 1])) / (o.Length - 1);
			}

			return float.NaN;
		}

		public static vec2 Lerp(this float t, vec2 a, vec2 b)
		{
			return a * (1 - t) + b * t;
		}

		public static vec3 Lerp(this float t, vec3 a, vec3 b)
		{
			return a * (1 - t) + b * t;
		}

		public static color_hsv Lerp(this float t, color_hsv a, color_hsv b)
		{
			return new color_hsv(t.Lerp(a.h, b.h), t.Lerp(a.s, b.s), t.Lerp(a.v, b.v), t.Lerp(a.a, b.a));
		}

		public static color_hsl Lerp(this float t, color_hsl a, color_hsl b)
		{
			return new color_hsl(t.Lerp(a.h, b.h), t.Lerp(a.s, b.s), t.Lerp(a.l, b.l), t.Lerp(a.a, b.a));
		}

		static vec2 Slerp(this float t, vec2 a, vec2 b)
		{
			float d = a * b;

			d = Clamp(d, -1, 1);

			float theta = Acos(d) * t;
			var r = (b - a * d).normalized;

			return ((a * Cos(theta)) + (r * Sin(theta)));
		}

		public static vec3 Slerp(this float t, vec3 a, vec3 b)
		{
			float d = a * b;

			d = Clamp(d, -1, 1);

			float theta = Acos(d) * t;
			var r = (b - a * d).normalized;

			return ((a * Cos(theta)) + (r * Sin(theta)));
		}

		public static Tuple<int, int, float> IntRangeAndT(this float f)
		{
			int i = (int)f;
			if (i == f)
			{
				return Tuple.Create(i, i, 0.0f);
			}

			int a = UnityEngine.Mathf.FloorToInt(f);
			int b = UnityEngine.Mathf.CeilToInt(f);
			return Tuple.Create(a, b, f.InvLerp(a, b));
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

		public static IEnumerable<float> Range(this float delta, float min, float max)
		{
			int steps = ((max - min) / delta).Round();

			for (int i = 0; i <= steps; i++)
			{
				yield return min + delta * i;
			}
		}

		public static float Pow(this float v, float p)
		{
			return UnityEngine.Mathf.Pow(v, p);
		}

		public static float Sqrt(this float v)
		{
			return UnityEngine.Mathf.Sqrt(v);
		}

		public static float Cbrt(this float v)
		{
			if (v >= 0)
			{
				return Pow(v, 1 / 3.0f);
			}
			else
			{
				return -Pow(-v, 1 / 3.0f);
			}
		}

		public static float Log(this float v)
		{
			return UnityEngine.Mathf.Log(v);
		}

		public static int Round(this float f)
		{
			return (int)(f + 0.5f);
		}

		public static float Cos(this float ar)
		{
			return UnityEngine.Mathf.Cos(ar);
		}

		public static float Sin(this float ar)
		{
			return UnityEngine.Mathf.Sin(ar);
		}

		public static float Acos(this float ar)
		{
			return UnityEngine.Mathf.Acos(ar);
		}

		public static float Atan2(this float y, float x)
		{
			return UnityEngine.Mathf.Atan2(y, x);
		}

		public static bool IsZero(this float[] a)
		{
			for (int i = 0; i < a.Length; i++)
				if (a[i] != 0) return false;

			return true;
		}

		public static bool IsZero(this IEnumerable<float> a)
		{
			foreach (float f in a)
				if (f != 0) return false;

			return true;
		}

		public static bool IsEmpty(this float[] a)
		{
			for (int i = 0; i < a.Length; i++)
				if (float.IsNaN(a[i])) return true;

			return false;
		}

		public static bool IsEmpty(this IEnumerable<float> a)
		{
			foreach (float f in a)
				if (float.IsNaN(f)) return true;

			return false;
		}
	}
}

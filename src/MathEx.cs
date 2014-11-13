using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public static class MathEx
	{
		public static float Abs(float v) { return v > 0 ? v : -v; }
		public static float Sign(float v) { return v > 0 ? 1.0f : -1.0f; }

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


		public static vec2 Floor(this vec2 v)
		{
			return new vec2(UnityEngine.Mathf.Floor(v.x), UnityEngine.Mathf.Floor(v.y));
		}


		public static vec2 X(this vec2 v, float x)
		{
			return new vec2(x, v.y);
		}

		public static vec2 Y(this vec2 v, float y)
		{
			return new vec2(v.x, y);
		}

		public static vec3 X(this vec3 v, float x)
		{
			return new vec3(x, v.y, v.z);
		}

		public static vec3 Y(this vec3 v, float y)
		{
			return new vec3(v.x, y, v.z);
		}

		public static vec3 Z(this vec3 v, float z)
		{
			return new vec3(v.x, v.y, z);
		}

		public static vec2 dX(this vec2 v, float dx)
		{
			return new vec2(v.x + dx, v.y);
		}

		public static vec2 dY(this vec2 v, float dy)
		{
			return new vec2(v.x, v.y + dy);
		}

		public static vec2 iX(this vec2 v)
		{
			return new vec2(-v.x, v.y);
		}

		public static vec2 iY(this vec2 v)
		{
			return new vec2(v.x, -v.y);
		}

		public static vec3 dX(this vec3 v, float dx)
		{
			return new vec3(v.x + dx, v.y, v.z);
		}

		public static vec3 dY(this vec3 v, float dy)
		{
			return new vec3(v.x, v.y + dy, v.z);
		}

		public static vec3 dZ(this vec3 v, float dz)
		{
			return new vec3(v.x, v.y, v.z + dz);
		}

		public static vec3 xyz(this vec2 v, float z)
		{
			return new vec3(v.x, v.y, z);
		}

		public static vec3[] xyz(this vec2[] vs, float z)
		{
			var r = new vec3[vs.Length];
			for (int i = 0; i < vs.Length; i++)
				r[i] = vs[i].xyz(z);
			return r;
		}

		public static vec3 xzy(this vec2 v, float z)
		{
			return new vec3(v.x, z, v.y);
		}

		public static vec3 zxy(this vec2 v, float z)
		{
			return new vec3(z, v.x, v.y);
		}

		public static vec3 yxz(this vec2 v, float z)
		{
			return new vec3(v.y, v.x, z);
		}

		public static vec3 xzy(this vec3 v)
		{
			return new vec3(v.x, v.z, v.y);
		}
		
		public static vec2 xy(this vec3 v)
		{
			return new vec2(v.x, v.y);
		}

		public static vec2 xy(this vec4 v)
		{
			return new vec2(v.x, v.y);
		}

		public static vec2 xz(this vec3 v)
		{
			return new vec2(v.x, v.z);
		}

		public static vec2 zx(this vec3 v)
		{
			return new vec2(v.z, v.x);
		}

		public static vec2 yz(this vec3 v)
		{
			return new vec2(v.y, v.z);
		}

		public static vec2 zy(this vec3 v)
		{
			return new vec2(v.z, v.y);
		}

	}
}

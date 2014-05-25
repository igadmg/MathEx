using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public static class MathEx
	{
		public static int Clamp(int f, int p1, int p2)
		{
			throw new NotImplementedException();
		}

		public static float Clamp(float f, float p1, float p2)
		{
			throw new NotImplementedException();
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

			float a = length * (int)(t / length);
			return t - length;
		}

		private static float Pow(float v, float p)
		{
			throw new NotImplementedException();
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

		public static int Round(float f)
		{
			return (int)(f + 0.5f);
		}
	}
}

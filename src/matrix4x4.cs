using System;
using System.Runtime.InteropServices;

namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct matrix4x4
	{
		float m0, m4, m8, m12;
		float m1, m5, m9, m13;
		float m2, m6, m10, m14;
		float m3, m7, m11, m15;

		public static matrix4x4 identity => new matrix4x4(1.0f);

		public static matrix4x4 Translate(vec3 v)
			=> new matrix4x4 {
				m0 = 1, m4 = 0, m8 = 0, m12 = v.x,
				m1 = 0, m5 = 1, m9 = 0, m13 = v.y,
				m2 = 0, m6 = 0, m10 = 1, m14 = v.z,
				m3 = 0, m7 = 0, m11 = 0, m15 = 1 };

		public static matrix4x4 Rotate(vec3 axis, float ar)
		{
			matrix4x4 result = new matrix4x4(0);

			float x = axis.x, y = axis.y, z = axis.z;

			float length = (x * x + y * y + z * z).Sqrt();

			if ((length != 1.0f) && (length != 0.0f))
			{
				length = 1.0f / length;
				x *= length;
				y *= length;
				z *= length;
			}

			float sinres = ar.Sin();
			float cosres = ar.Cos();
			float t = 1.0f - cosres;

			result.m0 = x * x * t + cosres;
			result.m1 = y * x * t + z * sinres;
			result.m2 = z * x * t - y * sinres;
			result.m3 = 0.0f;

			result.m4 = x * y * t - z * sinres;
			result.m5 = y * y * t + cosres;
			result.m6 = z * y * t + x * sinres;
			result.m7 = 0.0f;

			result.m8 = x * z * t + y * sinres;
			result.m9 = y * z * t - x * sinres;
			result.m10 = z * z * t + cosres;
			result.m11 = 0.0f;

			result.m12 = 0.0f;
			result.m13 = 0.0f;
			result.m14 = 0.0f;
			result.m15 = 1.0f;

			return result;
		}

		public static matrix4x4 Scale(vec3 v)
			=> new matrix4x4 {
				m0 = v.x, m4 = 0, m8 = 0, m12 = 0,
				m1 = 0, m5 = v.y, m9 = 0, m13 = 0,
				m2 = 0, m6 = 0, m10 = v.z, m14 = 0,
				m3 = 0, m7 = 0, m11 = 0, m15 = 1 };

		public matrix4x4(float i)
		{
			m0 = i; m4 = 0; m8 = 0; m12 = 0;
			m1 = 0; m5 = i; m9 = 0; m13 = 0;
			m2 = 0; m6 = 0; m10 = i; m14 = 0;
			m3 = 0; m7 = 0; m11 = 0; m15 = i;
		}

		public vec3 translaion => new vec3(m12, m13, m14);
		public vec3 scale => new vec3(m0, m5, m10);

		public float determinant {
			get {
				float a00 = m0, a01 = m1, a02 = m2, a03 = m3;
				float a10 = m4, a11 = m5, a12 = m6, a13 = m7;
				float a20 = m8, a21 = m9, a22 = m10, a23 = m11;
				float a30 = m12, a31 = m13, a32 = m14, a33 = m15;

				float result = a30 * a21 * a12 * a03 - a20 * a31 * a12 * a03 - a30 * a11 * a22 * a03 + a10 * a31 * a22 * a03 +
							   a20 * a11 * a32 * a03 - a10 * a21 * a32 * a03 - a30 * a21 * a02 * a13 + a20 * a31 * a02 * a13 +
							   a30 * a01 * a22 * a13 - a00 * a31 * a22 * a13 - a20 * a01 * a32 * a13 + a00 * a21 * a32 * a13 +
							   a30 * a11 * a02 * a23 - a10 * a31 * a02 * a23 - a30 * a01 * a12 * a23 + a00 * a31 * a12 * a23 +
							   a10 * a01 * a32 * a23 - a00 * a11 * a32 * a23 - a20 * a11 * a02 * a33 + a10 * a21 * a02 * a33 +
							   a20 * a01 * a12 * a33 - a00 * a21 * a12 * a33 - a10 * a01 * a22 * a33 + a00 * a11 * a22 * a33;

				return result;
			}
		}

		public float trace => m0 + m5 + m10 + m15;

		public matrix4x4 inverted {
			get {
				matrix4x4 result = new matrix4x4(0);

				float a00 = m0, a01 = m1, a02 = m2, a03 = m3;
				float a10 = m4, a11 = m5, a12 = m6, a13 = m7;
				float a20 = m8, a21 = m9, a22 = m10, a23 = m11;
				float a30 = m12, a31 = m13, a32 = m14, a33 = m15;

				float b00 = a00 * a11 - a01 * a10;
				float b01 = a00 * a12 - a02 * a10;
				float b02 = a00 * a13 - a03 * a10;
				float b03 = a01 * a12 - a02 * a11;
				float b04 = a01 * a13 - a03 * a11;
				float b05 = a02 * a13 - a03 * a12;
				float b06 = a20 * a31 - a21 * a30;
				float b07 = a20 * a32 - a22 * a30;
				float b08 = a20 * a33 - a23 * a30;
				float b09 = a21 * a32 - a22 * a31;
				float b10 = a21 * a33 - a23 * a31;
				float b11 = a22 * a33 - a23 * a32;

				float invDet = 1.0f / (b00 * b11 - b01 * b10 + b02 * b09 + b03 * b08 - b04 * b07 + b05 * b06);

				result.m0 = (a11 * b11 - a12 * b10 + a13 * b09) * invDet;
				result.m1 = (-a01 * b11 + a02 * b10 - a03 * b09) * invDet;
				result.m2 = (a31 * b05 - a32 * b04 + a33 * b03) * invDet;
				result.m3 = (-a21 * b05 + a22 * b04 - a23 * b03) * invDet;
				result.m4 = (-a10 * b11 + a12 * b08 - a13 * b07) * invDet;
				result.m5 = (a00 * b11 - a02 * b08 + a03 * b07) * invDet;
				result.m6 = (-a30 * b05 + a32 * b02 - a33 * b01) * invDet;
				result.m7 = (a20 * b05 - a22 * b02 + a23 * b01) * invDet;
				result.m8 = (a10 * b10 - a11 * b08 + a13 * b06) * invDet;
				result.m9 = (-a00 * b10 + a01 * b08 - a03 * b06) * invDet;
				result.m10 = (a30 * b04 - a31 * b02 + a33 * b00) * invDet;
				result.m11 = (-a20 * b04 + a21 * b02 - a23 * b00) * invDet;
				result.m12 = (-a10 * b09 + a11 * b07 - a12 * b06) * invDet;
				result.m13 = (a00 * b09 - a01 * b07 + a02 * b06) * invDet;
				result.m14 = (-a30 * b03 + a31 * b01 - a32 * b00) * invDet;
				result.m15 = (a20 * b03 - a21 * b01 + a22 * b00) * invDet;

				return result;
			}
		}

		public matrix4x4 normalized {
			get {
				matrix4x4 result = new matrix4x4(0);

				float det = determinant;

				result.m0 = m0 / det;
				result.m1 = m1 / det;
				result.m2 = m2 / det;
				result.m3 = m3 / det;
				result.m4 = m4 / det;
				result.m5 = m5 / det;
				result.m6 = m6 / det;
				result.m7 = m7 / det;
				result.m8 = m8 / det;
				result.m9 = m9 / det;
				result.m10 = m10 / det;
				result.m11 = m11 / det;
				result.m12 = m12 / det;
				result.m13 = m13 / det;
				result.m14 = m14 / det;
				result.m15 = m15 / det;

				return result;
			}
		}

		public static matrix4x4 operator +(matrix4x4 a, matrix4x4 b)
		{
			matrix4x4 result = new matrix4x4(0);

			result.m0 = a.m0 + b.m0;
			result.m1 = a.m1 + b.m1;
			result.m2 = a.m2 + b.m2;
			result.m3 = a.m3 + b.m3;
			result.m4 = a.m4 + b.m4;
			result.m5 = a.m5 + b.m5;
			result.m6 = a.m6 + b.m6;
			result.m7 = a.m7 + b.m7;
			result.m8 = a.m8 + b.m8;
			result.m9 = a.m9 + b.m9;
			result.m10 = a.m10 + b.m10;
			result.m11 = a.m11 + b.m11;
			result.m12 = a.m12 + b.m12;
			result.m13 = a.m13 + b.m13;
			result.m14 = a.m14 + b.m14;
			result.m15 = a.m15 + b.m15;

			return result;
		}

		public static matrix4x4 operator -(matrix4x4 a, matrix4x4 b)
		{
			matrix4x4 result = new matrix4x4(0);

			result.m0 = a.m0 - b.m0;
			result.m1 = a.m1 - b.m1;
			result.m2 = a.m2 - b.m2;
			result.m3 = a.m3 - b.m3;
			result.m4 = a.m4 - b.m4;
			result.m5 = a.m5 - b.m5;
			result.m6 = a.m6 - b.m6;
			result.m7 = a.m7 - b.m7;
			result.m8 = a.m8 - b.m8;
			result.m9 = a.m9 - b.m9;
			result.m10 = a.m10 - b.m10;
			result.m11 = a.m11 - b.m11;
			result.m12 = a.m12 - b.m12;
			result.m13 = a.m13 - b.m13;
			result.m14 = a.m14 - b.m14;
			result.m15 = a.m15 - b.m15;

			return result;
		}

		public static matrix4x4 operator *(matrix4x4 a, matrix4x4 b)
		{
			matrix4x4 result = new matrix4x4(0);

			result.m0 = a.m0 * b.m0 + a.m1 * b.m4 + a.m2 * b.m8 + a.m3 * b.m12;
			result.m1 = a.m0 * b.m1 + a.m1 * b.m5 + a.m2 * b.m9 + a.m3 * b.m13;
			result.m2 = a.m0 * b.m2 + a.m1 * b.m6 + a.m2 * b.m10 + a.m3 * b.m14;
			result.m3 = a.m0 * b.m3 + a.m1 * b.m7 + a.m2 * b.m11 + a.m3 * b.m15;
			result.m4 = a.m4 * b.m0 + a.m5 * b.m4 + a.m6 * b.m8 + a.m7 * b.m12;
			result.m5 = a.m4 * b.m1 + a.m5 * b.m5 + a.m6 * b.m9 + a.m7 * b.m13;
			result.m6 = a.m4 * b.m2 + a.m5 * b.m6 + a.m6 * b.m10 + a.m7 * b.m14;
			result.m7 = a.m4 * b.m3 + a.m5 * b.m7 + a.m6 * b.m11 + a.m7 * b.m15;
			result.m8 = a.m8 * b.m0 + a.m9 * b.m4 + a.m10 * b.m8 + a.m11 * b.m12;
			result.m9 = a.m8 * b.m1 + a.m9 * b.m5 + a.m10 * b.m9 + a.m11 * b.m13;
			result.m10 = a.m8 * b.m2 + a.m9 * b.m6 + a.m10 * b.m10 + a.m11 * b.m14;
			result.m11 = a.m8 * b.m3 + a.m9 * b.m7 + a.m10 * b.m11 + a.m11 * b.m15;
			result.m12 = a.m12 * b.m0 + a.m13 * b.m4 + a.m14 * b.m8 + a.m15 * b.m12;
			result.m13 = a.m12 * b.m1 + a.m13 * b.m5 + a.m14 * b.m9 + a.m15 * b.m13;
			result.m14 = a.m12 * b.m2 + a.m13 * b.m6 + a.m14 * b.m10 + a.m15 * b.m14;
			result.m15 = a.m12 * b.m3 + a.m13 * b.m7 + a.m14 * b.m11 + a.m15 * b.m15;

			return result;
		}

		public static vec4 operator *(vec4 a, matrix4x4 b)
		{
			vec4 result = new vec4();

			result.x = a.x * b.m0 + a.y * b.m4 + a.z * b.m8 + a.w * b.m12;
			result.y = a.x * b.m1 + a.y * b.m5 + a.z * b.m9 + a.w * b.m13;
			result.z = a.x * b.m2 + a.y * b.m6 + a.z * b.m10 + a.w * b.m14;
			result.w = a.x * b.m3 + a.y * b.m7 + a.z * b.m11 + a.w * b.m15;

			return result;
		}

		public static vec4 operator *(vec2 a, matrix4x4 b)
			=> (a.xyzw() * b);

		public static vec4 operator *(vec2i a, matrix4x4 b)
			=> (a.xyzw() * b);
	}
}

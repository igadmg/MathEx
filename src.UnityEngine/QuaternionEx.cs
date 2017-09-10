using System.Collections.Generic;
using UnityEngine;

namespace MathEx
{
	public static class QuaternionEx
	{
		public static IEnumerable<Quaternion> Lerp(this Quaternion a, Quaternion b, float dT)
		{
			float t = 0;
			while (t < 1) {
				yield return Quaternion.Lerp(a, b, t); t += dT;
			}
			yield return Quaternion.Lerp(a, b, 1);
			yield break;
		}

		public static IEnumerable<Quaternion> Slerp(this Quaternion a, Quaternion b, float dT)
		{
			float t = 0;
			while (t < 1) {
				yield return Quaternion.Slerp(a, b, t); t += dT;
			}
			yield return Quaternion.Slerp(a, b, 1);
			yield break;
		}

		public static Quaternion Pow(this Quaternion q, float n)
		{
			return q.Ln().Scale(n).Exp();
		}

		public static Quaternion Ln(this Quaternion q)
		{
			float l = q.x * q.x + q.y * q.y + q.z * q.z;
			float r = Mathf.Sqrt(l);
			float t = r > 0.00001f ? Mathf.Atan2(r, q.w) / r : 0.0f;

			return new Quaternion(
				q.x * t,
				q.y * t,
				q.z * t,
				0.5f * Mathf.Log(q.w * q.w + l));
		}

		public static Quaternion Exp(this Quaternion q)
		{
			float r = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z);
			float et = Mathf.Exp(q.w);
			float s = r >= 0.00001f ? et * Mathf.Sin(r) / r : 0f;

			return new Quaternion(
				q.x * s,
				q.y * s,
				q.z * s,
				et * Mathf.Cos(r));
		}

		public static Quaternion Scale(this Quaternion q, float s)
		{
			return new Quaternion(
				q.x * s,
				q.y * s,
				q.z * s,
				q.w * s);
		}
	}
}

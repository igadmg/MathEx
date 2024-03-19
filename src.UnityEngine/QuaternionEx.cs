using System.Collections.Generic;
using UnityEngine;



namespace MathEx
{
	public static class QuaternionEx
	{
		//public static Quaternion Opposite(this Quaternion q) {
		//	return Quaternion.Euler(q.eulerAngles - new Vector3(180, 180, 180));
		//}

		public static Quaternion Inverse(this Quaternion q)
		{
			return Quaternion.Inverse(q);
		}

		public static Quaternion Lerp(this float t, Quaternion a, Quaternion b)
		{
			return Quaternion.Lerp(a, b, t);
		}

		public static Quaternion Slerp(this float t, Quaternion a, Quaternion b)
		{
			return Quaternion.Slerp(a, b, t);
		}

		public static IEnumerable<Quaternion> LerpStep(this float dT, Quaternion a, Quaternion b)
		{
			float t = 0;
			while (t < 1)
			{
				yield return Quaternion.Lerp(a, b, t); t += dT;
			}
			yield return Quaternion.Lerp(a, b, 1);
			yield break;
		}

		public static IEnumerable<Quaternion> SlerpStep(this float dT, Quaternion a, Quaternion b)
		{
			float t = 0;
			while (t < 1)
			{
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

		public static float Magnitude(this Quaternion q)
		{
			return new Vector4(q.x, q.y, q.z, q.w).magnitude;
		}

		public static Quaternion Normalized(this Quaternion q)
		{
			Vector4 n = new Vector4(q.x, q.y, q.z, q.w).normalized;
			return new Quaternion(n.x, n.y, n.z, n.w);
		}
	}
}

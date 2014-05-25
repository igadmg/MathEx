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
	}
}

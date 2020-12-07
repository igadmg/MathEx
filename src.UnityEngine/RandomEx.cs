using SystemEx;
using UnityEngine;

namespace MathEx
{
	public static class RandomEx
	{
		public static int Next(this IRandomGenerator<int> irg, Vector2Int range)
			=> irg.Next(range.x, range.y + 1);

		public static float Next(this IRandomGenerator<float> frg, Vector2 range)
			=> frg.Next01().Lerp(range.x, range.y);

		/// Random value [0, 1]
		/// Output is increasingly biased toward 1 as biasStrength goes from 0 to 1
		public static float NextBiasUpper(this IRandomGenerator<float> frg, float biasStrength)
			=> 1 - frg.NextBiasLower(biasStrength);

		/// Random value [0, 1]
		/// Output is increasingly biased toward 0 as biasStrength goes from 0 to 1
		public static float NextBiasLower(this IRandomGenerator<float> frg, float biasStrength)
		{
			float t = frg.Next01();

			// Avoid possible division by zero
			if (biasStrength == 1)
			{
				return 0;
			}

			// Remap strength for nicer input -> output relationship
			float k = Mathf.Clamp01(1 - biasStrength);
			k = k * k * k - 1;

			// Thanks to www.shadertoy.com/view/Xd2yRd
			return Mathf.Clamp01((t + t * k) / (t * k + 1));
		}

		/// Random value [0, 1]
		/// Output is increasingly biased toward the extremes (0 or 1) as biasStrength goes from 0 to 1
		public static float NextBiasExtremes(this IRandomGenerator<float> frg, float biasStrength)
		{
			float t = frg.NextBiasLower(biasStrength);
			return (frg.Sign() < 0.0f) ? t : 1 - t;
		}

		/// Random value [0, 1]
		/// Output is increasingly biased toward 0.5 as biasStrength goes from 0 to 1
		public static float NextBiasCentre(this IRandomGenerator<float> frg, float biasStrength)
		{
			float t = frg.NextBiasLower(biasStrength);
			return 0.5f + t * 0.5f * frg.Sign();
		}

		public static Vector2 NextPointInRing(this IRandomGenerator<float> frg, Vector2 ring)
		{
			float r = frg.Next(ring);
			float phi = frg.Next(0, 2 * Mathf.PI);

			return new Vector2(r * Mathf.Cos(phi), r * Mathf.Sin(phi));
		}

		public static Vector2 NextPointOnCircle(this IRandomGenerator<float> frg, float r = 1.0f)
		{
			float phi = frg.Next(0, 2 * Mathf.PI);

			return new Vector2(r * Mathf.Cos(phi), r * Mathf.Sin(phi));
		}

		public static Vector3 NextPointOnSphere(this IRandomGenerator<float> frg, float r = 1.0f)
		{
			float phi = frg.Next(0, 2 * Mathf.PI);
			float theta = frg.Next01().Lerp(0, Mathf.PI);

			float cp = Mathf.Cos(phi);
			float sp = Mathf.Sin(phi);
			float ct = Mathf.Cos(theta);
			float st = Mathf.Sin(theta);

			return new Vector3(r * st * cp, r * st * sp, r * ct);
		}
	}
}

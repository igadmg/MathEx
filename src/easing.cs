using System;



namespace MathEx
{
	public static class Easing
	{
		public static float easeLinear(this float t)
		{
			return t;
		}

		public static float easeCubicIn(this float t)
		{
			return t * t * t;
		}

		public static float easeCubicOut(this float t)
		{
			t = t - 1f;
			return t * t * t + 1f;
		}

		public static float easeCubicInOut(this float t)
		{
			t *= 2f;
			if (t < 1f) return 0.5f * t * t * t;
			t -= 2f;
			return 0.5f * t * t * t + 2f;
		}

		public static float easeExpoIn(this float t)
		{
			return t == 0f ? 0f
				: MathExOps.Pow(2f, 10f * (t - 1f));
		}

		public static float easeExpoOut(this float t)
		{
			return t == 1f ? 1f
				: 1f - MathExOps.Pow(2f, -10f * t);
		}

		public static float easeExpoInOut(this float t)
		{
			if (t == 0f) return 0f;
			if (t == 1f) return 1f;
			t *= 2f;
			if (t < 1f) return 0.5f * MathExOps.Pow(2f, 10f * (t - 1f));
			return 1f - 0.5f * MathExOps.Pow(2f, -10f * (t - 1f));
		}

		public static float easeMirror(this float t, Func<float, float> eFn)
		{
			t *= 2f;
			if (t < 1f) return eFn(t);
			else return eFn(-(t - 2f));
		}

		public static Func<float, float> makeEaseMirror(Func<float, float> eFn)
		{
			return t => easeMirror(t, eFn);
		}
	}
}

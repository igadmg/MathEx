using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public static class random
	{
		static Random rnd = new Random();

		public static float Next
		{
			get { return (float)rnd.NextDouble(); }
		}

		public static IEnumerable<float> NormalDistribution()
		{
			bool ready = false;
			float second = 0.0f;

			while (true) {
				if (ready) {
					ready = false;
					yield return second;
				}
				else {
					var cr = UnityEngine.Random.insideUnitCircle;
					var lcr = cr.magnitude;
					var r = MathEx.Sqrt(-2.0f * MathEx.Log(lcr) / lcr);

					second = r * cr.x;
					ready = true;
					yield return r * cr.y;
				}
			}

			yield break;
		}
	}
}

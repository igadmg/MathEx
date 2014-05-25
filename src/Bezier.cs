using System;
using System.Collections.Generic;

namespace MathEx
{
	public class Bezier
	{
		vec3[] P;
		
		
		public Bezier()
		{
			P = new vec3[] {
				vec3.zero, new vec3(0, 0, 1), new vec3(1, 0, 1), new vec3(1, 0, 0)
			};
		}

		public Bezier(ICollection<vec3> p)
		{
			System.Diagnostics.Debug.Assert(p.Count == 4);

			P = new vec3[p.Count];
			p.CopyTo(P, 0);
		}

		public vec3 Evaluate(float t)
		{
			float t1 = 1 - t;
			return P[0] * t1 * t1 * t1
				+  P[1] * 3 * t * t1 * t1
				+  P[2] * 3 * t * t * t1
				+  P[3] * t * t * t;
		}
	}
}

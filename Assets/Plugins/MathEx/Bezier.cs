using System;
using System.Collections.Generic;
using UnityEngine;

namespace MathEx
{
	public class Bezier
	{
		Vector3[] P;
		
		
		public Bezier()
		{
			P = new Vector3[] {
				Vector3.zero, new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0)
			};
		}

		public Bezier(ICollection<Vector3> p)
		{
			System.Diagnostics.Debug.Assert(p.Count == 4);

			P = new Vector3[p.Count];
			p.CopyTo(P, 0);
		}

		public Vector3 Evaluate(float t)
		{
			float t1 = 1 - t;
			return P[0] * t1 * t1 * t1
				+  P[1] * 3 * t * t1 * t1
				+  P[2] * 3 * t * t * t1
				+  P[3] * t * t * t;
		}
	}
}

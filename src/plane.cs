using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class plane
	{
		public static readonly plane empty = new plane();
		public static readonly plane xy = new plane(vec3.zero, vec3.forward);


		float pa, pb, pc, pd;

		public vec3 normal { get { return new vec3(pa, pb, pc); } }
		public float d { get { return pd; } }

		public plane()
		{
			pa = pb = pc = pd = float.NaN;
		}

		public plane(vec3 o, vec3 n)
		{
			pa = n.x;
			pb = n.y;
			pc = n.z;
			pd = -(o * n);
		}

		public vec3 project(vec3 v)
		{
			var n = normal;
			return v - (v * n) * n;
		}

		public vec3 cast(ray r, out float d)
		{
			d = normal * r.direction;

			if (d == 0)
				return vec3.empty;

			d = -(r.origin * normal + pd) / d;

			if (d < 0)
				return vec3.empty;

			return r.origin + d * r.direction;
		}
	}
}

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

		public plane(vec3 n, float d)
		{
			pa = n.x;
			pb = n.y;
			pc = n.z;
			pd = d;
		}

		public vec3 project(vec3 v)
		{
			var n = normal;
			return v - (v * n) * n;
		}

		public vec3 cast(ray r)
		{
			float d;

			return cast(r, out d);
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

		public ray intersect(plane p)
		{
			vec3 dir = p.normal % p.normal;

			if (dir.IsZero)
				return ray.empty;

			float d = (p.pb * pc - p.pc);
			if (pb != 0 && d != 0) {
				float z = (p.pd - p.pb * pd) / (p.pb * pc - p.pc);
				float y = (pd - pc * z) / pb;
				return new ray(new vec3(0, y, z), dir);
			}

			d = (p.pc * pa - p.pa);
			if (pc != 0 && d != 0) {
				float x = (p.pd - p.pc * pd) / (p.pc * pa - p.pa);
				float z = (pd - pa * x) / pc;
				return new ray(new vec3(x, 0, z), dir);
			}

			d = (p.pa * pb - p.pb);
			if (pa != 0 && d != 0) {
				float y = (p.pd - p.pa * pd) / (p.pa * pb - p.pb);
				float x = (pd - pb * y) / pa;
				return new ray(new vec3(x, y, 0), dir);
			}

			return new ray(vec3.empty, dir);
		}
	}
}

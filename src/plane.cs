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
		public float a { get { return pa; } }
		public float b { get { return pb; } }
		public float c { get { return pc; } }
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

#if UNITY || UNITY_5_3_OR_NEWER
		public static implicit operator UnityEngine.Plane(plane p)
		{
			return new UnityEngine.Plane(p.normal, p.d);
		}

		public static implicit operator plane(UnityEngine.Plane p)
		{
			return new plane(p.normal, p.distance);
		}
#endif
	}
}

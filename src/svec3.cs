using UnityEngine;

namespace MathEx
{
	public struct svec3
	{
		public float u;
		public float e;
		public float n;


		public svec3(float u, float e, float n)
		{
			this.u = u;
			this.e = e;
			this.n = n;
		}

		public static implicit operator svec3(Vector3 v)
		{
			float u = v.magnitude;
			return new svec3(u, Mathf.Atan2(v.x, v.y), Mathf.Acos(v.z / u));
		}

		public static implicit operator vec3(svec3 v)
		{
			float sE = Mathf.Sin(v.e);
			float cE = Mathf.Cos(v.e);
			float sN = Mathf.Sin(v.n);
			float cN = Mathf.Cos(v.n);
			return new vec3(v.u * sN * cE, v.u * sN * sE, v.u * cN);
		}
	}
}


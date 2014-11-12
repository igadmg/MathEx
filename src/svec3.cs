using UnityEngine;

namespace MathEx
{
	public struct svec3
	{
		public float r; // magnitude
		public float i; // inclination
		public float a; // azimuth


		public svec3(float r, float i, float a)
		{
			this.r = r;
			this.i = i;
			this.a = a;
		}

		public static implicit operator svec3(Vector3 v)
		{
			float u = v.magnitude;
			return new svec3(u, Mathf.Acos(v.z / u), Mathf.Atan2(v.x, v.y));
		}

		public static implicit operator vec3(svec3 v)
		{
			float sI = Mathf.Sin(v.i);
			float cI = Mathf.Cos(v.i);
			float sA = Mathf.Sin(v.a);
			float cA = Mathf.Cos(v.a);
			return new vec3(v.r * sI * cA, v.r * sI * sA, v.r * cI);
		}

		public vec3 ToVec3() { return this; }

		public override string ToString() { return string.Format("({0},{1},{2})", r, i, a); }
		public string ToString(string f) { return string.Format("({0},{1},{2})", r.ToString(f), i.ToString(f), a.ToString(f)); }
	}
}


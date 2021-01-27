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

		public svec3(vec3 v)
		{
			this.r = v.length;
			this.i = (v.z / r).Acos();
			this.a = v.y.Atan2(v.x);
		}

		public static explicit operator svec3(vec3 v)
		{
			float u = v.length;
			return new svec3(u, (v.z / u).Acos(), v.y.Atan2(v.x));
		}

		public static explicit operator vec3(svec3 v)
		{
			float sI = v.i.Sin();
			float cI = v.i.Cos();
			float sA = v.a.Sin();
			float cA = v.a.Cos();
			return new vec3(v.r * sI * cA, v.r * sI * sA, v.r * cI);
		}

		public vec3 ToVec3() { return (vec3)this; }

		public override string ToString() { return string.Format("({0},{1},{2})", r, i, a); }
		public string ToString(string f) { return string.Format("({0},{1},{2})", r.ToString(f), i.ToString(f), a.ToString(f)); }
	}
}


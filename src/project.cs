namespace MathEx
{
	public static class ProjectEx	
	{
		static vec3 project(this plane pl, vec3 p)
		{
			return p - (pl.normal * p + pl.d) * pl.normal;
		}

		static vec2 project(this plane pl, vec3 v, vec3 origin, vec3[] basis)
		{
			vec3 pv = pl.project(v);
			return new vec2(pv * basis[0], pv * basis[1]);
		}
	}
}

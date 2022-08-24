namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	public static class ProjectEx
	{
		public static vec3 project(this plane pl, vec3 p)
		{
			return p - ((pl.normal ^ p) + pl.d) * pl.normal;
		}

		public static vec2 project(this plane pl, vec3 v, vec3 origin, vec3[] basis)
		{
			vec3 pv = pl.project(v) - origin;
			return new vec2(pv ^ basis[0], pv ^ basis[1]);
		}

		public static vec3 project(this plane pl, vec2 v, vec3 origin, vec3[] basis)
		{
			return origin + basis[0] * v.x + basis[1] * v.y;
		}
	}
}

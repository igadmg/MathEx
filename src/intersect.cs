namespace MathEx
{
	public enum IntersectResult
	{
		None = 0x00,
		Intersect = 0x11,
		Contain1 = 0x01,
		Contain2 = 0x10,
	}

	public static class IntersectEx
	{
		public static IntersectResult intersect(this aabb2 aabb, vec2 v)
		{
			if (v.x < aabb.a.x && v.x > aabb.b.x)
				return IntersectResult.None;
			if (v.y < aabb.a.y && v.y > aabb.b.y)
				return IntersectResult.None;
			if ((v.x == aabb.a.x || v.x == aabb.b.x) && (v.y == aabb.a.y && v.y == aabb.b.y))
				return IntersectResult.Intersect;

			return IntersectResult.Contain1;
		}

		public static IntersectResult intersect(this aabb2 aabb, triangle<vec2> tri)
		{
			int siflags = 0x000000;
			for (int i = 0; i < tri.p.Length; i++)
			{
				var r = aabb.intersect(tri.p[i]);
				siflags = (int)(r) << (i << 8);
			}

			if (siflags == 0x010101)
				return IntersectResult.Contain1;

			return IntersectResult.None;
		}

		public static ray intersect(this plane a, plane b)
		{
			vec3 dir = a.normal % b.normal;

			if (dir.isZero)
				return ray.empty;

			if (a.b != 0)
			{
				float d = (b.b * a.c - b.c);
				if (d != 0)
				{
					float z = (b.d - b.b * a.d) / (b.b * a.c - b.c);
					float y = (a.d - a.c * z) / a.b;
					return new ray(new vec3(0, y, z), dir);
				}
			}

			if (a.c != 0)
			{
				float d = (b.c * a.a - b.a);
				if (d != 0)
				{
					float x = (b.d - b.c * a.d) / (b.c * a.a - b.a);
					float z = (a.d - a.a * x) / a.c;
					return new ray(new vec3(x, 0, z), dir);
				}
			}

			if (a.a != 0)
			{
				float d = (b.a * a.b - b.b);
				if (a.a != 0 && d != 0)
				{
					float y = (b.d - b.a * a.d) / (b.a * a.b - b.b);
					float x = (a.d - a.b * y) / a.a;
					return new ray(new vec3(x, y, 0), dir);
				}
			}

			return new ray(vec3.empty, dir);
		}

		public static vec3 intersect(this plane pl, ray r)
		{
			float d = pl.normal * -r.direction;

			if (d == 0)
			{ // line is parallel to plane
				return vec3.empty;
			}

			float n = pl.d + (pl.normal * r.origin);

			return r.origin + r.direction * (n / d);
		}

		public static line3_segment intersect(this ray a, ray b)
		{
			vec3 d = a.direction % b.direction;

			if (d.isZero)
				return line3_segment.empty;

			plane pa = new plane(a.origin, (a.direction % d).normalized);
			plane pb = new plane(b.origin, (b.direction % d).normalized);

			return new line3_segment(pa.intersect(b), pb.intersect(a));
		}
	}
}

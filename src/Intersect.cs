using System;
using System.Collections.Generic;

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
		public static IntersectResult Intersect(this aabb2 aabb, vec2 v)
		{
			if (v.x < aabb.a.x && v.x > aabb.b.x)
				return IntersectResult.None;
			if (v.y < aabb.a.y && v.y > aabb.b.y)
				return IntersectResult.None;
			if ((v.x == aabb.a.x || v.x == aabb.b.x) && (v.y == aabb.a.y && v.y == aabb.b.y))
				return IntersectResult.Intersect;

			return IntersectResult.Contain1;
		}

		public static IntersectResult Intersect(this aabb2 aabb, Triangle2 tri)
		{
			int siflags = 0x000000;
			for (int i = 0; i < tri.p.Length; i++) {
				var r = aabb.Intersect(tri.p[i]);
				siflags = (int)(r) << (i << 8);
			}

			if (siflags == 0x010101)
				return IntersectResult.Contain1;

			return IntersectResult.None;
		}

		public static vec3 Intersect(this plane pl, ray r)
		{
			float d = pl.normal * r.direction;

			if (d == 0)
				return vec3.empty;

			float t = -(r.origin * pl.normal + pl.d)/d;
			if (t < 0)
				return vec3.empty;

			return r.origin + t * r.direction;
		}
	}
}

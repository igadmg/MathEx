using System;
using System.Collections.Generic;

namespace MathEx
{
	public class Polygon2
	{
		public vec2[] p;

		public Polygon2(params vec2[] ps)
		{
			p = ps;
		}
	}

	public class Triangle2 : Polygon2
	{
		public Triangle2()
			: base(vec2.zero, vec2.zero, vec2.zero)
		{
		}

		public Triangle2(vec2 a, vec2 b, vec2 c)
			: base(a, b, c)
		{
		}
	}

	public class Polygon3
	{
		public vec3[] p;

		public Polygon3(params vec3[] ps)
		{
			p = ps;
		}
	}

	public class Triangle3 : Polygon3
	{
		public Triangle3()
			: base(vec3.zero, vec3.zero, vec3.zero)
		{
		}

		public Triangle3(vec3 a, vec3 b, vec3 c)
			: base(a, b, c)
		{
		}
	}
}

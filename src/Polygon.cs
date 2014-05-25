using System;
using System.Collections.Generic;

namespace MathEx
{
	public class Polygon2
	{
		public Vector2[] p;

		public Polygon2(params Vector2[] ps)
		{
			p = ps;
		}
	}

	public class Triangle2 : Polygon2
	{
		public Triangle2()
			: base(Vector2.zero, Vector2.zero, Vector2.zero)
		{
		}

		public Triangle2(Vector2 a, Vector2 b, Vector2 c)
			: base(a, b, c)
		{
		}
	}

	public class Polygon3
	{
		public Vector3[] p;

		public Polygon3(params Vector3[] ps)
		{
			p = ps;
		}
	}

	public class Triangle3 : Polygon3
	{
		public Triangle3()
			: base(Vector3.zero, Vector3.zero, Vector3.zero)
		{
		}

		public Triangle3(Vector3 a, Vector3 b, Vector3 c)
			: base(a, b, c)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace MathEx
{
	public class AaBb2
	{
		public static readonly AaBb2 empty = new AaBb2(VectorEx.Empty2, VectorEx.Empty2);

		public Vector2 a;
		public Vector2 b;

		public Vector2 size { get { return b - a; } }
		
		public AaBb2(Vector2 a, Vector2 b)
		{
			this.a = a;
			this.b = b;
		}

		public bool IsEmpty() { return a.IsEmpty() || b.IsEmpty(); }

		public AaBb2 Extend(Vector2 p)
		{
			if (IsEmpty())
				return new AaBb2(p, p);

			var min = Vector2.Min(a, p);
			var max = Vector2.Max(b, p);

			return new AaBb2(min, max);
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})", a, b);
		}
	}

	public class AaBb3
	{
		public Vector3 a;
		public Vector3 b;
	}
}

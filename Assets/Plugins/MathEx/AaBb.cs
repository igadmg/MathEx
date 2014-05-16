using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace MathEx
{
	public class AaBb2
	{
		public static readonly AaBb2 empty = new AaBb2(VectorEx.empty2, VectorEx.empty2);
		public static readonly AaBb2 zero = new AaBb2(Vector2.zero, Vector2.zero);
		public static readonly AaBb2 one = new AaBb2(Vector2.zero, Vector2.one);

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

		public Vector2[] ToArray()
		{
			return new Vector2[4] {
				a, new Vector2(a.x, b.y),
				b, new Vector2(b.x, a.y)
			};
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})", a, b);
		}

		public string ToString(string f)
		{
			return string.Format("({0}, {1})", a.ToString(f), b.ToString(f));
		}
	}

	public class AaBb3
	{
		public Vector3 a;
		public Vector3 b;
	}
}

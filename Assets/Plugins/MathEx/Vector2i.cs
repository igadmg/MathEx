using System;
using UnityEngine;

namespace MathEx
{
	public struct Vector2i
	{
		//
		// Fields
		//
		public int y;
		public int x;

		//
		// Static Properties
		//
		public static Vector2i zero     { get { return new Vector2i(0, 0); } }
		public static Vector2i one      { get { return new Vector2i(1, 1); } }
		public static Vector2i right    { get { return new Vector2i(1, 0); } }
		public static Vector2i up       { get { return new Vector2i(0, 1); } }

		//
		// Operators
		//

		public static Vector2i operator *(Vector2i a, int d)
		{
			return new Vector2i(a.x * d, a.y * d);
		}

		public Vector2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public static class Vector2iEx
	{
		public static Vector2 Div(this Vector2 l, Vector2i r)
		{
			return new Vector2(l.x / r.x, l.y / r.y);
		}
	}
}


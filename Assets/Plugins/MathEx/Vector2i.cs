using System;

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

		public Vector2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}


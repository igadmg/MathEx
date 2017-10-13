using UnityEngine;

namespace MathEx
{
	public static class BoundsEx
	{
		public static Bounds Empty = new Bounds(VectorEx.empty3, VectorEx.empty3);
		
		public static bool IsEmpty(this Bounds bounds)
		{
			return bounds.center.IsEmpty() || bounds.size.IsEmpty();
		}

		public static Rect GetRect(this Bounds bounds)
		{
			if (bounds.IsEmpty())
				return RectEx.Empty;

			return RectEx.Empty.Extend(bounds.min).Extend(bounds.max);
		}

		public static Bounds Extend(this Bounds bounds, Bounds b)
		{
			if (bounds.IsEmpty())
				return b;
			if (b.IsEmpty())
				return bounds;

			Vector3 min = Vector3.Min(bounds.min, b.min);
			Vector3 max = Vector3.Max(bounds.max, b.max);

			return new Bounds(Vector3.Lerp(min, max, 0.5f), max - min);
		}

		public static int Position(this Bounds bounds, Vector3 v)
		{
			if (bounds.IsEmpty())
				return 0x3f;
			if (v.IsEmpty())
				return 0x00;

			Vector3 a = bounds.min;
			Vector3 b = bounds.max;

			int res = 0;
			if (v.x < a.x)
				res |= 0x01;
			else if (v.x > b.x)
				res |= 0x02;

			if (v.y < a.y)
				res |= 0x04;
			else if (v.y > b.y)
				res |= 0x08;

			if (v.z < a.z)
				res |= 0x10;
			else if (v.z > b.z)
				res |= 0x12;

			return res;
		}
	}
}


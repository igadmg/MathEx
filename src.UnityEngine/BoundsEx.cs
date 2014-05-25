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
	}
}


using UnityEngine;

namespace MathEx
{
	public static class PlaneEx
	{
		public static bool Intersect(this Plane plane, Vector3 a, Vector3 b, out Vector3 r, out float d)
		{
			Vector3 dir = b - a;
			float dirm = dir.magnitude;

			if (plane.Raycast(new Ray(a, dir), out d)) {
				if (d <= dirm) {
					d /= dirm;
					r = Vector3.Lerp(a, b, d);
					return true;
				}
			}

			d = float.NaN;
			r = VectorEx.empty3;
			return false;
		}

		public static Vector3 Project(this Plane plane, Vector3 p)
		{
			if (plane.normal.z == 1)
				return p.Z(0);
			if (plane.normal.y == 1)
				return p.Y(0);
			if (plane.normal.x == 1)
				return p.X(0);

			var dist = plane.GetDistanceToPoint(p);
			return p - plane.normal * dist;
		}
	}
}


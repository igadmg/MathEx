using UnityEngine;

namespace MathEx
{
	public static class TransformEx
	{
		/// <summary>
		/// Cast a Ray from transform position in transform's forward direction.
		/// </summary>
		/// <param name="transfrom"></param>
		/// <returns></returns>
		public static Ray Ray(this Transform transform)
		{
			return new Ray(transform.position, transform.forward);
		}

		public static Ray Ray(this Transform transform, Vector3 dPosition)
		{
			return new Ray(transform.position + dPosition, transform.forward);
		}

		public static Plane Plane(this Transform transform)
		{
			return new Plane(transform.up, transform.position);
		}
	}
}


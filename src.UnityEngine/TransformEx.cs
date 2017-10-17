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
	}
}


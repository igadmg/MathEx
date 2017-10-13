using UnityEngine;



namespace MathEx
{
	public static class BoxColliderEx
	{
		public static Bounds GetColliderBounds(this BoxCollider collider)
		{
			return new Bounds(collider.center, collider.size);
		}
	}
}

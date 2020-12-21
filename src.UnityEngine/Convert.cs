using UnityEngine;


namespace MathEx
{
	public static class Convert
	{
		#region Conversion to Unity3D Math library
		public static Vector2 ToVector2(this vec2 v) => new Vector2(v.x, v.y);
		public static Vector3 ToVector3(this vec2 v) => new Vector3(v.x, v.y, 0);
		public static Vector3 ToVector3(this vec3 v) => new Vector3(v.x, v.y, v.z);
		public static Vector4 ToVector4(this vec4 v) => new Vector4(v.x, v.y, v.z, v.w);

		public static Rect ToRect(this aabb2 r) => new Rect(r.a.x, r.a.y, r.x, r.y);
		#endregion

		#region Conversion from Unity3D Math library
		public static vec2 ToVec2(this Vector2 v) => new vec2(v.x, v.y);
		public static vec2 ToVec2(this Vector3 v) => new vec2(v.x, v.y);
		public static vec2i ToVec2i(this Vector2 v) => new vec2i((int)v.x, (int)v.y);
		public static vec2i ToVec2i(this Vector2Int v) => new vec2i(v.x, v.y);
		public static vec3 ToVec3(this Vector3 v) => new vec3(v.x, v.y, v.z);
		public static ray ToRay(this Ray r) => new ray { origin = r.origin.ToVec3(), direction = r.direction.ToVec3() };
		public static plane ToPlane(this Plane p) => new plane(p.normal.ToVec3(), p.distance);
		public static aabb2 ToAaBb2(this Rect rect) => new aabb2(rect.GetMin().ToVec2(), rect.GetMax().ToVec2());
		#endregion
	}
}

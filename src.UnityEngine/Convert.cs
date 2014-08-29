using UnityEngine;


namespace MathEx
{
	public static class Convert
	{
		#region Conversion to Unity3D Math library
		public static Vector2 ToVector2(this vec2 v) { return new Vector2(v.x, v.y); }
		public static Vector3 ToVector3(this vec2 v) { return new Vector3(v.x, v.y, 0); }
		public static Vector3 ToVector3(this vec3 v) { return new Vector3(v.x, v.y, v.z); }
		public static Vector4 ToVector4(this vec4 v) { return new Vector4(v.x, v.y, v.z, v.w); }

        public static Rect ToRect(this aabb2 r) { return new Rect(r.a.x, r.a.y, r.x, r.y); }
		#endregion

		#region Conversion from Unity3D Math library
		public static vec2 ToVec2(this Vector2 v) { return new vec2(v.x, v.y); }
		public static vec2 ToVec2(this Vector3 v) { return new vec2(v.x, v.y); }
		public static vec3 ToVec3(this Vector3 v) { return new vec3(v.x, v.y, v.z); }
		public static aabb2 ToAaBb2(this Rect rect) { return new aabb2(rect.GetMin().ToVec2(), rect.GetMax().ToVec2()); }
		#endregion
	}
}

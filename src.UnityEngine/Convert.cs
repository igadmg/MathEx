using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace MathEx
{
	public static class Convert
	{
		#region Conversion to Unity3D Math library
		public static Vector2 ToVector2(vec2 v) { return new Vector2(v.x, v.y); }
		public static Vector3 ToVector3(vec3 v) { return new Vector3(v.x, v.y, v.z); }
		#endregion

		#region Conversion from Unity3D Math library
		public static vec2 ToVec2(Vector2 v) { return new vec2(v.x, v.y); }
		public static vec3 ToVec3(Vector3 v) { return new vec3(v.x, v.y, v.z); }
		public static aabb2 Toaabb2(Rect rect) { return new aabb2(ToVec2(rect.GetMin()), ToVec2(rect.GetMax())); }
		#endregion
	}
}

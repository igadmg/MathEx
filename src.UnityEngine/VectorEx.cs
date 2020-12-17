using System;
using System.Collections.Generic;
using UnityEngine;

namespace MathEx
{
	public static class VectorEx
	{
		public static Vector2 empty2 = new Vector2(float.NaN, float.NaN);
		public static Vector2 zero2 = new Vector2(0, 0);
		public static Vector3 empty3 = new Vector3(float.NaN, float.NaN, float.NaN);
		public static Vector4 empty4 = new Vector4(float.NaN, float.NaN, float.NaN, float.NaN);

		/// <summary>
		/// guiZ 1 / 2 ^ (log2(float)/2)
		/// 0.000000100000
		/// 0.000000110000
		/// 0.000000111000
		/// |
		/// v
		/// log2(float)/2 z levels.
		/// </summary>
		/// <value>The GUI z.</value>
		public static Vector3 guiZ {
			get { return new Vector3(0, 0, 1.0f / (2 << 8)); }
		}

		public static int quad(this Vector2 v)
		{
			if (v.x > 0)
				if (v.y > 0)
					return 0;
				else
					return 1;
			else
				if (v.y < 0)
				return 2;
			else
				return 3;
		}

		public static Vector2 X(this Vector2 v, float x)
		{
			return new Vector2(x, v.y);
		}

		public static Vector2 Y(this Vector2 v, float y)
		{
			return new Vector2(v.x, y);
		}

		public static Vector3 X(this Vector3 v, float x)
		{
			return new Vector3(x, v.y, v.z);
		}

		public static Vector3 Y(this Vector3 v, float y)
		{
			return new Vector3(v.x, y, v.z);
		}

		public static Vector3 Z(this Vector3 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}

		public static Vector2 dX(this Vector2 v, float dx)
		{
			return new Vector2(v.x + dx, v.y);
		}

		public static Vector2 dY(this Vector2 v, float dy)
		{
			return new Vector2(v.x, v.y + dy);
		}

		public static Vector3 dX(this Vector3 v, float dx)
		{
			return new Vector3(v.x + dx, v.y, v.z);
		}

		public static Vector3 dY(this Vector3 v, float dy)
		{
			return new Vector3(v.x, v.y + dy, v.z);
		}

		public static Vector3 dZ(this Vector3 v, float dz)
		{
			return new Vector3(v.x, v.y, v.z + dz);
		}

		public static Vector3 xyz(this Vector2 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}

		public static Vector3[] xyz(this Vector2[] vs, float z)
		{
			var r = new Vector3[vs.Length];
			for (int i = 0; i < vs.Length; i++)
				r[i] = vs[i].xyz(z);
			return r;
		}

		public static Vector3 xzy(this Vector2 v, float z)
		{
			return new Vector3(v.x, z, v.y);
		}

		public static Vector3 zxy(this Vector2 v, float z)
		{
			return new Vector3(z, v.x, v.y);
		}

		public static Vector2 nx(this Vector2 v) => v.X(-v.x);
		public static Vector2 ny(this Vector2 v) => v.Y(-v.y);

		public static Vector3 nx(this Vector3 v) => v.X(-v.x);
		public static Vector3 ny(this Vector3 v) => v.Y(-v.y);
		public static Vector3 nz(this Vector3 v) => v.Z(-v.z);

		public static Vector3 xzy(this Vector3 v)
		{
			return new Vector3(v.x, v.z, v.y);
		}

		public static Vector2 xy(this Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		public static Vector2 xy(this Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		public static Vector2 xz(this Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		public static Vector2 zx(this Vector3 v)
		{
			return new Vector2(v.z, v.x);
		}

		public static Vector2 yz(this Vector3 v)
		{
			return new Vector2(v.y, v.z);
		}

		public static Vector2 zy(this Vector3 v)
		{
			return new Vector2(v.z, v.y);
		}

		public static Vector3 Sub(this Vector3 l, Vector2 r)
		{
			return new Vector3(l.x - r.x, l.y - r.y, l.z);
		}


		public static Vector3 Invert(this Vector3 v) { return new Vector3(1.0f / v.x, 1.0f / v.y, 1.0f / v.z); }

		public static Vector2 Add(this Vector2 l, vec2 r) { return new Vector2(l.x + r.x, l.y + r.y); }
		public static Vector3 Add(this Vector3 l, vec2 r) { return new Vector3(l.x + r.x, l.y + r.y, l.z); }
		public static Vector3 Add(this Vector3 l, vec3 r) { return new Vector3(l.x + r.x, l.y + r.y, l.z + r.z); }

		public static Vector2 Sub(this Vector2 l, vec2 r) { return new Vector2(l.x - r.x, l.y - r.y); }
		public static Vector3 Sub(this Vector3 l, vec2 r) { return new Vector3(l.x - r.x, l.y - r.y, l.z); }
		public static Vector3 Sub(this Vector3 l, vec3 r) { return new Vector3(l.x - r.x, l.y - r.y, l.z - r.z); }

		public static Vector2 Mul(this Vector2 l, vec2i r) { return new Vector2(l.x * r.x, l.y * r.y); }
		public static Vector2 Mul(this Vector2 l, vec2 r) { return new Vector2(l.x * r.x, l.y * r.y); }
		public static Vector3 Mul(this Vector3 l, vec2 r) { return new Vector3(l.x * r.x, l.y * r.y, l.z); }
		public static Vector3 Mul(this Vector3 l, vec3 r) { return new Vector3(l.x * r.x, l.y * r.y, l.z * r.z); }

		public static Vector2 Mul(this Vector2 l, Vector2 r) { return new Vector2(l.x * r.x, l.y * r.y); }
		public static Vector3 Mul(this Vector3 l, Vector2 r) { return new Vector3(l.x * r.x, l.y * r.y, l.z); }
		public static Vector3 Mul(this Vector3 l, Vector3 r) { return new Vector3(l.x * r.x, l.y * r.y, l.z * r.z); }

		public static Vector2 Div(this Vector2 l, vec2i r) { return new Vector2(l.x / r.x, l.y / r.y); }
		public static Vector2 Div(this Vector2 l, vec2 r) { return new Vector2(l.x / r.x, l.y / r.y); }
		public static Vector3 Div(this Vector3 l, vec2 r) { return new Vector3(l.x / r.x, l.y / r.y, l.z); }
		public static Vector3 Div(this Vector3 l, vec3 r) { return new Vector3(l.x / r.x, l.y / r.y, l.z / r.z); }

		public static Vector2 Div(this Vector2 l, Vector2 r) { return new Vector2(l.x / r.x, l.y / r.y); }
		public static Vector3 Div(this Vector3 l, Vector2 r) { return new Vector3(l.x / r.x, l.y / r.y, l.z); }
		public static Vector3 Div(this Vector3 l, Vector3 r) { return new Vector3(l.x / r.x, l.y / r.y, l.z / r.z); }

		public static Vector2 Div(this Vector2 l, float x, float y) { return new Vector2(l.x / x, l.y / y); }
		public static Vector3 Div(this Vector3 l, float x, float y) { return new Vector3(l.x / x, l.y / y, l.z); }
		public static Vector3 Div(this Vector3 l, float x, float y, float z) { return new Vector3(l.x / x, l.y / y, l.z / z); }


		#region Boolean operation

		public static Vector3 Or(this Vector3 l, Vector3 r)
		{
			return new Vector3(r.x != 0 ? r.x : l.x, r.y != 0 ? r.y : l.y, r.z != 0 ? r.z : l.z);
		}

		#endregion

		public static bool IsEmpty(this Vector2 v)
		{
			return float.IsNaN(v.x) || float.IsNaN(v.y);
		}

		public static bool IsZero(this Vector2 v)
		{
			return v.x == 0 && v.y == 0;
		}

		public static bool IsZero(this Vector2 v, float eps)
		{
			return Mathf.Abs(v.x) < eps && Mathf.Abs(v.y) < eps;
		}

		public static bool IsEmpty(this Vector3 v)
		{
			return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
		}

		public static bool IsZero(this Vector3 v)
		{
			return v.x == 0 && v.y == 0 && v.z == 0;
		}

		public static bool IsZero(this Vector3 v, float eps)
		{
			return Mathf.Abs(v.x) < eps && Mathf.Abs(v.y) < eps && Mathf.Abs(v.z) < eps;
		}

		public static bool IsEmpty(this Vector4 v)
		{
			return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z) || float.IsNaN(v.w);
		}

		public static bool IsZero(this Vector4 v)
		{
			return v.x == 0 && v.y == 0 && v.z == 0 && v.w == 0;
		}

		public static bool IsZero(this Vector4 v, float eps)
		{
			return Mathf.Abs(v.x) < eps && Mathf.Abs(v.y) < eps && Mathf.Abs(v.z) < eps && Mathf.Abs(v.w) < eps;
		}

		public static Vector4 ToVector4(this Vector3 v, float w)
		{
			return new Vector4(v.x, v.y, v.z, w);
		}

		public static Vector4 ToPoint(this Vector2 v)
		{
			return new Vector4(v.x, v.y, 0.0f, 1.0f);
		}

		public static Vector4 ToPoint(this Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 1.0f);
		}

		public static Vector3 Rotate(this Vector3 v, Vector3 euler)
		{
			return Quaternion.Euler(euler) * v;
		}


		public static float Lerp(this Vector2 ab, float t)
		{
			return Mathf.Lerp(ab.x, ab.y, t);
		}

		public static float InvLerp(this Vector2 ab, float v)
		{
			return (v - ab.x) / (ab.y - ab.x);
		}

		public static int Clamp(this Vector2 v, int f)
		{
			return Mathf.Clamp(f, (int)v.x, (int)v.y);
		}

		public static float Clamp(this Vector2 v, float f)
		{
			return Mathf.Clamp(f, v.x, v.y);
		}



		public static float Dot(this Vector2 a, Vector2 b)
		{
			return Vector2.Dot(a, b);
		}

		public static float Dot(this Vector3 a, Vector3 b)
		{
			return Vector3.Dot(a, b);
		}

		public static Vector3 Cross(this Vector3 a, Vector3 b)
		{
			return Vector3.Cross(a, b);
		}

		#region Vector enumrators

		public static IEnumerable<Vector3> Line(this Vector3 v, Vector3 Direction, float Step, float Distance)
		{
			for (float d = 0; d < Distance; d += Step)
			{
				yield return v + Direction * d;
			}
			yield return v + Direction * Distance;
			yield break;
		}

		public static IEnumerable<Vector2> Circle(this Vector2 v, float Radius, int Sectors)
		{
			return v.Circle(Radius, Sectors, 0);
		}

		public static IEnumerable<Vector2> Circle(this Vector2 v, float Radius, int Sectors, float dA0)
		{
			float dA = 2 * Mathf.PI / Sectors;
			for (int i = 0; i < Sectors; i++)
			{
				float a = dA0 + i * dA;
				yield return v + Radius * (new Vector2(Mathf.Cos(a), Mathf.Sin(a)));
			}
			yield break;
		}

		public static IEnumerable<Vector3> Circle(this Vector3 v, float Radius, int Sectors)
		{
			return v.Circle(Radius, Sectors, 0);
		}

		public static IEnumerable<Vector3> Circle(this Vector3 v, float Radius, int Sectors, float dA0)
		{
			float dA = 2 * Mathf.PI / Sectors;
			for (int i = 0; i < Sectors; i++)
			{
				float a = dA0 + i * dA;
				yield return v + Radius * (new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0));
			}
			yield break;
		}

		public static IEnumerable<Vector2> Ellipse(this Vector2 v, Vector2 Radius, int Sectors)
		{
			return v.Ellipse(Radius, Sectors, 0);
		}

		public static IEnumerable<Vector2> Ellipse(this Vector2 v, Vector2 Radius, int Sectors, float dA0)
		{
			float dA = 2 * Mathf.PI / Sectors;
			for (int i = 0; i < Sectors; i++)
			{
				float a = dA0 + i * dA;
				yield return v + new Vector2(Mathf.Cos(a), Mathf.Sin(a)).Mul(Radius);
			}
			yield break;
		}

		public static IEnumerable<Vector3> Ellipse(this Vector3 v, Vector2 Radius, int Sectors)
		{
			return v.Ellipse(Radius, Sectors, 0);
		}

		public static IEnumerable<Vector3> Ellipse(this Vector3 v, Vector2 Radius, int Sectors, float dA0)
		{
			float dA = 2 * Mathf.PI / Sectors;
			for (int i = 0; i < Sectors; i++)
			{
				float a = dA0 + i * dA;
				yield return v + new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0).Mul(Radius);
			}
			yield break;
		}

		public static Vector2 Lerp(this float t, Vector2 a, Vector2 b)
			=> Vector2.Lerp(a, b, t);

		public static IEnumerable<Vector2> Lerp(this Vector2 a, Vector2 b, float dT)
		{
			float t = 0;
			while (t < 1)
			{
				yield return Vector2.Lerp(a, b, t); t += dT;
			}
			yield return Vector2.Lerp(a, b, 1);
			yield break;
		}

		public static Vector3 Lerp(this float t, Vector3 a, Vector3 b)
			=> Vector3.Lerp(a, b, t);

		public static IEnumerable<Vector3> Lerp(this Vector3 a, Vector3 b, float dT)
		{
			float t = 0;
			while (t < 1)
			{
				yield return Vector3.Lerp(a, b, t); t += dT;
			}
			yield return Vector3.Lerp(a, b, 1);
			yield break;
		}

		public static IEnumerable<Vector4> Lerp(this Vector4 a, Vector4 b, float dT)
		{
			float t = 0;
			while (t < 1)
			{
				yield return Vector4.Lerp(a, b, t); t += dT;
			}
			yield return Vector4.Lerp(a, b, 1);
			yield break;
		}

		public static IEnumerable<Vector3> Slerp(this Vector3 a, Vector3 b, float dT)
		{
			float t = 0;
			while (t < 1)
			{
				yield return Vector3.Slerp(a, b, t); t += dT;
			}
			yield return Vector3.Slerp(a, b, 1);
			yield break;
		}

		#endregion

		/// <summary>
		/// Returns random vector. Max values are defined by original vector.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector2 Random(this Vector2 v)
		{
			return new Vector2(UnityEngine.Random.value * v.x, UnityEngine.Random.value * v.y);
		}

		/// <summary>
		/// Returns random vector. Max values are defined by original vector.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector3 Random(this Vector3 v)
		{
			return new Vector3(UnityEngine.Random.value * v.x, UnityEngine.Random.value * v.y, UnityEngine.Random.value * v.z);
		}

		/// <summary>
		/// Returns random vector on a Circle. Radius is determined by original vector.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector2 RandomOnCircle(this Vector2 v)
		{
			Vector2 rv = UnityEngine.Random.insideUnitCircle;
			while (rv.x == 0 && rv.y == 0)
			{
				rv = UnityEngine.Random.insideUnitCircle;
			}

			return Mul(rv.normalized, v);
		}

		/// <summary>
		/// Returns random vector inside a Circle. Radius is determined by original vector.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector2 RandomInsideCircle(this Vector2 v)
		{
			return Mul(UnityEngine.Random.insideUnitCircle, v);
		}

		#region Vector collection enumrators

		/*
		public static IEnumerable<Vector3> Bezier(this ICollection<Vector3> ps, int steps)
		{
			bezier b = new bezier(new List<Vector3>(ps).ConvertAll(x => x.ToVec3()));

			for (int i = 0; i < steps; i++) {
				yield return (b.Evaluate((float)(i) / (steps - 1))).ToVector3();
			}

			yield break;
		}
		*/

		public static IEnumerable<Vector3> Spline(this ICollection<Vector3> ps, int steps)
		{
			for (int i = 0; i < steps; i++)
			{

			}

			yield break;
		}

		#endregion

		public static Vector2 Rotate(this Vector2 v, float degrees)
		{
			float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
			float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

			float tx = v.x;
			float ty = v.y;
			v.x = (cos * tx) - (sin * ty);
			v.y = (sin * tx) + (cos * ty);
			return v;
		}

		public static float Angle(this Vector2 from, Vector2 to)
		{
			float cos = Vector2.Dot(from, to);
			float sin = -Vector3.Cross(from, to).z;

			return Mathf.Acos(cos) * Mathf.Sign(sin);
		}

		public static Vector2 ToBarycentric(this Vector2 v, Vector2 a, Vector2 b, Vector2 c)
		{
			float d = (b.y - c.y) * (a.x - c.x) + (b.x - c.x) * (a.y - c.y);
			float x = ((b.y - c.y) * (v.x - c.x) + (b.x - c.x) * (v.y - c.y)) / d;
			float y = ((c.y - a.y) * (v.x - c.x) + (c.x - a.x) * (v.y - c.y)) / d;
			return new Vector2(x, y);
		}

		public static Vector4 CalcST(this Vector2 v, aabb2 rect)
		{
			return new Vector4(
				rect.size.x / v.x,
				rect.size.y / v.y,
				rect.a.x / v.x,
				rect.a.y / v.y
				);
		}

		public static Ray ScreenPointToRay(this Vector2 v) => Camera.main.ScreenPointToRay(v);
		public static Ray ScreenPointToRay(this Vector2 v, Camera camera) => camera.ScreenPointToRay(v);
		public static Ray ScreenPointToRay(this Vector3 v) => Camera.main.ScreenPointToRay(v);
		public static Ray ScreenPointToRay(this Vector3 v, Camera camera) => camera.ScreenPointToRay(v);

		public static Vector3 WorldToScreenPoint(this Vector3 v) => Camera.main.WorldToScreenPoint(v);
		public static Vector3 WorldToScreenPoint(this Vector3 v, Camera camera) => camera.WorldToScreenPoint(v);

		public static Rect SetSize(this Rect r, Vector2 size) => new Rect(r.position, size);

		public static bool IsInFrustum(this Vector3 v, float fov, float aspect, float zNear, float zFar)
		{
			float vMagnitude = v.sqrMagnitude;
			if (vMagnitude < zNear * zNear || vMagnitude > zFar * zFar)
				return false;

			float vCosX = v.Y(0).normalized.z;
			float vCosY = v.X(0).normalized.z;
			fov *= Mathf.Deg2Rad / 2.0f;
			float xCos = Mathf.Cos(fov * aspect);
			float yCos = Mathf.Cos(fov);

			return vCosX > xCos && vCosY > yCos;
		}

		public static Vector2 ToVector2(this float[] v)
		{
			return new Vector2(v[0], v[1]);
		}

		public static Vector2 ToVector2(this float[] v, int i)
		{
			return new Vector2(v[i + 0], v[i + 1]);
		}

		public static Vector3 ToVector3(this float[] v)
		{
			return new Vector3(v[0], v[1], v[2]);
		}

		public static Vector3 ToVector3(this float[] v, int i)
		{
			return new Vector3(v[i + 0], v[i + 1], v[i + 2]);
		}

		public static Vector3 Cross(this ValueTuple<Vector3, Vector3> v)
			=> Vector3.Cross(v.Item1, v.Item2);

		public static float Dot(this ValueTuple<Vector3, Vector3> v)
			=> Vector3.Dot(v.Item1, v.Item2);

		public static float PlaneDistance(this (Vector3 forward, Vector3 normal) v)
			=> (v.normal, v.forward - Vector3.zero).Dot();

		public static Vector3 PlaneIntersect(this (Vector3 origin, Vector3 forward, Vector3 normal) v)
			=> v.origin + v.forward * ((Vector3.zero - v.origin, v.normal).Dot()
							/ (v.forward, v.normal).Dot());

		public static Vector3 PlaneIntersect(this (Ray ray, Vector3 normal) v)
			=> (v.ray.origin, v.ray.direction, v.normal).PlaneIntersect();

		public static Vector3 Intersect(this (Ray ray, Plane plane) v)
		{
			if (v.plane.Raycast(v.ray, out float t))
				return v.ray.GetPoint(t);

			return empty3;
		}

		public static Vector3 Plane(this (Vector3 origin, Vector3 normal) v)
			=> v.origin + v.normal * (v.normal, Vector3.zero - v.origin).Dot();

		public static Vector3 Vector(this ValueTuple<Vector3, Component> v)
			=> (v.Item2.transform.position - v.Item1);

		public static Vector3 Vector(this ValueTuple<Component, Component> v)
			=> (v.Item2.transform.position - v.Item1.transform.position);

		public static float Distance(this ValueTuple<Vector2, Vector2> v)
			=> (v.Item1 - v.Item2).magnitude;

		public static float Distance(this ValueTuple<Vector3, Vector3> v)
			=> (v.Item1 - v.Item2).magnitude;

		public static float Distance(this ValueTuple<Component, Vector3> v)
			=> (v.Item1.transform.position - v.Item2).magnitude;

		public static float Distance(this ValueTuple<Component, Component> v)
			=> (v.Item1.transform.position - v.Item2.transform.position).magnitude;

		public static float Distance(this ValueTuple<GameObject, GameObject> v)
			=> (v.Item1.transform.position - v.Item2.transform.position).magnitude;

		public static float DistanceSquared(this ValueTuple<Component, Component> v)
			=> (v.Item1.transform.position - v.Item2.transform.position).sqrMagnitude;

		public static float DistanceSquared(this ValueTuple<GameObject, GameObject> v)
			=> (v.Item1.transform.position - v.Item2.transform.position).sqrMagnitude;
	}
}


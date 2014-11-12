using UnityEngine;

namespace MathEx
{
	public static class Matrix4x4Ex
	{
		public static Matrix4x4 cf2d = Matrix4x4.identity.Columns(Vector3.right, Vector3.forward, Vector3.up);

		public static Matrix4x4 Column(this Matrix4x4 m4x4, int column, Vector3 v)
		{
			m4x4.SetColumn(column, v);
			return m4x4;
		}

		public static Matrix4x4 Columns(this Matrix4x4 m4x4, params Vector3[] vs)
		{
			int i = 0;
			foreach (var v in vs) {
				m4x4.SetColumn(i++, v);
			}
			return m4x4;
		}

		public static Matrix4x4 Columns(this Matrix4x4 m4x4, params Vector4[] vs)
		{
			int i = 0;
			foreach (var v in vs) {
				m4x4.SetColumn(i++, v);
			}
			return m4x4;
		}

		public static Quaternion ToQuaternion(this Matrix4x4 m)
		{
			Quaternion q = new Quaternion();
			q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
			q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
			q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
			q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
			q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
			q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
			q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
			return q;
		}
	}
}

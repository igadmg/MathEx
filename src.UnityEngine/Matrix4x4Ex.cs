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
	}
}

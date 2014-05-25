using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class CoordinateSystem
	{
		public Vector3 right;
		public Vector3 forward;
		public Vector3 up;

		public CoordinateSystem()
		{
			right = Vector3.right;
			forward = Vector3.forward;
			up = Vector3.up;
		}

		public CoordinateSystem(Vector3 right, Vector3 forward, Vector3 up)
		{
			this.right = right;
			this.forward = forward;
			this.up = up;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class CoordinateSystem
	{
		public vec3 right;
		public vec3 forward;
		public vec3 up;

		public CoordinateSystem()
		{
			right = vec3.right;
			forward = vec3.forward;
			up = vec3.up;
		}

		public CoordinateSystem(vec3 right, vec3 forward, vec3 up)
		{
			this.right = right;
			this.forward = forward;
			this.up = up;
		}
	}
}

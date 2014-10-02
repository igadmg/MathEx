using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class ray
	{
		public vec3 origin;
		public vec3 direction;

		public ray()
		{
			origin = vec3.zero;
			direction = vec3.forward;
		}

		public ray(vec3 origin, vec3 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}
	}
}

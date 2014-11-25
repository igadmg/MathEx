using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class ray
	{
		public static readonly ray empty = new ray(vec3.empty, vec3.empty);

		public vec3 origin;
		public vec3 direction;

		public bool IsEmpty { get { return origin.isEmpty || direction.isEmpty; } }

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

		public float distance(vec3 point)
		{
			return (direction % (point - origin)).magnitude;
		}

		public vec3 projection(vec3 point)
		{
			return origin + direction * (direction * (point - origin));
		}
	}
}

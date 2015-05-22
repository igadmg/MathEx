namespace MathEx
{
	public class ray2
	{
		public static readonly ray2 empty = new ray2(vec2.empty, vec2.empty);

		public vec2 origin;
		public vec2 direction;

		public bool isEmpty { get { return origin.isEmpty || direction.isEmpty; } }

		public ray2()
		{
			origin = vec2.zero;
			direction = vec2.right;
		}

		public ray2(vec2 origin, vec2 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}

		public float distance(vec2 point)
		{
			return (direction.x * (origin.y - point.y) - (origin.x - point.x) * direction.y) / direction.length;
		}

		public float projection(vec2 point)
		{
			return direction * (point - origin);
		}

		public vec2 project(vec2 point)
		{
			return origin + direction * projection(point);
        }
	}

	public class ray
	{
		public static readonly ray empty = new ray(vec3.empty, vec3.empty);

		public vec3 origin;
		public vec3 direction;

		public bool isEmpty { get { return origin.isEmpty || direction.isEmpty; } }

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

		public float projection(vec3 point)
		{
			return direction * (point - origin);
		}

		public vec3 project(vec3 point)
		{
			return origin + direction * projection(point);
		}
	}
}

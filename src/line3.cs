namespace MathEx
{
	public class line3_segment
	{
		public static readonly line3_segment empty = new line3_segment(vec3.empty, vec3.empty);

		vec3 a;
		vec3 b;

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }

		public vec3 center { get { return (a + b) / 2; } }

		public line3_segment()
		{
			a = vec3.zero;
			b = a + vec3.forward;
		}

		public line3_segment(vec3 a, vec3 b)
		{
			this.a = a;
			this.b = b;
		}
	}
}
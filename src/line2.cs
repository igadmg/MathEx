namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	public class line2
	{
		public float a;
		public float b;
		public float c;


		public line2()
		{

		}
	}

	public class line2_segment
	{
		public static readonly line2_segment empty = new line2_segment(vec2.empty, vec2.empty);

		vec2 a;
		vec2 b;

		public bool isEmpty { get { return a.isEmpty || b.isEmpty; } }

		public vec2 center { get { return (a + b) / 2; } }

		public line2_segment()
		{
			a = vec2.zero;
			b = a + vec2.up;
		}

		public line2_segment(vec2 a, vec2 b)
		{
			this.a = a;
			this.b = b;
		}
	}
}
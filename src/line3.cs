using System;



namespace MathEx
{
	[Serializable]
	public class line3_segment
	{
		public static readonly line3_segment zero = new line3_segment(vec3.zero, vec3.zero);
		public static readonly line3_segment one = new line3_segment(vec3.zero, vec3.one);
		public static readonly line3_segment empty = new line3_segment(vec3.empty, vec3.empty);

		public static readonly line3_segment right = new line3_segment(vec3.zero, vec3.right);
		public static readonly line3_segment left = new line3_segment(vec3.zero, vec3.left);
		public static readonly line3_segment up = new line3_segment(vec3.zero, vec3.up);
		public static readonly line3_segment down = new line3_segment(vec3.zero, vec3.down);
		public static readonly line3_segment forward = new line3_segment(vec3.zero, vec3.forward);
		public static readonly line3_segment backward = new line3_segment(vec3.zero, vec3.backward);

		public vec3 a;
		public vec3 b;

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

		public vec3 value(float t)
		{
			return t.Lerp(a, b);
		}
	}
}

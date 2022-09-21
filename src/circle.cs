using System;

namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	[Serializable]
	public class circle
	{
		public static readonly circle zero = new circle(vec2.zero, 0f);
		public static readonly circle one = new circle(vec2.zero, 1f);
		public static readonly circle empty = new circle(vec2.empty, 0f);

		public vec2 o;
		public float r;

		public bool isEmpty => o.isEmpty || float.IsNaN(r);
		public bool isZero => o.isZero && r == 0;

		public aabb2 bound => new aabb2(o - r.xx(), o + r.xx());

		public circle(vec2 o, float r)
		{
			this.o = o;
			this.r = r;
		}

		public circle(vec2 a, vec2 b, vec2 c)
		{
			float det = (a.x - b.x) * (b.y - c.y) - (b.x - c.x) * (a.y - b.y);

			if (MathExOps.Abs(det) < float.Epsilon)
			{
				o = vec2.empty;
				r = 0f;
			}
			else
			{
				float offset = b ^ b;
				float bc = ((a ^ a) - offset) / 2f;
				float cd = (offset - (c ^ c)) / 2f;

				float idet = 1f / det;

				o = new vec2((bc * (b.y - c.y) - cd * (a.y - b.y)) * idet
					, (cd * (a.x - b.x) - bc * (b.x - c.x)) * idet);
				r = (a - o).length;
			}
		}

		public circle(vec2 a, vec2 b, vec2 tangent, bool _)
		{
			float ca = tangent.y - a.y;
			float cb = -(tangent.x - a.x);

			float a1 = cb;
			float b1 = -ca;
			float a2 = a.x - b.x;
			float b2 = a.y - b.y;

			float d = a1 * b2 - b1 * a2;
			if (d == 0)
			{
				o = vec2.empty;
				r = 0f;
			}
			else
			{
				float c1 = cb * a.x - ca * a.y;
				float c2 = 0.5f * (a.x * a.x + a.y * a.y - b.x * b.x - b.y * b.y);

				o = new vec2((c1 * b2 - b1 * c2) / d, (a1 * c2 - c1 * a2) / d);
				r = (a - o).length;
			}
		}

		public vec2 value(float t)
		{
			float a = t * 2 * MathExOps.PI;
			return new vec2(r * a.Cos(), r * a.Sin()) + o;
		}
	}
}

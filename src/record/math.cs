namespace MathEx.Record
{
	/*
	public record struct vec2i(int x, int y)
	{
		public static vec2i zero => new (x: 0, y: 0);

		public static vec2i operator +(vec2i a, vec2i b) => new vec2i(a.x + b.x, a.y + b.y);
		public static vec2i operator -(vec2i a, vec2i b) => new vec2i(a.x - b.x, a.y - b.y);

		public aabb2i hw(vec2i _) => new aabb2i(this, this + _);
	}

	public record struct aabb2i(vec2i a, vec2i b)
	{
		public static aabb2i zero => new aabb2i(vec2i.zero, vec2i.zero);

		public int w => b.x - a.x;
		public int h => b.y - a.y;

		public static aabb2i xywh(vec2i xy, vec2i wh)
			=> new aabb2i(xy, xy + wh);

		public vec2i clamp(vec2i p) => new(p.x.Clamp(a.x, b.x), p.y.Clamp(a.y, b.y));
	}
	*/
}

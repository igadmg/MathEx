namespace MathEx
{
	public static class vecEc
	{
		public static int Clamp(this vec2 v, int f) { return MathEx.Clamp(f, (int)v.x, (int)v.y); }
		public static float Clamp(this vec2 v, float f) { return MathEx.Clamp(f, v.x, v.y); }
		public static vec2 Clamp(this vec2 v, vec2 min, vec2 max) { return MathEx.Clamp(v, min, max); }

		public static int Clamp(this vec2i v, int f) { return MathEx.Clamp(f, v.x, v.y); }
		public static float Clamp(this vec2i v, float f) { return MathEx.Clamp(f, v.x, v.y); }
		public static vec2i Clamp(this vec2i v, vec2i min, vec2i max) { return MathEx.Clamp(v, min, max); }

		public static vec2 Mul(this vec2 l, float x, float y) { return new vec2(l.x * x, l.y * y); }
		public static vec2 Mul(this vec2 l, vec2 r) { return new vec2(l.x * r.x, l.y * r.y); }
		public static vec2 Div(this vec2 l, float x, float y) { return new vec2(l.x / x, l.y / y); }
		public static vec2 Div(this vec2 l, vec2 r) { return new vec2(l.x / r.x, l.y / r.y); }

		public static vec2 Mul(this vec2 l, int x, int y) { return new vec2(l.x * x, l.y * y); }
		public static vec2 Mul(this vec2 l, vec2i r) { return new vec2(l.x * r.x, l.y * r.y); }
		public static vec2 Div(this vec2 l, int x, int y) { return new vec2(l.x / x, l.y / y); }
		public static vec2 Div(this vec2 l, vec2i r) { return new vec2(l.x / r.x, l.y / r.y); }

		public static vec3 Div(this vec3 l, vec3 r) { return new vec3(l.x / r.x, l.y / r.y, l.z / r.z); }

		public static vec4 Div(this vec4 l, vec4 r) { return new vec4(l.x / r.x, l.y / r.y, l.z / r.z, l.w / r.w); }
	}
}
using System;

namespace MathEx
{
	public static class TransfromStackEx
	{
		public static vec2 local2(this ValueTuple<vec4, TransformStack> p) => p.Item1.xy();
		public static vec2 world2(this ValueTuple<vec4, TransformStack> p) => p.Item2 != null ? (p.Item1 * p.Item2).xy() : p.Item1.xy();

		public static vec3 local3(this ValueTuple<vec4, TransformStack> p) => p.Item1.xyz();
		public static vec3 world3(this ValueTuple<vec4, TransformStack> p) => p.Item2 != null ? (p.Item1 * p.Item2).xyz() : p.Item1.xyz();

		public static rect2 local(this ValueTuple<rect2, TransformStack> p) => p.Item1;
		public static rect2 world(this ValueTuple<rect2, TransformStack> p) => p.Item2 != null ? rect2.xywh(p.Item1.xy * p.Item2, p.Item1.size % p.Item2) : p.Item1;
	}
}

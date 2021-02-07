using System;

namespace MathEx
{
	public static class TransfromStackEx
	{
		public static vec2 local2(this ValueTuple<vec4, TransformStack> p) => p.Item1.xy();
		public static vec2 world2(this ValueTuple<vec4, TransformStack> p) => (p.Item1 * p.Item2).xy();

		public static vec3 local3(this ValueTuple<vec4, TransformStack> p) => p.Item1.xyz();
		public static vec3 world3(this ValueTuple<vec4, TransformStack> p) => (p.Item1 * p.Item2).xyz();
	}
}

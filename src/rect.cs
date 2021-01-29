using System;
using System.Runtime.InteropServices;

namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct rect2
	{
		public vec2 a;
		public vec2 size;
		public vec2 b {
			get => a + size;
			set => size = value - a;
		}

		public rect2(vec2 xy, vec2 wh)
		{
			a = xy;
			size = wh;
		}

		public static rect2 wh(float w, float h) => new rect2(vec2.zero, (w, h));
		public static rect2 wh(vec2 wh)	=> new rect2(vec2.zero, wh);
		public static rect2 wh(vec2i wh) => new rect2(vec2.zero, (vec2)wh);

		public static rect2 xywh(float x, float y, float w, float h) => new rect2((x, y), (w, h));
		public static rect2 xywh(vec2 xy, vec2 wh) => new rect2(xy, wh);
		public static rect2 xywh(vec2 xy, vec2i wh) => new rect2(xy, (vec2)wh);
		public static rect2 xywh(vec2i xy, vec2 wh) => new rect2((vec2)xy, wh);
		public static rect2 xywh(vec2i xy, vec2i wh) => new rect2((vec2)xy, (vec2)wh);
	}
}

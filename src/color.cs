using System;
using System.Globalization;
using System.Runtime.InteropServices;
using SystemEx;



namespace MathEx
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct color
	{
		public float r;
		public float g;
		public float b;
		public float a;

		public readonly static color zero = new color(0, 0, 0, 0);
		public readonly static color empty = new color(float.NaN, float.NaN, float.NaN, float.NaN);

		public readonly static color black = new color(0, 0, 0, 1);
		public readonly static color white = new color(1, 1, 1, 1);

		public readonly static color red = new color(1, 0, 0, 1);
		public readonly static color green = new color(0, 1, 0, 1);
		public readonly static color blue = new color(0, 0, 1, 1);

		public static color yellow;
		public static color olive;
		public static color magenta;
		public static color purple;
		public static color cyan;
		public static color teal;

		public static color light_blue;
		public static color dark_blue;


		public bool isEmpty { get { return float.IsNaN(r) || float.IsNaN(g) || float.IsNaN(b) || float.IsNaN(a); } }


		public color(float r, float g, float b, float a = 1)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public static explicit operator color(color_hsl hsl)
		{
			if (hsl.s == 0.0f)
			{
				return new color(hsl.l, hsl.l, hsl.l, hsl.a);
			}
			else
			{
				float hue = hsl.h / 60.0f;
				int i = (int)hue;
				float f = hue - i;

				float p = (hsl.l <= 0.5f)
					? hsl.l * (1.0f - hsl.s)
					: hsl.l * (1.0f + hsl.s) - hsl.s;

				float s1 = hsl.s * (1.0f - 2.0f * f);
				float s2 = hsl.s * (2.0f * f - 1.0f);
				float t = (hsl.l <= 0.5f)
					? hsl.l * (1.0f - s1)
					: hsl.l * (1.0f - s2);
				float q = (hsl.l <= 0.5f)
					? hsl.l * (1.0f - s2) - s1
					: hsl.l * (1.0f - s1) - s2;

				float u = (hsl.l <= 0.5f)
					? hsl.l * (1.0f + hsl.s)
					: hsl.l * (1.0f - hsl.s) + hsl.s;

				switch (i)
				{
					case 0: return new color(u, t, p, hsl.a);
					case 1: return new color(q, u, p, hsl.a);
					case 2: return new color(p, u, t, hsl.a);
					case 3: return new color(p, q, u, hsl.a);
					case 4: return new color(t, p, u, hsl.a);
					case 5: return new color(u, p, q, hsl.a);
				}
			}

			return color.empty;
		}

		public static explicit operator color(colorb cb)
			=> new color(cb.r.Normalized(), cb.g.Normalized(), cb.b.Normalized(), cb.a.Normalized());

		public static explicit operator color(color_hsv hsv)
		{
			if (hsv.s == 0.0f)
			{
				return new color(hsv.v, hsv.v, hsv.v, hsv.a);
			}
			else
			{
				float hue = hsv.h / 60.0f;
				int i = (int)hue;
				float f = hue - i;

				float p = hsv.v * (1.0F - hsv.s);
				float q = hsv.v * (1.0F - hsv.s * f);
				float t = hsv.v * (1.0F - hsv.s * (1.0F - f));

				switch (i)
				{
					case 0: return new color(hsv.v, t, p, hsv.a);
					case 1: return new color(q, hsv.v, p, hsv.a);
					case 2: return new color(p, hsv.v, t, hsv.a);
					case 3: return new color(p, q, hsv.v, hsv.a);
					case 4: return new color(t, p, hsv.v, hsv.a);
					case 5: return new color(hsv.v, p, q, hsv.a);
				}
			}

			return color.empty;
		}

		public color reset(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;

			return this;
		}

#if UNITY || UNITY_5_3_OR_NEWER
		public static implicit operator UnityEngine.Color(color c)
		{
			return new UnityEngine.Color(c.r, c.g, c.b, c.a);
		}

		public static implicit operator color(UnityEngine.Color c)
		{
			return new color(c.r, c.g, c.b, c.a);
		}
#endif
	};

	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct colorb
		: IComparable<colorb>
		, IEquatable<colorb>
	{
		public byte r;
		public byte g;
		public byte b;
		public byte a;

		// Example - Color.RED instead of RED
		// Custom raylib color palette for amazing visuals
		public readonly static colorb LIGHTGRAY = new colorb(200, 200, 200, 255);
		public readonly static colorb GRAY = new colorb(130, 130, 130, 255);
		public readonly static colorb DARKGRAY = new colorb(80, 80, 80, 255);
		public readonly static colorb YELLOW = new colorb(253, 249, 0, 255);
		public readonly static colorb GOLD = new colorb(255, 203, 0, 255);
		public readonly static colorb ORANGE = new colorb(255, 161, 0, 255);
		public readonly static colorb PINK = new colorb(255, 109, 194, 255);
		public readonly static colorb RED = new colorb(230, 41, 55, 255);
		public readonly static colorb MAROON = new colorb(190, 33, 55, 255);
		public readonly static colorb GREEN = new colorb(0, 228, 48, 255);
		public readonly static colorb LIME = new colorb(0, 158, 47, 255);
		public readonly static colorb DARKGREEN = new colorb(0, 117, 44, 255);
		public readonly static colorb SKYBLUE = new colorb(102, 191, 255, 255);
		public readonly static colorb BLUE = new colorb(0, 121, 241, 255);
		public readonly static colorb DARKBLUE = new colorb(0, 82, 172, 255);
		public readonly static colorb PURPLE = new colorb(200, 122, 255, 255);
		public readonly static colorb VIOLET = new colorb(135, 60, 190, 255);
		public readonly static colorb DARKPURPLE = new colorb(112, 31, 126, 255);
		public readonly static colorb BEIGE = new colorb(211, 176, 131, 255);
		public readonly static colorb BROWN = new colorb(127, 106, 79, 255);
		public readonly static colorb DARKBROWN = new colorb(76, 63, 47, 255);
		public readonly static colorb WHITE = new colorb(255, 255, 255, 255);
		public readonly static colorb BLACK = new colorb(0, 0, 0, 255);
		public readonly static colorb BLANK = new colorb(0, 0, 0, 0);
		public readonly static colorb MAGENTA = new colorb(255, 0, 255, 255);
		public readonly static colorb RAYWHITE = new colorb(245, 245, 245, 255);

		public readonly static colorb TRANSPARENT = new colorb(0, 0, 0, 0);

		public colorb(byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public colorb(int r, int g, int b, int a)
		{
			this.r = Convert.ToByte(r);
			this.g = Convert.ToByte(g);
			this.b = Convert.ToByte(b);
			this.a = Convert.ToByte(a);
		}

		public colorb(Span<byte> c)
			: this(c[0], c[1], c[2], c[3])
		{
		}

		public static bool operator ==(colorb l, colorb r) => l.Equals(r);
		public static bool operator !=(colorb l, colorb r) => !(l == r);

		public override string ToString() => "{0}, {1}, {2}, {3}".format(CultureInfo.InvariantCulture, r, g, b, a);

		public int CompareTo(colorb other) => this == other ? 0 : -1;
		public bool Equals(colorb other) => r == other.r && g == other.g && b == other.b && a == other.a;
		public override bool Equals(object obj) => obj is colorb && Equals((colorb)obj);
		public override int GetHashCode() => ObjectEx.GetHashCode(r, g, b, a);

		public static explicit operator colorb(color c)
			=> new colorb(c.r.DenormalizeByte(), c.g.DenormalizeByte(), c.b.DenormalizeByte(), c.a.DenormalizeByte());
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct color_hsl
	{
		public float h;
		public float s;
		public float l;
		public float a;

		public color_hsl(float h, float s, float l, float a = 1)
		{
			this.h = h;
			this.s = s;
			this.l = l;
			this.a = a;
		}

		public static explicit operator color_hsl(color rgb)
		{
			color_hsl hsl = new color_hsl();
			float r = rgb.r, g = rgb.g, b = rgb.b;

			float uMax = r.max(g).max(b);
			float uMin = r.min(g).min(b);

			hsl.a = rgb.a;
			hsl.l = (uMax + uMin) / 2.0f;

			if (uMax != 0)
			{
				hsl.s = (uMax - uMin) / uMax;
			}
			else
			{
				hsl.s = 0;
			}

			if (hsl.s == 0)
			{
				hsl.s = -1.0f;
			}
			else
			{
				float d = uMax - uMin;

				if (uMax == r)
				{
					hsl.h = (g - b) / d;
				}
				else if (uMax == g)
				{
					hsl.h = 2.0f + (b - r) / d;
				}
				else
				{
					hsl.h = 4.0f + (r - g) / d;
				}

				hsl.h *= 60.0f;

				if (hsl.h < 0) hsl.h += 360.0f;
				else if (hsl.h >= 360.0f) hsl.h -= 360.0f;
			}

			return hsl;
		}

		public color_hsl normalize()
		{
			while (h >= 360.0f) h -= 360.0f;
			while (h < 0) h += 360.0f;
			if (s > 1.0f) s = 1.0f;
			else if (s < 0) s = 0;
			if (l > 1.0f) l = 1.0f;
			else if (l < 0) l = 0;

			return this;
		}
	};

	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct color_hsv
	{
		public float h;
		public float s;
		public float v;
		public float a;

		public color_hsv(float h, float s, float v, float a = 1)
		{
			this.h = h;
			this.s = s;
			this.v = v;
			this.a = a;
		}

		public static explicit operator color_hsv(color rgb)
		{
			color_hsv hsv = new color_hsv();
			float r = rgb.r, g = rgb.g, b = rgb.b;

			float uMax = r.max(g).max(b);
			float uMin = r.min(g).min(b);

			hsv.a = rgb.a;
			hsv.v = uMax;

			if (uMax != 0)
			{
				hsv.s = (uMax - uMin) / uMax;
			}
			else
			{
				hsv.s = 0;
			}

			if (hsv.s == 0)
			{
				hsv.s = -1.0f;
			}
			else
			{
				float d = uMax - uMin;

				if (uMax == r)
				{
					hsv.h = (g - b) / d;
				}
				else if (uMax == g)
				{
					hsv.h = 2.0f + (b - r) / d;
				}
				else
				{
					hsv.h = 4.0f + (r - g) / d;
				}

				hsv.h *= 60.0f;

				if (hsv.h < 0) hsv.h += 360.0f;
				else if (hsv.h >= 360.0f) hsv.h -= 360.0f;
			}

			return hsv;
		}

		public color_hsv normalize()
		{
			while (h >= 360.0f) h -= 360.0f;
			while (h < 0) h += 360.0f;
			if (s > 1.0f) s = 1.0f;
			else if (s < 0) s = 0;
			if (v > 1.0f) v = 1.0f;
			else if (v < 0) v = 0;

			return this;
		}
	};


}

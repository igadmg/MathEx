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

		public static vec2 Adc(this vec2 v, float th) { return new vec2(MathEx.Abs(v.x) < th ? 0 : MathEx.Sign(v.x), MathEx.Abs(v.y) < th ? 0 : MathEx.Sign(v.y)); }

		public static vec2 Add(this vec2 l, float x, float y) { return new vec2(l.x + x, l.y + y); }
		public static vec2 Add(this vec2 l, vec2 r) { return new vec2(l.x + r.x, l.y + r.y); }
		public static vec2 Sub(this vec2 l, float x, float y) { return new vec2(l.x - x, l.y - y); }
		public static vec2 Sub(this vec2 l, vec2 r) { return new vec2(l.x - r.x, l.y - r.y); }
		public static vec2 Mul(this vec2 l, float x, float y) { return new vec2(l.x * x, l.y * y); }
		public static vec2 Mul(this vec2 l, vec2 r) { return new vec2(l.x * r.x, l.y * r.y); }
		public static vec2 Div(this vec2 l, float v) { return new vec2(l.x / v, l.y / v); }
		public static vec2 Div(this vec2 l, float x, float y) { return new vec2(l.x / x, l.y / y); }
		public static vec2 Div(this vec2 l, vec2 r) { return new vec2(l.x / r.x, l.y / r.y); }

		public static vec2 Mul(this vec2 l, int x, int y) { return new vec2(l.x * x, l.y * y); }
		public static vec2 Mul(this vec2 l, vec2i r) { return new vec2(l.x * r.x, l.y * r.y); }
		public static vec2 Div(this vec2 l, int v) { return new vec2(l.x / v, l.y / v); }
		public static vec2 Div(this vec2 l, int x, int y) { return new vec2(l.x / x, l.y / y); }
		public static vec2 Div(this vec2 l, vec2i r) { return new vec2(l.x / r.x, l.y / r.y); }

		public static vec3 Mul(this vec3 l, vec3 r) { return new vec3(l.x * r.x, l.y * r.y, l.z * r.z); }
		public static vec3 Div(this vec3 l, vec3 r) { return new vec3(l.x / r.x, l.y / r.y, l.z / r.z); }

		public static vec4 Div(this vec4 l, vec4 r) { return new vec4(l.x / r.x, l.y / r.y, l.z / r.z, l.w / r.w); }


		public static vec2 Floor(this vec2 v)
		{
			return new vec2(UnityEngine.Mathf.Floor(v.x), UnityEngine.Mathf.Floor(v.y));
		}


		public static vec2i X(this vec2i v, int x) { return new vec2i(x, v.y); }
		public static vec2i Y(this vec2i v, int y) { return new vec2i(v.x, y); }

		public static vec2 X(this vec2 v, float x) { return new vec2(x, v.y); }
		public static vec2 Y(this vec2 v, float y) { return new vec2(v.x, y); }

		public static vec3i X(this vec3i v, int x) { return new vec3i(x, v.y, v.z); }
		public static vec3i Y(this vec3i v, int y) { return new vec3i(v.x, y, v.z); }
		public static vec3i Z(this vec3i v, int z) { return new vec3i(v.x, v.y, z); }

		public static vec3 X(this vec3 v, float x) { return new vec3(x, v.y, v.z); }
		public static vec3 Y(this vec3 v, float y) { return new vec3(v.x, y, v.z); }
		public static vec3 Z(this vec3 v, float z) { return new vec3(v.x, v.y, z); }

		public static vec2 dX(this vec2 v, float dx) { return new vec2(v.x + dx, v.y); }
		public static vec2 dY(this vec2 v, float dy) { return new vec2(v.x, v.y + dy); }

		public static vec3 dX(this vec3 v, float dx) { return new vec3(v.x + dx, v.y, v.z); }
		public static vec3 dY(this vec3 v, float dy) { return new vec3(v.x, v.y + dy, v.z); }
		public static vec3 dZ(this vec3 v, float dz) { return new vec3(v.x, v.y, v.z + dz);	}

		public static vec2 iX(this vec2 v) { return new vec2(-v.x, v.y); }
		public static vec2 iY(this vec2 v) { return new vec2(v.x, -v.y); }


		public static vec3i xyz(this vec2i v, int z) { return new vec3i(v.x, v.y, z); }
		public static vec3i zxy(this vec2i v, int z) { return new vec3i(z, v.x, v.y); }
		public static vec3i yzx(this vec2i v, int z) { return new vec3i(v.y, z, v.x); }

		public static vec3 xyz(this vec2 v, float z)
		{
			return new vec3(v.x, v.y, z);
		}

		public static vec3[] xyz(this vec2[] vs, float z)
		{
			var r = new vec3[vs.Length];
			for (int i = 0; i < vs.Length; i++)
				r[i] = vs[i].xyz(z);
			return r;
		}

		public static vec3 xzy(this vec2 v, float z)
		{
			return new vec3(v.x, z, v.y);
		}

		public static vec3 zxy(this vec2 v, float z)
		{
			return new vec3(z, v.x, v.y);
		}

		public static vec3 yxz(this vec2 v, float z)
		{
			return new vec3(v.y, v.x, z);
		}

		public static vec2i xy(this vec3i v) { return new vec2i(v.x, v.y); }
		public static vec2i yz(this vec3i v) { return new vec2i(v.y, v.z); }
		public static vec2i zx(this vec3i v) { return new vec2i(v.z, v.x); }

		public static vec3 xzy(this vec3 v)
		{
			return new vec3(v.x, v.z, v.y);
		}

		public static vec2 xy(this vec3 v)
		{
			return new vec2(v.x, v.y);
		}

		public static vec2 xy(this vec4 v)
		{
			return new vec2(v.x, v.y);
		}

		public static vec2 xz(this vec3 v)
		{
			return new vec2(v.x, v.z);
		}

		public static vec2 zx(this vec3 v)
		{
			return new vec2(v.z, v.x);
		}

		public static vec2 yz(this vec3 v)
		{
			return new vec2(v.y, v.z);
		}

		public static vec2 zy(this vec3 v)
		{
			return new vec2(v.z, v.y);
		}
	}
}
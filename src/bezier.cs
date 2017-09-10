using System.Collections.Generic;

namespace MathEx
{
	public class bezier
	{
		public vec3[] p;


		public bezier()
		{
			p = new vec3[] {
				vec3.zero, new vec3(0, 0, 1), new vec3(1, 0, 1), new vec3(1, 0, 0)
			};
		}

		public bezier(params vec3[] p)
		{
			System.Diagnostics.Debug.Assert(p.Length == 4);

			this.p = new vec3[p.Length];
			p.CopyTo(this.p, 0);
		}

		public bezier(ICollection<vec3> p)
		{
			System.Diagnostics.Debug.Assert(p.Count == 4);

			this.p = new vec3[p.Count];
			p.CopyTo(this.p, 0);
		}

		public vec3 Derivative(float t)
		{
			float t1 = 1f - t;
			return
				(p[1] - p[0]) * 3f * t1 * t1
				+ (p[2] - p[1]) * 6f * t1 * t
				+ (p[3] - p[2]) * 3f * t * t;
		}

		public vec3 Evaluate(float t)
		{
			float t1 = 1f - t;
			return 
				p[0] * t1 * t1 * t1
				+ p[1] * 3f * t * t1 * t1
				+ p[2] * 3f * t * t * t1
				+ p[3] * t * t * t;
		}

		public static vec3 Derivative(vec3[] p, int i, float t)
		{
			float t1 = 1f - t;
			return
				(p[i + 1] - p[i + 0]) * 3f * t1 * t1
				+ (p[i + 2] - p[i + 1]) * 6f * t1 * t
				+ (p[i + 3] - p[i + 2]) * 3f * t * t;
		}

		public static vec3 Evaluate(vec3[] p, int i, float t)
		{
			float t1 = 1f - t;
			return
				p[i + 0] * t1 * t1 * t1
				+ p[i + 1] * 3f * t * t1 * t1
				+ p[i + 2] * 3f * t * t * t1
				+ p[i + 3] * t * t * t;
		}
	}

	public class bezier_spline
	{
		public enum control_mode
		{
			free,
			aligned,
			mirrored,
		}

		public bool loop = false;
		public vec3[] p;
		public control_mode[] m;

		public bezier_spline()
		{
			p = new vec3[] { vec3.zero };
			m = new control_mode[] { control_mode.free };
		}

		public bezier_spline(int size)
		{
			p = new vec3[1 + size * 3];
			m = new control_mode[size];
		}

		public int length
		{
			get { return p.Length / 3; }
		}

		public vec3 Evaluate(float t)
		{
			int i;
			if (t >= 1f)
			{
				t = 1f;
				i = p.Length - 4;
			}
			else
			{
				t = MathEx.Clamp(t, 0, 1) * length;
				i = (int)t;
				t -= i;
				i *= 3;
			}

			return bezier.Evaluate(p, i, t);
		}

		public vec3 Derivative(float t)
		{
			int i;
			if (t >= 1f)
			{
				t = 1f;
				i = p.Length - 4;
			}
			else
			{
				t = MathEx.Clamp(t, 0, 1) * length;
				i = (int)t;
				t -= i;
				i *= 3;
			}

			return bezier.Derivative(p, i, t);
		}

		public vec3 this[int i]
		{
			get
			{
				return p[i];
			}
			set
			{
				p[i] = value;
				update_tangents(i);
			}
		}

		public vec3 getNode(int i)
		{
			return p[i * 3];
		}

		public void setNode(int i, vec3 p)
		{
			this.p[i * 3] = p;
			update_tangents(i * 3);
		}

		public control_mode getControlMode(int i)
		{
			return m[i];
		}

		public void setControlMode(int i, control_mode mode)
		{
			m[i] = mode;
			update_tangents(3 * i);
		}

		protected void update_tangents(int i)
		{
			int mi = (i + 1) / 3;
			control_mode mode = m[mi];
			if (mode == control_mode.free || !loop && (mi == 0 || mi == m.Length - 1))
			{
				return;
			}

			int pi = mi * 3;

			int fixedIndex, enforcedIndex;
			if (i <= pi)
			{
				fixedIndex = pi - 1;
				if (fixedIndex < 0)
				{
					fixedIndex = p.Length - 2;
				}
				enforcedIndex = pi + 1;
				if (enforcedIndex >= p.Length)
				{
					enforcedIndex = 1;
				}
			}
			else
			{
				fixedIndex = pi + 1;
				if (fixedIndex >= p.Length)
				{
					fixedIndex = 1;
				}
				enforcedIndex = pi - 1;
				if (enforcedIndex < 0)
				{
					enforcedIndex = p.Length - 2;
				}
			}

			vec3 middle = p[pi];
			vec3 enforcedTangent = middle - p[fixedIndex];
			if (mode == control_mode.aligned)
			{
				enforcedTangent = enforcedTangent.normalized * (middle - p[enforcedIndex]).length;
			}
			p[enforcedIndex] = middle + enforcedTangent;
		}
	}
}

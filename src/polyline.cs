using System.Collections.Generic;
using SystemEx;

namespace MathEx
{
	public class polyline3
	{
		public vec3[] p;
	}

	public class polyline_builder<T>
	{
		protected static MathTypeTag<T> mtt = MathTypeTag<T>.Get();
		public List<T> points = new List<T>();

		public void add(T p)
		{
			add(p, false);
		}

		public void add(T p, bool optimize)
		{
			if (!optimize || points.Count < 2)
			{
				points.Add(p);
			}
			else
			{
				T dir = mtt.diff(points.at(-1), points.at(-2));
				dir = mtt.mul(1f / mtt.scalar(dir), dir);
				T pdir = mtt.diff(p, points.at(-2));
				pdir = mtt.mul(1f / mtt.scalar(pdir), pdir);

				if (!mtt.eq(dir, pdir))
				{
					points.Add(p);
				}
				else
				{
					points[points.Count - 1] = p;
				}
			}
		}
	}
}

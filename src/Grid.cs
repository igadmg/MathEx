using System.Collections.Generic;

namespace MathEx
{
	public class Grid<T>
	{
		private vec2i size_;
		private Dictionary<vec2, List<T>> cells_ = null;

		public vec2i size { get { return size_; } }

		public Grid(int x, int y)
			: this(new vec2i(x, y))
		{
		}

		public Grid(vec2i size)
		{
			size_ = size;
			cells_ = new Dictionary<vec2, List<T>>(size.product);
		}


		public class GridIterator
		{
			public Grid<T> c;
			public vec2 p;

			public GridIterator(Grid<T> c, vec2 p)
			{
				this.c = c;
				this.p = p;
			}

			public T this[int i]
			{
				get { return c.cells_[p][i]; }
			}

			public static GridIterator operator +(GridIterator i, T v)
			{
				List<T> l;
				if (!i.c.cells_.TryGetValue(i.p, out l)) {
					l = new List<T>(1);
					i.c.cells_.Add(i.p, l);
				}
				l.Add(v);

				return i;
			}

			public static GridIterator operator -(GridIterator i, T v)
			{
				List<T> l;
				if (!i.c.cells_.TryGetValue(i.p, out l)) {
					return i;
				}
				l.Remove(v);

				return i;
			}
		}

		public GridIterator this[float x, float y]
		{
			get { return new GridIterator(this, new vec2(x, y)); }
			set { }
		}

		public GridIterator this[vec2 p]
		{
			get { return new GridIterator(this, p); }
			set { }
		}
	}
}
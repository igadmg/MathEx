using System.Collections.Generic;

namespace MathEx
{
	public class Grid<T>
	{
		private vec2i size_;
		private List<T>[,] cells_ = null;

		public vec2i size { get { return size_; } }

		public Grid(int x, int y)
			: this(new vec2i(x, y))
		{
		}

		public Grid(vec2i size)
		{
			size_ = size;
			cells_ = new List<T>[size.x, size.y];
		}

		public List<T> this[vec2i p]
		{
			get { return cells_[p.x, p.y]; }
		}
	}
}
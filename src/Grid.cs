using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class Grid<T>
	{
		List<T>[,] cells_ = null;

		public Grid(vec2i size)
		{
			cells_ = new List<T>[size.x, size.y];
		}

		public List<T> this[vec2i p]
		{
			get { return cells_[p.x, p.y]; }
		}
	}
}

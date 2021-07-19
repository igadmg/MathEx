using SystemEx;

namespace MathEx
{
	public class Grid<T>
	{
		private vec2i size_;
		private T[] cells_ = null;

		public vec2i size => size_;
		public T[] cells => cells_;

		public Grid(vec2i size)
		{
			size_ = size;
			cells_ = new T[size.product];
		}

		public Grid(vec2i size, T data)
		{
			size_ = size;
			cells_ = new T[size.product].Initialize(data);
		}

		public Grid(vec2i size, T[] data)
		{
			size_ = size;
			cells_ = data;
		}

		public bool isValidIndex(vec2i i)
			=> i.x >= 0 && i.x < size.x
			&& i.y >= 0 && i.y < size.y;

		public T this[int x, int y] {
			get { return cells_[x + y * size_.x]; }
			set { cells_[x + y * size_.x] = value; }
		}

		public T this[vec2i p] {
			get => this[p.x, p.y];
			set { this[p.x, p.y] = value; }
		}
	}
}

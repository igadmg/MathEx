using System.Collections.Generic;

namespace MathEx
{
	public class grid3<T> : IEnumerable<vec3i>
	{
		private vec3i size_;
		private T[,,] cells_ = null;

		public vec3i size { get { return size_; } }
		public aabb3i bound { get { return new aabb3i(vec3i.zero, size_); } }

		public grid3(int x, int y, int z)
			: this(new vec3i(x, y, z))
		{
		}

		public grid3(vec3i size)
		{
			size_ = size;
			cells_ = new T[size_.x, size_.y, size_.z];
		}


		public T this[int x, int y, int z]
		{
			get { return cells_[x, y, z]; }
			set { cells_[x, y, z] = value; }
		}

		public T this[vec3i p]
		{
			get { return cells_[p.x, p.y, p.z]; }
			set { cells_[p.x, p.y, p.z] = value; }
		}

		public IEnumerator<vec3i> GetEnumerator()
		{
			for (int z = 0; z < size_.z; z++)
				for (int y = 0; y < size_.y; y++)
					for (int x = 0; x < size_.x; x++)
						yield return new vec3i(x, y, z);

			yield break;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
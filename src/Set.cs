using System;
using System.Collections.Generic;

namespace MathEx
{
	[Serializable]
	public class Set<T> : IEnumerable<Tuple<int, T>>
	{
		protected List<T> nodes_;

		public Set()
		{
			nodes_ = new List<T>();
		}

		public Set(List<T> nodes)
		{
			nodes_ = nodes;
		}

		public virtual void Add(T item)
		{
			nodes_.Add(item);
		}

		public virtual int Remove(T item)
		{
			var i = nodes_.IndexOf(item);
			if (i < 0) return i;

			nodes_.RemoveAt(i);
			return i;
		}

		public IEnumerator<Tuple<int, T>> GetEnumerator()
		{
			for (int i = 0; i < nodes_.Count; i++)
				yield return Tuple.Create(i, nodes_[i]);

			yield break;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
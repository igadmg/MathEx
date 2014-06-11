using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	[Serializable]
	public class Set<T>
	{
		protected List<T> nodes_ = new List<T>();

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
	}
}

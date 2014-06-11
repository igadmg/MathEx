using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	[Serializable]
	public class Graph<T, U> : Set<T>
	{
		public class Edge : Tuple<long, long, float>
		{
			public long a { get { return Item1; } }
			public long b { get { return Item2; } }

			U o_;


			public Edge(long a, long b)
				: base(a, b, float.NaN)
			{ }

			public Edge(long a, long b, float f)
				: base(a, b, f)
			{ }

			public Edge(long a, long b, float f, U o)
				: base(a, b, f)
			{ o_ = o; }

			public override bool Equals(object obj)
			{
				var e = obj as Edge;
				return e != null ? (e.a == a) && (e.b == b) : base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return a.GetHashCode() ^ b.GetHashCode();
			}
		}

		protected HashSet<Edge> edges_ = new HashSet<Edge>();


		public override int Remove(T item)
		{
			var ri = base.Remove(item);

			var nedges = new HashSet<Edge>();
			foreach (var edge in edges_) {
				if (edge.a == ri || edge.b == ri)
					continue;
				if (edge.a > ri) edge.Item1--;
				if (edge.b > ri) edge.Item2--;

				nedges.Add(edge);
			}
			edges_ = nedges;

			return ri;
		}


		public virtual bool Link(long a, long b, float d, U o)
		{
			if (edges_.Contains(new Edge(a, b)) || edges_.Contains(new Edge(b, a)))
				return false;

			edges_.Add(new Edge(a, b, d, o));
			edges_.Add(new Edge(b, a, d, o));

			return true;
		}
	}
}

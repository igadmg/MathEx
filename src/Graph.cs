using System;
using System.Collections.Generic;

namespace MathEx
{
	[Serializable]
	public class Graph<T> : Set<T>
	{
		protected HashSet<Edge> edges_ = new HashSet<Edge>();

		public Graph()
			: base()
		{
		}

		public Graph(List<T> nodes)
			: base(nodes)
		{
		}

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

		public virtual Edge Link(long a, long b)
		{
			Edge e = new Edge(a, b);

			if (edges_.Contains(e))
				return e;

			edges_.Add(e);

			return e;
		}

		public virtual Edge Link(T a, T b)
		{
			int ai = nodes_.IndexOf(a);
			int bi = nodes_.IndexOf(b);

			if (ai < 0 || bi < 0)
				return null;

			return Link(ai, bi);
		}

		public virtual bool Unlink(Edge e)
		{
			return edges_.Remove(e);
		}

		public virtual bool Unlink(long a, long b)
		{
			Edge e = new Edge(a, b);

			return Unlink(e);
		}

		public virtual bool Unlink(T a, T b)
		{
			int ai = nodes_.IndexOf(a);
			int bi = nodes_.IndexOf(b);

			if (ai < 0 || bi < 0)
				return false;

			return Unlink(ai, bi);
		}
	}

	public class Edge : Tuple<long, long>
	{
		public long a { get { return Item1; } }

		public long b { get { return Item2; } }

		public Edge(long a, long b)
			: base(a, b)
		{ }

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
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace MathEx
{
	public class QuadTree
	{
		Vector2 center;
		Vector2 size;
		QuadTreeIterator root = null;

		public QuadTreeIterator put(Vector2 i, object o)
		{
			if (root == null) {
				center = i;
				size = new Vector2(128, 128);
				root = new QuadTreeIterator(new QuadTreeNode(null), center, size);
			}

			return root.prepare(i).put(o);
		}

		public object get(Vector2 i)
		{
			return root.get(i);
		}
	}

	public class QuadTreeNode
	{
		static Vector2[] tr = new Vector2[4] {
			new Vector2(-0.25f, -0.25f),
			new Vector2(-0.25f,  0.25f),
			new Vector2( 0.25f,  0.25f),
			new Vector2( 0.25f, -0.25f),
		};

		QuadTreeNode p;
		QuadTreeNode[] child;
		public object o { get; set; }

		public QuadTreeNode(QuadTreeNode parent)
		{
			p = parent;
		}

		public object search(Vector2 i, int depth)
		{
			if (depth == 0)
				return o;

			if (child == null)
				return o;

			var iq = i.quad();
			var c = child[iq];
			if (c != null)
				return c.search((i + tr[iq]) * 2.0f, depth - 1);

			return null;
		}

		public QuadTreeNode allocate(Vector2 i, int depth)
		{
			if (depth == 0) {
				return this;
			}

			if (child == null)
				child = new QuadTreeNode[4] { null, null, null, null };

			var iq = i.quad();
			child[iq] = child[iq] ?? new QuadTreeNode(this);

			return child[iq].allocate((i + tr[iq]) * 2.0f, depth - 1);
		}
	}

	public class QuadTreeIterator
	{
		QuadTreeNode n;
		Vector2 c;
		Vector2 d;

		public QuadTreeNode node { get { return n; } }

		public QuadTreeIterator(QuadTreeNode node, Vector2 center, Vector2 dimensions)
		{
			n = node;
			c = center;
			d = dimensions;
		}

		public QuadTreeIterator(QuadTreeIterator node, Vector2 center, Vector2 dimensions)
		{
			n = node.node;
			c = center;
			d = dimensions;
		}

		Vector2 normalize(Vector2 i)
		{
			return (i - c).Div(d).Clamp(-Vector2.one, Vector2.one);
		}

		public QuadTreeIterator prepare(Vector2 i)
		{
			n.allocate(normalize(i), 2);

			

			return this;
		}

		public object get(Vector2 i)
		{
			return node.search(normalize(i), -1);
		}

		public QuadTreeIterator put(object o)
		{
			node.o = o;

			return this;
		}
	}
}

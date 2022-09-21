using System;
using System.Collections.Generic;

namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	public class BVHTree2
	{
		BVHTreeNode2 root = null;

		public void Add(aabb2 bound, object value)
		{
			if (root == null)
			{
				root = new BVHTreeNode2(bound, value);
			}
		}

		public T Find<T>(vec2 point)
		{
			throw new NotImplementedException();
		}

		protected BVHTreeNode2 FindNode(aabb2 bound)
		{
			Stack<BVHTreeNode2> stack = new Stack<BVHTreeNode2>();
			stack.Push(root);

			BVHTreeNode2 lastNode = null;
			while (stack.Count > 0)
			{
				var node = stack.Pop();

				var res = node.bound.intersect(bound);
				if (res == IntersectResult.None)
					continue;
				if (res == IntersectResult.Contain1)
				{
					lastNode = node;
					stack.Push(node.a);
					stack.Push(node.b);
				}
			}

			return lastNode;
		}
	}

	public class BVHTreeNode2
	{
		public aabb2 bound = aabb2.empty;
		public object value = null;

		public BVHTreeNode2 a = null;
		public BVHTreeNode2 b = null;

		public BVHTreeNode2(aabb2 bound, object value)
		{
			this.bound = bound;
			this.value = value;
		}
	}
}

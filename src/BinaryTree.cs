using System;
using System.Collections.Generic;

namespace MathEx
{
	public class BinaryTree
	{
		QuadTreeNode root = null;
	}

	public class BinaryTreeNode
	{
		float center;
		float extent;

		BinaryTreeNode parent;
		BinaryTreeNode[] child = new BinaryTreeNode[2] { null, null };
	}
}

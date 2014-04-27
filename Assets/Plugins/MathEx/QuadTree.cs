using System;
using System.Collections.Generic;
using UnityEngine;

namespace MathEx
{
	public class QuadTree
	{
		QuadTreeNode root = null;
	}

	public class QuadTreeNode
	{
		Vector2 center;

		QuadTreeNode parent;
		QuadTreeNode[] child = new QuadTreeNode[4] { null, null, null, null };
	}
}

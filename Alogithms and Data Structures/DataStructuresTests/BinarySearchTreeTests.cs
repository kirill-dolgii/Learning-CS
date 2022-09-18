using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using DataStructures.BinarySearchTree;

namespace DataStructuresTests;

[TestClass]
public class BinarySearchTreeTests : BinarySearchTree<int>
{
	[TestMethod]
	public void InOrderTraversalTest()
	{
		int[] arry = { 3, 16, 2, 14, 19, 8, 12, 1, 15, 2, 5, 78, 92, 122, 0, -1, 12, -155, 2, 67, 81, 93, 12 };
		var   bst  = new BinarySearchTree<int>(arry);

		var sorted = new List<int>();

		using var iter = bst.GetEnumerator(TreeTraversal.InOrder);
		while (iter.MoveNext()) sorted.Add(iter.Current);

		var list = new List<int>(arry).OrderBy(v => v);

		Assert.IsTrue(list.SequenceEqual(sorted));
	}

	[TestMethod]
	public void PreOrderTraversalTest()
	{
		void PreOrderTraversal(IBinaryNode<TreeNode, int> root, ref List<int> ret)
		{
			ret.Add(root.Value);
			if (root.Left != null) PreOrderTraversal(root.Left, ref ret);
			if (root.Right != null) PreOrderTraversal(root.Right, ref ret);
		}

		int[] arry = { 3, 16, 2, 14, 19, 8, 12, 1, 15, 2, 5, 78, 92, 122, 0, -1, 12, -155, 2, 67, 81, 93, 12 };
		var   bst  = new BinarySearchTree<int>(arry);

		var trav = new List<int>();

		using var iter = bst.GetEnumerator(TreeTraversal.PreOrder);
		while (iter.MoveNext()) trav.Add(iter.Current);

		var goodTrav = new List<int>();
		PreOrderTraversal(this.GetTreeNode(bst)!, ref goodTrav);
		
		Assert.IsTrue(goodTrav.SequenceEqual(trav));
	}

	[TestMethod]
	public void MinMaxTest()
	{
		int[] arry = { 6, 5, 12, 15, 0, 4, 7, -1, 17 };
		var   bst  = new BinarySearchTree<int>(arry);

		Assert.AreEqual(17, bst.Max());
		Assert.AreEqual(-1, bst.Min());
	}

	[TestMethod]
	public void AddTest()
	{
		int[] arry = { 6, 5, 12, 15, 0, 4, 7 };
		var   bst  = new BinarySearchTree<int>(arry);

		bst.Add(-1);
		Assert.AreEqual(-1, bst.Min());

		bst.Add(22);
		Assert.AreEqual(22, bst.Max());

		bst.Add(3);
		Assert.AreEqual(3, GetTreeNode(bst)!.Left!.Left!.Right!.Left!.Value);

		bst.Add(8);
		Assert.AreEqual(8, GetTreeNode(bst)!.Right!.Left!.Right!.Value);

		bst.Add(14);
		Assert.AreEqual(14, GetTreeNode(bst)!.Right!.Right!.Left!.Value);
	}

	[TestMethod]
	public void RemoveTest()
	{
		int[] arry = { 6, 5, 12, 15, 0, 4, 7 };
		var   bst  = new BinarySearchTree<int>(arry);

		Assert.AreEqual(arry.Length, bst.Count);

		//Remove a node with 2 children
		bst.Remove(6);
		Assert.AreEqual(7, GetTreeNode(bst)!.Value);
		Assert.AreEqual(null, GetTreeNode(bst)!.Right!.Left);
		Assert.AreEqual(arry.Length - 1, bst.Count);

		//Remove a leaf
		bst.Remove(4);
		Assert.AreEqual(null, GetTreeNode(bst)!.Left!.Left!.Right);

		//remove a node with one right child
		bst.Remove(12);
		Assert.AreEqual(15, GetTreeNode(bst)!.Right!.Value);

		//remove a node with one left child
		bst.Remove(5);
		Assert.AreEqual(0, GetTreeNode(bst)!.Left!.Value);
	}

	[TestMethod]
	public void CopyTo_From_0()
	{
		int[] arry     = { 6, 5, 12, 15, 0, 4, 7 };
		var   bst      = new BinarySearchTree<int>(arry);
		int[] arryCopy = new int[arry.Length];
		
		bst.CopyTo(arryCopy, 0);

		arry.SequenceEqual(arryCopy);
	}

	[TestMethod]
	public void CopyTo_From_5()
	{
		int[] arry     = { 6, 5, 12, 15, 0, 4, 7 };
		var   bst      = new BinarySearchTree<int>(arry);
		int[] arryCopy = new int[arry.Length];

		bst.CopyTo(arryCopy, 5);

		arry.SequenceEqual(arryCopy.Skip(5));
	}

	[TestMethod]
	public void ContainsTest()
	{
		int[] arry = { 6, 5, 12, 15, 0, 4, 7 };
		var   bst  = new BinarySearchTree<int>(arry);

		Assert.AreEqual(false, bst.Contains(-322));
		Assert.AreEqual(true, bst.Contains(12));

		bst.Add(-322);
		Assert.AreEqual(true, bst.Contains(-322));

		bst.Remove(6);
		Assert.AreEqual(false, bst.Contains(6));
	}
}
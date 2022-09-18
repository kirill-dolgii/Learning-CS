using System.Reflection;
using DataStructures.RedBlackTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresTests;

[TestClass]
public class RebBlackTreeTests
{
	private RedBlackTree<int>  _tree;
	private IRedBlackNode<int> _root;

	private IRedBlackNode<int> GetRoot(RedBlackTree<int> tree)
	{
		var rootObj = _tree!.GetType()!
							.GetField("_root", System.Reflection.BindingFlags.NonPublic |
											   System.Reflection.BindingFlags.Instance)!
							.GetValue(_tree);
		return (IRedBlackNode<int>)rootObj!;
	}

	private class Node<T> : IRedBlackNode<T>
	{
		public Node(IRedBlackNode<T>? parent,
					T value,
					IRedBlackNode<T>? left,
					IRedBlackNode<T>? right,
					bool isBlack)
		{
			Parent = parent;
			Value = value;
			Left = left;
			Right = right;
			IsBlack = isBlack;
		}

		public IRedBlackNode<T>? Parent  { get; set; }
		public IRedBlackNode<T>? Left    { get; set; }
		public IRedBlackNode<T>? Right   { get; set; }
		public bool              IsBlack { get; set; }
		public T                 Value   { get; }
	}

	[TestInitialize]
	public void Init()
	{
		_tree = new();
		_root = GetRoot(_tree);
	}

	[TestMethod]
	public void ROTATION()
	{
		_tree = new();
		_tree.Add(6);
		_root = GetRoot(_tree);

		var l1 = new Node<int>(_root, 4, null, null, false);
		_root.Left = l1;
		var l2 = new Node<int>(l1, 3, null, null, false);
		_root.Left.Left = l2;
		var l1r1 = new Node<int>(l1, 5, null, null, false);
		l1.Right = l1r1;
		var r1 = new Node<int>(_root, 8, null, null, false);
		_root.Right = r1;
		var r1l1 = new Node<int>(r1, 7, null, null, false);
		_root.Right.Left = r1l1;
		var r2 = new Node<int>(r1, 9, null, null, false);
		_root.Right.Right = r2;

		MethodInfo leftRotate = _tree!.GetType()!
									  .GetMethod("RotateLeft", BindingFlags.NonPublic | BindingFlags.Instance)!;
		leftRotate.Invoke(_tree, new object[] { _root });

		Assert.AreEqual(r1, GetRoot(_tree));
		Assert.AreEqual(r1.Left, _root);
		Assert.AreEqual(r1.Right, r2);
		Assert.AreEqual(_root.Left, l1);
		Assert.AreEqual(_root.Right, r1l1);
		Assert.AreEqual(l1.Left, l2);
		Assert.AreEqual(l1.Right, l1r1);

		Assert.AreEqual(l2.Left, null);
		Assert.AreEqual(l2.Right, null);

		Assert.AreEqual(r1l1.Left, null);
		Assert.AreEqual(r1l1.Right, null);

		Assert.AreEqual(r2.Left, null);
		Assert.AreEqual(r2.Right, null);

		Assert.AreEqual(l2.Parent, l1);
		Assert.AreEqual(l1r1.Parent, l1);

		Assert.AreEqual(l1.Parent, _root);
		Assert.AreEqual(r1l1.Parent, _root);

		Assert.AreEqual(_root.Parent, r1);

		Assert.AreEqual(r1.Parent, null);

		Assert.AreEqual(r2.Parent, r1);
	}

	[TestMethod]
	public void RED_BLACK_INSERTION()
	{
		_tree = new RedBlackTree<int>();
		_tree.Add(33);
		_root = GetRoot(_tree);

		var l = new Node<int>(_root, 13, null, null, false);
		_root.Left = l;
		var ll = new Node<int>(_root.Left, 11, null, null, true);
		_root.Left.Left = ll;
		var lr = new Node<int>(_root.Left, 21, null, null, true);
		_root.Left.Right = lr;
		var lrl = new Node<int>(_root.Left.Right, 15, null, null, false);
		_root.Left.Right.Left = lrl;
		var lrr = new Node<int>(_root.Left.Right, 31, null, null, false);
		_root.Left.Right.Right = lrr;
		var r = new Node<int>(_root, 53, null, null, true);
		_root.Right = r;
		var rl = new Node<int>(_root.Right, 41, null, null, false);
		_root.Right.Left = rl;
		var rr = new Node<int>(_root.Right, 61, null, null, false);
		_root.Right.Right = rr;

		_tree.Add(20);

		Assert.AreEqual(lr, GetRoot(_tree));
		Assert.AreEqual(lr.Left, l);
		Assert.AreEqual(lr.Left.Left, ll);
		Assert.AreEqual(lr.Left.Right, lrl);
		Assert.AreEqual(lr.Left.Right.Right.Value, 20);

		Assert.AreEqual(lr.Right, _root);
		Assert.AreEqual(lr.Right.Left, lrr);
		Assert.AreEqual(lr.Right.Right, r);
		Assert.AreEqual(lr.Right.Right.Left, rl);
		Assert.AreEqual(lr.Right.Right.Right, rr);

		Assert.AreEqual(lr.Parent, null);
		Assert.AreEqual(l.Parent, lr);
		Assert.AreEqual(ll.Parent, l);
		Assert.AreEqual(lrl.Parent, l);
		Assert.AreEqual(lrl.Right.Parent, lrl);
		Assert.AreEqual(_root.Parent, lr);
		Assert.AreEqual(lrr.Parent, _root);

		Assert.AreEqual(r.Parent, _root);
		Assert.AreEqual(rl.Parent, r);
		Assert.AreEqual(rr.Parent, r);

		Assert.IsTrue(lr.IsBlack);
		Assert.IsTrue(ll.IsBlack);
		Assert.IsTrue(lrl.IsBlack);
		Assert.IsTrue(lrr.IsBlack);
		Assert.IsTrue(r.IsBlack);

		Assert.IsFalse(l.IsBlack);
		Assert.IsFalse(_root.IsBlack);
		Assert.IsFalse(lrl.Right.IsBlack);
		Assert.IsFalse(rl.IsBlack);
		Assert.IsFalse(rr.IsBlack);

	}
}

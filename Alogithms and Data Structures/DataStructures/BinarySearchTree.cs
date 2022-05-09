using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures;

public class BinarySearchTree<T> : ICollection<T>
	where T : IComparable<T>
{
	public class TreeNode<T> where T : IComparable<T>
	{
		public TreeNode<T>? LeftLeaf;
		public TreeNode<T>? RightLeaf;

		public readonly T Value;

		public TreeNode(T elem)
		{
			Value = elem;
			LeftLeaf = null;
			RightLeaf = null;
		}

		public TreeNode(T elem, TreeNode<T>? left, TreeNode<T>? right) : this(elem)
		{
			LeftLeaf = left;
			RightLeaf = right;
		}
	}

	public TreeNode<T>? Root;

	public BinarySearchTree(T firstElem)
	{
		Root = new TreeNode<T>(firstElem);
		_size = 0;
	}

	private int _size;

	public void Add(T item)
	{
		if (Contains(item)) return;
		Add(Root, item);
		_size++;
	}

	private TreeNode<T> Add(TreeNode<T>? addRoot, T item)
	{
		if (addRoot == null)
		{
			addRoot = new TreeNode<T>(item);
		}
		else
		{
			var cmp = item.CompareTo(addRoot.Value);
			if (cmp < 0) addRoot.LeftLeaf = Add(addRoot.LeftLeaf, item);
			if (cmp > 0) addRoot.RightLeaf = Add(addRoot.RightLeaf, item);
		}

		return addRoot;
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public bool Contains(T item)
	{
		var node = Root;
		while (node != null)
		{
			var cmp = item.CompareTo(node.Value);
			if (cmp < 0) node = node.LeftLeaf;
			else if (cmp > 0) node = node.RightLeaf;
			else return true;
		}

		return false;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

	private TreeNode<T> Min(TreeNode<T> minRoot)
	{
		while (minRoot.LeftLeaf != null) minRoot = minRoot.LeftLeaf;
		return minRoot;
	}

	private TreeNode<T>? Remove(TreeNode<T>? removeRoot, T item)
	{
		if (removeRoot == null) return null;

		var cmp = item.CompareTo(removeRoot.Value);

		if (cmp == 1)
		{
			removeRoot.RightLeaf = Remove(removeRoot.RightLeaf, item);
		}
		else if (cmp == -1)
		{
			removeRoot.LeftLeaf = Remove(removeRoot.LeftLeaf, item);
		}
		else
		{
			if (removeRoot.LeftLeaf == null && removeRoot.RightLeaf == null) return null;
			//there are 2 leafs
			if (removeRoot.RightLeaf != null && removeRoot.LeftLeaf != null)
			{
				//find min, replace root with the min and remove min
				var rightMin = Min(removeRoot.RightLeaf);

				rightMin.RightLeaf = Remove(removeRoot.RightLeaf, rightMin.Value);
				rightMin.LeftLeaf = removeRoot.LeftLeaf;
				return rightMin;
			}

			return removeRoot.LeftLeaf ?? removeRoot.RightLeaf;
		}

		return removeRoot;
	}

	public bool Remove(T item)
	{
		if (!Contains(item)) return false;

		Root = Remove(Root, item);
		_size--;
		return true;
	}

	public int  Count      => _size;
	public bool IsReadOnly => false;


	public enum Traversal
	{
		InOrder,
		PreOrder,
		LevelOrder,
		PostOrder
	}

	public IEnumerator<T> GetEnumerator()
	{
		return GetEnumerator(Traversal.InOrder);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator(Traversal.InOrder);
	}

	public IEnumerator<T> GetEnumerator(Traversal travType)
	{
		switch (travType)
		{
			case Traversal.InOrder:
			{
				return new InOrderIterator(this);
			}
			case Traversal.PreOrder:
			{
				return new PreOrderIterator(this);
			}
			case Traversal.LevelOrder:
			{
				return new LevelOrderIterator(this);
			}
		}

		return new InOrderIterator(this);
	}

	private class InOrderIterator : IEnumerator<T>
	{
		public InOrderIterator(BinarySearchTree<T> bst)
		{
			if (bst.Root != null)
			{
				_bst = bst;
				_trav = bst.Root;
				_stack.Push(bst.Root);
			}
		}

		private readonly BinarySearchTree<T> _bst;

		private readonly Stack<TreeNode<T>> _stack = new();

		private TreeNode<T> _trav;
		private TreeNode<T> _visited;

		public bool MoveNext()
		{
			if (_stack.Count == 0) return false;

			while (_trav.LeftLeaf != null)
			{
				_stack.Push(_trav.LeftLeaf);
				_trav = _trav.LeftLeaf;
			}

			_visited = _stack.Pop();

			if (_visited.RightLeaf != null)
			{
				_stack.Push(_visited.RightLeaf);
				_trav = _visited.RightLeaf;
			}

			return true;
		}

		public void Reset()
		{
			if (_bst.Root != null)
			{
				_trav = _bst.Root;
				_stack.Push(_bst.Root);
			}
		}

		public T Current => _visited.Value;

		object IEnumerator.Current => Current;

		public void Dispose()
		{
			/*throw new NotImplementedException();*/
		}
	}

	private class PreOrderIterator : IEnumerator<T>
	{
		public PreOrderIterator(BinarySearchTree<T> bst)
		{
			if (bst.Root != null)
			{
				_bst = bst;
				_current = bst.Root;
				_stack.Push(bst.Root);
			}
		}

		private readonly BinarySearchTree<T> _bst;

		private Stack<TreeNode<T>> _stack = new();

		private TreeNode<T> _current;

		public T Current => _current.Value;

		object IEnumerator.Current => throw new NotImplementedException();

		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		public bool MoveNext()
		{
			if (_stack.Count == 0) return false;
			_current = _stack.Pop();
			if (_current.RightLeaf != null) _stack.Push(_current.RightLeaf);
			if (_current.LeftLeaf != null) _stack.Push(_current.LeftLeaf);
			return true;
		}

		public void Reset()
		{
			if (_bst.Root != null)
			{
				_stack.Clear();
				_stack.Push(_bst.Root);
			}
		}
	}

	private class LevelOrderIterator : IEnumerator<T>
	{
		public LevelOrderIterator(BinarySearchTree<T> bst)
		{
			if (bst.Root != null)
			{
				this.bst = bst;
				_current = new TreeNode<T>(bst.Root.Value, bst.Root.LeftLeaf, bst.Root.RightLeaf);
				_queue.Enqueue(bst.Root);
			}
		}

		private BinarySearchTree<T> bst;

		private Queue<TreeNode<T>> _queue = new();

		private TreeNode<T> _current;

		public bool MoveNext()
		{
			if (_queue.Count == 0) return false;

			_current = _queue.Dequeue();

			if (_current.LeftLeaf != null) _queue.Enqueue(_current.LeftLeaf);
			if (_current.RightLeaf != null) _queue.Enqueue(_current.RightLeaf);

			return true;
		}

		public void Reset()
		{
			if (bst.Root != null)
			{
				_queue.Clear();
				_queue.Enqueue(bst.Root);
			}
		}

		public T Current => _current.Value;

		object IEnumerator.Current => throw new NotImplementedException();

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}

}
using System.Collections;

namespace DataStructures.BinarySearchTree;

/// <summary>
/// Represents a mutable unbalanced BinarySearchTree that stores elements in sorted order.
/// The main operations in binary tree are: search, insert and delete.
/// Worst time complexity of the main operations is O(n) in case of not balanced tree.
/// In case of balanced tree performs the main operations in O(log(n)).
/// </summary>
/// <typeparam name="T"></typeparam>
public class BinarySearchTree<T> : ICollection<T>
{
	/// <summary>
	/// Represents a BinarySearchTree node - the base element of a tree.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	protected class TreeNode
	{
		public readonly T Value;

		public TreeNode? LeftChild;
		public TreeNode? RightChild;

		public TreeNode(T elem)
		{
			Value = elem;
			LeftChild = null;
			RightChild = null;
		}

		public TreeNode(T elem, TreeNode? left, TreeNode? right) : this(elem)
		{
			LeftChild = left;
			RightChild = right;
		}
	}

	/// <summary>
	/// Method used by test class to obtain protected Root outside of the instance.
	/// </summary>
	protected virtual TreeNode? GetTreeNode(BinarySearchTree<T> bst) => bst.Root;


	private int       _size;
	protected TreeNode? Root;

	private readonly BinarySearchTreeSortOrder _order;
	private readonly IComparer<T>              _comparer;
	public BinarySearchTree()
	{
		Root = null;
		_size = 0;
		_comparer = Comparer<T>.Default;
	}

	public BinarySearchTree(IEnumerable<T> data,
							 BinarySearchTreeSortOrder order,
							 IComparer<T>? comparer)
	{
		if (data == null) throw new ArgumentNullException(nameof(data));
		if (!data.Any()) throw new ArgumentException(nameof(data));

		this.Root = new TreeNode(data.First());
		this._size = 1;

		_order = order;
		_comparer = comparer ?? Comparer<T>.Default;

		foreach (T elem in data.Skip(1)) this.Add(elem);
	}

	public BinarySearchTree(IEnumerable<T> data) : this(data, BinarySearchTreeSortOrder.Ascdending, null) {}

	public BinarySearchTree(IComparer<T> comparer) : this() => _comparer = comparer;

	public BinarySearchTree(BinarySearchTreeSortOrder order) : this() => _order = order;

	public BinarySearchTree(IEnumerable<T> data, 
							IComparer<T> comparer) : this (data, BinarySearchTreeSortOrder.Ascdending, comparer) {}

	public BinarySearchTree(IEnumerable<T> data,
							BinarySearchTreeSortOrder order) : this(data, order, null) { }

	public BinarySearchTree(BinarySearchTreeSortOrder order,
							IComparer<T> comparer) : this()
	{
		_order = order;
		_comparer = comparer;
	}

	/// <summary>
	/// Adds an element to the Binary Search Tree in O(n) worse case and O(log(n)) average case.
	/// If the given element already exists in the tree adds a duplicate.
	/// </summary>
	/// <param name="item"></param>
	public void Add(T item)
	{
		Add(Root, item);
		_size++;
	}

	// returns the subtree of the given TreeNode that contains new item.
	private TreeNode Add(TreeNode? addRoot, T item)
	{
		if (addRoot == null) addRoot = new TreeNode(item);
		else
		{
			var cmp = this.Compare(item, addRoot.Value);
			if (cmp <= 0) addRoot.LeftChild = Add(addRoot.LeftChild, item);
			if (cmp > 0) addRoot.RightChild = Add(addRoot.RightChild, item);
		}

		return addRoot;
	}

	private int Compare(T item1, T item2)
	{
		int compVal = this._comparer.Compare(item1, item2);
		return compVal * (int)_order;
	}
	
	/// <summary>
	/// Clears the BinarySearchTree. Assigns null to the root element.
	/// </summary>
	public void Clear()
	{
		this.Root = null;
		this._size = 0;
	}

	/// <summary>
	/// If BinarySearchTree contains the specified element returns true and false otherwise.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool Contains(T item)
	{
		var node = Root;
		while (node != null)
		{
			int cmp = this.Compare(item, node.Value);
			if (cmp < 0) node = node.LeftChild;
			else if (cmp > 0) node = node.RightChild;
			else return true;
		}

		return false;
	}

	/// <summary>
	/// Copies elements of the BinarySearchTree to the specified array in sorted order.
	/// </summary>
	/// <param name="array"></param>
	/// <param name="arrayIndex"></param>
	public void CopyTo(T[] array, int arrayIndex)
	{
		using (var enumerator = this.GetEnumerator(Traversal.InOrder))
		{
			int i = arrayIndex;
			for (int j = 0; j < i; j++) enumerator.MoveNext();
			while (enumerator.MoveNext()) array[i++] = enumerator.Current;
		}

	}

	/// <summary>
	/// Extracts the minimal element of the BinarySearchTree in O(1) time.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NullReferenceException"></exception>
	public T Min()
	{
		if (Root == null) throw new NullReferenceException(nameof(Root));
		return this.Min(Root).Value;
	}

	private TreeNode Min(TreeNode minRoot)
	{
		while (minRoot.LeftChild != null) minRoot = minRoot.LeftChild;
		return minRoot;
	}

	/// <summary>
	/// Extracts the biggest element of the BinarySearchTree in O(n) worse case and O(log(n)) average case.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NullReferenceException"></exception>
	public T Max()
	{
		if (Root == null) throw new NullReferenceException(nameof(Root));
		return this.Max(this.Root).Value;
	}

	private TreeNode Max(TreeNode maxRoot)
	{
		while (maxRoot.RightChild != null) maxRoot = maxRoot.RightChild;
		return maxRoot;
	}

	/// <summary>
	/// Performs removal of specified item from the BinarySearchTree in O(n) worse case and O(log(n)) average case.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool Remove(T item)
	{
		if (!Contains(item)) return false;

		Root = Remove(Root, item);
		_size--;
		return true;
	}

	private TreeNode? Remove(TreeNode? removeRoot, T item)
	{
		if (removeRoot == null) return null;

		var cmp = this.Compare(item, removeRoot.Value);

		if (cmp == 1) removeRoot.RightChild = Remove(removeRoot.RightChild, item);
		else if (cmp == -1) removeRoot.LeftChild = Remove(removeRoot.LeftChild, item);
		else
		{
			if (removeRoot.LeftChild == null && removeRoot.RightChild == null) return null;
			//there are 2 leafs
			if (removeRoot.RightChild != null && removeRoot.LeftChild != null)
			{
				//find min, replace root with the min and remove min
				var rightMin = Min(removeRoot.RightChild);

				rightMin.RightChild = Remove(removeRoot.RightChild, rightMin.Value);
				rightMin.LeftChild = removeRoot.LeftChild;
				return rightMin;
			}

			return removeRoot.LeftChild ?? removeRoot.RightChild;
		}

		return removeRoot;
	}
	
	public int Count => _size;
	public bool IsReadOnly => false;

	public IEnumerator<T> GetEnumerator() => GetEnumerator(Traversal.InOrder);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(Traversal.InOrder);

	public IEnumerator<T> GetEnumerator(Traversal traversal)
	{
		switch (traversal)
		{
			case Traversal.InOrder: return new InOrderIterator(this);
			case Traversal.PreOrder: return new PreOrderIterator(this);
			case Traversal.LevelOrder: return new LevelOrderIterator(this);
		}

		return new InOrderIterator(this);
	}

	private class InOrderIterator : IEnumerator<T>
	{

		private readonly BinarySearchTree<T> _bst;

		private readonly Stack<TreeNode>  _stack = new();

		private TreeNode _trav;
		private TreeNode _current;

		public InOrderIterator(BinarySearchTree<T> bst)
		{
			this._bst = bst ?? throw new NullReferenceException(nameof(bst));
			_trav = bst.Root ?? throw new NullReferenceException(nameof(bst.Root));
			_stack.Push(bst.Root);
			_current = bst.Root;
		}
		
		public bool MoveNext()
		{
			if (_stack.Count == 0) return false;

			while (_trav.LeftChild != null)
			{
				_stack.Push(_trav.LeftChild);
				_trav = _trav.LeftChild;
			}

			_current = _stack.Pop();

			if (_current.RightChild != null)
			{
				_stack.Push(_current.RightChild);
				_trav = _current.RightChild;
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

		public T Current => _current.Value;

		object IEnumerator.Current => Current;

		public void Dispose()
		{
			/*throw new NotImplementedException();*/
		}
	}

	private class PreOrderIterator : IEnumerator<T>
	{
		private readonly BinarySearchTree<T> _bst;

		private readonly Stack<TreeNode> _stack = new();

		private TreeNode _current;

		public PreOrderIterator(BinarySearchTree<T> bst)
		{
			this._bst = bst ?? throw new NullReferenceException(nameof(bst));
			_current = bst.Root ?? throw new NullReferenceException(nameof(bst.Root));
			_stack.Push(bst.Root);
		}
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
			if (_current.RightChild != null) _stack.Push(_current.RightChild);
			if (_current.LeftChild != null) _stack.Push(_current.LeftChild);
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
		private BinarySearchTree<T> _bst;

		private Queue<TreeNode> _queue = new();

		private TreeNode _current;

		public LevelOrderIterator(BinarySearchTree<T> bst)
		{
			this._bst = bst ?? throw new ArgumentNullException(nameof(bst));
			if (bst.Root == null) throw new NullReferenceException($"{nameof(bst.Root)} is null.");
			_current = new TreeNode(bst.Root.Value, bst.Root.LeftChild, bst.Root.RightChild);
			_queue.Enqueue(bst.Root);
		}
		
		public bool MoveNext()
		{
			if (_queue.Count == 0) return false;

			_current = _queue.Dequeue();

			if (_current.LeftChild != null) _queue.Enqueue(_current.LeftChild);
			if (_current.RightChild != null) _queue.Enqueue(_current.RightChild);

			return true;
		}

		public void Reset()
		{
			if (_bst.Root != null)
			{
				_queue.Clear();
				_queue.Enqueue(_bst.Root);
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
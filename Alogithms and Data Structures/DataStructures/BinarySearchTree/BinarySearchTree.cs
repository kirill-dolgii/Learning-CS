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
where T : IComparable<T>
{
	/// <summary>
	/// Represents a BinarySearchTree node - the base element of a tree.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TreeNode
	{
		public readonly T Value;

		public TreeNode? LeftLeaf;
		public TreeNode? RightLeaf;

		public TreeNode(T elem)
		{
			Value = elem;
			LeftLeaf = null;
			RightLeaf = null;
		}

		public TreeNode(T elem, TreeNode? left, TreeNode? right) : this(elem)
		{
			LeftLeaf = left;
			RightLeaf = right;
		}
	}

	private          int                       _size;
	public           TreeNode?              Root;

	private readonly BinarySearchTreeSortOrder _order;
	private readonly Comparer<T>?              _comparer = null;

	public BinarySearchTree()
	{
		Root = null;
		_size = 0;
	}

	public BinarySearchTree(T firstElem)
	{
		Root = new TreeNode(firstElem);
		_size = 0;
	}

	public BinarySearchTree(T[] arry, 
							BinarySearchTreeSortOrder order = BinarySearchTreeSortOrder.Ascdending,
							Comparer<T>? comparer = null)
	{
		if (arry == null) throw new ArgumentNullException(nameof(arry));
		if (arry.Length == 0) throw new ArgumentException(nameof(arry));

		this.Root = new TreeNode(arry[0]);
		this._size = 1;

		_order = order;
		_comparer = comparer;

		foreach (T elem in arry.Skip(1)) this.Add(elem);
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
			if (cmp <= 0) addRoot.LeftLeaf = Add(addRoot.LeftLeaf, item);
			if (cmp > 0) addRoot.RightLeaf = Add(addRoot.RightLeaf, item);
		}

		return addRoot;
	}

	private int Compare(T item1, T item2)
	{
		int compVal = this._comparer?.Compare(item1, item2) ?? item1.CompareTo(item2);
		return compVal * (_order == BinarySearchTreeSortOrder.Ascdending ? 1 : -1);
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
			if (cmp < 0) node = node.LeftLeaf;
			else if (cmp > 0) node = node.RightLeaf;
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
		while (minRoot.LeftLeaf != null) minRoot = minRoot.LeftLeaf;
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
		while (maxRoot.RightLeaf != null) maxRoot = maxRoot.RightLeaf;
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

		if (cmp == 1) removeRoot.RightLeaf = Remove(removeRoot.RightLeaf, item);
		else if (cmp == -1) removeRoot.LeftLeaf = Remove(removeRoot.LeftLeaf, item);
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

			while (_trav.LeftLeaf != null)
			{
				_stack.Push(_trav.LeftLeaf);
				_trav = _trav.LeftLeaf;
			}

			_current = _stack.Pop();

			if (_current.RightLeaf != null)
			{
				_stack.Push(_current.RightLeaf);
				_trav = _current.RightLeaf;
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
		private BinarySearchTree<T> _bst;

		private Queue<TreeNode> _queue = new();

		private TreeNode _current;

		public LevelOrderIterator(BinarySearchTree<T> bst)
		{
			this._bst = bst ?? throw new ArgumentNullException(nameof(bst));
			if (bst.Root == null) throw new NullReferenceException($"{nameof(bst.Root)} is null.");
			_current = new TreeNode(bst.Root.Value, bst.Root.LeftLeaf, bst.Root.RightLeaf);
			_queue.Enqueue(bst.Root);
		}
		
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
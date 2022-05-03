using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinarySearchTree<T> : ICollection<T>
    where T : IComparable<T>
    {
		public class TreeNode<T> where T : IComparable<T>
		{
			public TreeNode<T>? LeftLeaf;
			public TreeNode<T>? RightLeaf;

			public TreeNode<T>? Parent;

			public T value;

			public TreeNode(T elem)
			{
				this.value = elem;
				this.LeftLeaf = null;
				this.RightLeaf = null;
				this.Parent = null;
			}

			public TreeNode(T elem, TreeNode<T> parent) : this(elem)
			{
				this.Parent = parent;
			}
		}

		public TreeNode<T>? Root;

		public BinarySearchTree(T firstElem)
		{
			this.Root = new TreeNode<T>(firstElem);
		}
		
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			this.Add(this.Root, item);
		}

		private TreeNode<T> Add(TreeNode<T>? addRoot, T item)
		{
			if (addRoot == null) addRoot = new (item, null, null, null);
			else
			{
				if (item.CompareTo(addRoot.value) < 0)
				{
					addRoot.LeftLeaf = this.Add(addRoot.LeftLeaf, item);
					addRoot.LeftLeaf.Parent = addRoot;
				}

				if (item.CompareTo(addRoot.value) > 0)
				{
					addRoot.RightLeaf = this.Add(addRoot.RightLeaf, item);
					addRoot.RightLeaf.Parent = addRoot;
				}
			}
			return addRoot;
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public int  Count      { get; }
		public bool IsReadOnly { get; }
	}
}

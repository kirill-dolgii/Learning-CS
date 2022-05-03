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

			public TreeNode(T elem, TreeNode<T>? parent) : this(elem)
			{
				this.Parent = parent;
			}

			public TreeNode(T elem, TreeNode<T>? parent, TreeNode<T>? left, TreeNode<T>? right) : this(elem, parent)
			{
				this.LeftLeaf = left;
				this.RightLeaf = right;
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
			TreeNode<T> node = this.Root;
			while (node != null)
			{
				if (item.CompareTo(node.value) < 0) node = node.LeftLeaf;
				else if (item.CompareTo(node.value) > 0) node = node.RightLeaf;
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

		private TreeNode<T> Remove(TreeNode<T> removeRoot, T item)
		{
			if (removeRoot == null) return null;
			if (item.CompareTo(removeRoot.value) == 0)
			{
				if (removeRoot.LeftLeaf == null && removeRoot.RightLeaf == null) return null;
				else if (removeRoot.LeftLeaf != null && removeRoot.RightLeaf != null)
				{
					TreeNode<T> min = new(this.Min(removeRoot.RightLeaf).value, removeRoot.Parent, 
										  removeRoot.LeftLeaf, removeRoot.RightLeaf);

					min.RightLeaf.Parent = min;
					min.LeftLeaf.Parent = min;

					this.Remove(removeRoot, min.value);
					return min;
				}
				else if (removeRoot.LeftLeaf != null || removeRoot.RightLeaf != null)
				{
					var notNUllChild = removeRoot.LeftLeaf != null? removeRoot.LeftLeaf : removeRoot.RightLeaf;
					notNUllChild.Parent = removeRoot.Parent;
					return notNUllChild;
				}
			}

			if (item.CompareTo(removeRoot.value) < 0)
			{
				removeRoot.LeftLeaf = this.Remove(removeRoot.LeftLeaf, item);
				return removeRoot;
			}

			if (item.CompareTo(removeRoot.value) > 0)
			{
				removeRoot.RightLeaf = this.Remove(removeRoot.RightLeaf, item);
				return removeRoot;
			}

            return new(item);
        }

		public bool Remove(T item)
		{
			if (!this.Contains(item)) return false;
			else
			{
				this.Remove(this.Root, item);
				return true;
			}
		}

		public int  Count      { get; }
		public bool IsReadOnly { get; }
	}
}

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

            public readonly T Value;

            public TreeNode(T elem)
            {
                this.Value = elem;
                this.LeftLeaf = null;
                this.RightLeaf = null;
            }

            public TreeNode(T elem, TreeNode<T>? left, TreeNode<T>? right) : this(elem)
            {
                this.LeftLeaf = left;
                this.RightLeaf = right;
            }
        }

        public TreeNode<T>? Root;

        public BinarySearchTree(T firstElem)
        {
            this.Root = new TreeNode<T>(firstElem);
            this._size = 0;
        }

        private int _size;

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
			if (this.Contains(item)) return;
            this.Add(this.Root, item);
            this._size++;
        }

        private TreeNode<T> Add(TreeNode<T>? addRoot, T item)
        {
            if (addRoot == null) addRoot = new(item);
            else
            {
                int cmp = item.CompareTo(addRoot.Value);
                if (cmp < 0) addRoot.LeftLeaf = this.Add(addRoot.LeftLeaf, item);
                if (cmp > 0) addRoot.RightLeaf = this.Add(addRoot.RightLeaf, item);
            }
			return addRoot;
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            TreeNode<T>? node = this.Root;
            while (node != null)
            {
                int cmp = item.CompareTo(node.Value);
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

            int cmp = item.CompareTo(removeRoot.Value);

            if (cmp == 1) removeRoot.RightLeaf = this.Remove(removeRoot.RightLeaf, item);
            else if (cmp == -1) removeRoot.LeftLeaf = this.Remove(removeRoot.LeftLeaf, item);
            else
            {
				if (removeRoot.LeftLeaf == null && removeRoot.RightLeaf == null) return null;
                //there are 2 leafs
                if (removeRoot.RightLeaf != null && removeRoot.LeftLeaf != null)
                {
                    //find min, replace root with the min and remove min
                    TreeNode<T> rightMin = this.Min(removeRoot.RightLeaf);

                    rightMin.RightLeaf = this.Remove(removeRoot.RightLeaf, rightMin.Value);
                    rightMin.LeftLeaf = removeRoot.LeftLeaf;
                    return rightMin;
                }
                return removeRoot.LeftLeaf ?? removeRoot.RightLeaf;
            }
            return removeRoot;
        }

        public bool Remove(T item)
        {
            if (!this.Contains(item)) return false;

            this.Root = this.Remove(this.Root, item);
            this._size--;
            return true;
        }

        public int Count => this._size;
        public bool IsReadOnly => false;
    }
}

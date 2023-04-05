using DataStructures.Collections.BinarySearchTree;

namespace Algorithms.Sorting;

public class TreeSorter<T> : Sorter<T>
where T : IComparable<T>
{
	public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { this.Sort(data, null, order); }

	public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
	{
		// Data Structures project doesn't contain reference to the current project =>
		// in order to specify the sorting order we need to wrap BSTSortOrder
		var wrappedOrder = order == SortingOrder.Ascending
			? BinarySearchTreeSortOrder.Ascdending
			: BinarySearchTreeSortOrder.Descending;

		BinarySearchTree<T> bst        = new(data, wrappedOrder);
		bst.CopyTo(data, 0);
	}
}


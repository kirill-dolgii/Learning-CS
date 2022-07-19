namespace Algorithms.Sorting;

public class BubbleSorter<T> : ISorter<T>
where T : IComparable<T>
{
    public void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { this.Sort(data, null, order); }

    public void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
    {
        // after each iteration [0, i] elements of the given array are sorted
        for (int i = 0; i < data.Length; i++)
			// after each iteration the smallest (biggest) element of the subsequence
			// that contains n - i elements: (i, n] of data
			// is pushed to the left
            for (int j = data.Length - 1; j > i; j--)
				if (this.Compare(data[j], data[j - 1], comparer) * (order == SortingOrder.Ascending ? 1 : -1) < 0) 
					(data[j], data[j - 1]) = (data[j - 1], data[j]);
	}

    private int Compare(T item1, T item2, IComparer<T>? comparer) => (comparer?.Compare(item1, item2) ?? item1.CompareTo(item2));

}

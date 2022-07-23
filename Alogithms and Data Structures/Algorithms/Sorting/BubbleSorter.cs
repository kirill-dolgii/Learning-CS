namespace Algorithms.Sorting;

public class BubbleSorter<T> : Sorter<T>
where T : IComparable<T>
{
    public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { this.Sort(data, null, order); }

    public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
    {
        // after each iteration [0, i] elements of the given array are sorted
        for (int i = 0; i < data.Length; i++)
			// after each iteration the smallest (biggest) element of the subsequence
			// that contains n - i elements: (i, n] of data
			// is pushed to the left
            for (int j = data.Length - 1; j > i; j--)
				if (Compare(data[j], data[j - 1], comparer) * ((int)order) < 0) 
					(data[j], data[j - 1]) = (data[j - 1], data[j]);
	}
}

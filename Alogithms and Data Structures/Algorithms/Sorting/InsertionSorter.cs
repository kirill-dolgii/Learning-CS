namespace Algorithms.Sorting;
public class InsertionSorter<T> : Sorter<T>
where T : IComparable<T>
{
    public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { this.Sort(data, null, order); }

    public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
    {
		for (int i = 0; i < data.Length - 1; i++)
        {
            int j = i + 1;
            // subsequently swap new element with those elements from the sorted subsequence that are
            // smaller (bigger) than this one
			while (j > 0 && Compare(data[j], data[j - 1], comparer) * ((int)order) < 0)
			{
				(data[j], data[j - 1]) = (data[j - 1], data[j]);
				j--;
			}
        }
    }
}


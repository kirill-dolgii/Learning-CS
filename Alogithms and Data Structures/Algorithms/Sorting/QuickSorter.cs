namespace Algorithms.Sorting;
public class QuickSorter<T> : Sorter<T>
where T : IComparable<T>
{
	public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending)
		=> this.Sort(data, null, order);

	public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
	{
		int split = Partition(data, comparer, order, 0, data.GetUpperBound(0));
		Sort(data, comparer, order, 0, split - 1);
		Sort(data, comparer, order, split + 1, data.GetUpperBound(0));
	}

	private int Partition(T[] data, IComparer<T>? comparer, SortingOrder order, int start, int end)
	{
		if (end - start > 2)
		{
			int pivot = new int[] { start, start + (end - start) / 2, end }.ToDictionary(idx => idx, idx => data[idx]).
																			OrderBy(kv => kv.Value).
																			ToList()[1].Key;
			(data[end], data[pivot]) = (data[pivot], data[end]);
		}

		int i = start - 1;
		for (int j = start; j <= end - 1; j++)
			if (Compare(data[j], data[end], comparer) * ((int)order) <= 0)
				(data[++i], data[j]) = (data[j], data[i]);

		(data[end], data[++i]) = (data[i], data[end]);

		return i;
	}

    private void Sort(T[] data, IComparer<T>? comparer, SortingOrder order, int start, int end)
	{
		if (start >= end || end < 0) return;

		int split = Partition(data, comparer, order, start, end);
		Sort(data, comparer, order, start, split - 1);
		Sort(data, comparer, order, split + 1, end);
	}

}

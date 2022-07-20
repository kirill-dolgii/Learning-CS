namespace Algorithms.Sorting;
public class QuickSorter<T> : Sorter<T>
where T : IComparable<T>
{
	public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { Sort(data, null, order); }

	public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
	{
		
	}

	private void SubSort(T[] subData)
	{

	}

	private void Partition(T[] data)
	{
		T pivot = data[data.Length / 2];

		(data[data.Length / 2], data[^1]) = (data[^1], data[data.Length / 2]);

	}
}


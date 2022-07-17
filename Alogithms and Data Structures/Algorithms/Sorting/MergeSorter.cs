namespace Algorithms.Sorting;


public class MergeSorter<T> : ISorter<T>
where T : IComparable<T>
{
	public MergeSorter(IComparer<T>? comparer = null) {this._comparer = comparer;}
	
	public void Sort(T[] data, SortingOrder order = SortingOrder.Ascending)
	{
        T[] tmpArry = new T[data.Length];
		data.CopyTo(tmpArry, 0);

		SplitMerge(data, tmpArry, 0, data.Length, order);
	}
	
	// split data from tmpData, sort and merge into data
	//						[3, 1, 5, 2]
	//						/			\
	//					[3, 1]		   [5, 2]
	//				   /	 \		  /	     \
	// base case 	 [1]     [3]	 [5]     [2]
	// of recursion
	private void SplitMerge(T[] data, T[] tmpData, int start, int end, SortingOrder order)
	{
		if (end - start <= 1) return;
		int middle = (end + start) / 2;

		SplitMerge(tmpData, data, start, middle, order);
		SplitMerge(tmpData, data, middle, end, order);

		Merge(tmpData, data, start, middle, end, order);
	}

	// merge sorted items of data from start to the end into tmpData
	//			data:	[1, 3]    [2, 5]
	//				         \    /
    //			tmpData	  [1, 2, 3, 5]
	private void Merge(T[] data, T[] tmpData, int start, int middle, int end, SortingOrder order)
	{
		int i = start;
		int j = middle;

		for (int k = start; k < end; k++)
		{
			if (i < middle && (j >= end || this.Compare(data[i], data[j]) * (order == SortingOrder.Ascending ? 1 : -1) <= 0)) tmpData[k] = data[i++];
			else tmpData[k] = data[j++];
		}
	}

	private IComparer<T>? _comparer = null;

	private int Compare(T item1, T item2)
	{
		if (_comparer == null) return item1.CompareTo(item2);
		return _comparer.Compare(item1, item2);
	}

	public void Sort(T[] data, IComparer<T> comparer, SortingOrder order = SortingOrder.Ascending)
	{
		this.Sort(data, order);
	}
}

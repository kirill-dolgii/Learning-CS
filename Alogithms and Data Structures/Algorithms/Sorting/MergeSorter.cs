namespace Algorithms.Sorting;


public class MergeSorter<T> : Sorter<T>
where T : IComparable<T>
{
    public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { this.Sort(data, null, order); }

    // split data from tmpData, sort and merge into data
    //						[3, 1, 5, 2]
    //						/			\
    //					[3, 1]		   [5, 2]
    //				   /	 \		  /	     \
    // base case 	 [1]     [3]	 [5]     [2]
    // of recursion
    private void SplitMerge(T[] data, T[] tmpData, int start, int end, SortingOrder order, IComparer<T>? comp)
    {
        if (end - start <= 1) return;
        int middle = (end + start) / 2;

        SplitMerge(tmpData, data, start, middle, order, comp);
        SplitMerge(tmpData, data, middle, end, order, comp);

        Merge(tmpData, data, start, middle, end, order, comp);
    }

    // merge sorted items of data from start to the end into tmpData
    //			data:	[1, 3]    [2, 5]
    //				         \    /
    //			tmpData	  [1, 2, 3, 5]
    private void Merge(T[] data, T[] tmpData, int start, int middle, int end, SortingOrder order, IComparer<T>? comp)
    {
        int i = start;
        int j = middle;

        for (int k = start; k < end; k++)
        {
            if (i < middle && (j >= end || Compare(data[i], data[j], comp) * ((int)order) <= 0)) tmpData[k] = data[i++];
            else tmpData[k] = data[j++];
        }
    }

    //private int Compare(T item1, T item2, IComparer<T>? comp) => comp?.Compare(item1, item2) ?? item1.CompareTo(item2);

    public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
    {
        T[] tmpArry = new T[data.Length];
        data.CopyTo(tmpArry, 0);

        SplitMerge(data, tmpArry, 0, data.Length, order, comparer);
    }
}

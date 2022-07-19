namespace Algorithms.Sorting;
public class SelectionSorter<T> : Sorter<T>
where T : IComparable<T>
{
    public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending)
    {
        this.Sort(data, null, order);
    }

    public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
    {
        int reverse = (order == SortingOrder.Ascending ? 1 : -1);

        for (int i = 0; i < data.Length; i++)
        {
            // variable to store current smallest (biggest) element
            int nextPriorityIdx = i;

            // find the smallest (biggest) element of subsequence of data that contains n - i elements:
            // data(i, n]
            for (int j = i + 1; j < data.Length; j++) 
				if (Compare(data[j], data[nextPriorityIdx], comparer) * reverse < 0)  
                    nextPriorityIdx = j;

			// add newly found elem to the sorted subsequence data[0, i]
			(data[i], data[nextPriorityIdx]) = (data[nextPriorityIdx], data[i]);
		}
    }
}

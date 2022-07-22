namespace Algorithms.Sorting;

/// <summary>
/// Represents a non-comparative radix sort algorithm implementation for integers.
/// Performs insanely fast sorting by distributing elements into buckets
/// with accordance to their radix. For numbers > 9 performs bucketing
/// process until all digits have been considered.
/// Worse case time complexity is O(w * n), where n is the number of keys and w is
/// the number of digits. 
/// </summary>
public class RadixSorter : Sorter<int>
{
	public override void Sort(int[] data, SortingOrder order = SortingOrder.Ascending)
	{
		int[][] buckets = new int[10][];

		int[] tempArray = new int[data.Length];
		data.CopyTo(tempArray, 0);

		int max = tempArray.Max();
		var maxDigits = (int)Math.Floor(Math.Log10(max) + 1);

		for (int i = 0; i < maxDigits; i++)
		{
			int curDivider = (int)(1 * Math.Pow(10, i + 1));

			tempArray.GroupBy(item => (item % curDivider) / (curDivider / 10)).ToList()
					 .ForEach(gr => buckets[gr.Key] = gr.ToArray());

			tempArray = buckets.Where(bucket => bucket != null).SelectMany(bucket => bucket).ToArray();
		}

		if (order == SortingOrder.Descending) tempArray = tempArray.Reverse().ToArray();

		tempArray.CopyTo(data, 0);
	}

	public override void Sort(int[] data, IComparer<int>? comparer, SortingOrder order = SortingOrder.Ascending)
	{
		if (comparer == null) this.Sort(data, order);
		else throw new NotImplementedException("Not supported");
	}
}

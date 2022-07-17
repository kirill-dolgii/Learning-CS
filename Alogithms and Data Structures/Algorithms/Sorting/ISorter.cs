namespace Algorithms.Sorting;

internal interface ISorter<T>
where T : IComparable<T>
{
	/// <summary>
	/// Sorts sequence by applying default comparer of generic type
	/// </summary>
	/// <param name="data"></param>
	/// <param name="order"></param>
	public static void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) {}

	/// <summary>
	/// Sort sequence with specific comparer
	/// </summary>
	/// <param name="data"></param>
	/// <param name="comparer"></param>
	/// <param name="order"></param>
	public static void Sort(T[] data, IComparer<T> comparer, SortingOrder order = SortingOrder.Ascending) {}
}
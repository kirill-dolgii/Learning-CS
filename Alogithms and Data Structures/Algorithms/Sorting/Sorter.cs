namespace Algorithms.Sorting;

public abstract class Sorter<T>
where T : IComparable<T>
{
	/// <summary>
	/// Sorts sequence by applying default comparer of generic type
	/// </summary>
	/// <param name="data"></param>
	/// <param name="order"></param>
	public abstract void Sort(T[] data, SortingOrder order = SortingOrder.Ascending);

	/// <summary>
	/// Sort sequence with specific comparer
	/// </summary>
	/// <param name="data"></param>
	/// <param name="comparer"></param>
	/// <param name="order"></param>
	public abstract void Sort(T[] data, 
							  IComparer<T>? comparer, 
							  SortingOrder order = SortingOrder.Ascending);

	public static int Compare(T item1, T item2, IComparer<T>? comp) 
		=> comp?.Compare(item1, item2) ?? item1.CompareTo(item2);

}
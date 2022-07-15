using DataStructures;


namespace Algorithms;
public static class Sorting

{
	/// <summary>
	///     Sorts input data using heap sort algorithm.
	/// </summary>
	/// <param name="data">Input data.</param>
	/// <param name="ascending">Specify sorting order.</param>
	public static IEnumerable<T> HeapSort<T>(IEnumerable<T> data, bool ascending = false)
	where T : IComparable<T>
	{
		var          heap   = new Heap<T>(data, ascending);
		foreach (T val in data) yield return heap.ExtractHead();
	}



}
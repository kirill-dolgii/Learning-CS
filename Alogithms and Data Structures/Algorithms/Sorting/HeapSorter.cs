﻿using DataStructures;

namespace Algorithms.Sorting;

public class HeapSorter<T> : Sorter<T>
where T : IComparable<T>
{
	public HeapSorter() { }

	/// <summary>
	///     Sorts input data using the heap sort algorithm.
	/// </summary>
	/// <param name="data">Input data.</param>
	/// <param name="order">Sorting order.</param>
	public override void Sort(T[] data, SortingOrder order = SortingOrder.Ascending) { this.Sort(data, null, order); }

	/// <summary>
	///		Sorts input data using the heap sort algorithm and specified comparer.
	/// </summary>
	/// <param name="data">Input data.</param>
	/// <param name="comparer">Custom comparer</param>
	/// <param name="order">Sorting order.</param>
	public override void Sort(T[] data, IComparer<T>? comparer, SortingOrder order = SortingOrder.Ascending)
	{
		var heap = new Heap<T>(data, order == SortingOrder.Ascending, comparer);
		for (int i = 0; i < data.Length; i++) data[i] = heap.ExtractHead();
	}
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Heap

{	// Author : Dolghi Chirill, 07.03.2022
	//-------------------------------------------------------------------------------------------
	// this data structure implements priority queue -> heap (data is stored inside of an array).
	// both min and max queues are supported.
	// priority queue operations: insert O(log(n) and extractHead O(log(N)) are supported.
	// both O(n) and O(log(N)) constructors are implemented.

	internal class MyHeap<T>
		where T : IComparable<T>
	{
		private T[]  container;
		private uint capacity;

		private uint size = 0;

		private const uint DEFAULT_CAPACITY = 10;

		private IComparer<T> comparer;

		bool IsMinHeap = true;

		public MyHeap() : this(DEFAULT_CAPACITY) { }

		public MyHeap(uint capacity, bool IsMinHeap = false)
		{
			this.IsMinHeap = IsMinHeap;

			this.capacity = capacity;
			container = new T[capacity];
		}

		// O(n * logN) heap constructor
		public MyHeap(T[] data, bool IsMinHeap)
		{
			this.IsMinHeap = IsMinHeap;

			container = new T[data.Count()];
			capacity = (uint)data.Count();
			size = (uint)data.Count();

			foreach (T item in data)
			{
				this.Insert(item);
            }
        }

		// O(n) heap constructor (bubble down all nodes from the half untill the beginning)
		public MyHeap(ICollection<T> data, bool IsMinHeap = false)
		{
			this.IsMinHeap = IsMinHeap;	

			container = new T[data.Count()];
			data.CopyTo(container, 0);
			capacity = (uint)data.Count();
			size = (uint)data.Count();

			for (int i = ((int)size / 2) - 1; i >= 0; i--)
				this.BubbleDown((uint)i);
        }

		public bool IsEmpty() => size == 0;

		public void Clear() 
		{ 
			this.container = new T[capacity < DEFAULT_CAPACITY ? DEFAULT_CAPACITY : capacity];
			size = 0;
		}

		public uint Size() => size;

		public T Peek() => container[0];

		public bool Contains(T elem) => container.Contains(elem);

		// O(log(N)) 
		public void Insert(T item)
		{
			if (size >= capacity)
				throw new IndexOutOfRangeException("not enough space");

			container[size++] = item;
			BubbleUp(size - 1);
		}

		public bool Remove(T elem)
		{
			int elemPos = -1;
			
			foreach (var item in container)
				{
					elemPos++;
					if (item.CompareTo(elem) == 0)
						break;
				}

			if (elemPos >= size - 1)
				return false;

			Swap((uint)elemPos, size - 1);
			Swap((uint)size - 1, capacity-- - 1);
			container = container.SkipLast(1).ToArray();
			BubbleDown((uint)elemPos);

			return true;
		}

		public T ExtractHead()
		{
			T ret = container[0];
			this.Remove(ret);
			size--;
			return ret;
        }

		private void BubbleDown(uint index)
		{
			if (2 * index + 1 >= size)
				return;

			uint minIdx = index;

			for (int i = 1; i <= 2 && index * 2 + i < size - 1; i++)
				{

				if (Compare(container[minIdx], container[index * 2 + i]))
					minIdx = (uint)(index * 2 + i);
				}
			if (minIdx != index)
			{
				Swap(index, minIdx);
				BubbleDown(minIdx);
			}			
		}

		private void BubbleUp(uint index)
		{
			if (index == 0 || index >= size)
				return;

			uint parent = (index - 1) / 2;

			if (Compare(container[parent], container[index]))
				{
					Swap(index, parent);
					BubbleUp(parent);
				}
		}

		private void Swap(uint item1, uint item2)
		{
			if (item1 == item2)
				return;

			var tmp   = container[item1];
			container[item1] = container[item2];
			container[item2] = tmp;
		}

		private bool Compare(T item1, T item2) 
		{
			if (IsMinHeap)
				return item1.CompareTo(item2) > 0 ? true : false;
			else
				return item1.CompareTo(item2) < 0 ? true : false; 
		}
	}

    internal class Program
	{
		private static void Main(string[] args)
		{
			var mh = new MyHeap<int>(10);

			mh.Insert(0);
			mh.Insert(2);
			mh.Insert(3);
			mh.Insert(7);
			mh.Insert(19);
			mh.Insert(24);
			mh.Insert(1);


			int[] d = new int[] { 0, 2, 3, 7, 19, 24, 1 };

			var mh1 = new MyHeap<int>(new List<int>(d), true);

			mh1.ExtractHead();
			mh1.ExtractHead();
			mh1.ExtractHead();
			mh1.ExtractHead();
			mh1.ExtractHead();

		}
	}
}
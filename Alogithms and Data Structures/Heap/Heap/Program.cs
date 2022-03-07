using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heap
{
	
	internal class MyHeap<T>
		where T : IComparable<T>
	{
		private T[]  container;
		private uint capacity;

		private uint size = 0;

		private const uint DEFAULT_CAPACITY = 10;

		public MyHeap() : this(DEFAULT_CAPACITY) { }

		public MyHeap(uint capacity)
		{
			this.capacity = capacity;
			container = new T[capacity];
		}

		public bool IsEmpty() => size == 0;

		public void Clear() { this.container = new T[capacity < DEFAULT_CAPACITY ? DEFAULT_CAPACITY : capacity];}

		public uint Size() => size;

		public T Peek() => container[0];

		public bool Contains(T elem) => container.Contains(elem);

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

		private void BubbleDown(uint index)
		{
			if (2 * index + 1 > size)
				return;

			uint minIdx = index;

			for (int i = 1; i <= 2; i++)
				{
				if (Compare(container[index], container[index * 2 + i]))
					minIdx = (uint)(index * 2 + i);
				}

			Swap(index, minIdx);
			BubbleDown(minIdx);
		}

		private void BubbleUp(uint index)
		{
			if (index == 0)
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
			var tmp   = container[item1];
			container[item1] = container[item2];
			container[item2] = tmp;
		}

		private bool Compare(T item1, T item2) => item1.CompareTo(item2) < 0 ? true : false;
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

			mh.Remove(7);
		}
	}
}
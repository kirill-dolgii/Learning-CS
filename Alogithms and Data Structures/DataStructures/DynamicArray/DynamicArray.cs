using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class DynamicArray<T> : IList<T>
    {
		private int _size;
		private int _capacity;

		private const int DefaultCapacity = 8;

		private int[] _storage;

		public DynamicArray()
		{
			int size;
			this._size = 0;
			this._capacity = DefaultCapacity;
			this._storage = new int[this._capacity];
		}

		public DynamicArray(int size)
		{
			this._size = size;
			this._capacity = this._size > DefaultCapacity ? this._size : DefaultCapacity;
			this._storage = new int[this._capacity];
		}

		public DynamicArray(int[] data)
		{
			this._size = data.Length;
			this._capacity = (int)Math.Pow(2, (int)Math.Ceiling(Math.Log2(this._size)));

			this._storage = new int[this._capacity];
			data.CopyTo(this._storage, 0);
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public int  Count      { get; }
		public bool IsReadOnly { get; }
		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public T this[int index]
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}
	}
}

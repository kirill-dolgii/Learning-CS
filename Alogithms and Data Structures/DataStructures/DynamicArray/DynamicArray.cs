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

		private T[] _storage;

		public DynamicArray()
		{
			this._size = 0;
			this._capacity = DefaultCapacity;
			this._storage = new T[this._capacity];
		}

		public DynamicArray(int size)
		{
			this._size = size;
			this._capacity = this._size > DefaultCapacity ? this._size : DefaultCapacity;
			this._storage = new T[this._capacity];
		}

		public DynamicArray(T[] data)
		{
			this._size = data.Length;
			this._capacity = (int)Math.Pow(2, (int)Math.Ceiling(Math.Log2(this._size)));

			this._storage = new T[this._capacity];
			data.CopyTo(this._storage, 0);
		}

		private class Enumerator<T> : IEnumerator<T>
		{
			public Enumerator(DynamicArray<T> array) {this._array = array;}

			private readonly DynamicArray<T> _array;

			public bool MoveNext()
			{
				return ++this._idx < _array._size;
			}

			public void Reset()
			{
				this._idx = -1;
			}

			private int _idx = -1;

			public T Current => this._array[this._idx];

			object IEnumerator.Current => this.Current;

			public void Dispose()
			{
				//throw new NotImplementedException();
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new Enumerator<T>(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator<T>(this);
		}

		private void Resize()
		{
			this._capacity *= 2;

			T[] tmp = new T[this._capacity];
			this._storage.CopyTo(tmp, 0);
			this._storage = tmp;
		}

		public void Add(T item)
		{
			if (this._size == this._capacity) {this.Resize();}
			this._storage[this._size++] = item;
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
			get
			{
				if (index >= this._size || index < 0) throw new ArgumentOutOfRangeException(nameof(index));
				return this._storage[index];
			}
			set
			{
				if (index >= this._size || index < 0) throw new ArgumentOutOfRangeException(nameof(index));
				throw new NotImplementedException();
			}
		}
	}
}

using System.Collections;

namespace DataStructures;
public class HashSet<T> : ISet<T>
{
	private const int    DEFAULT_CAPACITY    = 32;
	private const double DEFAULT_LOAD_FACTOR = 0.75;

	private const double RESIZE_SCALE = 0.5;

	private readonly Func<T, int>?         _hashFunc;
	private readonly IEqualityComparer<T>? _comparer;

	private Entity[]     _items;
	private int          _size;
	private List<Entity> _addedItems;

	private readonly double _loadFactor;

	private HashSet(IEnumerable<T> data,
					IEqualityComparer<T> comparer,
					Func<T, int>? hashFunc,
					int initialCapacity,
					double loadFactor = DEFAULT_LOAD_FACTOR)
	{
		if (data == null) throw new ArgumentNullException($"{nameof(data)} was null.");
		if (initialCapacity < 0) throw new ArgumentException($"{nameof(initialCapacity)} is below zero.");
		if (loadFactor <= 0) throw new ArgumentException($"{loadFactor} must me > 0.");

		_hashFunc = hashFunc;

		_loadFactor = loadFactor;
		_comparer = comparer;

		int capacity = new[] { initialCapacity, DEFAULT_CAPACITY, (int)(data.Count() / loadFactor) }.Max();

		_items = new Entity[capacity];
		_addedItems = new List<Entity>(capacity);

		foreach (var d in data) (this as ICollection<T>).Add(d);
	}

	/// <summary>
	/// Instantiates an empty HashSet with default EqualityComparer and GetHashCode.
	/// </summary>
	public HashSet() : this(Enumerable.Empty<T>(), 
							EqualityComparer<T>.Default, 
							null, 
							DEFAULT_CAPACITY) {}

	public HashSet(IEnumerable<T> data) : this(data,
											   EqualityComparer<T>.Default,
											   null,
											   DEFAULT_CAPACITY) {}

	public HashSet(IEqualityComparer<T> comparer) : this(Enumerable.Empty<T>(), comparer) {}

	public HashSet(int count) : this(Enumerable.Empty<T>(), null, null, count) {}

	public HashSet(int count, EqualityComparer<T> comparer) : this(Enumerable.Empty<T>(), comparer, null, count) {}

	public HashSet(IEnumerable<T> data,
				   IEqualityComparer<T> comparer) : this(data,
														 comparer, 
														 null, DEFAULT_CAPACITY) {}

	public HashSet(IEnumerable<T> data, 
				   IEqualityComparer<T> comparer, 
				   Func<T, int> hashFunc) : this(data, 
												 comparer, 
												 hashFunc, 
												 DEFAULT_CAPACITY){}

	private int GetHash(T item)
	{
		if (item == null) throw new ArgumentNullException(nameof(item));
		return Math.Abs(_hashFunc?.Invoke(item) ?? _comparer?.GetHashCode(item) ?? item.GetHashCode());
	}

	private int Probe(int index, int i)
	{
		return (index + 1) % Capacity;
	}

	void ICollection<T>.Add(T item)
	{
		if (item == null) throw new ArgumentNullException();
		if (_size >= (int)(Capacity * _loadFactor)) Capacity = (int)(Capacity / _loadFactor / RESIZE_SCALE);

		if ((this as ICollection<T>).Contains(item)) 
			throw new NotSupportedException($"{nameof(item)} is already presented in the hashset.");
		
		int hashCode = GetHash(item);
		int index    = hashCode % Capacity;
		
		for (int i = 0; 
			 _items[index] != null &&
			 !_items[index]!.IsDeleted;
			 i++)
		{
			index = Probe(index, i);
		}

		var entity = new Entity(item, GetHash(item));

		_items[index] = entity;
		_size++;
		_addedItems.Add(entity);
	}

	public void ExceptWith(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public void IntersectWith(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public bool IsProperSubsetOf(IEnumerable<T> other) => throw new NotImplementedException();

	public bool IsProperSupersetOf(IEnumerable<T> other) => throw new NotImplementedException();

	public bool IsSubsetOf(IEnumerable<T> other) => throw new NotImplementedException();

	public bool IsSupersetOf(IEnumerable<T> other) => throw new NotImplementedException();

	public bool Overlaps(IEnumerable<T> other) => throw new NotImplementedException();

	public bool SetEquals(IEnumerable<T> other) => throw new NotImplementedException();

	public void SymmetricExceptWith(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	public void UnionWith(IEnumerable<T> other)
	{
		throw new NotImplementedException();
	}

	bool ISet<T>.Add(T item)
	{
		if (Contains(item)) return false;
		(this as ICollection<T>).Add(item);
		return true;
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public bool Contains(T item)
	{
		if (item == null) throw new NullReferenceException("item");

		int hashCode = GetHash(item);
		int index    = hashCode % Capacity;

		for (int i = 0; _items[index] != null; i++)
		{
			if (_comparer?.Equals(_items[index]!.Item, item) ??
				EqualityComparer<T>.Default.Equals(_items[index]!.Item, item) 
				&& _items[index]!.HashCode == hashCode)
				return true;
			index = Probe(index, i);
		}

		return false;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

	public bool Remove(T item)
    {
        if (item == null) throw new ArgumentNullException($"{nameof(item)} was null.");
        if (!Contains(item)) return false;

        int hashCode = GetHash(item);
        int index = hashCode % Capacity;

        for (int i = 0; _items[index] != null; index = Probe(index, i++))
            if (_comparer?.Equals(_items[index]!.Item, item) ??
                EqualityComparer<T>.Default.Equals(_items[index]!.Item, item) && 
				_items[index]!.HashCode == hashCode)
            {
                _items[index]!.IsDeleted = true;
				if (_size <= (int)(0.5 * _loadFactor * Capacity)) Capacity = (int)(Capacity * 0.5);
                return true;
            }

        return false;
    }

	public int Capacity
	{
		get => _items.Length;
		set
		{
			if (value < 0) throw new ArgumentOutOfRangeException("value");
			if (value == Capacity) return;
			
			var tmp = _addedItems.Where(ai => !ai.IsDeleted).ToList();
			_items = new Entity[value];;
			_addedItems = new List<Entity>(value);
			_size = 0;

			foreach (var entity in tmp) (this as ICollection<T>).Add(entity.Item);
		}
	}

	public int Count => _size;

	public bool IsReadOnly => false;

	public IEnumerator<T> GetEnumerator() => _addedItems.Where(ai => !ai.IsDeleted).Select(ent => ent.Item).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private class Entity
	{
		public readonly T    Item;
		public readonly int  HashCode;
		public bool IsDeleted;

		public Entity(T item, int hashCode)
		{
			Item = item;
			HashCode = hashCode;
			IsDeleted = false;
		}
	}
}


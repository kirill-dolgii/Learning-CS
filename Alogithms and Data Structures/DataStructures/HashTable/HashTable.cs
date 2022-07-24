using System.Collections;

namespace DataStructures.HashTable;

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
{
	private const int    DEFAULT_CAPACITY    = 32;
	private const double DEFAULT_LOAD_FACTOR = 0.75;
	private const double RESIZE_SCALE        = 0.65;

	private TKey[]               _keys;
	private TValue[]             _values;
	private LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;
	private int                  _size;
	private int                  _capacity;

	private readonly double             _loadFactor;
	private readonly HashFunction<TKey> _hashFunc;

	private HashTable(IEnumerable<KeyValuePair<TKey, TValue>> kvs,
					  int initialCapacity, 
					  double loadFactor, 
					  HashFunction<TKey> hashFunc)
	{
		if (kvs == null) throw new NullReferenceException(nameof(kvs));
		if (initialCapacity < 0) throw new ArgumentException(nameof(initialCapacity));
		if (loadFactor < 0) throw new ArgumentException(nameof(loadFactor));

		_keys = new TKey[initialCapacity];
		_values = new TValue[initialCapacity];
		_buckets = new LinkedList<KeyValuePair<TKey, TValue>>[initialCapacity];
		_size = kvs.Count();
        _capacity = initialCapacity;
        _loadFactor = loadFactor;
        _hashFunc = hashFunc;

		foreach (var kv in kvs) this.Add(kv);
	}

	public HashTable() : this(Enumerable.Empty<KeyValuePair<TKey, TValue>>(),
							  DEFAULT_CAPACITY,
							  DEFAULT_LOAD_FACTOR, new()) {}

	public HashTable(HashFunction<TKey> hf) : this() { _hashFunc = hf; }

	private int AdjustIndex(int hash) => hash % _capacity;

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		Resize();
		
		int hash = _hashFunc.GetHash(item.Key);
		int bucketIndex = AdjustIndex(hash);

		_buckets[bucketIndex] ??= new();

		_buckets[bucketIndex].AddLast(item);
		_keys[_size] = item.Key;
		_values[_size++] = item.Value;
	}

	private void Resize()
	{
		if (_size <= _capacity * _loadFactor && _size <= _capacity * Math.Pow(_loadFactor, 2)) return;

		var tmp = _keys.Where(k => k != null).Zip(_values.Where(v => v != null), 
												  (k, v) => new KeyValuePair<TKey, TValue>(k, v)).ToArray();

		if (_capacity >= _capacity * _loadFactor) _capacity = (int)(_capacity / _loadFactor / RESIZE_SCALE);
		if (_capacity <= _capacity * _loadFactor * RESIZE_SCALE) _capacity = (int)(_capacity * _loadFactor);

		_keys = new TKey[_capacity];
		_values = new TValue[_capacity];
		_buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
		_size = 0;

		foreach (var kv in tmp) this.Add(kv);
	}

	public void Clear()
	{
		_keys = new TKey[_capacity];
		_values = new TValue[_capacity];
		_size = 0;
		_buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
		Resize();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		if (_size == 0) return false;
		int hash = _hashFunc.GetHash(item.Key);
		int bucketIndex = AdjustIndex(hash);
		return _buckets[bucketIndex].Contains(item);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

	public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		throw new NotImplementedException();
	}

	public int  Count      => _size;
	public bool IsReadOnly => false;
	public void Add(TKey key, TValue value)
	{
		throw new NotImplementedException();
	}

	public bool ContainsKey(TKey key)
	{
		throw new NotImplementedException();
	}

	public bool Remove(TKey key)
	{
		throw new NotImplementedException();
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		throw new NotImplementedException();
	}

	public TValue this[TKey key]
	{
		get
		{
			int hash = _hashFunc.GetHash(key);
			int adjustedHash = AdjustIndex(hash);

			return _buckets[adjustedHash].First(node => EqualityComparer<TKey>.Default.Equals(node.Key, key)).Value;
		}
		set => throw new NotImplementedException();
	}

	public ICollection<TKey> Keys => this._keys;

	public ICollection<TValue> Values => null;
	
	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}


using System.Collections;

namespace DataStructures.HashTable;

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
{
	private const int DEFAULT_CAPACITY = 32;
	private const double DEFAULT_LOAD_FACTOR = 0.75;

	private TKey[]               _keys;
	private LinkedList<TValue>[] _buckets;
	private int                  _size;
	private int                  _capacity;

	private double               _loadFactor;
	private HashFunction<TKey>   _hashFunc;
	

	public HashTable()
	{
		_keys = new TKey[DEFAULT_CAPACITY];
		_buckets = Enumerable.Range(0, _capacity + 1).Select(i => new LinkedList<TValue>()).ToArray();
		_size = 0;
		_capacity = DEFAULT_CAPACITY;
		_loadFactor = DEFAULT_LOAD_FACTOR;
		_hashFunc = new();
	}

	private int AdjustIndex(int hash) => hash % _capacity;

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		int hash = _hashFunc.GetHash(item.Key);
		int bucketIndex = AdjustIndex(hash);
		
		_buckets[bucketIndex].AddLast(item.Value);
		_keys[bucketIndex] = item.Key;
		_size++;
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		throw new NotImplementedException();
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
		get => throw new NotImplementedException();
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


using System.Collections;

namespace DataStructures.HashTable;
public abstract class HashTableOpenAddressingBase<TKey, TValue> : IDictionary<TKey, TValue>
{
	private struct KeyValuePairEntity
	{
		public bool                       isDeleted;
		public KeyValuePair<TKey, TValue> kv;
		public KeyValuePairEntity(KeyValuePair<TKey, TValue> kv) { this.kv = kv; isDeleted = false; }
	}


	private const int    DEFAILT_CAPACITY    = 32;
	private const double DEFAULT_LOAD_FACTOR = 0.75;
	private const double RESIZE_SCALE        = 0.65;

	private TKey[]   _keys;
	private TValue[] _values;

	private KeyValuePairEntity?[] _entities;

	private int _size;
	private int _capacity;

	private double             _loadFactor;
	private HashFunction<TKey> _hf;

	private HashTableOpenAddressingBase(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								       int initialCapacity, 
								       double loadFactor, HashFunction<TKey> hf)
	{
		if (initialCapacity < 0) throw new ArgumentException(nameof(initialCapacity));
		if (loadFactor <= 0 || loadFactor > 1) throw new ArgumentException(nameof(loadFactor));
		if (data == null) throw new ArgumentNullException(nameof(data));

		_capacity = new int[] { DEFAILT_CAPACITY, initialCapacity, data.Count() }.Max();
		_entities = new KeyValuePairEntity?[_capacity];
		_loadFactor = loadFactor;
		_hf = hf;
	}

	public HashTableOpenAddressingBase() : this(Enumerable.Empty<KeyValuePair<TKey, TValue>>(), 
											   DEFAILT_CAPACITY, 
										       DEFAULT_LOAD_FACTOR, 
										       new()) {}

	private int AdjustedHash(TKey key) => Math.Abs(_hf.GetHash(key)) % _capacity;

	protected virtual int Probe(int index, int i) => index + i;

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		Resize();

		if (item.Key == null) throw new NotSupportedException($"{nameof(item.Key)} was null.");
		int index = AdjustedHash(item.Key);

		for (var i = 1; _entities[index].HasValue; i++) index = Probe(index, i);
		_entities[index] = new KeyValuePairEntity(item);
		_size++;
	}

	private void Resize()
	{
		if (_size <= _capacity * _loadFactor || _size > _capacity * _loadFactor * RESIZE_SCALE) return;

		var existing = _entities.Where(ent => ent is { isDeleted: false }).ToArray();

		if (_capacity >= _capacity * _loadFactor) _capacity = (int)(_capacity / _loadFactor / RESIZE_SCALE);
		if (_capacity <= _capacity * _loadFactor * RESIZE_SCALE) _capacity = (int)(_capacity * _loadFactor);

		_entities = new KeyValuePairEntity?[_capacity];

		foreach (var ex in existing) this.Add(ex.Value.kv);
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

	public int  Count      { get; }
	public bool IsReadOnly { get; }
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

	public ICollection<TKey>   Keys   { get; }
	public ICollection<TValue> Values { get; }

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}


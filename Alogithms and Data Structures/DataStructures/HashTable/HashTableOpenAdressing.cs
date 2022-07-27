using System.Collections;

namespace DataStructures.HashTable;
public abstract class HashTableOpenAddressingBase<TKey, TValue> : IDictionary<TKey, TValue>
{
	private class KeyValuePairEntity
	{
		public bool                       isDeleted;
		public KeyValuePair<TKey, TValue> kv;
		public KeyValuePairEntity(KeyValuePair<TKey, TValue> kv, bool isDeleted) { this.kv = kv; this.isDeleted = isDeleted; }

		public void Delete() { isDeleted = true; }
	}

	private const int    DEFAILT_CAPACITY    = 4;
	private const double DEFAULT_LOAD_FACTOR = 0.75;
	private const double RESIZE_SCALE        = 0.65;

	private IEqualityComparer<TKey> keyCmp = EqualityComparer<TKey>.Default;
	private IEqualityComparer<TValue> valCmp = EqualityComparer<TValue>.Default;

	private TKey[]   _keys;
	private TValue[] _values;

	private KeyValuePairEntity?[] _entities;

	private int _size;
	protected int _capacity;

	private double             _loadFactor;
	private HashFunction<TKey> _hf = new HashFunction<TKey>();

	private List<KeyValuePairEntity> _addedValues;

	private HashTableOpenAddressingBase(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								       int initialCapacity, 
								       double loadFactor, HashFunction<TKey> hf)
	{
		if (initialCapacity < 0) throw new ArgumentException(nameof(initialCapacity));
		if (loadFactor <= 0 || loadFactor > 1) throw new ArgumentException(nameof(loadFactor));
		if (data == null) throw new ArgumentNullException(nameof(data));

		_loadFactor = loadFactor;
		_capacity = new int[] { DEFAILT_CAPACITY, initialCapacity, 
								(int)(data.Count() / _loadFactor) }.Max();

		_entities = new KeyValuePairEntity?[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);
		_hf = hf;

		foreach (var kv in data) this.Add(kv);
	}

	private protected HashTableOpenAddressingBase() : this(Enumerable.Empty<KeyValuePair<TKey, TValue>>(), 
														   DEFAILT_CAPACITY, 
														   DEFAULT_LOAD_FACTOR, 
														   new()) {}

	private protected HashTableOpenAddressingBase(IEnumerable<KeyValuePair<TKey, TValue>> data) : this(data, 
																									   DEFAILT_CAPACITY, 
																									   DEFAULT_LOAD_FACTOR, 
																									   new()) {}

	private protected HashTableOpenAddressingBase(IEnumerable<KeyValuePair<TKey, TValue>> data, 
												  HashFunction<TKey> hashFunc) : this(data,
																					  DEFAILT_CAPACITY,
																					  DEFAULT_LOAD_FACTOR,
																					  hashFunc) {}

	private int AdjustedHash(TKey key) => Math.Abs(_hf.GetHash(key)) % _capacity;

	protected abstract int Probe(int index, int i);

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new ArgumentNullException($"{nameof(item.Key)} was null.");
		if (ContainsKey(item.Key))
			throw new NotSupportedException($"An element with {nameof(item.Key)} key already exists");

		if (_size >= _capacity * _loadFactor) Resize((int)(_capacity / RESIZE_SCALE));

		int index = AdjustedHash(item.Key);

		for (var i = 0; _entities[index] != null && !_entities[index]!.isDeleted; index = Probe(index, i++)) { }
		
		_entities[index] = new KeyValuePairEntity(item, false);
		_addedValues.Add(_entities[index]!);
		_size++;
	}

	private void Resize(int newCapacity)
	{
		var existing = _addedValues.Where(ent => ent is { isDeleted: false }).ToArray();

		_capacity = newCapacity;

		_entities = new KeyValuePairEntity?[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);
		_size = 0;

		foreach (var ex in existing) this.Add(ex.kv);
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	private int FindEntityIndex(TKey key)
	{
		int index = AdjustedHash(key);
		if (_entities[index] == null) return -1;

		for (int i = 0; _entities[index] != null && i <= _capacity; index = Probe(index, i++))
			if (keyCmp.Equals(_entities[index]!.kv.Key, key))
			{
				if (!_entities[index]!.isDeleted) return index;
				return -1;
			}

		return -1;
	}

	public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key);

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

    public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new ArgumentNullException($"{nameof(item.Key)} was null.");
		int index = AdjustedHash(item.Key);

		if (_entities[index] == null) return false;
		for (int i = 0; _entities[index] != null && i <= _capacity; index = Probe(index, i++))
			if (valCmp.Equals(_entities[index]!.kv.Value, item.Value))
			{
				_entities[index]!.Delete();
				_entities[index] = new KeyValuePairEntity(item, true);
				_size--;
				if (_size <= _capacity * _loadFactor * RESIZE_SCALE && _size > DEFAILT_CAPACITY)
					Resize((int)(_capacity * RESIZE_SCALE));
				return true;
			}

		return false;
	}

	public int  Count      { get => _size; }
	public bool IsReadOnly { get => false; }
	public void Add(TKey key, TValue value)
	{
		if (this.ContainsKey(key)) 
			throw new NotSupportedException($"{nameof(key)} is already presented in the hash table");

		this.Add(new KeyValuePair<TKey, TValue>(key, value));
	}

	public bool ContainsKey(TKey key)
	{
		if (key == null) throw new ArgumentNullException($"{nameof(key)} was null.");
		int index = FindEntityIndex(key);
		return index != -1;
	}

	public bool Remove(TKey key)
	{
		if (key == null) throw new ArgumentNullException($"{nameof(key)} was null.");
		int index = AdjustedHash(key);

		if (_entities[index] == null) return false;
		for (int i = 0; _entities[index] != null&& i <= _capacity; index = Probe(index, i++))
		{
			if (keyCmp.Equals(key, _entities[index].kv.Key))
			{
				_entities[index]!.Delete();
				_entities[index] = new KeyValuePairEntity(new KeyValuePair<TKey, TValue>(key, default(TValue)!), true);
				_size--;
				if (_size <= _capacity * _loadFactor * RESIZE_SCALE && _size > DEFAILT_CAPACITY)
					Resize((int)(_capacity * RESIZE_SCALE));
				return true;
			}
		}

		return false;
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


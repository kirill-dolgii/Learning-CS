using System.Collections;

namespace DataStructures.HashTable;
public abstract class HashTableOpenAddressingBase<TKey, TValue> : IDictionary<TKey, TValue>
{
	private class KeyValuePairEntity
	{
		public bool                       IsDeleted;
		public KeyValuePair<TKey, TValue> Kv;
		public KeyValuePairEntity(KeyValuePair<TKey, TValue> kv, bool isDeleted) { this.Kv = kv; this.IsDeleted = isDeleted; }

		public void Delete() { IsDeleted = true; }
	}

	private const int    DEFAILT_CAPACITY    = 4;
	private const double DEFAULT_LOAD_FACTOR = 0.75;
	private const double RESIZE_SCALE        = 0.65;

	private readonly IEqualityComparer<TKey>   _keyCmp = EqualityComparer<TKey>.Default;
	private readonly IEqualityComparer<TValue> _valCmp = EqualityComparer<TValue>.Default;

	private KeyValuePairEntity?[] _entities;

	private   int _size;
	protected int Capacity;

	private readonly double             _loadFactor;
	private readonly HashFunction<TKey> _hf;

	private List<KeyValuePairEntity> _addedValues;

	private protected HashTableOpenAddressingBase(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								        int initialCapacity, 
								        double loadFactor, 
										HashFunction<TKey> hf)
	{
		if (initialCapacity < 0) throw new ArgumentException(nameof(initialCapacity));
		if (loadFactor <= 0 || loadFactor > 1) throw new ArgumentException(nameof(loadFactor));
		if (data == null) throw new ArgumentNullException(nameof(data));

		_loadFactor = loadFactor;
		Capacity = new int[] { DEFAILT_CAPACITY, initialCapacity, 
								(int)(data.Count() / _loadFactor) }.Max();

		_entities = new KeyValuePairEntity?[Capacity];
		_addedValues = new List<KeyValuePairEntity>(Capacity);
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

	private int AdjustedHash(TKey key) => Math.Abs(_hf.GetHash(key)) % Capacity;

	protected abstract int Probe(int index, int i);

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new ArgumentNullException($"{nameof(item.Key)} was null.");
		if (ContainsKey(item.Key))
			throw new NotSupportedException($"An element with {nameof(item.Key)} key already exists");

		if (_size >= Capacity * _loadFactor) Resize((int)(Capacity / RESIZE_SCALE));

		int index = AdjustedHash(item.Key);

		for (var i = 0; _entities[index] != null && !_entities[index]!.IsDeleted; index = Probe(index, i++)) { }
		
		_entities[index] = new KeyValuePairEntity(item, false);
		_addedValues.Add(_entities[index]!);
		_size++;
	}

	private void Resize(int newCapacity)
	{
		var existing = _addedValues.Where(ent => ent is { IsDeleted: false }).ToArray();

		Capacity = newCapacity;

		_entities = new KeyValuePairEntity?[Capacity];
		_addedValues = new List<KeyValuePairEntity>(Capacity);
		_size = 0;

		foreach (var ex in existing) this.Add(ex.Kv);
	}

	public void Clear()
	{
		_entities = new KeyValuePairEntity[Capacity];
		_addedValues = new List<KeyValuePairEntity>(Capacity);
		_size = 0;
	}

	private int FindEntityIndex(TKey key)
	{
		int index = AdjustedHash(key);
		if (_entities[index] == null) return -1;

		for (int i = 0; _entities[index] != null && i <= Capacity; index = Probe(index, i++))
			if (_keyCmp.Equals(_entities[index]!.Kv.Key, key))
			{
				if (!_entities[index]!.IsDeleted) return index;
				return -1;
			}

		return -1;
	}

	public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key);

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		if (array == null) throw new ArgumentNullException("array");
		if (arrayIndex < 0) throw new ArgumentException("arrayIndex");
		_addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv).ToArray().CopyTo(array, arrayIndex);
	}

	public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

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
		int index = FindEntityIndex(key);

		if (index == -1) return false;

        _entities[index]!.Delete();
        _entities[index] = new KeyValuePairEntity(new KeyValuePair<TKey, TValue>(key, default(TValue)!), true);

		if (--_size <= Capacity * _loadFactor * RESIZE_SCALE && _size > DEFAILT_CAPACITY)
            Resize((int)(Capacity * RESIZE_SCALE));

		return true;
    }

	public bool TryGetValue(TKey key, out TValue value)
	{
		throw new NotImplementedException();
	}

	public TValue this[TKey key]
	{
		get
		{
			if (key == null) throw new ArgumentNullException($"{nameof(key)} was null");
			int index = FindEntityIndex(key);
			if (index == -1) throw new KeyNotFoundException($"{nameof(key)} is not presented in the hash table.");

			return _entities[index]!.Kv.Value;
		}
		set
		{
			if (key == null) throw new ArgumentNullException($"{nameof(key)} was null");
			int index = FindEntityIndex(key);
			if (index == -1) throw new KeyNotFoundException($"{nameof(key)} is not presented in the hash table.");
			_entities[index]!.Kv = new KeyValuePair<TKey, TValue>(key, value);
		}
	}

	public ICollection<TKey> Keys => _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv.Key).ToList();
	public ICollection<TValue> Values => _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv.Value).ToList();

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() 
		=> _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


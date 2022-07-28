using System.Collections;

namespace DataStructures.HashTable;

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
{
	private class KeyValuePairEntity
	{
		public bool                       IsDeleted;
		public KeyValuePair<TKey, TValue> Kv;
		public KeyValuePairEntity(KeyValuePair<TKey, TValue> kv, bool isDeleted) { this.Kv = kv; this.IsDeleted = isDeleted; }

		public void Delete() { IsDeleted = true; }
	}

	private const int    DEFAULT_CAPACITY    = 32;
	private const double DEFAULT_LOAD_FACTOR = 0.75;
	private const double RESIZE_SCALE        = 0.65;

	private readonly IEqualityComparer<TKey> _keyComp = EqualityComparer<TKey>.Default;

	private LinkedList<KeyValuePairEntity>?[] _buckets;
	private List<KeyValuePairEntity>          _addedValues;
	private int                               _size;
	private int                               _capacity;

	private readonly double              _loadFactor;
	private readonly HashFunction<TKey> _hashFunc;

	public HashTable(IEnumerable<KeyValuePair<TKey, TValue>> kvs,
					 int initialCapacity, 
					 double loadFactor, 
					 HashFunction<TKey> hashFunc)
	{
		if (kvs == null) throw new NullReferenceException(nameof(kvs));
		if (initialCapacity < 0) throw new ArgumentException(nameof(initialCapacity));
		if (loadFactor < 0 || loadFactor > 1) throw new ArgumentException(nameof(loadFactor));

		int size = kvs.Count();
		_capacity = new int[] { initialCapacity, DEFAULT_CAPACITY, (int)(size / _loadFactor) }.Max();

		_buckets = new LinkedList<KeyValuePairEntity>[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);

		_loadFactor = loadFactor;
        _hashFunc = hashFunc ?? new();

		foreach (var kv in kvs) this.Add(kv);
	}

	public HashTable() : this(Enumerable.Empty<KeyValuePair<TKey, TValue>>(),
							  DEFAULT_CAPACITY,
							  DEFAULT_LOAD_FACTOR, new()) {}

	public HashTable(HashFunction<TKey> hf) : this() { _hashFunc = hf; }

	public HashTable(IEnumerable<KeyValuePair<TKey, TValue>> data, 
					 HashFunction<TKey> hf) : this (data, data.Count(), DEFAULT_LOAD_FACTOR, hf) {}

	private int AdjustedHash(TKey key) => (int)(_hashFunc.GetHash(key) % _capacity);
	
	public void Add(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new ArgumentNullException(nameof(item.Key));
		if (ContainsKey(item.Key))
			throw new NotSupportedException($"{nameof(item.Key)} key is already presented in the hash table.");
		
		int index = AdjustedHash(item.Key);

		_buckets[index] ??= new();
		var entity = new KeyValuePairEntity(item, false);

		_buckets[index]!.AddLast(entity);
		_addedValues.Add(entity);

		_size++;
		if (this._size >= _capacity * _loadFactor) Resize((int)(_capacity / _loadFactor / RESIZE_SCALE));
	}

	private void Resize(int newCapacity)
	{
		var tmp = _addedValues.Where(ent => !ent.IsDeleted).Select(b => b.Kv).ToArray();
		_capacity = newCapacity;

		_buckets = new LinkedList<KeyValuePairEntity>[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);

		_size = 0;

		foreach (var kv in tmp) this.Add(kv);
	}

	public void Clear()
	{
		_size = 0;
		_buckets = new LinkedList<KeyValuePairEntity>[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);
	}

	private LinkedListNode<KeyValuePairEntity>? BucketFindNode(TKey key)
	{
		if (key == null) throw new ArgumentNullException(nameof(key));
		int index = AdjustedHash(key);
		if (_buckets[index] == null || _buckets[index]!.First == null) return null;
		var head = _buckets[index]!.First;
		for (int i = 0; !_keyComp.Equals(head!.Value.Kv.Key, key); head = head.Next) 
			if (head.Next == null) return null;
		return head;
	}

	public bool Contains(KeyValuePair<TKey, TValue> item) => BucketFindNode(item.Key) != null;

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) 
		=> _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv).ToArray().CopyTo(array, arrayIndex);

	public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

	public int  Count      => _size;
	public bool IsReadOnly => false;
	public void Add(TKey key, TValue value) => Add(new KeyValuePair<TKey, TValue>(key, value));

	public bool ContainsKey(TKey key) => Contains(new KeyValuePair<TKey, TValue>(key, default(TValue)));

	public bool Remove(TKey key)
	{
		if (key == null) throw new ArgumentNullException(nameof(key));
		var node = BucketFindNode(key);
		if (node == null) return false;
        node.Value.Delete();
		node.List!.Remove(node);
		node.Value.Delete();
		_size--;
		if (_size <= _capacity * _loadFactor * 0.4) Resize((int)(_capacity / _loadFactor * 0.5));
		return true;
	}
	
	public bool TryGetValue(TKey key, out TValue value)
	{
		bool comp = ContainsKey(key);
		value = (comp ? this[key] : default)!;
		return comp;
	}

	public TValue this[TKey key]
	{
		get
		{
			if (key == null) throw new ArgumentNullException(nameof(key));
			var node = BucketFindNode(key);
			if (node == null) throw new KeyNotFoundException($"{nameof(key)} is not presented in the hash table.");
			return node.Value.Kv.Value;
		}
		set
		{
			if (key == null) throw new ArgumentNullException(nameof(key));
			var node = BucketFindNode(key);
			if (node == null) throw new KeyNotFoundException($"{nameof(key)} is not presented in the hash table.");
			node.Value = new KeyValuePairEntity(new KeyValuePair<TKey, TValue>(key, value), false);
		}
	}
	
	public ICollection<TKey> Keys => _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv.Key).ToList();

	public ICollection<TValue> Values => _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv.Value).ToList();
	
	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		var enumerable = _addedValues.Where(ent => !ent.IsDeleted).Select(ent => ent.Kv);
		return enumerable.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


﻿using System.Collections;

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

	private TKey[]   _keys;
	private TValue[] _values;

	private KeyValuePairEntity?[] _entities;

	private int _size;
	private int _capacity;

	private double             _loadFactor;
	private HashFunction<TKey> _hf;

	private List<KeyValuePairEntity> _addedValues;

	private HashTableOpenAddressingBase(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								       int initialCapacity, 
								       double loadFactor, HashFunction<TKey> hf)
	{
		if (initialCapacity < 0) throw new ArgumentException(nameof(initialCapacity));
		if (loadFactor <= 0 || loadFactor > 1) throw new ArgumentException(nameof(loadFactor));
		if (data == null) throw new ArgumentNullException(nameof(data));

		_capacity = new int[] { DEFAILT_CAPACITY, initialCapacity, data.Count() }.Max();
		_entities = new KeyValuePairEntity?[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);
		_loadFactor = loadFactor;
		_hf = hf;
	}

	public HashTableOpenAddressingBase() : this(Enumerable.Empty<KeyValuePair<TKey, TValue>>(), 
											   DEFAILT_CAPACITY, 
										       DEFAULT_LOAD_FACTOR, 
										       new()) {}

	private int AdjustedHash(TKey key) => Math.Abs(_hf.GetHash(key)) % _capacity;

	protected virtual int Probe(int index, int i) => (index + i) % _capacity;

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new NotSupportedException($"{nameof(item.Key)} was null.");
		Resize();

		int index = AdjustedHash(item.Key);

		for (var i = 1; _entities[index] != null && !_entities[index]!.isDeleted; i++) index = Probe(index, i);
		_entities[index] = new KeyValuePairEntity(item, false);
		_addedValues.Add(_entities[index]!);
		_size++;
	}

	private void Resize()
	{
		if (_size < _capacity * _loadFactor) return;

		var existing = _addedValues.Where(ent => ent is { isDeleted: false }).ToArray();

		if (_capacity >= _capacity * _loadFactor) _capacity = (int)(_capacity / _loadFactor / RESIZE_SCALE);
		if (_capacity <= _capacity * _loadFactor * RESIZE_SCALE) _capacity = (int)(_capacity * _loadFactor);
		
		_entities = new KeyValuePairEntity?[_capacity];
		_addedValues = new List<KeyValuePairEntity>(_capacity);
		_size = 0;

		foreach (var ex in existing) this.Add(ex.kv);
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new NotSupportedException($"{nameof(item.Key)} was null.");
		int index = AdjustedHash(item.Key);

		if (_entities[index] == null) return false;

		for (int i = 1; _entities[index] != null; i++)
		{
			if (EqualityComparer<TValue>.Default.Equals(_entities[index]!.kv.Value, item.Value)) 
				return true;
			index = Probe(index, i);
		}
		return false;
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

    public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		if (item.Key == null) throw new NotSupportedException($"{nameof(item.Key)} was null.");
		int index = AdjustedHash(item.Key);

		if (_entities[index] == null) return false;
		for (int i = 1; _entities[index] != null; i++)
		{
			if (EqualityComparer<TValue>.Default.Equals(_entities[index]!.kv.Value, item.Value))
			{
				_entities[index]!.Delete();
				_entities[index] = new KeyValuePairEntity(item, true);
				_size--;
				return true;
			}

			index = Probe(index, i);
		}

		return false;
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


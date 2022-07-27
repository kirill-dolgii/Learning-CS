namespace DataStructures.HashTable;
public class HashTableLinearProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue>
{
	public HashTableLinearProbing() : base() {}

	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data) : base(data) {}

	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								  HashFunction<TKey> hf) : base(data, hf) {}

    public HashTableLinearProbing(HashFunction<TKey> hashFunc) : base(Enumerable.Empty<KeyValuePair<TKey, TValue>>(), 
																	  hashFunc) {}

	protected override int Probe(int index, int i) => (index + i) % this._capacity;
}


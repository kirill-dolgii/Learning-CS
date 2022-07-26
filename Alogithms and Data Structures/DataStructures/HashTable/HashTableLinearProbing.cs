namespace DataStructures.HashTable;
public class HashTableLinearProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue>
{
	public HashTableLinearProbing() : base() {}

	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data) : base(data) {}

	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								  HashFunction<TKey> hf) : base(data, hf) {}

	protected override int Probe(int index, int i) => (index + i) % this._capacity;
}


namespace DataStructures.HashTable;
public class HashTableLinearProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue>
{
	public HashTableLinearProbing() : base() {}

	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data) : base(data) {}

	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								  HashFunction<TKey> hf) : base(data, hf) {}

    public HashTableLinearProbing(HashFunction<TKey> hashFunc) : base(Enumerable.Empty<KeyValuePair<TKey, TValue>>(), 
																	  hashFunc) {}
	
	public HashTableLinearProbing(IEnumerable<KeyValuePair<TKey, TValue>> data, 
								  int capacity, 
								  double loadFactor, 
								  HashFunction<TKey> hashFunc) : base(data, capacity, loadFactor, hashFunc) {}

	public static HashTableLinearProbing<TKey, TValue> Create(IEnumerable<KeyValuePair<TKey, TValue>> data,
													   int capacity,
													   double loadFactor,
													   HashFunction<TKey> hashFunc) 
												=> new(data, capacity, loadFactor, hashFunc);

protected override int Probe(int index, int i) => (index + 1) % this.Capacity;
}


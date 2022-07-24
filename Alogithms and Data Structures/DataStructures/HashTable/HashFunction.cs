namespace DataStructures.HashTable;

public class HashFunction<T>
{
	private readonly Func<T, int>? _hashGenerator;

	public HashFunction() {}

	protected HashFunction(Func<T, int> hashGenerator) => _hashGenerator = hashGenerator;

	public int GetHash(T value)
	{
		if (value == null) throw new NullReferenceException(nameof(value));
		return _hashGenerator == null ? value.GetHashCode() : _hashGenerator!.Invoke(value);
	}
}


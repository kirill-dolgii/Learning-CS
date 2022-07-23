namespace AlgorithmsTests.SortersTests;

public class TestDataGenerator<T>
where T : IComparable<T>
{
    public TestDataGenerator(Func<T> generator) { _generatorFunc = generator; }

    private readonly Func<T> _generatorFunc;
    
    public (T[] input, T[] expdOutput) GenerateData(IComparer<T>? comparer = null)
    {
        Random rand = new Random(323);
        var size = 5000;

        var input = new T[size];
        var expectedOutput = new T[size];

        input = Enumerable.Range(0, size).Select(i => _generatorFunc.Invoke()).ToArray();
        expectedOutput = comparer == null ? input.OrderBy(i => i).ToArray() : input.OrderBy(i => i, comparer).ToArray();

        return (input, expectedOutput);
    }
}

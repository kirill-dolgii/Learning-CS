namespace AlgorithmsTests.SortersTests;

public class TestDataGenerator<T>
where T : IComparable<T>
{
    public TestDataGenerator(Func<T> generator) { _generatorFunc = generator; }
    
    private readonly Func<T> _generatorFunc;

    public (T[] input, T[] expdOutput) GenerateData()
    {
        Random rand = new Random(323);
        var size = rand.Next(50000);

        var input = new T[size];
        var expectedOutput = new T[size];

        input = Enumerable.Range(0, size).Select(i => _generatorFunc.Invoke()).ToArray();
        expectedOutput = input.OrderBy(i => i).ToArray();

        return (input, expectedOutput);
    }
}

using Algorithms;

namespace AlgorithmsTests;


[TestClass]
public class SortingTests
{
	private IEnumerable<int> _input;
	private IEnumerable<int> _expectedOutput;

	private int _size;

    [TestInitialize]
    public void Initialize()
    {
        Random rand = new Random(323);
        _size = rand.Next(50000);

        _input = new int[_size];
        _expectedOutput = new int[_size];

        _input = Enumerable.Range(0, _size).Select(i => rand.Next()).ToList();
        _expectedOutput = _input.OrderBy(i => i).ToList();
    }

    [TestMethod]
    public void HeapSort()
	{
		var sorted           = Sorting.HeapSort(_input, ascending:true);
		var testEnumerator   = sorted.GetEnumerator();
		var sortedEnumerator = _expectedOutput.GetEnumerator();

		for (int i = 0; i < _size; i++)
		{
			testEnumerator.MoveNext();
			sortedEnumerator.MoveNext();
			Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
		}

		sortedEnumerator.Dispose();
		testEnumerator.Dispose();
	}
}
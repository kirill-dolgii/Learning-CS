using Algorithms.Sorting;

namespace AlgorithmsTests.SortersTests;

[TestClass]
public class SorterTests
{
    private class TestComparer<T> : IComparer<T>
    {
        public TestComparer(Func<T, T, int> compFunc) { this.CompFunc = compFunc; }

        private Func<T, T, int> CompFunc { get; }

        public int Compare(T? x, T? y)
        {
            return CompFunc.Invoke(x, y);
        }
    }

	private TestContext _testContextInstance = null!;
	public TestContext TestContext
	{
		get => _testContextInstance;
		set => _testContextInstance = value;
	}

    [TestMethod]
    public void HEAP_SORT_INT()
    {
        int IntCompFunc(int x, int y) => x.CompareTo(y);
        var comp = new TestComparer<int>(IntCompFunc);

        Random random = new Random();

        var heapSorter = new HeapSorter<int>();

        var helper = new SorterTestHelper<int>(() => random.Next(10000), heapSorter, comp, _testContextInstance);
        helper.Sort_Ascending();
		helper.Sort_Descending();
        helper.Sort_Ascending_Comparer();
        helper.Sort_Descending_Comparer();
    }

	[TestMethod]
	public void HEAP_SORT_DBL()
	{
		int IntCompFunc(double x, double y) => x.CompareTo(y);
		var comp = new TestComparer<double>(IntCompFunc);

		Random random = new Random();

		var heapSorter = new HeapSorter<double>();

		var helper = new SorterTestHelper<double>(() => random.Next(10000), heapSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
		helper.Sort_Ascending_Comparer();
		helper.Sort_Descending_Comparer();
	}
}


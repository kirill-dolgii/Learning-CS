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
	
	/// <summary>
	/// Sample class to use in generic sorting tests.
	/// </summary>
	private class GentleMan : IComparable<GentleMan>, IComparable
	{
		public GentleMan(int bucks) { this.Bucks = bucks; }

		public readonly int Bucks = 0;
		
		public int CompareTo(GentleMan? other)
		{
			if (other == null) return 1;
			if (this.Bucks > other.Bucks) return 1;
			return this.Bucks < other.Bucks ? -1 : 0;
		}

		public int CompareTo(object? obj)
		{
			throw new NotImplementedException();
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

		var helper = new SorterTestHelper<double>(() => random.NextDouble(), heapSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
		helper.Sort_Ascending_Comparer();
		helper.Sort_Descending_Comparer();
	}

    [TestMethod]
    public void MERGE_SORT_INT()
    {
        int IntCompFunc(int x, int y) => x.CompareTo(y);
        var comp = new TestComparer<int>(IntCompFunc);

        Random random = new Random();

        var mergeSorter = new MergeSorter<int>();

        var helper = new SorterTestHelper<int>(() => random.Next(10000), mergeSorter, comp, _testContextInstance);
        helper.Sort_Ascending();
        helper.Sort_Descending();
        helper.Sort_Ascending_Comparer();
        helper.Sort_Descending_Comparer();
    }

    [TestMethod]
    public void MERGE_SORT_GENTLEMAN()
    {
        int GManCompFunc(GentleMan x, GentleMan y) => x.CompareTo(y);
        var comp = new TestComparer<GentleMan>(GManCompFunc);

        Random random = new Random();

        var mergeSorter = new MergeSorter<GentleMan>();

        var helper = new SorterTestHelper<GentleMan>(() => new GentleMan(random.Next(10000)), mergeSorter, comp, _testContextInstance);
        helper.Sort_Ascending();
        helper.Sort_Descending();
        helper.Sort_Ascending_Comparer();
        helper.Sort_Descending_Comparer();
    }

    [TestMethod]
	public void BUBBLE_SORT_INT()
	{
		int IntCompFunc(int x, int y) => x.CompareTo(y);
		var comp = new TestComparer<int>(IntCompFunc);

		Random random = new Random();

		var mergeSorter = new BubbleSorter<int>();

		var helper = new SorterTestHelper<int>(() => random.Next(10000), mergeSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
        helper.Sort_Descending();
        helper.Sort_Ascending_Comparer();
        helper.Sort_Descending_Comparer();
    }

    [TestMethod]
    public void BUBBLE_SORT_GENTLEMAN()
    {
		int GManCompFunc(GentleMan x, GentleMan y) => x.CompareTo(y);
		var comp = new TestComparer<GentleMan>(GManCompFunc);

		Random random = new Random();

		var mergeSorter = new BubbleSorter<GentleMan>();

		var helper = new SorterTestHelper<GentleMan>(() => new GentleMan(random.Next(10000)), mergeSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
		helper.Sort_Ascending_Comparer();
		helper.Sort_Descending_Comparer();
    }

    [TestMethod]
	public void SELECTION_SORT_INT()
	{
		int IntCompFunc(int x, int y) => x.CompareTo(y);
		var comp = new TestComparer<int>(IntCompFunc);

		Random random = new Random();

		var mergeSorter = new BubbleSorter<int>();

		var helper = new SorterTestHelper<int>(() => random.Next(10000), mergeSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
		helper.Sort_Ascending_Comparer();
		helper.Sort_Descending_Comparer();
	}

	[TestMethod]
	public void INSERTION_SORT_INT()
	{
		int IntCompFunc(int x, int y) => x.CompareTo(y);
		var comp = new TestComparer<int>(IntCompFunc);

		Random random = new Random();

		var mergeSorter = new InsertionSorter<int>();

		var helper = new SorterTestHelper<int>(() => random.Next(10000), mergeSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
		helper.Sort_Ascending_Comparer();
		helper.Sort_Descending_Comparer();
	}

	[TestMethod]
	public void TREE_SORT_INT()
	{
		int IntCompFunc(int x, int y) => x.CompareTo(y);
		var comp = new TestComparer<int>(IntCompFunc);

		Random random = new Random();

		var mergeSorter = new TreeSorter<int>();

		var helper = new SorterTestHelper<int>(() => random.Next(10000), mergeSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
		helper.Sort_Ascending_Comparer();
		helper.Sort_Descending_Comparer();
	}

	[TestMethod]
	public void RADIX_SORT_INT()
	{
		int IntCompFunc(int x, int y) => x.CompareTo(y);
		var comp = new TestComparer<int>(IntCompFunc);

		Random random = new Random();

		var mergeSorter = new RadixSorter();

		var helper = new SorterTestHelper<int>(() => random.Next(10000), mergeSorter, comp, _testContextInstance);
		helper.Sort_Ascending();
		helper.Sort_Descending();
	}

}


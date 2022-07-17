using Algorithms.Sorting;

namespace AlgorithmsTests.SortersTests;

[TestClass]
public class SorterTestHelper<T>
where T : IComparable<T>
{
	public SorterTestHelper(Func<T> generator, ISorter<T> sorter, IComparer<T> comparer, TestContext tc)
    {
		this.Generator = new TestDataGenerator<T>(generator);
        this.Sorter = sorter;
        this.Comparer = comparer;
        this.tc = tc;
    }

    private TestContext tc { get; }

	private TestDataGenerator<T> Generator { get; }

	private void PrepareTestData()
	{
		var testData = Generator.GenerateData();
		this.Input = testData.input;
		this.ExpectedOutput = testData.expdOutput;
	}

	[TestMethod]
	public void Sort_Ascending()
	{
		PrepareTestData();
		Sorter.Sort(this.Input);
		var testEnumerator   = this.Input.GetEnumerator();
		var sortedEnumerator = this.ExpectedOutput.GetEnumerator();

		for (int i = 0; i < this.Input.Length; i++)
		{
			testEnumerator.MoveNext();
			sortedEnumerator.MoveNext();
			Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
		}
	}

	[TestMethod]
	public void Sort_Descending()
	{
		PrepareTestData();
		Sorter.Sort(this.Input, SortingOrder.Descending);
		var testEnumerator   = this.Input.GetEnumerator();
		var sortedEnumerator = this.ExpectedOutput.Reverse().GetEnumerator();

		for (int i = 0; i < this.Input.Length; i++)
		{
			testEnumerator.MoveNext();
			sortedEnumerator.MoveNext();
			Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
		}

		sortedEnumerator.Dispose();
	}

	private IComparer<T> Comparer { get; }

	[TestMethod]
	public void Sort_Ascending_Comparer()
	{
		PrepareTestData();
		Sorter.Sort(this.Input, Comparer);
		var testEnumerator   = this.Input.GetEnumerator();
		var sortedEnumerator = this.ExpectedOutput.GetEnumerator();

		for (int i = 0; i < this.Input.Length; i++)
		{
			testEnumerator.MoveNext();
			sortedEnumerator.MoveNext();
			Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
		}
	}

	[TestMethod]
	public void Sort_Descending_Comparer()
	{
		PrepareTestData();
		Sorter.Sort(this.Input, Comparer, SortingOrder.Descending);
		var testEnumerator   = this.Input.GetEnumerator();
		var sortedEnumerator = this.ExpectedOutput.Reverse().GetEnumerator();

		for (int i = 0; i < this.Input.Length; i++)
		{
			testEnumerator.MoveNext();
			sortedEnumerator.MoveNext();
			Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
		}

		sortedEnumerator.Dispose();
	}

	private ISorter<T> Sorter { get; }

	private T[] Input { get; set; } = null!;

	private T[] ExpectedOutput { get; set; } = null!;
}

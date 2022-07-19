using Algorithms.Sorting;

namespace AlgorithmsTests.SortersTests;

[TestClass]
public class SorterTestHelper<T>
where T : IComparable<T>
{
	public SorterTestHelper(Func<T> generator, Sorter<T> sorter, IComparer<T> comparer, TestContext tc)
    {
		this.Generator = new TestDataGenerator<T>(generator);
        this.Sorter = sorter;
        this.Comparer = comparer;
        this.Tc = tc;
    }

    private TestContext Tc { get; }

	private TestDataGenerator<T> Generator { get; }

	private void PrepareTestData(IComparer<T>? comparer = null)
	{
		var testData = Generator.GenerateData(comparer);
		this.Input = testData.input;
		this.ExpectedOutput = testData.expdOutput;
	}

	private IComparer<T> Comparer { get; }

	private void TestTemplate(SortingOrder order, bool useComp)
	{
		PrepareTestData(useComp ? this.Comparer : null);
		Sorter.Sort(this.Input, useComp ? this.Comparer : null, order);

		if (order == SortingOrder.Descending) this.ExpectedOutput = this.ExpectedOutput.Reverse().ToArray();
		for (int i = 0; i < this.Input.Length; i++) 
			Assert.AreEqual(0, this.Input[i].CompareTo(this.ExpectedOutput[i]));
	}

	[TestMethod]
	public void Sort_Ascending() { TestTemplate(SortingOrder.Ascending, false); }

	[TestMethod]
	public void Sort_Descending() { TestTemplate(SortingOrder.Descending, false); }

	[TestMethod]
	public void Sort_Ascending_Comparer() { TestTemplate(SortingOrder.Ascending, true); }

	[TestMethod]
	public void Sort_Descending_Comparer() { TestTemplate(SortingOrder.Descending, true); }

	private Sorter<T> Sorter { get; }

	private T[] Input { get; set; } = null!;

	private T[] ExpectedOutput { get; set; } = null!;
}

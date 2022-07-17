using Algorithms.Sorting;

namespace AlgorithmsTests.SortersTests;


[TestClass]
public class HeapSorterTests
{
    private int[] _input = Array.Empty<int>();
    private int[] _expectedOutput = Array.Empty<int>();

    private int _size;

    [TestInitialize]
    public void Initialize()
    {
        Random rand = new Random(323);
        _size = rand.Next(50000);

        _input = new int[_size];
        _expectedOutput = new int[_size];

        _input = Enumerable.Range(0, _size).Select(i => rand.Next()).ToArray();
        _expectedOutput = _input.OrderBy(i => i).ToArray();
    }

    [TestMethod]
    public void HeapSort_Ascending()
    {
        HeapSorter<int>.Sort(_input, SortingOrder.Ascending);
        var testEnumerator = _input.GetEnumerator();
        var sortedEnumerator = _expectedOutput.GetEnumerator();

        for (int i = 0; i < _size; i++)
        {
            testEnumerator.MoveNext();
            sortedEnumerator.MoveNext();
            Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
        }
    }

    [TestMethod]
    public void HeapSort_Descending()
    {
        HeapSorter<int>.Sort(_input, SortingOrder.Descending);
        var testEnumerator = _input.GetEnumerator();
        var sortedEnumerator = _expectedOutput.Reverse().GetEnumerator();

        for (int i = 0; i < _size; i++)
        {
            testEnumerator.MoveNext();
            sortedEnumerator.MoveNext();
            Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
        }

        sortedEnumerator.Dispose();
    }

    private class TestComparator<T> : IComparer<T>
    where T : IComparable<T>
    {
        public int Compare(T? x, T? y)
        {
            return x.CompareTo(y);
        }
    }

    [TestMethod]
    public void HeapSort_Ascending_Comparator()
    {
        HeapSorter<int>.Sort(_input, new TestComparator<int>(), SortingOrder.Ascending);
        var testEnumerator = _input.GetEnumerator();
        var sortedEnumerator = _expectedOutput.GetEnumerator();

        for (int i = 0; i < _size; i++)
        {
            testEnumerator.MoveNext();
            sortedEnumerator.MoveNext();
            Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
        }
    }

    [TestMethod]
    public void HeapSort_Descending_Comparator()
    {
        HeapSorter<int>.Sort(_input, new TestComparator<int>(), SortingOrder.Descending);
        var testEnumerator = _input.GetEnumerator();
        var sortedEnumerator = _expectedOutput.Reverse().GetEnumerator();

        for (int i = 0; i < _size; i++)
        {
            testEnumerator.MoveNext();
            sortedEnumerator.MoveNext();
            Assert.AreEqual(sortedEnumerator.Current, testEnumerator.Current);
        }

        sortedEnumerator.Dispose();
    }
}
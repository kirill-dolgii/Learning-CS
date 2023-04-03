namespace Algorithms.Numeric.Sequences;

public static class Fibonacci
{
	public static IEnumerable<int> FibonacciSequence()
	{
		var n1 = 0;
		var n2 = 1;
		yield return n1;
		yield return n2;
		while (true)
		{
			var next = n1 + n2;
			n1 = n2;
			n2 = next;
			yield return next;
		}
	}
}
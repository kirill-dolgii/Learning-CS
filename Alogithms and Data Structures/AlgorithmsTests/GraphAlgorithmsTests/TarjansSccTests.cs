using DataStructures.Graph;
using Algorithms.GraphAlgorithms;

namespace AlgorithmsTests.GraphAlgorithmsTests;

[TestClass]
public class TarjansSccTests
{
    [TestMethod]
    public void Test1()
    {
        var graph = new Graph<int, int>(true);
        graph.AddEdge(0, 1, 5);
        graph.AddEdge(1, 2, 4);
        graph.AddEdge(2, 0, 3);
        graph.AddEdge(0, 3, 2);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(4, 3, 1);

        var solver = new TarjansSCC<int, int>(graph);
        solver.Solve();

        var firstComponent = new[] { 0, 1, 2 };
        var secondComponent = new[] { 3, 4 };

        Assert.AreEqual(solver.Result.Count, 2);
        Assert.AreEqual(solver.Result[0].Count, 2);
        Assert.AreEqual(solver.Result[1].Count, 3);

        bool ContainsComponent(ICollection<int> comp) => 
            solver.Result.Any(c => comp.All(n => c.Contains(n, graph.NodeEqualityComparer)));

        Assert.IsTrue(ContainsComponent(firstComponent));
        Assert.IsTrue(ContainsComponent(secondComponent));
    }
}
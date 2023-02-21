using Algorithms.GraphAlgorithms;
using DataStructures.Graph;

namespace AlgorithmsTests.GraphAlgorithmsTests;

[TestClass]
public class DepthFirstSearchTests
{
    [TestMethod]
    public void Test()
    {
        var graph = new Graph<int, int>(false);
        var testDataPath = Path.Combine(Constants.GraphTestDataPath, "graph.txt");
        var testData = File.ReadAllLines(testDataPath).
                            Select(raw =>
                            {
                                var nums = raw.Split(", ").Select(int.Parse).ToArray();
                                return new { X = nums[0], Y = nums[1], Edge = nums[2] };
                            });
        foreach (var raw in testData) 
            graph.AddEdge(raw.X, raw.Y, raw.Edge);

        var walkOrder = new List<int>(graph.NodesCount);

        void Action(int node) => walkOrder.Add(node);

        var solver = new DepthFirstSearch<int, int>(graph, graph.Nodes.First(), 
                                                    earlEarlyNodeAction:Action);
        solver.Solve();
        Assert.IsTrue(graph.Nodes.All(n => walkOrder.Contains(n)));
    }
}
using DataStructures.Graph.Interfaces;

namespace Algorithms.GraphAlgorithms;

public class TarjansSCC<TNode, TEdge> : ISolver<IList<ICollection<TNode>>> 
where TNode : notnull
{
	private readonly IDictionary<TNode, int>         _low;
	private readonly IDictionary<TNode, bool>        _visited;
	private readonly IDictionary<TNode, int>         _id;
	private readonly IDictionary<TNode, bool>        _onStack;
	private readonly Stack<TNode>                    _stack;
    private readonly IList<ICollection<TNode>> _components;
    private int _counter = -1;

    public readonly IGraph<TNode, TEdge> Graph;

    public TarjansSCC(IGraph<TNode, TEdge> graph)
    {
        if (graph == null) throw new ArgumentNullException(nameof(graph));
        if (!graph.Directed) throw new InvalidOperationException("graph must be directed.");

        Graph = graph;
        _low = Graph.Nodes.ToDictionary(n => n, _ => -1, Graph.NodeEqualityComparer);
        _id = Graph.Nodes.ToDictionary(n => n, _ => -1, Graph.NodeEqualityComparer);
        _visited = Graph.Nodes.ToDictionary(n => n, _ => false, Graph.NodeEqualityComparer);
        _onStack = Graph.Nodes.ToDictionary(n => n, _ => false, Graph.NodeEqualityComparer);
        _stack = new Stack<TNode>(Graph.NodesCount);
        IsSolved = false;
        _components = new List<ICollection<TNode>>();
    }

    public bool IsSolved { get; }
	public IList<ICollection<TNode>> Solve()
	{
        if (IsSolved) return Result;
		foreach (var vertex in Graph.Nodes)
			if (!_visited[vertex])
			{
				var node = vertex;
				DFS(ref node);
			}
        return Result;
	}

    public IList<ICollection<TNode>> Result => _components;

    private void DFS(ref TNode node)
	{
		_id[node] = _low[node] = ++_counter;
        _visited[node] = true;
        _stack.Push(node);
        _onStack[node] = true;

        foreach (var adj in Graph.AdjacentNodes(node))
		{
			var n = adj;
            if (!_visited[n]) DFS(ref n);
            if (_onStack[n])
                _low[node] = Math.Min(_low[n], _low[node]);
        }

        if (_low[node] != _id[node]) return;
        var component = new List<TNode>();
        for (TNode n = _stack.Pop();; n = _stack.Pop())
        {
            component.Add(n);
            _onStack[n] = false;
            if (Graph.NodeEqualityComparer.Equals(node, n)) break;
        }
        _components.Add(component);
    }
}

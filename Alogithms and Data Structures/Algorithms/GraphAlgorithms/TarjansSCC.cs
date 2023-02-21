using DataStructures.Graph;

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
	private          int                             _counter = -1;

    public readonly IGraph<TNode, TEdge> Graph;
    public readonly IEqualityComparer<TNode> NodeEqualityComparer;

    public TarjansSCC(IGraph<TNode, TEdge> graph, 
                      IEqualityComparer<TNode> nodeEqualityComparer)
    {
        if (graph == null) throw new ArgumentNullException(nameof(graph));
        if (!graph.Directed) throw new InvalidOperationException("graph must be directed.");

        Graph = graph;
        _low = Graph.Vertices.ToDictionary(n => n, _ => -1);
        _id = Graph.Vertices.ToDictionary(n => n, _ => -1);
        _visited = Graph.Vertices.ToDictionary(n => n, _ => false);
        _onStack = Graph.Vertices.ToDictionary(n => n, _ => false);
        _stack = new Stack<TNode>(Graph.VerticesCount);
        IsSolved = false;
        _components = new List<ICollection<TNode>>();
        NodeEqualityComparer = nodeEqualityComparer;
    }

    public bool IsSolved { get; }
	public IList<ICollection<TNode>> Solve()
	{
        if (IsSolved) return Result;
		foreach (var vertex in Graph.Vertices)
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
            if (NodeEqualityComparer.Equals(node, n)) break;
        }
        _components.Add(component);
    }
}
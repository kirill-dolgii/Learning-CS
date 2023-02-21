using DataStructures.Graph;

namespace Algorithms.GraphAlgorithms;

public class TarjansSCC<TNode, TEdge> : ISolver 
where TNode : notnull
{
	public readonly IGraph<TNode, TEdge> Graph;

	public TarjansSCC(IGraph<TNode, TEdge> graph)
	{
		Graph = graph ?? throw new ArgumentNullException(nameof(graph));
		_low = Graph.Vertices.ToDictionary(n => n, n => -1);
		_id = Graph.Vertices.ToDictionary(n => n, n => -1);
		_visited = Graph.Vertices.ToDictionary(n => n, n => false);
		_onStack = Graph.Vertices.ToDictionary(n => n, n => false);
		_stack = new Stack<TNode>(Graph.VerticesCount);
		IsSolved = false;
		_components = new List<ICollection<TNode>>();
	}

	private readonly IDictionary<TNode, int>         _low;
	private readonly IDictionary<TNode, bool>        _visited;
	private readonly IDictionary<TNode, int>         _id;
	private readonly IDictionary<TNode, bool>        _onStack;
	private readonly Stack<TNode>                    _stack;
	private readonly ICollection<ICollection<TNode>> _components;
	private          int                             _counter = -1;

	public bool IsSolved { get; }
	public void Solve()
	{
		foreach (var vertex in Graph.Vertices)
			if (_onStack[vertex])
			{
				var node = vertex;
				DFS(ref node);
			}
	}

	private void DFS(ref TNode node)
	{
		_id[node] = _low[node] = _counter++;
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

        if (_low[node] == _id[node])
        {
            var component = new List<TNode>();
            for (TNode n = _stack.Pop(); ; n = _stack.Pop())
            {
                component.Add(n);
                _onStack[n] = false;
                if (node.Equals(n)) break;
            }
            _components.Add(component);
        }
    }
}

//	public void Tarjans()
//	{
//		foreach (var node in _nodes)
//		{
//			var key = node;
//			if (!_visited[key]) DFS(ref key);
//		}
//	}

//	private void DFS(ref T node)
//	{
//		`
//	}
//}
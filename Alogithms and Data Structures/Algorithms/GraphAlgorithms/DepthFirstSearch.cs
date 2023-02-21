using DataStructures.Graph.Interfaces;

namespace Algorithms.GraphAlgorithms;

public class DepthFirstSearch<TNode, TEdge> : ISolver<IList<TNode>>
where TNode : notnull
{
    protected readonly IDictionary<TNode, bool> _discovered;
    protected readonly IDictionary<TNode, int> _entryTime;
    protected readonly IDictionary<TNode, int> _exitTime;
    protected readonly IDictionary<TNode, bool> _processed;
    protected readonly IDictionary<TNode, TNode> _parent;
    protected int _counter = -1;
    protected bool _isSolved = false;

    public readonly IGraph<TNode, TEdge> Graph;
    public readonly TNode Start;

    public readonly Action<TNode>? EarlyNodeAction;
    public readonly Action<TNode>? LateNodeAction;
    public readonly Action<TNode, TNode, TEdge>? EdgeAction;

    protected DepthFirstSearch() { }

    public DepthFirstSearch(IGraph<TNode, TEdge> graph, 
                            TNode start,
                            Action<TNode>? earlEarlyNodeAction = null, 
                            Action<TNode>? lateNodeAction = null, 
                            Action<TNode, TNode, TEdge>? edgeAction = null)
    {
        Graph = graph ?? throw new ArgumentNullException(nameof(graph));
        Start = start ?? throw new ArgumentNullException(nameof(start));
        EarlyNodeAction = earlEarlyNodeAction;
        LateNodeAction = lateNodeAction;
        EdgeAction = edgeAction;
        
        _discovered = Graph.Nodes.ToDictionary(n => n, _ => false, Graph.NodeEqualityComparer);
        _processed = Graph.Nodes.ToDictionary(n => n, _ => false, Graph.NodeEqualityComparer);
        _entryTime = Graph.Nodes.ToDictionary(n => n, _ => -1, Graph.NodeEqualityComparer);
        _exitTime = Graph.Nodes.ToDictionary(n => n, _ => -1, Graph.NodeEqualityComparer);
        _parent = Graph.Nodes.ToDictionary(n => n, n => n, Graph.NodeEqualityComparer);
        Result = new List<TNode>(graph.NodesCount);
    }

    public bool IsSolved => _isSolved;

    public IList<TNode> Solve()
    {
        if (IsSolved) return Result;
        DepthFirstSearchImpl(Start);
        return Result;
    }

    public IList<TNode> Result { get; }

    private void DepthFirstSearchImpl(TNode x)
    {
        _discovered[x] = true;
        _entryTime[x] = ++_counter;
        Result.Add(x);
        EarlyNodeAction?.Invoke(x);
        foreach (var y in Graph.AdjacentNodes(x))
        {
            if (!_discovered[y])
            {
                _parent[y] = x;
                foreach (var edge in Graph.Edges(x, y)) 
                    EdgeAction?.Invoke(x, y, edge);
                DepthFirstSearchImpl(y);
            }
            else if (((!_processed[y]) && Graph.NodeEqualityComparer.Equals(_parent[x], y)) || Graph.Directed)
                foreach (var edge in Graph.Edges(x, y))
                    EdgeAction?.Invoke(x, y, edge);

            if (IsSolved) return;
        }

        LateNodeAction?.Invoke(x);
        _exitTime[x] = ++_counter;
        _processed[x] = true;
    }
}
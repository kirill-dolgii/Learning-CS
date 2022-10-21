namespace Algorithms.GraphAlgorithms;
/*
public class ArticulationPoints<T> : ISolver
{
	public ArticulationPoints(IGraph<T> graph)
	{
		
	}

	private Dictionary<T, bool> _visited;
	private T _articulationPoints;
	private Stack<T> _stack;
	private IGraph<T> _graph;
	private Dictionary<T, int> _id;
	private Dictionary<T, int> _low;
	public bool IsSolved { get; }
	public void Solve()
	{
		int id   = 0;
		T   prev = default(T);
		foreach (T vertice in _graph.Vertices)
		{
			if (_visited[vertice]) continue;
			_stack.Push(vertice);
			while (_stack.Count != 0)
			{
				var current = _stack.Pop();
				if (!_graph.Adjacent(current).Any())
				{
					while (_low[current] )
				}
				else
					foreach (var adj in _graph.Adjacent(current))
					{
						_stack.Push(adj);
						_id[adj] = id;
						_low[adj] = id;
						id++;
					}

			}

		}
	}
}
*/
public class TarjansSCC<T>
	{
		private Dictionary<T, HashSet<T>> _adjacent;
		private Dictionary<T, int>        _low;
		private Dictionary<T, bool>       _visited;
	    private Dictionary<T, int>        _id;
		private Dictionary<T, bool>       _onStack;
		private Stack<T>                  _stack;

	    public IList<IList<T>> _components;

		private int _counter = 0;

		private IEnumerable<T> _nodes
		{
			get => _adjacent.Keys;
		}

		public TarjansSCC(Dictionary<T, HashSet<T>> adj)
		{
			this._adjacent = adj;
			_id = adj.ToDictionary(kv => kv.Key, _ => -1);
			_low = adj.ToDictionary(kv => kv.Key, kv => -1);
			_visited = adj.ToDictionary(kv => kv.Key, _ => false);
			_onStack = adj.ToDictionary(kv => kv.Key, _ => false);
			_stack = new Stack<T>(adj.Count);
			_components = new List<IList<T>>();
		}

		public void Tarjans()
		{
			foreach (var node in _nodes)
			{
				var key = node;
				if (!_visited[key]) DFS(ref key);
			}
		}

		private void DFS(ref T node)
		{
			_id[node] = _low[node] = _counter++;
			_visited[node] = true;
			_stack.Push(node);
		    _onStack[node] = true;
			
			foreach (var adj in _adjacent[node])
			{
				var key = adj;
				if (!_visited[key]) DFS(ref key);
				if (_onStack[key]) 
					_low[node] = Math.Min(_low[key], _low[node]);
			}

			if (_low[node] == _id[node])
			{
				var component = new List<T>();
				for (T n = _stack.Pop();; n = _stack.Pop())
				{
					component.Add(n);
					_onStack[n] = false;
					if (node.Equals(n)) break;
				}
				_components.Add(component);
			}
		}
	}
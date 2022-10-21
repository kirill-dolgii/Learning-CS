namespace DataStructures.Graph;

public interface IGraph<T>
{
	public IEnumerable<T> Adjacent(T node);
	public void AddNode(T node);
	public void AddEdge(T x, T y);
	public void RemoveEdge(T x, T y);
	public int VerticeCount { get; }
	public IEnumerable<T> Vertices { get; }
}

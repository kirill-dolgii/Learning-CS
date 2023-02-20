namespace DataStructures.Graph;

public interface IGraph<TNode, TEdge>
{
	public ICollection<TNode> AdjacentNodes(TNode node);
	public void AddNode(TNode node);
	public bool ContainsNode(TNode node);
	public bool RemoveNode(TNode node);
	public int Degree(TNode node);
	public void AddEdge(TNode x, TNode y, TEdge edge);
	public bool ContainsEdge(TNode x, TNode y, TEdge edge);
	public bool RemoveEdge(TNode x, TNode y, TEdge edge);
	public ICollection<TNode> Vertices { get; }
	public int VerticesCount { get; }
	public ICollection<TEdge> Edges(TNode x, TNode y);
    public ICollection<TEdge> Edges(TNode x);
	public int EdgesCount { get; }
	public bool Directed { get; }
}

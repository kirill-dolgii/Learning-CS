using DataStructures.BinarySearchTree;

namespace DataStructures.RedBlackTree;
public interface IRedBlackNode<T> : IBinaryNode<IRedBlackNode<T>, T>
{
    public bool IsBlack { get; set; }
}
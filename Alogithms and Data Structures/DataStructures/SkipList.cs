using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

namespace DataStructures;

public class SkipList<T> : ICollection<T>
where T : IComparable<T>
{
	private class SkipNode
	{
		public SkipNode(int level)
		{
			this.value = default(T);
			this.forward = new SkipNode[level + 1];
		}

		public SkipNode(T item, int level) : this(level)
		{
			this.value = item;
		}

		public T?          value;
		public SkipNode?[] forward;

	}
	
	private SkipList(SkipNode head, int initLevel)
	{
		this._head = new(initLevel);
		this._maxLevel = initLevel;
		_size = 0;

	}
	public SkipList() : this(new(1), 1) { }
	
	private int      _size;
	private SkipNode _head;

	private int _maxLevel;

	public int Count => _size;

	public bool IsReadOnly => false;

    private SkipNode[] FindPrecedingNodes(T value, SkipNode startNode, int level)
	{
		if (startNode == null) throw new NullReferenceException();
		var predecessors = new SkipNode[level + 1];

		for (int lvl = level; lvl >= 0; lvl--)
		{
			while (startNode!.forward[lvl] != null && 
				   this.Compare(value, startNode.forward[lvl]!.value!) > 0)
				startNode = startNode.forward[lvl]!;
			predecessors[lvl] = startNode;
		}
		return predecessors;
	}

	private void AdjustHead(int newMaxLevel)
	{
		_maxLevel = newMaxLevel;
		SkipNode newHead = new SkipNode(_head.value!, _maxLevel);
		for (int lvl = _head.forward.Length - 1; lvl >= 0; lvl--)
			newHead.forward[lvl] = _head.forward[lvl];
		_head = newHead;
	}

	public bool Remove(T value)
	{
		if (value == null) throw new ArgumentNullException($"{nameof(value)} was null");
		var predecessors = FindPrecedingNodes(value, _head, _maxLevel);
		if (predecessors[0].forward[0] != null && predecessors[0].forward[0]!.value!.CompareTo(value) != 0) return false;

		for (int i = _maxLevel; i >= 0; i--)
			if (predecessors[i].forward[i] != null && predecessors[i].forward[i]!.value!.CompareTo(value) == 0)
				predecessors[i].forward[i] = predecessors[i]!.forward[i]!.forward[i];

		_size--;
		return true;
	}

	public void Print()
	{
		var trav = _head;
		for (int i = 0; i <= _maxLevel; i++)
		{
			var sb = new StringBuilder();
			while (trav!.forward[0] != null)
			{
				var app = i < trav.forward.Length && trav.forward[0] != null ? 
							sb.Append($"---{trav.forward[0]!.value!.ToString()}") : 
							sb.Append("---**");
				trav = trav.forward[0];
			}
			Console.WriteLine(sb.ToString());
			trav = _head;
		}
	}

	private int Compare(T value1, T value2)
	{
		return value1.CompareTo(value2);
		throw new NotImplementedException();
	}

	public IEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException($"{nameof(item)} was null.");
        Random random = new Random();
        int newLevel = 0;

        for (; random.Next() % 2 == 0; newLevel++) { }
        if (newLevel > _maxLevel) AdjustHead(newLevel);
        SkipNode newNode = new(item, newLevel);

        var predecessors = FindPrecedingNodes(item, this._head, _maxLevel);

        for (int lvl = newLevel; lvl >= 0; lvl--)
        {
            newNode.forward[lvl] = predecessors[lvl]!.forward[lvl];
            predecessors[lvl]!.forward[lvl] = newNode;
        }

        _size++;
	}

	public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
	{
		var node = FindPrecedingNodes(item, _head, _maxLevel);
		return node[0].forward[0] != null && 
			   node[0].forward[0]!.value != null &&
			   this.Compare(node[0].forward[0]!.value!, item) == 0;
	}

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }
}
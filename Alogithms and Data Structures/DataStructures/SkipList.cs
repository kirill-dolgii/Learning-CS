using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

namespace DataStructures;

public class SkipList<T> : IEnumerable<T>
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

	public SkipList()
	{
		this._head = new(1);
		this.maxLevel = 1;
	}

	public SkipList(int initLevel)
	{
		this._head = new(initLevel);
		this.maxLevel = initLevel;
	}

	private readonly SkipNode _head;

	private int maxLevel;

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

	public bool Add(T value)
	{
		Random random   = new Random();
		int    newLevel = 0;

		for (; newLevel < maxLevel && random.Next() % 2 == 0; newLevel++) {}
		SkipNode newNode      = new(value, newLevel);

		var      predecessors = FindPrecedingNodes(value, this._head, maxLevel);

		for (int lvl = newLevel; lvl >= 0; lvl--)
		{
			newNode.forward[lvl] = predecessors[lvl]!.forward[lvl];
            predecessors[lvl]!.forward[lvl] = newNode;
		}

		return true;
	}

	public bool Remove(T value)
	{
		if (value == null) throw new ArgumentNullException($"{nameof(value)} was null");
		var predecessors = FindPrecedingNodes(value, _head, maxLevel);
		if (predecessors[0].forward[0] != null && predecessors[0].forward[0].value.CompareTo(value) != 0) return false;

		for (int i = maxLevel; i >= 0; i--)
			if (predecessors[i].forward[i] != null && predecessors[i].forward[i].value.CompareTo(value) == 0)
				predecessors[i].forward[i] = predecessors[i].forward[i].forward[i];

		return true;
	}

	public void Print()
	{
		var trav = _head;
		for (int i = 0; i <= maxLevel; i++)
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
}
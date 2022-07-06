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

			this.forward = new SkipNode[level];
			
			//for (int i = level - 1; i > 0; i--) this.forward[i]!.forward[i] = null;
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

	private SkipNode _head;

	private int maxLevel;

	private SkipNode? Find(T value, SkipNode startNode, int level)
	{
		if (level < 0) return startNode;
		if (startNode == null) throw new NullReferenceException();
		if (this.Compare(value, startNode.value!) == 0) return startNode;
		while (startNode.forward[level] != null && this.Compare(value, startNode.value!) > 0) 
			startNode = startNode.forward[level];

		SkipNode? ret = this.Find(value, startNode, level - 1);

		return ret ?? startNode;
	}

	public bool Find(T value)
	{
		SkipNode found = this.Find(value, this._head, this.maxLevel - 1);
		return found != null;
	}

	public bool Add(T value)
	{
		Random random   = new Random();
		int    newLevel = 0;

		for (int i = 0; random.Next() % 2 == 0; i++) newLevel++;

		return this.Add(value, this._head, newLevel);
	}

	private bool Add(T value, SkipNode startNode, int level)
	{
        SkipNode? found   = this.Find(value, startNode, level);
		SkipNode  newNode = new(value, level);
		for (int i = level; i > 0; i--) found.forward[i] = newNode.forward[i];

		return true;
        //if (found != null && this.Compare(found.value!, value) == 0) return false;
        throw new NotImplementedException();
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
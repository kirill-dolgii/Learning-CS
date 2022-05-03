#pragma once
#ifndef LIST_H

template <typename T>
class List {

	struct Node
	{
		Node* Next;
		T Data;
		Node(T data);
	};

public:
	Node* First;
	Node* Last;

	List (Node* node);
	List ();
	List(T value);

	bool IsEmpty();
	void AddFirst(T value);
	void Add(Node* node);
	void Push(T value);
	Node* FindByVal(T value);
	void RemoveFirst();
	void Pop();
	void RemoveByVal(T value);
	int IndexOf(Node* node);
	Node* operator[] (const int index);
	void PrintAll();
private:
	void AddToEmpty(T value);
	void AddToEmpty(Node* node);
	void Clear();
};

#endif // !LIST_H

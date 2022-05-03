#pragma once

template <class T>
class DynamicArray
{
public:
	DynamicArray();
	DynamicArray(const DynamicArray &to_copy);
	~DynamicArray();
	DynamicArray& operator = (const DynamicArray& to_assign);

	T& operator [] (unsigned int index);
	void Add(const T &item);

	unsigned int GetSize();
	void SetSize(unsigned int newSize);
	void Clear();
	void Delete(unsigned int pos);
	void* getptr();

	enum exception { MEMFAIL, INDEXFAIL };

private:

	T *storage;
	unsigned int capacity; // allocated memory
	unsigned int size;	   // number of actually stored elements

	const static int init_capacity = 2;
	const static int scale_factor = 2;

	void Resize(unsigned int new_size = 0);

};
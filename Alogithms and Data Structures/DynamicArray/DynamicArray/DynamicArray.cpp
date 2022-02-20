#include <iostream>
#include "DynamicArray.h"

using namespace std;

template <typename T>
DynamicArray<T>::DynamicArray()
{
	this->size = 0;
	this->capacity = this->init_capacity;

	this->storage = new T[init_capacity];

	if (this->storage == 0)
		throw MEMFAIL;
}

template <typename T>
DynamicArray<T>::DynamicArray(const DynamicArray &to_copy)
{
	this->storage = new T[to_copy.size];
		
	if (storage == 0)
		throw MEMFAIL;

	copy(to_copy.storage, to_copy.storage + to_copy.size, this->storage);

	this->size = to_copy.size;
	this->capacity = to_copy.capacity;
}

template <typename T>
DynamicArray<T>::~DynamicArray()
{
	delete [] this->storage;
	this->storage = 0;
}

template <typename T>
DynamicArray<T>& DynamicArray<T>::operator=(const DynamicArray<T> &to_assign)
{
	this->storage = new T[to_assign.capacity];
	
	if (storage = 0)
		throw MEMFAIL;
		
	copy(to_assign, to_assign + to_assign.size, this->storage);

	this->size = to_assign.size;
	this->capacity = to_assign.capacity;
}

template<class T>
T& DynamicArray<T>::operator[] (unsigned int index)
{
	return this->storage + index;
}

template<class T>
void DynamicArray<T>::Add(const T& value)
{
	size++;

	if (this->size > this->capacity)
		this->Resize();

	this->storage[size - 1] = value;
}

template<class T>
unsigned int DynamicArray<T>::GetSize() { return this->size; }

template<class T>
void DynamicArray<T>::SetSize(unsigned int new_size)
{	
	if (this->size < new_size && this->capacity < new_size)
		this->Resize(new_size);
}

template<class T>
void DynamicArray<T>::Clear()
{
	if (this->size != 0)
	{
		delete [] this->storage;
		this->storage = new T[this->capacity];

		this->size = 0;
	}
}

template<class T>
void DynamicArray<T>::Delete(unsigned int pos)
{
	if (this->size == 1)
		this->Clear();
	else if (pos > this->size)
		throw std::out_of_range("index is out of range");
	else
	{
		for (int i = 0; i < this->size - 1; i++)
			storage[i] = storage[i + 1];			
		
		size--;
		if (this->size < 0.25 * this->capacity)
			this->Resize(capacity / 2);
	}	
}

template<class T>
void* DynamicArray<T>::getptr()
{
	return this->storage;
}

template<class T>
void DynamicArray<T>::Resize(unsigned int new_size)
{	
	unsigned int to_resize = new_size;

	if (new_size == 0)
		to_resize = this->size * this->scale_factor;

	T* tmp_storage = new T[to_resize];
	copy(this->storage, this->storage + this->size, tmp_storage);

	delete[] this->storage;
	this->storage = tmp_storage;

	this->capacity = to_resize;
}


class asd
{
public:
	int val;
	asd() {};
	asd(int v) { val = v; }
};

int main() {
	
	DynamicArray<int> *da = new DynamicArray<int>();

	da->Add(1);
	da->Add(2);
	da->Add(3);
	da->Add(4);

	da->Delete(da->GetSize() - 1);
	da->Delete(da->GetSize() - 1);
	da->Delete(da->GetSize() - 1);
	da->Delete(da->GetSize() - 1);

}

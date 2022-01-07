#include <iostream>
using namespace std;

template <typename T>
class DynamicArray {

public:
	DynamicArray(int length) {
		capacity = length;
		storage = new T[length + 1];
	};

	~DynamicArray() {
		delete[] this->storage;
	};

	int Size() { return capacity; };
	bool IsEmpty() { return this->Size() == 0; };

	T Get(int index) { 
		if (index > this->size - 1 || index < 0) throw out_of_range("index is out of range");
		return storage[index]; 
	}

	void Set(int index, T val) {
		if (index > this->size - 1 || index < 0) throw out_of_range("index is out of range");
		storage[index] = val; 
	}
	
	void Resize() {
		if (capacity == 0) {
			capacity = 1;
			return;
		}

		capacity *= 2;
		T* tmp = new T[capacity];

		copy(storage, storage + size, tmp);

		delete[] storage;
		this->storage = tmp;
	}

	void Push(T value) {
		if (size == capacity) this->Resize();
		this->Set(size++, value);
	}

	T Pop() {
		T ret = this->Get(size - 1);
		size--;
		return ret;
	}

	T Last() {
		return storage[size - 1];
	}

	void InsertAt(int index, T value) {
		if (index > this->size - 1 || index < 0) throw out_of_range("index is out of range");

		if (size + 1 <= capacity) {
			for (int i = size; i >= index; i--) {
				storage[i + 1] = storage[i];
			}
		}
		storage[index] = value;
		size++;
	}

private:
	int capacity;
	int size = 0;
	T* storage;
};


int main() {
	DynamicArray<int>* da = new DynamicArray<int>(0);
	
}

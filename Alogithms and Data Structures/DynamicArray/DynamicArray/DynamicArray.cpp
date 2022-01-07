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

private:
	int capacity;
	int size = 0;
	T* storage;
};


int main() {
	DynamicArray<int>* da = new DynamicArray<int>(0);
	
}

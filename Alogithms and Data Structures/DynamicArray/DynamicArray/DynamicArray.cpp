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

	T* Get(int index) { 
		if (index > this->size - 1 || index < 0) throw out_of_range("index is out of range");
		return &storage[index]; 
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

	T* Minimum() {
		T* min = &storage[0];
		for (int i = 0; i < size; i++) {
			if (storage[i] < *min) min = &storage[i];
		}
		return min;
	}

	T* Maximum() {
		T* max = &storage[0];
		for (int i = 0; i < size; i++) {
			if (storage[i] > *max) max = &storage[i];
		}
		return max;
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

	void PrintAll() {
		cout << "[";
		for (int i = 0; i < size; i++) {
			cout << storage[i] << " ";
		}
		cout << "]" << " capacity = " << this->capacity << " current length = " << this->size << "\n";
	}

	int GetFirst(T value) {
		for (int i = 0; i < size; i++) {
			if (storage[i] == value) return i;
		}
		return -1;
	}
	
	int IndexOf(T* ptr) {
		for (int i = 0; i < size; i++) {
			if ((storage + i) == ptr) return i;
		}
		return -1;
	}

	void RemoveAt(int index) {
		if (index > this->size - 1 || index < 0) throw out_of_range("index is out of range");
		T* tmp = new T[size - 1];

		for (int i = 0, j = 0; i < size - 1; i++, j++) {
			if (i == index) j++;
			tmp[i] = storage[j];
		}
		size--;

		delete[] storage;
		storage = tmp;
	}

	bool Remove(T value) {
		for (int i = 0; i < size; i++) {
			if (storage[i] == value) { 
				this->RemoveAt(i); 
				return true;
			}
		}
		return false;
	}

	bool Remove(T* ptr) {
		for (int i = 0; i < size; i++) {
			if (&storage[i] == ptr) {
				this->RemoveAt(i);
				return true;
			}
		}
		return false;
	}

private:
	int capacity;
	int size = 0;
	T* storage;
};


int main() {
	DynamicArray<int>* da = new DynamicArray<int>(0);
	
	da->Push(0);
	da->Push(12);
	da->Push(17);
	da->Push(14);
	da->Push(17);
	da->Push(14);
	da->Push(100);
	da->PrintAll();


	int* ptrMin = da->Minimum();
	int* ptrMax = da->Maximum();
	

}

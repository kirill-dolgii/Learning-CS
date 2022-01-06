#include <iostream>
#include <vector>
using namespace std;

template <typename T>
class DynamicArray {

public:
	DynamicArray(int length) {
		capacity =  length;
		len = length;

		vector<T> vec(length);
		data = vec;
	};

	int Size() { return len; };
	bool IsEmpty() { return this->Size() == 0; };

	T* Get(int index) { return &data[index]; };
	void Set(int index, T value) { data[index] = value; };

	~DynamicArray();

private:
	vector<T> data;
	int len;
	int capacity;
};


int main() {
	//DynamicArray<int>* asd = new DynamicArray<int>(3);

	//asd->Set(0, 3);
	//asd->Set(1, 2);
	//asd->Set(2, 1);

	//int* c = asd->Get(0);
	//*c = 5;
	//cout << 3;
} 

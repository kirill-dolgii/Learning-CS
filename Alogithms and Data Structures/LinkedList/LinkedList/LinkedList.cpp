#include <iostream>

struct Node
{
public:
    Node* Next;
    int Data;

    Node(Node* next, int data) {
        this->Next = next;
        this->Data = data;
    }
};

class List
{
public:
    Node* First;

    List(Node* node) {
        this->First = node;
    }

    List() {
        this->First = nullptr;
    }

    bool IsEmpty() {
        return this->First == NULL;
    }

    void Add(Node* node) {
        if (this->IsEmpty() != true) {
            Node* tmp = this->First;

            this->First = node;
            this->First->Next = tmp;

            free(tmp);
        }
        else {
            this->First = node;
        }

    }
};

int main()
{

}

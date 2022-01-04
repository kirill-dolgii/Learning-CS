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
    Node* Last;

    List(Node* node) {
        this->First = node;
        this->Last = node;
    }

    List() {
        this->First = nullptr;
        this->Last = nullptr;
    }

    bool IsEmpty() {
        return this->First == NULL;
    }

    void AddFirst(Node* node) {
        if (this->IsEmpty()) {
            this->First = node;
            this->Last = node;
        }
        else {
            Node* tmp = this->First;

            this->First = node;
            this->First->Next = tmp;
        }

    }

    void AddLast(Node* node) {
        Node* tmp = this->Last;

        this->Last->Next = node;
        this->Last = node;
    }

    void PrintAllList(Node* startNode) {
        if (startNode != nullptr) {
            std::cout << startNode->Data << "\n";
            if (startNode->Next != nullptr) {
                PrintAllList(startNode->Next);
            }
        }
    }

};

int main()
{
    List l = List();

    Node n1(nullptr, 10);
    Node n2(nullptr, 13);
    Node n3(nullptr, 17);

    l.AddFirst(&n1);
    l.AddFirst(&n2);
    l.AddLast(&n3);

    l.PrintAllList(l.First);

}

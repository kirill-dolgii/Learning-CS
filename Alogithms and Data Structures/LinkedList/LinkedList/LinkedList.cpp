#include <iostream>

template <typename T>
class List {

    struct Node
    {
    public:
        Node* Next;
        T Data;

        Node(T data) : Next(nullptr), Data(data) {}
    };

public:
    Node* First;
    Node* Last;

    List(Node* node) {
        this->First = node;
        this->Last = node;
    }

    List() {
        this->First = 0;
        this->Last = 0;
    }

    List(T value) {
        Node* p = new Node(value);

        this->First = p;
        this->Last = p;
    }

    bool IsEmpty() {
        return this->First == NULL;
    }

    void AddFirst(T value) {
        Node* p = new Node(value);

        if (this->IsEmpty()) this->AddToEmpty(value);
        else {
            Node* tmp = this->First;

            this->First = p;
            this->First->Next = tmp;
        }
    }

    void Add(Node* node) {
        if (this->IsEmpty()) this->AddToEmpty(node->Data);
        else {
            this->Last->Next = node;
            this->Last = node;
        }
    }

    void Push(T value) {
        if (this->IsEmpty()) this->AddToEmpty(value);
        else {
            this->Add(new Node(value));
        }
    }

    Node* FindByVal(T value) {
        if (!this->IsEmpty()) {
            Node* p = this->First;            
            while (p && p->Data != value) p = p->Next;
                return (p && p->Data == value) ? p : 0;
        }
    }

    void RemoveFirst() {
        if (this->IsEmpty()) return;

        if (this->First == this->Last) this->Clear();
        else {
            Node* p = this->First;                
            this->First = p->Next;

            delete p;
        }
    }

    void Pop() {
        if (this->IsEmpty()) return;

        if (this->First == this->Last) this->Clear();
        else {
            Node* p = this->First;
            while (p->Next != this->Last) p = p->Next;

            p->Next = 0;
            delete this->Last;
            this->Last = p;
        }
    }

    void RemoveByVal(T value) {
        if (this->IsEmpty()) return;

        if (this->First->Data == value) {
            this->RemoveFirst();
            return;
        }

        if (this->Last->Data == value) {
            this->Pop();
            return;
        }

        Node* p = this->First;
        Node* nextP = this->First->Next;

        while (nextP->Data != value) {
            p = p->Next;
            nextP = nextP->Next;
        }

        p->Next = nextP->Next;
        delete nextP;
    }

    int IndexOf(Node* node) {
        if (this->IsEmpty()) return -1;
        Node* p = this->First;

        for (int i = 0; p != node; i++) {
            if (p == node) return i;
            p = p->Next;
        }
        
        return -1;
    }

    Node* operator[] (const int index) {
        if (this->IsEmpty()) return nullptr;
        Node* p = this->First;

        for (int i = 0; i < index; i++) {
            p = p->Next;
        }

        return (p) ? p : nullptr;
    }

    void PrintAll() {
        Node* p = this->First;
            for (int i = 0; p != 0; i++) {
                std::cout << "#" << i << "; Value:" << p->Data << std::endl;
                p = p->Next;
            }
    }

private:
    void AddToEmpty(T value) {
        Node* p = new Node(value);

        this->First = p;
        this->Last = p;
    }

    void AddToEmpty(Node* node) {
        this->First = node;
        this->Last = node;
    }

    void Clear() {
        this->First = 0;
        this->Last= 0;
    }
};

int main() {
    
    List<char>* list = new List<char>();

    list->Push('a');
    list->Push('b');
    list->Push('c');

    list->PrintAll();
}

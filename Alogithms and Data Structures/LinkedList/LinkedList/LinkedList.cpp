﻿#include <iostream>


struct Node
{
public:
    Node* Next;
    int Data;

    Node(int data) : Next(nullptr), Data(data) {}
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
        this->First = 0;
        this->Last = 0;
    }

    bool IsEmpty() {
        return this->First == NULL;
    }


    void AddFirst(int num) {
        Node* p = new Node(num);

        if (this->IsEmpty()) this->AddToEmpty(num);
        else {
            Node* tmp = this->First;

            this->First = p;
            this->First->Next = tmp;
        }
    }

    void AddLast(int num) {
        if (this->IsEmpty()) this->AddToEmpty(num);
        else {
            Node* p = new Node(num);

            this->Last->Next = p;
            this->Last = p;
        }
    }

    void PrintAllList(Node* startNode) {
        if (!this->IsEmpty()) {
            std::cout << startNode->Data << "\n";
            if (startNode->Next != 0) {
                PrintAllList(startNode->Next);
            }
        }
    }

    Node* FindByVal(int num) {
        if (!this->IsEmpty()) {
            Node* p = this->First;            
            while (p && p->Data != num) p = p->Next;
                return (p && p->Data == num) ? p : 0;            
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

    void RemoveLast() {
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

    void RemoveByVal(int num) {
        if (this->IsEmpty()) return;

        if (this->First->Data == num) {
            this->RemoveFirst();
            return;
        }

        if (this->Last->Data == num) {
            this->RemoveLast();
            return;
        }

        Node* p = this->First;
        Node* nextP = this->First->Next;

        while (nextP->Data != num) {
            p = p->Next;
            nextP = nextP->Next;
        }

        p->Next = nextP->Next;
        delete nextP;
    }

    Node* operator[] (const int index) {
        if (this->IsEmpty()) return nullptr;
        Node* p = this->First;

        for (int i = 0; i < index; i++) {
            p = p->Next;
        }

        return (p) ? p : nullptr;
    }

private:
    void AddToEmpty(int num) {
        Node* p = new Node(num);

        this->First = p;
        this->Last = p;
    }

    void Clear() {
        this->First = 0;
        this->Last= 0;
    }
};

int main() {

}

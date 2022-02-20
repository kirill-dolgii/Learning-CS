#include <iostream>

 struct ListNode {
    int val;
    ListNode* next;
    ListNode(int x) : val(x), next(NULL) {}
    
};

class Solution {
public:
    void deleteNode(ListNode* node) {
        ListNode* tmp = node->next;

        node->val = node->next->val;
        node->next = node->next->next;

        delete tmp;
    }
};

int main()
{
    std::cout << "Hello World!\n";
}

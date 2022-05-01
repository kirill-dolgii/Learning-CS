#include <iostream>
#include <stack>
#include <vector>
#include <iostream>

using namespace std;

template<class T>
class MyStack
{
public:
    MyStack() { }

    void Push(T value)
    {
        if (stack.empty())
        {
            this->min = value;
            this->stack.push(value);
            return;
        }
        if (value < this->min)
        {
            stack.push(2 * value - this->min);
            this->min = value;
        }
        else
            stack.push(value);
    }

    T Pop()
    {
        if (stack.top() < this->min)
        {
            T ret = this->min;
            this->min = -(stack.top() - 2 * this->min);
            stack.pop();
            return ret;
        }
        else
        {
            T ret = stack.top();
            stack.pop();
            return ret;
        }
    }

private:
    std::stack<T> stack;
    T min;
};

template <class T>
class MyBadStack
{
public:
    MyBadStack() {};

    void Push(T value)
    {
        if ((!stack.empty() && value < stack.top().prevMin) || stack.empty())
        {
            this->stack.push(*(new Node <T>(value, value)));
        }
        else
        {
            stack.push(*(new Node <T>(value, stack.top().prevMin)));
        }
    }

    T Pop()
    {
        Node <T> ret = stack.top();
        stack.pop();
        return ret.value;
    }

    T GetMin()
    {
        T ret = stack.top().prevMin;
        return ret;
    }

private:  

    template <class T>
    struct Node
    {
    public:
        Node(T value, T min)
        {
            this->value = value;
            this->prevMin = min;
        }

        bool operator>(Node <T> other)
        {
            if (this->value > other->value)
                return true;
            else
                return false;
        }

        T value;
        T prevMin;
    };

    stack<Node<T>> stack;

};

int main()
{
    MyStack<int> *st = new MyStack<int>;

    vector <int> vec = {10, 12, 7, 8, 4};

    for (int i = 0; i < vec.size(); i++)
    {
        st->Push(vec[i]);
    }

    for (int i = 0; i < vec.size(); i++)
    {
        int val = st->Pop();
        bool cond = val == vec[vec.size() - 1 - i];
        cout << cond;
    }   
    
    MyBadStack <int> s;

    s.Push(10);
    s.Push(3);
    s.Push(6);
    s.GetMin();
    s.Push(0);
    s.GetMin();

    s.Pop();
    s.GetMin();

}

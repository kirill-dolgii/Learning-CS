

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
    
}

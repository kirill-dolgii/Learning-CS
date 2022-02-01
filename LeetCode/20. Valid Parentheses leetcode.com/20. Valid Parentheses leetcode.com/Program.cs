using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static bool IsValid(string s)
        {
            Stack<char> sign = new Stack<char>();

            foreach (var item in s.ToCharArray())
                if (item == '(')
                    sign.Push(')');
                else if (item == '[')
                    sign.Push(']');
                else if (item == '{')
                    sign.Push('}');
                else if (sign.Count == 0 || sign.Pop() != item)
                    return false;

            return sign.Count == 0;
        }

        static bool MyCheck(string s)
        {
            Stack<char> stack = new Stack<char>(s.Length);

            foreach (char ch in s)
            {
                switch (ch)
                {
                    case '(' or '[' or '{':
                        stack.Push(ch);
                        break;
                    case ')' or ']' or '}':
                        if (stack.ToArray().Length == 0) return false;

                        if (ch == ')' && stack.Peek() == '(')
                        {
                            stack.Pop();
                            break;
                        }
                        else if (ch == ']' && stack.Peek() == '[')
                        {
                            stack.Pop();
                            break;
                        }
                        else if (ch == '}' && stack.Peek() == '{')
                        {
                            stack.Pop();
                            break;
                        }

                        return false;
                }
            }

            if (stack.ToArray().Length != 0)
                return false;

            return true;
        }

        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory() + "\\test_cases.txt";
;
            
            int correct = 0;
            int wrong = 0;

            foreach (var line in File.ReadAllLines(path).ToList().Select((value, i) => new { value, i }))
            {
                var tmp = line.value;

                if (MyCheck(tmp) == IsValid(tmp))
                {
                    Console.WriteLine("Check #" + line.i + " is sucsessful");
                    ++correct;
                }
                else
                {
                    Console.WriteLine("Check #" + line.i + " is unsucsessful");
                    ++wrong;
                }
            }
            string report = wrong == 0 ? "Test passed Sucessfully." : "Test is'nt passed. Number of wrong cases: " + wrong;
            Console.WriteLine(report);
        }
    }
}

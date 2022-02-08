using System;
using System.Collections.Generic;
using System.Linq;

namespace _206._Reverse_Linked_List_leetcode.com
{
    class Program
    {        
        public class ListNode
        {
              public int val;
              public ListNode next;
              public ListNode(int val = 0, ListNode next = null)
              {
                       this.val = val;
                       this.next = next;
              }

        }
        public static unsafe ListNode Reverse(ListNode head)
        {
            ListNode prev = null;

            while(head != null)
            {                
                ListNode next = head.next;
                head.next = prev;
                
                prev = head;

                head = next;
            }

            return prev;
        }

        static void Main(string[] args)
        {

            ListNode n1 = new ListNode(0);
            ListNode n2 = new ListNode(1, n1);
            ListNode n3 = new ListNode(2, n2);

            Reverse(n3);
        }
    }
}

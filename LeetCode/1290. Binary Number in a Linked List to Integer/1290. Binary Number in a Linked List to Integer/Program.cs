using System;

namespace _1290._Binary_Number_in_a_Linked_List_to_Integer
{
    public class Programm
    {   
        public static void Main()
        {
            
            ListNode p3 = new ListNode(1);
            ListNode p2 = new ListNode(0, p3);
            ListNode p1 = new ListNode(1, p2);

            int val = p1.GetDecimalValue(p1);
        }
    }

public class ListNode
   {
      public int val;
      public ListNode next;
      public ListNode(int val = 0, ListNode next = null)
      {
        this.val = val;
        this.next = next;
      }
    public int GetDecimalValue(ListNode head)
        {
            string number = "";

            while (head != null)
            {
                number += head.val.ToString();
                head = head.next;
            }

            return Convert.ToInt32(number, 2);
        }
    }


        

    
}

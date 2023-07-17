using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21880159
{
    public class LLStack<T>
    {
        Node<T> top;
        int size = 0;
        public LLStack()
        {
            top = null;
        }
        public void Push(T value)
        {
            Node<T> temp = new Node<T>(value);
            if (top == null)
            {
                temp.next = null;
            }
            else
            {
                temp.next = top;
            }
            top = temp;
            size++;
        }
        public bool isEmpty()
        {
            return top == null;
        }
        public T Peek()
        {
            if (top == null)
            {
                throw new Exception("Stack is empty.");
            }
            return top.value;
        }
        public void Pop()
        {
            if (top == null)
            {
                Console.WriteLine("Stack is empty.");
                return;
            }
            size--;
            top = top.next;
        }
        public int Count()
        {
            return size;
        }
    }

}

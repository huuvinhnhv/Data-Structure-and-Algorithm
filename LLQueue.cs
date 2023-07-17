using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21880159
{
    public class LLQueue<T>
    {
        Node<T> front, rear;
        int size = 0;
        public LLQueue()
        {
            front = rear = null;
        }
        public T Front()
        {
            return front.value;
        }
        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (rear == null)
            {
                front = rear = newNode;
            }
            else
            {
                rear.next = newNode;
                rear = newNode;
            }
            size++;
        }
        public void Dequeue()
        {
            if (front == null)
            {
                Console.WriteLine("Queue is Empty.");
                return;
            }
            Node<T> temp = front;

            front = front.next;

            if (front == null)
            {
                rear = null;
            }
            size--;
        }
        public int Count()
        {
            return size;
        }
    }
}

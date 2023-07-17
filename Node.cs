using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21880159
{
    public class Node<T>
    {
        internal T value;
        internal Node<T> next;
        public Node(T d)//
        {
            value = d;
            next = null;
        }
    }
}

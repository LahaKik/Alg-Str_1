using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Alg_Str_1
{
    internal class Queue<T> : DynamicArray<T> where T : IComparable
    {
        public Queue() : base() { }
        public Queue(int Count) : base(Count){ }

        public void Push(T data)
        {
            AddLast(data);
        }

        public T? Pop()
        {
            T? rez = data[0];
            if (RemoveFirst())
            {
                return rez;
            }
            return default;
        }
    }
}

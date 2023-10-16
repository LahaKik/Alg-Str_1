using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg_Str_1
{
    public class StackDArray<T> : DynamicArray<T>
    {
        private DynamicArray<T> array;

        public StackDArray()
        {
            array = new DynamicArray<T>();
            Count = 0;
        }

        public void Push(T item)
        {
            array.AddLast(item);
            Count++;
        }

        public T? Pop()
        {
            T? data = array[Count - 1];
            if (array.RemoveLast())
            {
                Count--;
                return data;

            }
            Console.WriteLine("Stack is empty");
            return default;
        }

        public override void Clear()
        {
            Count = 0;
            array.Clear();
        }

        public T this[int index]
        {
            get { return array[Count - 1 - index]; }
        }
        public override string? ToString()
        {
            string? result = "";
            for (int i = Count - 1; i >= 0; i--)
            {
                result += "[" + i + "]" + " " + array[i].ToString() + " ";
            }
            return result;
        }
    }
}

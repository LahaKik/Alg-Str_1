using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Alg_Str_1
{
    public class StackList<T> : MList<T>
    {
        private MList<T> mList;


        public StackList()
        {
            mList = new MList<T>();
            Count = 0;
        }

        public void Push(T item)
        {
            mList.AddFirst(item);
            Count++;
        }

        public T? Pop()
        {
            T? data = mList.GetDataFirst();
            if (mList.RemoveFirst())
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
            mList.Clear();
        }
        public override string ToString()
        {
            return mList.ToString();
        }
    }
}

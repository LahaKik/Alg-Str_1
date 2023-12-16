using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace Alg_Str_1
{
    
    internal class BinTree
    {
        public class Branch<U> : IComparable where U : IComparable
        {
            public Branch<U>? left = null;
            public Branch<U>? right = null;
            public Branch<U>? parent = null;
            public U? data;
            public bool IsEmpty;
            public U? Data
            {
                get { return data; }
                set { data = value; }
            }
            public Branch(U? data, Branch<U>? _parent = null, Branch<U>? _left = null, Branch<U>? _right = null)
            {
                this.data = data;
                left = _left;
                right = _right;
                parent = _parent;
            }
            public Branch(U? data, Branch<U>? _parent = null, bool _IsEmpty = true)
            {
                this.data = data;
                IsEmpty = _IsEmpty;
                left = null;
                right = null;
                parent = _parent;
            }
            public int CompareTo(object? obj)
            {
                if (obj is IComparable && Data is not null)
                    return Data.CompareTo(obj);
                else throw new ArgumentException("Non-comparable object");

            }
            public override string ToString()
            {
                return Data.ToString();
            }
        }
        public static readonly string Path = @"..\BinTree.txt";
        private Branch<int> root;
        private int countNodes;
        public int CountNodes
        {
            get { return countNodes; }
            protected set
            {
                countNodes = value;
                if (countNodes < 0)
                    countNodes = 0;
            }
        }


        private BinTree(out bool success)
        {
            using FileStream fstream = new FileStream(Path, FileMode.OpenOrCreate);
            success = ReadFromFile(fstream);
            delEmptyBranch();
        }

        public static BinTree? Create
        {
            get
            {
                BinTree tree = new BinTree(out bool success);
                if (success)
                    return tree;
                else throw new ArgumentException("Mistake in file");
            }
        }

        private bool ReadFromFile(FileStream fstream)
        {
            byte[] buff = new byte[fstream.Length];
            fstream.Read(buff, 0, buff.Length);
            string rezult = Encoding.Default.GetString(buff);
            string[] tokens = tokenize(rezult);
            Branch<int> curr = root;

            int cntOp = 0, cntCl = 0;
            foreach (var str in tokens)
            {
                if (str == "(")
                    cntOp++;
                else if (str == ")")
                {
                    if (curr != root)
                        curr = curr.parent!;
                    cntCl++;
                }
                else
                {
                    if (root == null && int.TryParse(str, out int num1))
                    {
                        curr = new Branch<int>(num1, null, false);
                        root = curr;
                    }
                    else if (curr.left == null)
                    {
                        if (str == "")
                        {
                            curr.left = new Branch<int>(default, curr, true);
                        }
                        else if (int.TryParse(str, out int num2))
                        {
                            curr.left = new Branch<int>(num2, curr, false);
                        }
                        else
                            return false;
                        curr = curr.left;
                    }
                    else if (curr.right == null)
                    {
                        if (str == "")
                        {
                            curr.right = new Branch<int>(default, curr, true);
                        }
                        else if (int.TryParse(str, out int num3))
                        {
                            curr.right = new Branch<int>(num3, curr, false);
                        }
                        else
                            return false;
                        curr = curr.right;
                    }
                    else
                        return false;
                    CountNodes++;
                }
            }
            if (cntCl != cntOp)
                return false;
            return true;
        }

        private string[] tokenize(string input)
        {
            DynamicArray<string> arr = new DynamicArray<string>();
            foreach (char sym in input)
            {
                if(sym == ')' || sym == '(')
                {
                    if (arr[arr.Count - 1] == "(" && sym == ')')
                        arr.AddLast("");
                    arr.AddLast(sym.ToString());
                }
                else
                {
                    if (arr[arr.Count - 1] != ")" && arr[arr.Count - 1] != "(")
                    {
                        arr.SwitchData(arr.Count - 1, arr[arr.Count - 1] + sym.ToString());
                    }
                    else
                        arr.AddLast(sym.ToString());
                }
            }
            string[] strings = new string[arr.Count];
            for (int i = 0; i < arr.Count; i++)
            {
                strings[i] = arr[i]!;
            }
            return strings;
        }

        private void delEmptyBranch()
        {
            Queue<Branch<int>> queueNodes = new Queue<Branch<int>>(CountNodes);
            Branch<int>? curr = root;
            int deferredMinus = 0;

            for (int i = 0; i < CountNodes; i++)
            {
                if(curr!.IsEmpty)
                {
                    if(curr == curr.parent!.left)
                    {
                        curr.parent.left = null;
                        curr.parent = null;
                        deferredMinus++;
                    }
                    else if (curr == curr.parent.right)
                    {
                        curr.parent.right = null;
                        curr.parent = null;
                        deferredMinus++;
                    }
                }
                else
                {
                    if (curr.left != null)
                        queueNodes.Push(curr.left);
                    if (curr.right != null)
                        queueNodes.Push(curr.right);
                }

                curr = queueNodes.Pop();
            }
            CountNodes -= deferredMinus;
        }

        public int[] ToArray()//to del
        {
            int[] rez = new int[CountNodes];
            Queue<Branch<int>> queueNodes = new Queue<Branch<int>>(CountNodes);
            Branch<int>? curr = root;

            for (int i = 0; i < CountNodes; i++)
            {
                rez[i] = curr.Data;
                if (curr.left != null)
                    queueNodes.Push(curr.left);
                if (curr.right != null)
                    queueNodes.Push(curr.right);

                curr = queueNodes.Pop();
            }
            return rez;
        }
        public override string ToString()
        {
            string rez = "";
            Branch<int>? curr = root;
            rez = StringGoInto(ref curr);
            int cntOp = 0, cntCl = 0;
            foreach (var sym in rez)
            {
                if (sym == '(') cntOp++;
                else if (sym == ')') cntCl++;
            }
            if (cntOp != cntCl)
                rez.Remove(rez.Length - (cntCl - cntOp), cntCl - cntOp);
            return rez;
        }

        private string StringGoInto(ref Branch<int> curr, Direction dir = Direction.DEFAULT)
        {
            string str = "";
            if (dir != Direction.LEFT)
            {
                if (curr.left == null && curr.right == null)
                    return "())";

                str += "(" + curr.Data.ToString();
                str += StringGoInto(ref curr.left, Direction.LEFT);
                str += StringGoInto(ref curr.right, Direction.RIGHT) + ")";
                return str;
            }
            else
            {
                if (curr.left == null && curr.right == null)
                    return "()";

                str += "(" + curr.Data.ToString();
                str += StringGoInto(ref curr.left, Direction.LEFT);
                str += StringGoInto(ref curr.right, Direction.RIGHT);
                return str;
            }
        }

        enum Direction
        {
            DEFAULT = 'D',
            RIGHT = 'R',
            LEFT = 'L'
        }

    }
}
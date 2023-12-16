using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alg_Str_1
{
    internal class RB_tree<T> where T : IComparable
    {
        public enum Color
        {
            BLACK = 'B',
            RED = 'R'
        }
        enum Direction
        {
            DEFAULT = 'D',
            RIGHT = 'R',
            LEFT = 'L'
        }
        public class RB_branch<U> : IComparable where U : IComparable
        {
            public RB_branch<U>? left = null;
            public RB_branch<U>? right = null;
            public RB_branch<U>? parent = null;
            private U? data = default;
            public U? Data
            {
                get { return data; }
                set
                {
                    data = value;
                    if (data != null)
                        IsLeaf = false;
                    else
                        IsLeaf = true;
                }
            }
            public Color color;
            public bool IsLeaf = true;
            public RB_branch(U? data, Color color, RB_branch<U>? _parent = null, RB_branch<U>? _left = null, RB_branch<U>? _right = null)
            {
                this.data = data;
                this.color = color;
                parent = _parent;
                IsLeaf = false;
                if (_left != null)
                    left = _left;
                else
                    left = new RB_branch<U>(Color.BLACK, this);
                if (_right != null)
                    right = _right;
                else
                    right = new RB_branch<U>(Color.BLACK, this);
            }
            public RB_branch(Color color, RB_branch<U>? _parent = null) //Leaf
            {
                parent = _parent;
                this.color = color;
                IsLeaf = true;
                left = null;
                right = null;
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

        public RB_branch<T>? root;
        
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

        public RB_tree()
        {
            root = new RB_branch<T>(Color.BLACK);
            CountNodes = 0;
        }
        public RB_tree(T data)
        {
            root = new RB_branch<T>(data, Color.BLACK);
            CountNodes = 1;
        }

        public RB_tree(ref BinTree binTree)
        {
            if (typeof(T) == typeof(int))
            {
                root = new RB_branch<T>(Color.BLACK);
                CountNodes = 0;
                int[] a = binTree.ToArray();
                foreach (dynamic item in a)
                {
                    Add(item);
                }
            }
        }

        public static RB_tree<int>? CreateFromBinTree(ref BinTree? bTree)
        {
            if (bTree == null || bTree.CountNodes == 0)
            {
                Console.WriteLine("Tree is empty");
                return null;
            }
            RB_tree<int> tree = new RB_tree<int>(ref bTree);
            return tree;
        }


        #region Insert
        public void Add(T data)
        {
            if(root!.IsLeaf == true)
            {
                root = new RB_branch<T>(data, Color.BLACK);
                countNodes++;
                return;
            }

            RB_branch<T>? curr = root;
            while(curr != null)
            {
                if (data.CompareTo(curr.Data) > 0)
                {
                    if (curr.right!.IsLeaf)
                    {
                        curr = curr.right = new RB_branch<T>(data, Color.RED, curr);
                        break;
                    }
                    else
                        curr = curr.right;
                }
                else if (data.CompareTo(curr.Data) < 0)
                {
                    if (curr.left!.IsLeaf)
                    {
                        curr = curr.left = new RB_branch<T>(data, Color.RED, curr);
                        break;
                    }
                    else
                        curr = curr.left;
                }
                else
                    return;
            }

            InsertBalance(ref curr);
            CountNodes++;
        }

        private void InsertBalance(ref RB_branch<T>? curr)
        {
            if (curr == null || curr.parent == null)
                return;
            if (curr.color == Color.RED && curr.parent.color == Color.BLACK)
                return;

            RB_branch<T>? uncle = RB_tree<T>.GetUncle(ref curr);
            if (uncle != null)
            {
                if (uncle.color == Color.RED)
                    balanceInsert_1(ref curr);
                else if (IsNFG_Straight(ref curr, out Direction dir) == true)
                    balanceInsert_2(ref curr, dir);
                else if (IsNFG_Straight(ref curr, out dir) == false)
                    balanceIncert_3(ref curr, dir);
            }
            if (root!.color == Color.RED)
                root.color = Color.BLACK;
        }

        private void balanceInsert_1(ref RB_branch<T>? curr)
        {
            RB_branch<T>? uncle = GetUncle(ref curr);
            if (curr == null || curr.parent == null || curr.parent.parent == null || uncle == null)
                return;
            curr.parent!.color = Color.BLACK;
            curr.parent.parent!.color = Color.RED;
            uncle.color = Color.BLACK;
            curr = curr.parent.parent;
            InsertBalance(ref curr);
        }

        private void balanceInsert_2(ref RB_branch<T> curr, Direction dir)
        {
            curr = curr.parent!;
            if (dir == Direction.LEFT)
            {
                
                rightRotate(ref curr!);
                curr.color = Color.BLACK;
                curr.right.color = Color.RED;
            }
            else
            {
                leftRotate(ref curr);
                curr.color = Color.BLACK;
                curr.left.color = Color.RED;
            }
        }

        private void balanceIncert_3(ref RB_branch<T> curr, Direction dir)
        {
            curr.parent.parent.color = Color.RED;
            curr.color = Color.BLACK;
            if(dir == Direction.RIGHT)
            {
                leftRotate(ref curr);
                rightRotate(ref curr);
            }
            else
            {
                rightRotate(ref curr);
                leftRotate(ref curr);
            }
        }

        private bool? IsNFG_Straight(ref RB_branch<T>? curr, out Direction dir)
        {
            dir = Direction.DEFAULT;
            if (curr == null || curr.parent == null || curr.parent.parent == null)
                return null;

            if (curr == curr.parent.left)
            {
                dir = Direction.LEFT;
                if (curr.parent == curr.parent.parent.left)
                    return true;
                else if (curr.parent == curr.parent.parent.right)
                    return false;
                return null;
            }
            else if (curr == curr.parent.right)
            {
                dir = Direction.RIGHT;
                if (curr.parent == curr.parent.parent.right)
                    return true;
                else if (curr.parent == curr.parent.parent.left)
                    return false;
                return null;
            }
            return null;
        }
        #endregion

        #region Delete
        public void Delete(T item)
        {
            if(countNodes == 0 || root == null)
                return;

            RB_branch<T> curr = root;
            while(curr!.IsLeaf != true)
            {
                if (item.CompareTo(curr.Data) > 0)
                    curr = curr.right!;
                else if (item.CompareTo(curr.Data) < 0)
                    curr = curr.left!;
                else if (item.CompareTo(curr.Data) == 0)
                    break;
            }
            if (curr.IsLeaf == true)
                return;


            DeleteBalance(ref curr);
            CountNodes--;
        }

        private void DeleteBalance(ref RB_branch<T> curr)
        {
            int countChildren = CountChildren(ref curr);
            if(curr.color == Color.RED)
            {
                if (countChildren == 0)
                    balanceDelete_R0(ref curr);
                else if (countChildren == 1)//imposible
                    return;
                else if (countChildren == 2)
                    balanceDelete_R2(ref curr);
            }
            else
            {
                if (countChildren == 0)
                    balanceDelete_B0(ref curr);
                else if (countChildren == 1)
                    balanceDelete_B1(ref curr);
                else if (countChildren == 2)
                    balanceDelete_B2(ref curr);
            }
        }

        private void recoverBlackHeight(ref RB_branch<T> curr)
        {
            RB_branch<T>? brother = GetBrother(ref curr);

            if(brother!.IsLeaf != true)
            {
                if(curr == curr.parent!.right)
                {
                    if (curr.parent.color == Color.RED)
                    {
                        if (brother.left!.color == Color.BLACK && brother.right!.color == Color.BLACK)
                        {
                            curr.parent.color = Color.BLACK;
                            brother.color = Color.RED;
                        }

                        else if (brother.left!.color == Color.RED)
                        {
                            rightRotate(ref brother);
                            brother.color = Color.RED;
                            brother.left!.color = Color.BLACK;
                            brother.right!.color = Color.BLACK;
                        }
                    }


                    else if (brother.right!.IsLeaf != true)
                    {
                        if (brother.color == Color.RED && brother.right.right.color == Color.BLACK && brother.right.left.color == Color.BLACK)
                        {
                            brother.color = Color.BLACK;
                            brother.right.color = Color.RED;
                            rightRotate(ref brother);
                        }

                        else if (brother.color == Color.RED && brother.right.left!.color == Color.RED)
                        {
                            RB_branch<T> grandson = brother.right;
                            grandson.left.color = Color.BLACK;
                            leftRotate(ref grandson);
                            rightRotate(ref grandson);
                        }

                        else if (brother.color == Color.BLACK && brother.right.color == Color.RED)
                        {
                            RB_branch<T> grandson = brother.right;
                            grandson.color = Color.BLACK;
                            leftRotate(ref grandson);
                            rightRotate(ref grandson);
                        }
                    }
                    else
                    {
                        brother.color = Color.RED;
                        recoverBlackHeight(ref curr.parent);
                    }
                }

                else
                {
                    if (curr.parent.color == Color.RED)
                    {
                        if (brother.left!.color == Color.BLACK && brother.right!.color == Color.BLACK)
                        {
                            curr.parent.color = Color.BLACK;
                            brother.color = Color.RED;
                        }

                        else if (brother.right!.color == Color.RED)
                        {
                            leftRotate(ref brother);
                            brother.color = Color.RED;
                            brother.left!.color = Color.BLACK;
                            brother.right!.color = Color.BLACK;
                        }
                    }


                    else if (brother.left!.IsLeaf != true)
                    {
                        if (brother.color == Color.RED && brother.left.left!.color == Color.BLACK && brother.left.right!.color == Color.BLACK)
                        {
                            brother.color = Color.BLACK;
                            brother.left.color = Color.RED;
                            leftRotate(ref brother);
                        }

                        else if (brother.color == Color.RED && brother.left.right!.color == Color.RED)
                        {
                            RB_branch<T> grandson = brother.left;
                            grandson.right.color = Color.BLACK;
                            rightRotate(ref grandson);
                            leftRotate(ref grandson);
                        }

                        else if (brother.color == Color.BLACK && brother.left.color == Color.RED)
                        {
                            RB_branch<T> grandson = brother.left;
                            grandson.color = Color.BLACK;
                            rightRotate(ref grandson);
                            leftRotate(ref grandson);
                        }
                    }
                    else
                    {
                        brother.color = Color.RED;
                        recoverBlackHeight(ref curr.parent);
                    }
                }
            }
        }

        private void balanceDelete_R0(ref RB_branch<T> curr) //red branch without children(rework)
        {
            if (curr == curr.parent!.left)
                curr.parent.left = new RB_branch<T>(Color.BLACK, curr.parent);
            else
                curr.parent.right = new RB_branch<T>(Color.BLACK, curr.parent);
        }

        private void balanceDelete_R2(ref RB_branch<T> curr)//red branch with two children
        {

            RB_branch<T> toSwap = curr.left!;

            while (toSwap.IsLeaf != true)
            {
                toSwap = toSwap.right!;
            }

            toSwap = toSwap.parent!;
            (curr.Data, toSwap.Data) = (toSwap.Data, curr.Data);
            curr = toSwap;
            if(curr.color == Color.RED)
                balanceDelete_R0(ref curr);
            else
            {
                int countCh = CountChildren(ref curr);
                if (countCh == 0)
                    balanceDelete_B0(ref curr);
                else if (countCh == 1)
                    balanceDelete_B1(ref curr);
            }
        }

        private void balanceDelete_B0(ref RB_branch<T> curr)//black branch without children
        {
            if (curr == curr.parent!.left)
            {
                curr.parent.left = new RB_branch<T>(Color.BLACK, curr.parent);
                recoverBlackHeight(ref curr.parent.left);
            }
            else
            {
                curr.parent.right = new RB_branch<T>(Color.BLACK, curr.parent);
                recoverBlackHeight(ref curr.parent.right);
            }
        }

        private void balanceDelete_B1(ref RB_branch<T> curr)//black branch with one child
        {
            if(curr.right!.IsLeaf != true)
            {
                curr.Data = curr.right.Data;
                curr.right = new RB_branch<T>(Color.BLACK, curr);
            }
            else
            {
                curr.Data = curr.left!.Data;
                curr.left = new RB_branch<T>(Color.BLACK, curr);
            }
        }

        private void balanceDelete_B2(ref RB_branch<T> curr)//black branch with two children
        {
            RB_branch<T> toSwap = curr.left!;

            while (toSwap.IsLeaf != true)
            {
                toSwap = toSwap.right!;
            }

            toSwap = toSwap.parent!;
            (curr.Data, toSwap.Data) = (toSwap.Data, curr.Data);
            curr = toSwap;
            if (curr.color == Color.RED)
                balanceDelete_R0(ref curr);
            else
            {
                int countCh = CountChildren(ref curr);
                if (countCh == 0)
                    balanceDelete_B0(ref curr);
                else if (countCh == 1)
                    balanceDelete_B1(ref curr);
            }
        }

        private int CountChildren(ref RB_branch<T> curr)
        {
            if(curr == null || curr.IsLeaf == true) return -1;

            int cntCh = 0;
            if(curr.left!.IsLeaf != true)
                cntCh++;
            if(curr.right!.IsLeaf != true)
                cntCh++;
            return cntCh;
        }

        private void rightRotate(ref RB_branch<T> curr)
        {
            RB_branch<T> father = curr.parent;
            if (root == father)
                root = curr;
            if(father.parent != null)
            {
                if (father == father.parent.left)
                    father.parent.left = curr;
                else
                    father.parent.right = curr;
            }
            curr.parent = father.parent;
            
            father.left = curr.right;
            curr.right.parent = father;
            curr.right = father;
            father.parent = curr;
        }

        private void leftRotate(ref RB_branch<T> curr)
        {
            RB_branch<T> father = curr.parent;
            if (root == father)
                root = curr;
            if (father.parent != null)
            {
                if (father == father.parent.left)
                    father.parent.left = curr;
                else
                    father.parent.right = curr;
            }
            curr.parent = father.parent;
            father.right = curr.left;
            curr.left.parent = father;
            curr.left = father;
            father.parent = curr;
        }

        #endregion


        private static RB_branch<T>? GetUncle(ref RB_branch<T>? curr)
        {
            if(curr != null && curr.parent != null && curr.parent.parent != null)
            {
                if (curr.parent == curr.parent.parent.left)
                    return curr.parent.parent.right;
                else if (curr.parent == curr.parent.parent.right) 
                    return curr.parent.parent.left;
            }
            return null;
        }
        private static RB_branch<T>? GetBrother(ref RB_branch<T>? curr)
        {
            if (curr != null && curr.parent != null)
            {
                if (curr == curr.parent.left)
                    return curr.parent.right;
                else if (curr == curr.parent.right)
                    return curr.parent.left;
            }
            return null;
        }
 
        public string[] ToGraphicProcess()
        {
            string[] rez = new string[CountNodes];
            Queue<RB_branch<T>> queueNodes = new Queue<RB_branch<T>>(CountNodes);
            RB_branch<T>? curr = root;

            for (int i = 0; i < CountNodes; i++)
            {
                rez[i] = RouteToRoot(curr!) + curr!.Data!.ToString() + (char)curr.color;
                if (curr.left.IsLeaf != true)
                    queueNodes.Push(curr.left);
                if (curr.right.IsLeaf != true)
                    queueNodes.Push(curr.right);

                curr = queueNodes.Pop();
            }
            return rez;
        }

        private string RouteToRoot(RB_branch<T> curr)
        {
            string rez = "";
            while(curr.parent != null)
            {
                if (curr == curr.parent.left)
                    rez += (char)Direction.LEFT;
                if (curr == curr.parent.right)
                    rez += (char)Direction.RIGHT;
                curr = curr.parent;
            }
            rez = StringHelper.ReverseString(rez);
            return rez;
        }

        public override string ToString()
        {
            string rez = "";
            RB_branch<T> ? curr = root;
            rez = StringGoInto(ref curr);
            int cntOp = 0, cntCl = 0;
            foreach (var sym in rez)
            {
                if(sym == '(') cntOp++;
                else if(sym ==  ')') cntCl++;
            }
            if(cntOp != cntCl)
                rez.Remove(rez.Length - (cntCl - cntOp), cntCl - cntOp);
            return rez;
        }

        private string StringGoInto(ref RB_branch<T> curr, Direction dir = Direction.DEFAULT)
        {
            string str = "";
            if(dir != Direction.LEFT)
            {
                if (curr.IsLeaf == true)
                    return "())";

                str += "(" + curr.Data.ToString() + ((char)curr.color);
                str += StringGoInto(ref curr.left, Direction.LEFT);
                str += StringGoInto(ref curr.right, Direction.RIGHT) + ")";
                return str;
            }
            else
            {
                if (curr.IsLeaf == true)
                    return "()";

                str += "(" + curr.Data.ToString() + ((char)curr.color);
                str += StringGoInto(ref curr.left, Direction.LEFT);
                str += StringGoInto(ref curr.right, Direction.RIGHT);
                return str;
            }
        }
        private string ToClearStringInto(ref RB_branch<T> curr, Direction dir = Direction.DEFAULT)
        {
            string str = "";
            if (dir != Direction.LEFT)
            {
                if (curr.IsLeaf == true)
                    return "())";

                str += "(" + curr.Data.ToString();
                str += ToClearStringInto(ref curr.left, Direction.LEFT);
                str += ToClearStringInto(ref curr.right, Direction.RIGHT) + ")";
                return str;
            }
            else
            {
                if (curr.IsLeaf == true)
                    return "()";

                str += "(" + curr.Data.ToString();
                str += ToClearStringInto(ref curr.left, Direction.LEFT);
                str += ToClearStringInto(ref curr.right, Direction.RIGHT);
                return str;
            }
        }
        public string ToClearString()
        {
            string rez = "";
            RB_branch<T>? curr = root;
            rez = ToClearStringInto(ref curr);
            int cntOp = 0, cntCl = 0;
            foreach (var sym in rez)
            {
                if (sym == '(') cntOp++;
                else if (sym == ')') cntCl++;
            }
            if (cntOp != cntCl)
                rez = rez.Remove(rez.Length - (cntCl - cntOp));
            return rez;
        }

        public void SaveToFile()
        {
            using FileStream stream = new FileStream(BinTree.Path, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = Encoding.Default.GetBytes(ToClearString());
            stream.Write(buff, 0, buff.Length);
        }
    }

    static class StringHelper
    {
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}

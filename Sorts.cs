using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alg_Str_1
{
    static class Sorts<T, U> where T : ISortObject<U> where U : IComparable
    {
        private static int Minrun(int n)
        {
            int r = 0;
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            return r + n;
        }

        private static void Swap(ref T mass, int firstIndex, int secondIndex)
        {
            if (firstIndex > mass.Count - 1 || secondIndex > mass.Count - 1 || firstIndex == secondIndex)
                return;
            var temp = mass[firstIndex];
            mass.SwitchData(firstIndex, mass[secondIndex]!);
            mass.SwitchData(secondIndex, temp!); //bool?
        }

        public static void InsertionSort(ref T mass, int start = 0, int finish = int.MaxValue)
        {
            if (finish == int.MaxValue)
                finish = mass.Count;
            if (mass.Count < 2 || start > finish || start > mass.Count || finish > mass.Count)
                return;

            for (int i = start; i <= finish; i++)
            {
                for (int j = i; j > start && mass[j - 1].CompareTo(mass[j]) > 0; j--)
                {
                    Swap(ref mass, j - 1, j);
                }
            }
        }
        
        public static void Merge(ref T mass, int firstStart, int firstFinish, int seconsStart, int seconsFinish)
        {
            U[] tempMass;
            int i;
            if (firstFinish - firstStart < seconsFinish - seconsStart)
            {
                tempMass = new U[firstFinish - firstStart + 1];
                int t = 0;
                for (int n = firstStart; n <= firstFinish; n++)
                {
                    tempMass[t] = mass[n]!;
                    t++;
                }
                i = seconsStart;
                int j = firstStart;
                int k = 0;
                while(i <= seconsFinish && k <= firstFinish - firstStart)
                {
                    if (tempMass[k].CompareTo(mass[i]) >= 0)
                    {
                        mass.SwitchData(j, mass[i]!);
                        i++;
                        j++;
                    }
                    else
                    {
                        mass.SwitchData(j, tempMass[k]);
                        k++;
                        j++;
                    }
                }
                if(i >= seconsFinish)
                {
                    for (int n = k; n <= firstFinish - firstStart; n++)
                    {
                        mass.SwitchData(j, tempMass[n]);
                        j++;
                    }
                }
                else
                {
                    for (int n = i; n <= seconsFinish; n++)
                    {
                        mass.SwitchData(j, mass[n]!);
                        j++;
                    }
                }
            }
            else
            {
                tempMass = new U[seconsFinish - seconsStart + 1];
                for (int n = seconsStart; n <= seconsFinish; n++)
                    tempMass[n - seconsStart] = mass[n]!;
                i = firstFinish;
                int j = seconsFinish;
                int k = seconsFinish - seconsStart;
                while (i >= firstStart && k >= 0)
                {
                    if (tempMass[k].CompareTo(mass[i]) >= 0)
                    {
                        mass.SwitchData(j, tempMass[k]);
                        k--;
                        j--;
                    }
                    else
                    {
                        mass.SwitchData(j, mass[i]!);
                        i--;
                        j--;
                    }
                }
                if(i <= firstStart)
                {
                    for (int n = k; n >= 0; n--)
                    {
                        mass.SwitchData(j, tempMass[n]);
                        j--;
                    }
                }
                else
                {
                    for (int n = i; n >= firstStart; n--)
                    {
                        mass.SwitchData(j, mass[n]!);
                        j--;
                    }
                }
            }      
        }

        public static void Reverse(ref T mass, int start, int finish)
        {
            if (start >= finish || finish > mass.Count - 1)
                return;

            for (int i = 0; i < (finish - start + 1) / 2 ; i++)
            {
                Swap(ref mass, start + i, finish - i);
            }
        }

        private static bool CheckSequence(ref T mass, ref StackDArray<(int, int)> stackLen, ref MList<int> runs, ref MList<int> LenRun)
        {
            if (stackLen.Count > 2)
            {
                if (stackLen[2].Item2 <= stackLen[1].Item2 + stackLen[0].Item2)
                {
                    if (stackLen[0].Item2 > stackLen[2].Item2)
                    {
                        Merge(ref mass, stackLen[2].Item1, stackLen[1].Item1 - 1, stackLen[1].Item1, stackLen[1].Item1 + stackLen[1].Item2 - 1);
                        stackLen.SwitchData(2, (stackLen[2].Item1, stackLen[1].Item2 + stackLen[2].Item2 - 1));
                        stackLen.Pop(1);
                    }
                    else
                    {
                        Merge(ref mass, stackLen[1].Item1, stackLen[0].Item1 - 1, stackLen[0].Item1, stackLen[0].Item1 + stackLen[0].Item2 - 1);
                        stackLen.SwitchData(1, (stackLen[1].Item1, stackLen[0].Item2 + stackLen[1].Item2 - 1));
                        stackLen.Pop();
                    return true;
                    }
                }
                return false;
            }
            else if (stackLen.Count > 1)
            {
                if (stackLen[1].Item2 <= stackLen[0].Item2)
                {
                    Merge(ref mass, stackLen[1].Item1, stackLen[0].Item1 - 1, stackLen[0].Item1, stackLen[0].Item1 + stackLen[0].Item2 - 1);
                    stackLen.SwitchData(1, (stackLen[1].Item1, stackLen[0].Item2 + stackLen[1].Item2 - 1));
                    stackLen.Pop();
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public static void TimSort(ref T mass)
        {
            if (mass.Count < 2)
                return;

            if (mass.Count < 64)
            {
                InsertionSort(ref mass);
                return;
            }

            int minrun = Minrun(mass.Count);

            MList<int> runs = new MList<int>();
            MList<int> LenRun = new MList<int>();
            runs.AddLast(0);

            if (mass[0]!.CompareTo(mass[1]) > 0)
                Swap(ref mass, 0, 1);

            bool? IsIncreasing = true;
            for (int i = 2; i < mass.Count; i++)
            {
                if(IsIncreasing == null)
                {
                    if (mass[i]!.CompareTo(mass[i - 1]) >= 0)
                        IsIncreasing = true;
                    else
                        IsIncreasing = false;
                }

                if (mass[i]!.CompareTo(mass[i-1]) < 0 && IsIncreasing == true)
                {
                    if (i - runs[-1] >= minrun)
                    {
                        runs.AddLast(i);
                        LenRun.AddLast(i - runs[-2]);
                        IsIncreasing = null;
                    }
                    else
                    {
                        i = (runs[-1] + minrun) < mass.Count ? (runs[-1] + minrun) : mass.Count - 1;
                        runs.AddLast(i);

                        LenRun.AddLast(i - runs[-2]);
                        InsertionSort(ref mass, runs[-2], i - 1);
                        IsIncreasing = null;
                    }
                }

                if (mass[i]!.CompareTo(mass[i - 1]) >= 0 && IsIncreasing == false)
                {
                    if (i - runs[-1] >= minrun)
                    {
                        runs.AddLast(i);
                        LenRun.AddLast(i - runs[-2]);
                        Reverse(ref mass, runs[-2], runs[-1] - 1);
                        IsIncreasing = null;
                    }
                    else
                    {
                        i = (runs[-1] + minrun) < mass.Count ? (runs[-1] + minrun) : mass.Count - 1;
                        runs.AddLast(i);
                        LenRun.AddLast(i - runs[-2]);
                        Reverse(ref mass, runs[-2], runs[-1] - 1);
                        InsertionSort(ref mass, runs[-2], i - 1);
                        IsIncreasing = null;
                    }
                }
            }

            if (IsIncreasing == false)
            {
                Reverse(ref mass, runs[0], mass.Count - 1);
            }
            LenRun.AddLast(mass.Count - runs[-1]);

            StackDArray<(int, int)> stackLen = new StackDArray<(int, int)>();
            while (runs.Count > 0)
            {
                if(!CheckSequence(ref mass, ref stackLen, ref runs, ref LenRun))
                {
                    stackLen.Push((runs[0], LenRun[0]));
                    runs.RemoveFirst();
                    LenRun.RemoveFirst();
                }
            }
            for (int i = 0; i < stackLen.Count - 1; i++)
            {
                Merge(ref mass, stackLen[1].Item1, stackLen[0].Item1 - 1, stackLen[0].Item1, stackLen[0].Item1 + stackLen[0].Item2 - 1);
                stackLen.SwitchData(1, (stackLen[1].Item1, stackLen[0].Item2 + stackLen[1].Item2 - 1));
                stackLen.Pop();
            }
        }
        public static void ShellSort(ref T mass)
        {
            for (int d = mass.Count; d != 0; d/=2)
            {
                for (int i = d; i < mass.Count; i++)
                {
                    for (int j = i - d; j >= 0 && mass[j]!.CompareTo(mass[j + d]) > 0; j -= d) 
                    {
                        Swap(ref mass, j, j + d);
                    }
                }
            }
        }
    }

    public interface ISortObject<T> where T : IComparable
    {
        public int Count { get; }
        public void AddAt(int index, T data);
        public void AddFirst(T data);
        public void AddLast(T data);
        public void Clear();
        public bool RemoveAt(int index);
        public bool RemoveFirst();
        public bool RemoveLast();
        public T? this[int index] { get; }
        public bool SwitchData(int index, T data);
    }
}

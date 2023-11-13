using System;
using System.Diagnostics;
using Alg_Str_1;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Prog
{
    private static int MainMenu(ref DynamicArray<int> dArray)
    {
        bool Success = false;
        int rezult = -1;
        while (!Success)
        {
            Console.WriteLine("Select menu item:");
            Console.WriteLine("1 - input value in array");
            Console.WriteLine("2 - delete value");
            Console.WriteLine("3 - use ramdom values to sort");
            Console.WriteLine("4 - Timsort");
            Console.WriteLine("5 - Shell sort");
            Console.WriteLine("6 - Compare time of algorithms");
            Console.WriteLine("9 - Exit");
            Console.WriteLine("Your choice:");
            if (dArray.Count < 200)
            {
                Console.WriteLine(dArray.ToString());
                Console.SetCursorPosition(12, 8);
            }
            string? inp = Console.ReadLine();
            Console.Clear();

            if (int.TryParse(inp, out rezult))
            {
                if (rezult == 9 || (rezult > 0 && rezult < 7))
                {
                    Success = true;
                }
                else
                {
                    Console.WriteLine("Incorrect input!");
                }
            }
            else
            {
                Console.WriteLine("Incorrect input!");
            }
        }

        Console.Clear();
        return rezult;
    }
    static void Main()
    {
        DynamicArray<int> dArray = new DynamicArray<int>();

        bool Exit = false;
        while (!Exit)
        {
            int choice = MainMenu(ref dArray);

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Enter adding value: ");
                    int rezultinp;
                    string? inpNum = Console.ReadLine();
                    Console.Clear();
                    if (int.TryParse(inpNum, out rezultinp))
                    {
                        dArray.AddLast(rezultinp);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Enter delete index (or 'all' to clear array): ");
                    int deleteNum;
                    string? InpNum = Console.ReadLine();
                    Console.Clear();
                    if (InpNum == "all")
                    {
                        dArray.Clear();
                        break;
                    }
                    if (!(int.TryParse(InpNum, out deleteNum) && dArray.RemoveAt(deleteNum)))
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Enter the number of ramdom numbers: ");
                    int rezultNum;
                    string? inputNum = Console.ReadLine();
                    Console.Clear();
                    if (int.TryParse(inputNum, out rezultNum))
                    {
                        Random rnd = new Random(DateTime.Now.Second);
                        for (int i = 0; i < rezultNum; i++)
                        {
                            dArray.AddLast(rnd.Next(0, 2));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;
                case 4:
                    Sorts<DynamicArray<int>, int>.TimSort(ref dArray);
                    break;
                case 5:
                    Sorts<DynamicArray<int>, int>.ShellSort(ref dArray);
                    break;
                case 6:
                    DynamicArray<int> tempArray = dArray;
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Sorts<DynamicArray<int>, int>.TimSort(ref tempArray);
                    stopwatch.Stop();
                    Console.WriteLine($"Timsort used {stopwatch.ElapsedTicks} ticks");
                    tempArray = dArray;
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    Sorts<DynamicArray<int>, int>.ShellSort(ref tempArray);
                    sw.Stop();
                    Console.WriteLine($"Shell sort used {sw.ElapsedTicks} ticks");
                    break;

                case 9:
                    Exit = true;
                    break;

                default:
                    break;
            }
        }
    }
}
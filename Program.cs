using System;
using Alg_Str_1;
public class Prog
{
    private static int MainMenu()
    {
        bool Success = false;
        int rezult = -1;
        while (!Success)
        {
            Console.WriteLine("Select menu item:");
            Console.WriteLine("1 - input linked list");
            Console.WriteLine("2 - input dynamic array");
            Console.WriteLine("3 - input stack based on linked list");
            Console.WriteLine("4 - input stack based on dynamic array");
            Console.WriteLine("5 - Sort station");
            Console.WriteLine("9 - Exit");
            Console.Write("Your choice:");
            string? inp = Console.ReadLine();
            Console.Clear();

            if (int.TryParse(inp, out rezult))
            {
                if (rezult == 9 || (rezult > 0 && rezult < 6))
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

    private static void LinkedListMenu()
    {
        MList<double> list = new MList<double>();
        bool Success = false;
        while (!Success)
        {
            Console.WriteLine("List values:");
            Console.WriteLine(list.ToString() + "\n");
            Console.WriteLine("Select action:");
            Console.WriteLine("1 - Add object");
            Console.WriteLine("2 - Remove object");
            Console.WriteLine("3 - Clear list");
            Console.WriteLine("9 - Exit");

            Console.Write("Your choice:");
            string? inp = Console.ReadLine();
            Console.Clear();

            switch (inp)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Your number(use \",\" for non-integer):");
                    double rezultinp;
                    string? inpNum = Console.ReadLine();
                    Console.Clear();
                    if (double.TryParse(inpNum, out rezultinp))
                    {
                        list.Add(rezultinp);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("Index:");
                    int rezultDel;
                    string? inpNumDel = Console.ReadLine();
                    Console.Clear();
                    if (int.TryParse(inpNumDel, out rezultDel))
                    {
                        list.RemoveAt(rezultDel);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;

                case "3":
                    Console.Clear();
                    list.Clear();
                    break;

                case "9":
                    Success = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }
    }

    private static void DynamicArrayMenu()
    {
        DynamicArray<double> array = new DynamicArray<double>();
        bool Success = false;
        while (!Success)
        {
            Console.WriteLine("Array values:");
            Console.WriteLine(array.ToString() + "\n");
            Console.WriteLine("Select action:");
            Console.WriteLine("1 - Add object");
            Console.WriteLine("2 - Remove object");
            Console.WriteLine("3 - Clear array");
            Console.WriteLine("9 - Exit");

            Console.Write("Your choice:");
            string? inp = Console.ReadLine();
            Console.Clear();

            switch (inp)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Your number(use \",\" for non-integer):");
                    double rezultinp;
                    string? inpNum = Console.ReadLine();
                    Console.Clear();
                    if (double.TryParse(inpNum, out rezultinp))
                    {
                        array.AddLast(rezultinp);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("Index:");
                    int rezultDel;
                    string? inpNumDel = Console.ReadLine();
                    Console.Clear();
                    if (int.TryParse(inpNumDel, out rezultDel))
                    {
                        array.RemoveAt(rezultDel);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;

                case "3":
                    Console.Clear();
                    array.Clear();
                    break;

                case "9":
                    Success = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }
    }

    private static void StackListMenu()
    {
        StackList<double> stack = new StackList<double>();
        bool Success = false;
        while (!Success)
        {
            Console.WriteLine("Stack values:");
            Console.WriteLine(stack.ToString() + "\n");
            Console.WriteLine("Select action:");
            Console.WriteLine("1 - Push");
            Console.WriteLine("2 - Pop");
            Console.WriteLine("3 - Clear stack");
            Console.WriteLine("9 - Exit");

            Console.Write("Your choice:");
            string? inp = Console.ReadLine();
            Console.Clear();

            switch (inp)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Your number(use \",\" for non-integer):");
                    double rezultinp;
                    string? inpNum = Console.ReadLine();
                    Console.Clear();
                    if (double.TryParse(inpNum, out rezultinp))
                    {
                        stack.Push(rezultinp);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Next value:" + stack.Pop());
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case "3":
                    Console.Clear();
                    stack.Clear();
                    break;

                case "9":
                    Success = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }
    }

    private static void StackDArrayMenu()
    {
        StackDArray<double> stack = new StackDArray<double>();
        bool Exit = false;
        while (!Exit)
        {
            Console.WriteLine("Stack values:");
            Console.WriteLine(stack.ToString() + "\n");
            Console.WriteLine("Select action:");
            Console.WriteLine("1 - Push");
            Console.WriteLine("2 - Pop");
            Console.WriteLine("3 - Clear stack");
            Console.WriteLine("9 - Exit");

            Console.Write("Your choice:");
            string? inp = Console.ReadLine();
            Console.Clear();

            switch (inp)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Your number(use \",\" for non-integer):");
                    double rezultinp;
                    string? inpNum = Console.ReadLine();
                    Console.Clear();
                    if (double.TryParse(inpNum, out rezultinp))
                    {
                        stack.Push(rezultinp);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Next value:" + stack.Pop());
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case "3":
                    Console.Clear();
                    stack.Clear();
                    break;

                case "9":
                    Exit = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }
    }

    private static void SortStationMenu()
    {
        bool Exit = false;
        MList<string> rezult = new MList<string>();
        string? stringInput = "";
        while (!Exit)
        {
            Console.WriteLine("Your input:");
            Console.WriteLine(stringInput + "\n");

            Console.WriteLine("Output algorithm:");
            Console.WriteLine(rezult.ToString() + "\n");

            Console.WriteLine("Select action:");
            Console.WriteLine("1 - Enter string");
            Console.WriteLine("9 - Exit");

            Console.Write("Your choice:");
            string? inp = Console.ReadLine();
            Console.Clear();

            switch (inp)
            {
                case "1":
                    Console.Clear();
                    rezult.Clear();
                    stringInput = Console.ReadLine();
                    string[] tokens = stringInput.Split(" ");
                    bool Success = SortStation(tokens, out rezult);

                    
                    break;

                case "9":
                    Exit = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }
    }

    private static bool SortStation(string[] tokens, out MList<string> rezult)
    {
        const string ArithmeticSym = "^ * / + -";
        const string FirstPrioritySym = "^";
        const string SecondPrioritySym = "* /";
        const string ThirdPrioritySym = "+ -";
        const string FuncSym = "sin cos";

        bool Error = false;
        rezult = new MList<string>();
        StackDArray<string> stack = new StackDArray<string>();

        for (int i = 0; i < tokens.Length; i++)
        {
            if (!AllowSym(tokens[i]))
            {
                Console.Clear();
                Error = true;
                Console.WriteLine("Detected unauthorized symbol!");
                break;
            }

            if (int.TryParse(tokens[i], out int temp))
            {
                rezult.Add(temp.ToString());
            }
            else if (tokens[i] == "sin" || tokens[i] == "cos")
            {
                stack.Push(tokens[i]);
            }
            else if (ArithmeticSym.Contains(tokens[i]))
            {
                if (FirstPrioritySym.Contains(tokens[i]))
                {
                    stack.Push(tokens[i]);
                }
                else if (SecondPrioritySym.Contains(tokens[i]))
                {
                    while (stack.Count > 0 && (FirstPrioritySym.Contains(stack[0])))
                        rezult.Add(stack.Pop()!);
                    stack.Push(tokens[i]);
                }
                else if (ThirdPrioritySym.Contains(tokens[i]))
                {
                    while (stack.Count > 0 && (SecondPrioritySym.Contains(stack[0]) || FirstPrioritySym.Contains(stack[0])))
                        rezult.Add(stack.Pop()!);
                    stack.Push(tokens[i]);
                }
            }
            else if (tokens[i] == "(")
            {
                stack.Push(tokens[i]);
            }
            else if (tokens[i] == ")")
            {
                while (stack[0] != "(")
                {
                    if (stack.Count == 0)
                    {
                        Console.Clear();
                        Error = true;
                        Console.WriteLine("Missing opening parenthesis(скобка)!");
                        break;
                    }
                    rezult.Add(stack.Pop()!);
                }
                stack.Pop();
                while (stack.Count > 0 && (FuncSym.Contains(stack[0])))
                {
                    rezult.Add(stack.Pop()!);
                }
            }
        }
        while (!Error && stack.Count > 0)
        {
            if (stack[0] == "(")
            {
                Console.Clear();
                Error = true;
                Console.WriteLine("Missing closing parenthesis(скобка)!");
                break;
            }
            rezult.Add(stack.Pop());
        }
        if(!Error) Console.Clear();
        
        return !Error;
    }

    private static bool AllowSym(string str)
    {
        const string AllowedSymbols = ". , + - * / sin cos ( ) ^";
        if (AllowedSymbols.Contains(str))
            return true;
        else if(int.TryParse(str, out int temp))
        {
            return true;
        }
        else
            return false;
    }

    static void Main()
    {
        bool Exit = false;
        while (!Exit)
        {
            int choice = MainMenu();

            switch (choice)
            {
                case 1:
                    LinkedListMenu();
                    break;
                case 2:
                    DynamicArrayMenu();
                    break;
                case 3:
                    StackListMenu();
                    break;
                case 4:
                    StackDArrayMenu();
                    break;
                case 5:
                    SortStationMenu();
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
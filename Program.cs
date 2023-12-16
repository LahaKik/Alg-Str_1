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
            Console.WriteLine("1 - Read bin tree from a file");
            Console.WriteLine("2 - Create a red-black tree");
            Console.WriteLine("3 - Add number to red-black tree");
            Console.WriteLine("4 - Delete number from red-black tree");
            Console.WriteLine("5 - Save tree to the file");
            Console.WriteLine("9 - Exit");
            Console.WriteLine("Your choice:");

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

    static void Main()
    {
        //BR_tree<int> tree = new BR_tree<int>();

        ////for (int i = 0; i < 15; i++) tree.Add(i);

        //GraphX<int> graphX = new GraphX<int>(ref tree);
        //new Thread(() => graphX.Start()).Start();
        //Random rand = new Random();
        //for (int i = 0; i < 20; i++)
        //{
        //    tree.Add(rand.Next(100));
        //}
        //Console.WriteLine(tree.ToString());

        BinTree? binTree = null;
        RB_tree<int>? rb_tree = null;
        GraphX<int>? graphX = null;
        Console.SetWindowSize(80, 20);
        Console.SetBufferSize(80, 20);

        bool Exit = false;
        while (!Exit)
        {
            int choice = MainMenu();

            switch (choice)
            {
                case 1:
                    try
                    {
                        binTree = BinTree.Create;
                        Console.WriteLine("Binary thee create successfully");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 2:
                    rb_tree = RB_tree<int>.CreateFromBinTree(ref binTree);
                    if (rb_tree != null)
                    {
                        if (graphX != null)
                            break;
                        graphX = new GraphX<int>(ref rb_tree);
                        new Thread(() => graphX.Start()).Start();
                        Console.WriteLine("Red-black thee create successfully");
                    }
                    break;
                case 3:
                    if(rb_tree == null)
                    {
                        rb_tree = new RB_tree<int>();
                        if (graphX == null)
                        {
                            graphX = new GraphX<int>(ref rb_tree);
                            new Thread(() => graphX.Start()).Start();
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Enter adding value: ");
                    int addNum;
                    string? inputNum = Console.ReadLine();
                    Console.Clear();

                    if (int.TryParse(inputNum, out addNum))
                    {
                        rb_tree.Add(addNum);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;
                case 4:
                    if(rb_tree == null)
                    {
                        Console.WriteLine("Red-black tree is empty");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Enter adding value: ");
                    int deleteNum;
                    string? inpNum = Console.ReadLine();
                    Console.Clear();

                    if (int.TryParse(inpNum, out deleteNum))
                    {
                        rb_tree.Delete(deleteNum);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                    }
                    break;
                case 5:
                    if( rb_tree == null)
                    {
                        Console.WriteLine("Red-black tree is empty");
                        break;
                    }
                    rb_tree.SaveToFile();
                    Console.WriteLine("Saved");
                    break;


                case 9:
                    if (graphX != null)
                        graphX.Close();
                    Exit = true;
                    break;

                default:
                    break;
            }
        }



        //BinTree? binTree;
        //try
        //{
        //    binTree = BinTree.Create;
        //    RB_tree<int> tree = new RB_tree<int>(ref binTree);
        //    GraphX<int> graphX = new GraphX<int>(ref tree);
        //    new Thread(() => graphX.Start()).Start();
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e.Message);
        //}
    }
}
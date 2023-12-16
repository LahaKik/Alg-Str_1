using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Alg_Str_1
{
    internal class GraphX<T> where T : IComparable
    {
        private RB_tree<T> tree;
        Node[]? nodes;
        private RenderWindow? window = null;
        private uint scrX = 1920;
        private uint scrY = 1080;


        public GraphX(ref RB_tree<T> _tree)
        {
            tree = _tree;
        }

        private Node[] GetNodes(ref RenderWindow window)
        {
            if(tree == null || tree.CountNodes == 0)
            {
                return null;
            }
            nodes = new Node[tree.CountNodes];
            string[] TreeString = tree.ToGraphicProcess();
            int x0 = 800, y0 = 10;
            int i = 0;
            int shift = (int)MathF.Log2(tree.CountNodes);
            shift = (shift > 1) ? shift - 1 : 1;
            foreach (var str in TreeString)
            {
                int cntLeft = 0;
                int cntRight = 0;
                char lastOp = ' ';
                Vector2f tPos = new Vector2f(x0, y0);
                float offset = shift;
                foreach (var sym in str)
                {
                    if (sym == 'R' || sym == 'L')
                    {
                        if (sym == 'R')
                        {
                            if (cntLeft == 0 && cntRight == 0)
                                tPos.X += 60 * shift;
                            else
                                tPos.X += 30 * offset;
                            cntRight++;
                            tPos.Y += 60;
                        }
                        else if (sym == 'L')
                        {
                            if (cntLeft == 0 && cntRight == 0)
                                tPos.X -= 60 * shift;
                            else
                                tPos.X -= 30 * offset;
                            cntLeft++;
                            tPos.Y += 60;
                        }
                        lastOp = sym;
                    }
                    else
                        break;
                    offset *= 0.75f;
                   
                }
                Color color = (str[str.Length - 1] == 'R') ? Color.Red : Color.Black;
                string data = str.Substring(cntRight + cntLeft, str.Length - cntLeft - cntRight - 1);
                Vector2f PosParent;
                if(lastOp != ' ')
                {
                    if (cntRight + cntLeft == 1)
                        PosParent = new Vector2f((lastOp == 'R') ? tPos.X - 60 * shift : tPos.X + 60 * shift, tPos.Y - 60);
                    else
                        PosParent = new Vector2f((lastOp == 'R') ? tPos.X - 30 * offset * 4/3f : tPos.X + 30 * offset * 4/3f, tPos.Y - 60);
                    nodes[i] = new Node(tPos, data, color, ref window, PosParent);
                }
                else
                    nodes[i] = new Node(tPos, data, color, ref window);
                i++;
            }

            return nodes;
        }

        public void Start()
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            window = new RenderWindow(new SFML.Window.VideoMode(scrX, scrY), "Красно-черое дерево", Styles.Fullscreen, settings);
            nodes = GetNodes(ref window);


            window.Closed += (sender, e) => { window.Close(); };

            int FrameCount = 0;
            while(window.IsOpen)
            {
                if(FrameCount % 30 == 0)
                {
                    nodes = GetNodes(ref window);
                    FrameCount = 0;
                }
                window.DispatchEvents();
                window.Clear(Color.White);
                if(nodes != null)
                {
                    foreach (Node node in nodes)
                    {
                        node.drow();
                    }
                }
                window.Display();
                FrameCount++;
            }
        }

        public void Close()
        {
            if(window != null)
                window.Close();
        }
    }

                                                                                    
    class Node                                                                      
    {
        public Vector2f Position;
        private RenderWindow window;
        private CircleShape shape;
        private Text text;
        private Font font;
        //VertexArray line;
        ConvexShape line;
        Vector2f? PosParent;
        public Node(Vector2f Pos, string txt, Color color, ref RenderWindow _window, Vector2f? PositionParent = null)
        {
            window = _window;
            Position = Pos;

            shape = new CircleShape(20f);
            shape.Position = new SFML.System.Vector2f(Position.X, Position.Y);
            shape.FillColor = color;

            FileStream FStream = new FileStream(@"C:\Users\hotki\source\repos\Alg&Str_1\Resources\ARIAL.TTF", FileMode.Open, FileAccess.Read);
            font = new Font(FStream);
            text = new Text(txt, font);
            text.Scale = new SFML.System.Vector2f(0.5f, 0.5f);
            text.Position = new SFML.System.Vector2f(Position.X + 19 - txt.Length * 4, Position.Y + 10);

            line = new ConvexShape(4);
            line.FillColor = Color.Black;
            line.SetPoint(0, Position + new Vector2f(19.5f, 0));
            line.SetPoint(1, Position + new Vector2f(21.5f, 0));
            PosParent = PositionParent;
            if(PositionParent != null)
            {
                line.SetPoint(2, (Vector2f)PositionParent + new Vector2f(21.5f, 40.5f));
                line.SetPoint(3, (Vector2f)PositionParent + new Vector2f(19.5f, 40.5f));
            }
        }
        public void drow()
        {
            window.Draw(shape);
            window.Draw(text);
            if (PosParent != null)
                window.Draw(line);
        }

    }
}

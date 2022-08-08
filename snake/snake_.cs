using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    //unsere schlange
    public class snake_
    {




        public int height;
        public int width;


        public Directions richtung = Directions.right;



        bool turned = false;
        //automatische richtung

        public Dictionary<Directions, (int X, int Y)> RichtungToVector = new Dictionary<Directions, (int X, int Y)>()
        {
            { Directions.left, (-1, 0) },
            { Directions.up, (0, -1) },
            { Directions.down, (0, 1) },
            { Directions.right, (1, 0) },
        };
        public int xHead => Elemente[0].X;
        public int yHead => Elemente[0].Y;




        public List<(int X, int Y)> Elemente = new List<(int, int)>() { (5, 5) };
        private bool eating;


        public void turn(char ch)
        {

            Directions neue_Richtung = richtung;

            //Tasten drücken

            switch (ch)
            {

                case 'a':
                    neue_Richtung = Directions.left;
                    break;
                case 'w':
                    neue_Richtung = Directions.up;
                    break;
                case 's':
                    neue_Richtung = Directions.down;
                    break;
                case 'd':
                    neue_Richtung = Directions.right;
                    break;

            }
            Dictionary<Directions, Directions> opposite_direction = new()
            {
                { Directions.left, Directions.right },
                { Directions.down, Directions.up },
                { Directions.up, Directions.down },
                { Directions.right, Directions.left },
            };
            if (neue_Richtung != richtung && neue_Richtung != opposite_direction[richtung] && !turned)
            {
                richtung = neue_Richtung;

                turned = true;
            }
        }

        //bewegung der schlange
        public bool Move(List<Block> blocks)
        {
            int x = xHead + RichtungToVector[richtung].X;
            int y = yHead + RichtungToVector[richtung].Y;

            if (y < 0)
            {
                y = height;
            }

            if (y > height)
            {
                y = 0;
            }


            if (x < 0)
            {
                x = width;
            }

            if (x > width)
            {
                x = 0;
            }

            if (!eating)
            {
                clear();
            }

            foreach (var item in Elemente)
            {
                if (item.X == x && item.Y == y)
                {
                    playerlist();
                    lose();


                    return false;
                }
            }


            if (blocks.Any(block => block.Collide(x, y)))
            {
                playerlist();
                lose();
                return false;
                Console.WriteLine();
            }


            Elemente.Insert(0, (x, y));

            eating = false;
            turned = false;

            Draw();

            return true;
        }
        //ob User verloren hat

        private void playerlist()
        {


            Console.SetCursorPosition(0, height + 5);
            Console.Write(" Enter your name  :");

            Console.CursorVisible = true;
            Console.SetCursorPosition(0, height + 6);

            string playerIN = Console.ReadLine();

            //System.IO.File.WriteAllText(@"c:\Users\andrei.moraru\source\repos\snake\snake\Log\Userlist.txt", playerIN + ", Your score is " + (Elemente.Count - 1).ToString());
            using (StreamWriter writetext = new StreamWriter(@"c:\Users\andrei.moraru\source\repos\snake\snake\Log\Userlist.txt", true))
            {
                writetext.WriteLine(playerIN + ", Your score is " + (Elemente.Count).ToString());
            }
            Console.SetCursorPosition(0, height + 7);

            Console.Write(playerIN + ", Your score is " + (Elemente.Count).ToString());


            Console.CursorVisible = false;
        }
        private void lose()
        {


            Console.SetCursorPosition(0, height + 8);
            Console.Write(" Game over");



            Console.CursorVisible = false;
        }


        // wenn er dieser Apfel isst
        public void eat()
        {
            eating = true;
        }


        // symbol for snake
        private void Draw()
        {



            Console.SetCursorPosition(xHead + 1, yHead + 1);
            Console.Write("#");

            foreach (var item in Elemente.Skip(1))
            {
                Console.SetCursorPosition(item.X + 1, item.Y + 1);
                Console.Write("O");


            }
        }


        //I delete last element in snake
        private void clear()
        {
            var last = Elemente.Last();
            Console.SetCursorPosition(last.X + 1, last.Y + 1);
            Console.Write(" ");
            Elemente.RemoveAt(Elemente.Count - 1);

        }
    }
}

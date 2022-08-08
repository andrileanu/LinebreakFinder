using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    //Apfel auf den karte 
    public class FoodFactory_
    {
        public int x;
        public int y;

        public int height;
        public int width;

        Random random = new Random();


        public void CreateFood(int height, int width, snake_ snake, List<Block> blocks)
        {
            x = random.Next(width);
            y = random.Next(height);

            //Apfelspawns nich auf die Blöke
            while (snake.Elemente.Contains((x, y)) || blocks.Any(block => block.Collide(x, y)))
            {
                x = random.Next(width);
                y = random.Next(height);
            }

            Console.SetCursorPosition(x + 1, y + 1);

            //symbol für Apfel
            Console.Write('@');

        }
    }

}

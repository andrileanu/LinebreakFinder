using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    public class Block

    {
        public Block(int size, int X, int Y)
        {
            this.position = (X, Y);
            this.size = size;

        }


        (int X, int Y) position;
        int size;
        // ich malle die Schlage
        public void draw()
        {

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.SetCursorPosition(position.X + 1 + i, position.Y + 1 + j);
                    Console.Write("X");
                }
            }

        }


        //
        public bool Collide(int x, int y)
        {
            if (x >= position.X && y >= position.Y
                && x < position.X + size && y < position.Y + size)
            {
                return true;
            }
            return false;
        }
        


        //die Blöcke
        public static List<Block> GetBlocks(int height, int width, int maxBlocks, int maxSize)
        {
            List<Block> blocks = new List<Block>();

            Random random = new Random();
            int block_count = random.Next(maxBlocks/2, maxBlocks);
            
            for (int i = 0; i < block_count; i++)
            {
                int x = random.Next(width);
                int y = random.Next(height);
               
                while (blocks.Any(block => block.position == (x, y)))
                {
                    x = random.Next(width);
                    y = random.Next(height);
                }
                int size = random.Next( new int[] { maxSize, height-y, width-x }.Min());
                blocks.Add(new Block( size, x ,y ));
            }
            return blocks;

        }
    }
}

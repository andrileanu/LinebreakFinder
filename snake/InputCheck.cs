using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    public static class InputCheck  //ob man eine falsche Taste gedrückt hat
    {

        public static bool IsInt(string input)
        {

            return int.TryParse(input, out int result);

        }

        public static bool CheckHight(int hight)
        {
            if (hight >= 3 && hight <= 50)
            {
                return true;
            }
            return false;
        }

        public static bool CheckWidth(int width)
        {
            if (width >= 3 && width <= 100)
            {
                return true;
            }
            return false;
        }
        public static bool CheckBombs(int width)
        {
            if (width >= 3 && width <= 100)
            {
                return true;
            }
            return false;
        }


    }
}

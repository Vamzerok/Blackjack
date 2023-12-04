using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Screen.DrawPoint(10, 10);
            Screen.DrawText(20, 20,"I like\nmilk :D",ConsoleColor.Green,ConsoleColor.Black);
            Console.ReadKey();
        }
    }
}

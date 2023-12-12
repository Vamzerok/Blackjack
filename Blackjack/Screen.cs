using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class Screen
    {
        public static int width;
        public static int height;

        public static ConsoleColor backgroundColor = ConsoleColor.Black; //no use yet
        public static ConsoleColor foregroundColor = ConsoleColor.White; //no use yet

        /// <summary>
        /// A képernyő tetszóleges pontját, a megadott paraméterekre módosítja.
        /// </summary>
        /// <param name="x">A pont képernyő bal oldalától mért távolság.</param>
        /// <param name="y">A pont képernyő tetjétől oldalától mért távolság</param>
        /// <param name="c">Beállítandó karakter (pl.: 'a')</param>
        /// <param name="bgColor">Opcionális paraméter. Karakter háttérszínének beállítása. (pl.: ConsoleColor.White)</param>
        /// <param name="fgColor">Opcionális paraméter. Karakter színének beállítása. (pl.: ConsoleColor.White)</param>
        /// <param name="resetCursor">Opcionális paraméter. true > visszaállítja a cursort a (0,0) koordinátára</param>
        public static void DrawPoint(int x, int y, char c = ' ', ConsoleColor bgColor = ConsoleColor.White, ConsoleColor fgColor = ConsoleColor.Black, bool resetCursor = true)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(c);
            if(resetCursor) Console.SetCursorPosition(0, 0);
        }

        public static void DrawText(int x, int y, string text, ConsoleColor bgColor = ConsoleColor.White, ConsoleColor fgColor = ConsoleColor.Black)
        {
            int xOffset = 0;
            int yOffset = 0;
            for (int i = 0; i < text.Length; i++)
            {
                /*
                 Only accounting for:
                    - linebreak
                    - tabulator
                 */
                if (text[i] == '\n')
                {
                    yOffset++;
                    xOffset = 0;
                    continue;  //so \n doesn't get printed
                }

                if (text[i] == '\t')
                {
                    xOffset += 4;
                    continue;  //so \t doesn't get printed
                }

                //printing the upcoming character with the Offsets applied
                DrawPoint(x + xOffset, y + yOffset, text[i],bgColor,fgColor);
                xOffset++;
            }
        }

        static int CalculateXOffset(int width, int spaceing, int originX)
        {

        }

        public static void DrawHand(Player p)
        {
            int xOffset;
            int yOffset = 20;
            int spaceing = 0;

            xOffset = -(CARD_WIDTH*p.hand.Count() \ 2);
            foreach(Card card in p.hand)
            {
                card.x = xOffset + spaceing + p;
                DrawCard(card);
            }
        }

        public static void Update(int waitTimeInMillisec = 0)
        {
            Console.Clear();
            //Dealer
            Console.Write("Dealer" + $"({Program.Dealer.Points})");
            Console.Write(((Program.Dealer.Bust) ? " < Bust" : ""));
            Console.Write("\n");
            foreach (Card c in Program.Dealer.hand)
            {
                Console.Write(c.Show() + " ");
            }
            Console.Write("\n");
            
            //Players
            Console.WriteLine("------------------");
            foreach(Player p in Program.Players)
            {
                Console.Write(p.Name + $"({p.Points})" + " | ");
                Console.Write(p.BetAmount + " | ");
                Console.Write(p.Balance + " | ");
                Console.Write(((p.Bust) ? " < Bust" : ""));
                Console.Write("\n");
                foreach (Card c in p.hand)
                {
                    Console.Write(c.Show() + " ");
                }
                Console.Write("\n");
                Console.WriteLine("-----");
            }
            Thread.Sleep(waitTimeInMillisec);
        }
    }
}

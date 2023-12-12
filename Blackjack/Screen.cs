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

        public static int CARD_WIDTH;
        public static int CARD_HEIGHT;

        public const int CARD_Y_OFFSET = 2;

        public const ConsoleColor CARD_BACKGROUND = ConsoleColor.Black;
        public const ConsoleColor CARD_FOREGROUND = ConsoleColor.White;
        public const ConsoleColor CARD_EDGECOLOR= ConsoleColor.Gray;

        public static ConsoleColor backgroundColor = ConsoleColor.Black; //no use yet
        public static ConsoleColor foregroundColor = ConsoleColor.White; //no use yet

        public const string CARD_DESIGN =
            "+---------+" +
            "| 0       |" +
            "| 1       |" +
            "|         |" +
            "|    0    |" +
            "|         |" +
            "|       0 |" +
            "|       1 |" +
            "+---------+";

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
            try
            {
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write(c);
                if (resetCursor) Console.SetCursorPosition(0, 0);
            }
            catch
            {
                
            }
            
            Console.ResetColor();
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

        public static void DrawCard(Card c)
        {
            for (int i = 0; i < CARD_HEIGHT; i++)
            {
                for (int j = 0; j < CARD_WIDTH; j++)
                {
                    if (c.IsFaceDown)
                    {
                        if((0 < i && i < CARD_HEIGHT-1) && (0 < j && j < CARD_WIDTH-1))
                        {
                            DrawPoint(c.x + j, c.y + i, '#', bgColor: CARD_BACKGROUND, fgColor: CARD_FOREGROUND);
                            continue;
                        }
                    }

                    char selectedCharacter = CARD_DESIGN[i*CARD_WIDTH + j];
                    switch (selectedCharacter)
                    {
                        case '0':
                            DrawPoint(c.x + j, c.y + i, c.Face[0], bgColor: CARD_BACKGROUND, fgColor: CARD_FOREGROUND);
                            break;
                        case '1':
                            DrawPoint(c.x + j, c.y + i, c.Face[1], bgColor: CARD_BACKGROUND, fgColor: CARD_FOREGROUND);
                            break;
                        default:
                            DrawPoint(c.x + j, c.y + i, selectedCharacter, bgColor: CARD_BACKGROUND, fgColor: CARD_FOREGROUND);
                            break;
                    }
                }
            }
        }

        public static List<int> CreateSpaceing(int width, int spaceing, int quantity, int xOrigin)
        {
            List<int> coords = new List<int>();
            
            int totalWidth = (width * quantity) + (spaceing * (quantity-1));

            int absoluteLeftMostPoint = xOrigin - totalWidth / 2;

            for (int i = 0; i < quantity; i++)
            {
                coords.Add(absoluteLeftMostPoint + (width * i + spaceing * i));
            }

            return coords;
        }

        public static void DrawHand(Player p, int xOffset = 0, int yOffset = CARD_Y_OFFSET, int spaceing = -7)
        {
            List<int> coords = CreateSpaceing(CARD_WIDTH, spaceing, p.hand.Count(), p.x);
            for(int i = 0; i < p.hand.Count(); i++)
            {
                Card card = p.hand[i];
                card.x = xOffset + coords[i] ;
                card.y = p.y + CARD_Y_OFFSET;
                DrawCard(card);
            }
        }

        public static void DrawPlayerText(Player p, string text, int yOffset)
        {
            int totalWidth = text.Length / 2;
            DrawText(p.x - totalWidth, p.y + yOffset, text);
        }

        public static void Initialize()
        {
            CARD_WIDTH = 11;
            CARD_HEIGHT = 9;
        }

        public static void Update(int waitTimeInMillisec = 0)
        {
            Console.Clear();
            //Dealer
            DrawHand(Program.Dealer, yOffset: 1);
            DrawPlayerText(Program.Dealer, $"Dealer", 0);

            //Players
            foreach (Player p in Program.Players)
            {
                DrawHand(p);
                DrawPlayerText(p, $"{p.Name}", 0);
                DrawPlayerText(p, $"Pénz: {p.Balance} | Tét: {p.BetAmount}", CARD_Y_OFFSET + CARD_HEIGHT + 1);
            } 
            Thread.Sleep(waitTimeInMillisec);
        }
    }
}

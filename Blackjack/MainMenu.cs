using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class MainMenu
    {

        public static string Logo =
@"   
 ________   ___        ________   ________   ___  __           ___   ________   ________   ___  __       
|\   __  \ |\  \      |\   __  \ |\   ____\ |\  \|\  \        |\  \ |\   __  \ |\   ____\ |\  \|\  \     
\ \  \|\ /_\ \  \     \ \  \|\  \\ \  \___| \ \  \/  /|_      \ \  \\ \  \|\  \\ \  \___| \ \  \/  /|_   
 \ \   __  \\ \  \     \ \   __  \\ \  \     \ \   ___  \   __ \ \  \\ \   __  \\ \  \     \ \   ___  \  
  \ \  \|\  \\ \  \____ \ \  \ \  \\ \  \____ \ \  \\ \  \ |\  \\_\  \\ \  \ \  \\ \  \____ \ \  \\ \  \ 
   \ \_______\\ \_______\\ \__\ \__\\ \_______\\ \__\\ \__\\ \________\\ \__\ \__\\ \_______\\ \__\\ \__\
    \|_______| \|_______| \|__|\|__| \|_______| \|__| \|__| \|________| \|__|\|__| \|_______| \|__| \|__|
";
        public static List<Player> player()
        {
            string input;
            int count = 0;
            string prompt = $"Adja meg a(z){count}.játékos nevét:";
            int textOffset = 3;

            List<Player> players = new List<Player>();

            int logoHeight = Logo.Split('\n').Length - 2;
            int logoWidth = Logo.Split('\n')[1].Length - 1;

            Screen.DrawText(
                (Console.WindowWidth / 2) - (logoWidth / 2), 
                0,
                Logo,ConsoleColor.Black, 
                ConsoleColor.White
                );

            //players.Add(new Player("Jocó", 100, y: Program.PLAYER_Y_POS));
            //players.Add(new Player("Gábor", 100, y: Program.PLAYER_Y_POS));
            //players.Add(new Player("Andras", 100, y: Program.PLAYER_Y_POS));

            while (count < 3)
            {
                count++;
                Screen.DrawText((Console.WindowWidth/2) - (prompt.Length/2), (logoHeight + textOffset) + textOffset * count, prompt,ConsoleColor.Black,ConsoleColor.White);
                input = Console.ReadLine();
                if( input == "" )
                {
                    return players;
                }
                players.Add(new Player(input, y: Program.PLAYER_Y_POS));
            }  
            return players;
        }
    }
}

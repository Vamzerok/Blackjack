using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class MainMenu
    {
        public static List<Player> asdf()
        {
            string input;
            int count = 0;
            List<Player> players = new List<Player>();

            while(count < 5)
            {
                count++;
                Console.WriteLine($"Name of Player{count}: ");
                input = Console.ReadLine();
                if( input == "" )
                {
                    return players;
                }
                players.Add(new Player(input));
            }  
            return players;
        }
    }
}

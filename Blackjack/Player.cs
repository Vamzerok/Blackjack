using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        public int points;
        public string name;
        public Player(string name, int StartingPoints)
        {
            this.points = StartingPoints;
            this.name = name;
        }

    }
}

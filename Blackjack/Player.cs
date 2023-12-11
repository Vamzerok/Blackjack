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

        private List<Card> hand;

        public Player(string name)
        {
            this.name = name;
        }

        public void Draw(Card card)
        {
            hand.Add(card);
        }
    }
}

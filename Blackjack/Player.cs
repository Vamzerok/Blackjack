using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        public string Name;
        public int Points = 0;

        public double Balance = 0;
        public double BetAmount = 0;

        public bool Bust = false;
        public bool Won = false;

        public int x;
        public int y;

        internal static Random rnd = new Random();

        public List<Card> hand = new List<Card>();

        public Player(string name, int balance = 100, int x = 0, int y = 0)
        {
            this.Name = name;
            this.Balance = balance;
            this.x = x;
            this.y = y;
        }

        public int CalculatePoints()
        {
            //magic numbers for life 
            int sum = 0;
            Card AceInHand = null;

            foreach (Card card in hand)
            {
                if(card.Value == 11)
                {
                    //ace that still has a value of 11
                    AceInHand = card;
                }
                if (!card.IsFaceDown) 
                { 
                    sum += card.Value;
                }
            }
            //automatically saves the player if they're about to bust
            if(sum > Program.MAXIMUM_POINTS && AceInHand != null)
            {
                sum -= 10; //-11 + 1
                AceInHand.Value = 1;
            }
            return sum;
        }

        public void DrawRandomCard(bool isFaceDown = false)
        {
            int chosenCardIndex = rnd.Next(Program.Deck.Count());
            Card chosecard = Program.Deck[chosenCardIndex];

            chosecard.IsFaceDown = isFaceDown;

            this.hand.Add(chosecard);
            this.Points = CalculatePoints();

            Program.Deck.RemoveAt(chosenCardIndex);
        }
    }
}

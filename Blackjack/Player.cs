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

        public int Balance = 0;
        public int BetAmount = 0;

        public bool Bust = false;
        public bool Won = false;

        internal static Random rnd = new Random();

        public List<Card> hand = new List<Card>();

        public Player(string name, int balance = 100)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public void DrawRandomCard(bool isFaceDown = false)
        {
            int chosenCardIndex = rnd.Next(Program.Deck.Count());
            Card chosecard = Program.Deck[chosenCardIndex];

            chosecard.IsFaceDown = isFaceDown;

            this.hand.Add(chosecard);
            this.Points += chosecard.Value;

            Program.Deck.RemoveAt(chosenCardIndex);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public int value;
        public string face;
        public Card(int value, string face) 
        {
            this.value = value;
            this.face = face;
        }

        public static List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();

            char[] suits = {
            '\u2660', //Spade
            '\u2665', //Heart
            '\u2666', //Diamond
            '\u2663'  //Club
            };

            char[] faceCards = { 'J', 'Q', 'K' };

            foreach (char suit in suits)
            {
                //suit cards
                for (int i = 2; i <= 10; i++)
                {
                    string face = suit.ToString() + i.ToString();
                    deck.Add(new Card(i, face));
                }
                //face cards
                foreach (char f in faceCards)
                {
                    string face = suit.ToString() + f.ToString();
                    deck.Add(new Card(10, face));
                }
                //ase
                deck.Add(
                    new Card(10, suit.ToString() + "A")
                );
            }
            return deck;
        }
    }
}

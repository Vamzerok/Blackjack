using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public int Value;
        public string Face;
        public bool IsFaceDown;

        public int x;
        public int y;

        public Card(int value, string face, bool isFaceDown = false, int x = 0, int y = 0) 
        {
            this.Value = value;
            this.Face = face;
            this.IsFaceDown = isFaceDown;
            this.x = x;
            this.y = y;
        }

        public string Show()
        {
            if(IsFaceDown)
            {
                return "##";
            }
            return this.Face;
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
                //ace
                deck.Add(
                    new Card(11, suit.ToString() + "A")
                );
            }
            return deck;
        }
    }
}

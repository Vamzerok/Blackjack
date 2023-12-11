using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        public static List<Card> Deck;
        public static List<Player> Players = new List<Player>();

        static void Start()
        {
            //playerek beállítása
            Players = MainMenu.asdf();

            //kártyák létrehozása
            Deck = Card.GenerateDeck();
        }

        static void CardDealingPhase()
        {

        }

        static void PlayersPhase()
        {

        }

        static void DealerPhase()
        {

        }

        static void GameOverPhase()
        {

        }

        static void Main(string[] args)
        {
            Start();

            CardDealingPhase();

            PlayersPhase();

            DealerPhase();

            GameOverPhase();
            
            Console.ReadKey();
        }
    }
}

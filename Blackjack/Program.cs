using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Blackjack
{
    class Program
    {
        public static List<Card> Deck;
        public static List<Player> Players = new List<Player>();
        public static Player Dealer = new Player("dealer");

        public static int MAXIMUM_POINTS = 21;
        public static int DEALERS_REQUIRED_POINTS = 16;

        static string SafeInput(List<string> validUserInputs)
        {
            string input;
            while (true)
            {
                Console.Write("> ");
                input = Console.ReadLine();
                foreach(string c in validUserInputs)
                {
                    if (input == c)
                    {
                        Console.Write("\n"); // mindig jó ha új sort kezdünk ;)
                        return c;
                    }
                }
                Console.WriteLine("Invalid input");
            }
        }

        static void Start()
        {
            //playerek beállítása
            //Players = MainMenu.asdf();
            Players.Add(new Player("Jocó",   100));
            Players.Add(new Player("Gábor",  100));
            Players.Add(new Player("Andras", 100));

            //kártyák létrehozása
            Deck = Card.GenerateDeck();
        }

        static void BettingPhase()
        {
            foreach(Player p in Players) 
            {
                Console.WriteLine($"{p.Name} --> ")
            }
        }

        static void CardDealingPhase()
        {
            foreach (Player p in Players)
            {
                p.DrawRandomCard();

                Screen.Update();
            }  
            Dealer.DrawRandomCard();

            Screen.Update();

            foreach (Player p in Players)
            {
                p.DrawRandomCard();

                Screen.Update();
            }
            Dealer.DrawRandomCard(true);

            Screen.Update();

        }

        static void PlayersPhase()
        {
            List<string> validInputs = new List<string>() { "h", "s" };

            //interate through players
            foreach(Player p in Players)
            {
                Console.WriteLine($"{p.Name}({p.Points}) --> hit(h) or stay(s)");

                while(SafeInput(validInputs) == "h")
                {
                    p.DrawRandomCard();

                    Screen.Update(1000);

                    //bust
                    if (p.Points > MAXIMUM_POINTS)
                    {
                        p.Bust = true;
                        Console.WriteLine($"{p.Name} --> Bust");
                        Thread.Sleep(1000);
                        break;
                    }

                    //21
                    if (p.Points == MAXIMUM_POINTS)
                    {
                        p.Won = true;
                        Console.WriteLine($"{p.Name} --> GG's");
                        Thread.Sleep(1000);
                        break;
                    }

                    Console.WriteLine($"{p.Name}({p.Points}) --> hit(h) or stay(s)");
                }
                Screen.Update(1000);
            }
        }

        static void DealerPhase()
        {
            Dealer.hand[1].IsFaceDown = false;
            Screen.Update(1000);

            while(Dealer.Points <= DEALERS_REQUIRED_POINTS)
            {
                Dealer.DrawRandomCard();
                Screen.Update(1000);
            }
        }

        static void GameOverPhase()
        {

        }

        static void Main(string[] args)
        {
            Start();

            BettingPhase();

            CardDealingPhase();

            PlayersPhase();

            DealerPhase();
            
            Console.ReadKey();
        }
    }
}

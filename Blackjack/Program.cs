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
        public static int MINIMUM_BET = 10;

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

        //overloading da function 
        static double SafeInputInt(double min = 0, double max = 10, bool isInt = false)
        {
            string input;
            while (true)
            {
                Console.Write("> ");
                input = Console.ReadLine();
                double numInput = double.Parse(input);

                //check #1 
                if (isInt && numInput % 1 != 0)
                {
                    Console.WriteLine("Input must be an integer.");
                    continue;
                }
                //check #2 
                if (numInput < min)
                {
                    Console.WriteLine($"Input must be greater than, or eaqual to {min}.");
                    continue;
                }
                //check #3 
                if (numInput > max)
                {
                    Console.WriteLine($"Input must be less than, or eaqual to {max}.");
                    continue;
                }
                return numInput;
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
                Console.WriteLine($"{p.Name} Bet amount (min: {MINIMUM_BET}): ");
                double bet = 15;//SafeInputInt(MINIMUM_BET, p.Balance, true);
                p.BetAmount = bet;
                p.Balance -= bet;
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

            Screen.Update(1000);

            //check for insta win
            foreach (Player p in Players)
            {
                if(p.Points == MAXIMUM_POINTS)
                {
                    p.Won = true;
                }
            }
        }

        static bool CorrectAce(Player p)
        {
            // don't look at it pls ＞﹏＜
            foreach (Card c in p.hand)
            {
                if (c.Face[1] == 'A')
                {
                    c.Value = 1;
                    if (p.CalculatePoints() > MAXIMUM_POINTS) 
                    { 
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static void PlayersPhase()
        {
            List<string> validInputs = new List<string>() { "h", "s" };

            //interate through players
            foreach(Player p in Players)
            {
                CorrectAce(p); // In case the player got 2 Aces
                if (p.Won || p.Bust) continue;

                Console.WriteLine($"{p.Name}({p.Points}) --> hit(h) or stay(s)");
                while(SafeInput(validInputs) == "h")
                {
                    p.DrawRandomCard();

                    Screen.Update(1000);

                    //bust
                    if (p.Points > MAXIMUM_POINTS && CorrectAce(p))
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

            //dealer busts
            if(Dealer.Points > MAXIMUM_POINTS)
            {
                GameOverPhase(true);
            }
            GameOverPhase(false);
        }

        static void GameOverPhase(bool dealerBust)
        {       
            foreach (Player p in Players)
            {
                //jump over players who are out
                if(p.Bust || p.Won) 
                {
                    continue;
                }

                //every player that is still in the round wins 2x their bet
                if (dealerBust || p.Points > Dealer.Points)
                {
                    p.Balance += p.BetAmount * 2;
                    Console.WriteLine($"{p.Name} wins {p.BetAmount * 2}");
                    continue;
                }

            }

            return;
            
        }

        static void CleanUp()
        {
            //reset bet amount
            foreach (Player p in Players)
            {
                p.BetAmount = 0;
            }

            //idk meg mi kell ide
        }

        static void Main(string[] args)
        {
            Start();

            BettingPhase();

            CardDealingPhase();

            PlayersPhase();

            DealerPhase();

            CleanUp();
            
            Console.ReadKey();
        }
    }
}

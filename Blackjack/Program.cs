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
        public static Player Dealer;

        public static int MAXIMUM_POINTS = 21;
        public static int DEALERS_REQUIRED_POINTS = 16;
        public static int MINIMUM_BET = 10;
        public static int WIN_AMOUNT_MULTIPLIER= 2;
        public static int PLAYER_Y_POS = 15;

        public const int MARGINS = 0;

        static void Start()
        {
            Screen.Initialize();
            //playerek beállítása
            //Players = MainMenu.player();

            Dealer = new Player("dealer", x: Console.WindowWidth / 2 -1, y: 2);

            Players.Add(new Player("Jocó",   100, y: PLAYER_Y_POS));
            Players.Add(new Player("Gábor",  100, y: PLAYER_Y_POS));
            Players.Add(new Player("Andras", 100, y: PLAYER_Y_POS));

            List<int> playerPos = Utils.CalculateIntervals(Console.WindowWidth - MARGINS, Players.Count() + 2);
            for(int i = 0; i < Players.Count; i++) 
            {
                Players[i].x = MARGINS / 2 + playerPos[i+1];
                Screen.DrawPoint(Players[i].x, Players[i].y, bgColor: ConsoleColor.Magenta);
            }

            //kártyák létrehozása
            Deck = Card.GenerateDeck();
        }

        static void BettingPhase()
        {
            foreach(Player p in Players) 
            {
                Console.WriteLine($"{p.Name} Bet amount (min: {MINIMUM_BET}): ");
                double bet = 15; //Utils.SafeInputInt(MINIMUM_BET, p.Balance, true);
                p.BetAmount = bet;
                p.Balance -= bet;
            }
        }

        static void CardDealingPhase()
        {
            int waitBetweenDeals = 1000;

            foreach (Player p in Players)
            {
                p.DrawRandomCard();

                Screen.Update(waitBetweenDeals);
            }  
            Dealer.DrawRandomCard();

            Screen.Update(waitBetweenDeals);

            foreach (Player p in Players)
            {
                p.DrawRandomCard();

                Screen.Update(waitBetweenDeals);
            }
            Dealer.DrawRandomCard(true);

            Screen.Update(waitBetweenDeals);

            //check for insta win
            foreach (Player p in Players)
            {
                if(p.Points == MAXIMUM_POINTS)
                {
                    p.Won = true;

                    //instantly give them the nyeremény
                    p.Balance += p.BetAmount * WIN_AMOUNT_MULTIPLIER;
                    p.BetAmount = 0;
                }
            }
        }

        static void PlayersPhase()
        {
            List<string> validInputs = new List<string>() { "h", "s" };

            //interate through players
            foreach(Player p in Players)
            {
                if (p.Won || p.Bust)
                {
                    Screen.Update(1000);
                    continue;
                }

                Console.WriteLine($"{p.Name}({p.Points}) --> hit(h) or stay(s)");
                while(Utils.SafeInput(validInputs) == "h")
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
           
            Dealer.Points = Dealer.CalculatePoints();
            
            Screen.Update(2000);

            //check if all the players have busted
            bool allHaveBusted = true;
            foreach(Player p in Players)
            {
                if (!p.Bust) allHaveBusted = false;
            }
            if (allHaveBusted) return;

            //dealer draws until their points get bigger than DEALERS_REQUIRED_POINTS
            while (Dealer.Points <= DEALERS_REQUIRED_POINTS)
            {
                Dealer.DrawRandomCard();
                Screen.Update(2000);
            }

            //dealer busts
            if(Dealer.Points > MAXIMUM_POINTS)
            {
                Dealer.Bust = true;
            }
        }

        static void GameOverPhase()
        {
            Screen.Update(1000);
            bool dealerWins = true;
            foreach (Player p in Players)
            {
                //jump over players who are out
                if(p.Bust) 
                {
                    continue;
                }

                dealerWins = false;

                //every player that is still in the round wins 2x their bet
                if (Dealer.Bust || p.Points >= Dealer.Points)
                {
                    p.Won = true;
                }
                if (dealerWins)
                {
                    Dealer.Won = true;
                }
            }

            if (dealerWins)
            {
                Console.WriteLine("Dealer wins");
            }

            Screen.Update(1000);
        }

        static void CleanUp()
        {
            //reset bet amount
            foreach (Player p in Players)
            {
                p.Balance += p.BetAmount * WIN_AMOUNT_MULTIPLIER;
                p.BetAmount = 0;
            }

            //idk meg mi kell ide
            Screen.Update();
        }

        static void Main(string[] args)
        {

            Start();
            
            BettingPhase();

            CardDealingPhase();

            PlayersPhase();

            DealerPhase();

            GameOverPhase();

            CleanUp();

            Console.ReadKey();
        }
    }
}

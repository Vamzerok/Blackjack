using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Utils
    {
        public static List<int> CalculateIntervals(int containerWidth, int quantity)
        {
            List<int> intervals = new List<int>();

            //quantity is 0
            if (quantity < 0)
            {
                return intervals;
            }

            //quantity is 1
            if (quantity == 1)
            {
                intervals.Add(containerWidth/2);
                return intervals;
            }

            intervals.Add(0);
            for (int i = 0; i < quantity - 2; i++) 
            {
                intervals.Add((containerWidth / (quantity - 1)) * (i+1));
            }
            intervals.Add(containerWidth);
            return intervals;
        }

        public static string SafeInput(List<string> validUserInputs)
        {
            string input;
            while (true)
            {
                Console.Write("> ");
                input = Console.ReadLine();
                foreach (string c in validUserInputs)
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
        public static double SafeInputInt(double min = 0, double max = 10, bool isInt = false)
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
    }
}

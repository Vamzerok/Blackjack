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
    }
}

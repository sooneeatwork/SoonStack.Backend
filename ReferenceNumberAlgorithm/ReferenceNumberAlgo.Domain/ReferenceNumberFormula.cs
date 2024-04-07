using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceNumberAlgo.Domain
{

    public abstract class ReferenceNumberFormula
    {
        protected abstract int CalculateSum(string input);
        public abstract int CalculateSpecialDigit(string input);
        public abstract ConcurrentDictionary<int, int> CalculateSpecialDigitFrequency(int start, int end);

        protected int CalculateSingleDigit(int number)
        {
            while (number > 9)
            {
                number = number.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).Sum();
            }

            return number;
        }
    }

}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceNumberAlgo.Domain.ReferencesNumberFormula
{
    public class CurrentReferencesNumberFormula : ReferenceNumberFormula
    {
        private readonly int[] _multipliers = { 10, 8, 6, 4, 2 };
        private readonly int _groupSize = 5;

        protected override int CalculateSum(string input)
        {
            var sums = _multipliers
                        .Select((multiplier, index) => CalculateSumForMultiplier(input, multiplier, index));

            return sums.Sum();
        }

        public override int CalculateSpecialDigit(string input)
        {
            int sum = CalculateSum(input);
            int result = CalculateSingleDigit(sum);

            return result;
        }

        public override ConcurrentDictionary<int, int> CalculateSpecialDigitFrequency(int start, int end)
        {
            var frequency = new ConcurrentDictionary<int, int>();

            for (int i = 0; i <= 9; i++)
            {
                frequency[i] = 0;
            }

            var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

            Parallel.For(start, end + 1, options, i =>
            {
                int specialDigit = CalculateSpecialDigit(i.ToString());
                frequency[specialDigit]++;
            });

            return frequency;
        }


        private int CalculateSumForMultiplier(string input, int multiplier, int startIndex)
        {
            return input
                .Where((digit, index) => (index + 1) % _groupSize == startIndex % _groupSize + 1)
                .Select(digit => int.Parse(digit.ToString()))
                .Select(digit => digit * multiplier)
                .Sum();
        }
    }

}

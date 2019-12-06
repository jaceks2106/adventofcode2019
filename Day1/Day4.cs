using System;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day4
    {
        public async Task Run()
        {
            Console.WriteLine("Day 3 - puzzle 1 & 2");

            var minRange = 134564;
            var maxRange = 585159;

            var result1 = Enumerable.Range(minRange, maxRange - minRange + 1).Count(x => MeetsTheConditionsPart1(x));
            var result2 = Enumerable.Range(minRange, maxRange - minRange + 1).Count(x => MeetsTheConditionsPart2(x));

            Console.WriteLine($"Result for puzzle 1 is: {result1}");
            Console.WriteLine($"Result for puzzle 2 is: {result2}");
        }

        private bool MeetsTheConditionsPart1(int sample)
        {
            var sampleString = sample.ToString();

            // It is a six - digit number. - ALWAYS TRUE
            // The value is within the range given in your puzzle input. - ALWAYS TRUE
            // Two adjacent digits are the same(like 22 in 122345).
            if (sampleString.Distinct().Count() == 6)
            {
                return false;
            }

            // Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
            for (int i = 0; i < sampleString.Length - 1; i++)
            {
                if (sampleString[i] > sampleString[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private bool MeetsTheConditionsPart2(int sample)
        {
            var sampleString = sample.ToString();

            // It is a six - digit number. - ALWAYS TRUE
            // The value is within the range given in your puzzle input. - ALWAYS TRUE
            // Two adjacent digits are the same(like 22 in 122345).
            if (sampleString.Distinct().Count() == 6)
            {
                return false;
            }

            // Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
            for (int i = 0; i < sampleString.Length - 1; i++)
            {
                if (sampleString[i] > sampleString[i + 1])
                {
                    return false;
                }
            }

            // not part of a larger group of matching digits
            if (sampleString.GroupBy(x => x).All(x => x.Count() != 2))
            {
                return false;
            }

            return true;
        }
    }
}

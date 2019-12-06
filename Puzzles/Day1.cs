using System;
using System.Threading.Tasks;
using Shared;

namespace Puzzles
{
    public class Day1
    {
        public async Task Run1()
        {
            Console.WriteLine("Day 1 - puzzle 1");
            var masses = await SharedFunctions.GetDoublesFromInput("day1-1.txt");
            double result = 0;
            Array.ForEach(masses, mass =>
            {
                result += Math.Floor(mass / 3) - 2;
            });

            Console.WriteLine($"Result is: {result}");
        }

        public async Task Run2()
        {
            Console.WriteLine("Day 1 - puzzle 2");
            var masses = await SharedFunctions.GetDoublesFromInput("day1-2.txt");
            double result = 0;
            Array.ForEach(masses, mass =>
            {
                result += CalculateTotalFuelForMass(mass);
            });
            Console.WriteLine($"Total required fuel is: {result}");
        }

        private double CalculateTotalFuelForMass(double mass)
        {
            var result = Math.Floor(mass / 3) - 2;
            if (result > 0) result += CalculateTotalFuelForMass(result);
            else result = 0;
            return result;
        }
    }
}

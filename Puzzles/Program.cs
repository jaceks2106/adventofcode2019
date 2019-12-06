using System;
using System.Threading.Tasks;
using Puzzles;

namespace Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                //await new Day1().Run1();
                //await new Day1().Run2();

                //await new Day2().Run1();
                //await new Day2().Run2();

                //await new Day3().Run();

                //await new Day4().Run();

                //await new Day5().Run1();
                
                await new Day5().Run2();

                //Console.ReadKey();
            }
        }
    }
}

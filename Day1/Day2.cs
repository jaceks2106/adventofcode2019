using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day2
    {
        public async Task Run1()
        {
            Console.WriteLine("Day 2 - puzzle 1");
            var numbers = await GetNumbersFromInput();
            var result = ProcessInputs(new List<int>(numbers).ToArray());
            Console.WriteLine($"Result is: {result}");
        }

        public async Task Run2()
        {
            Console.WriteLine("Day 2 - puzzle 1");
            var result = await GetResultForRun2();
            Console.WriteLine($"Result is: {result}");
        }

        private async Task<int> GetResultForRun2()
        {
            var numbers = await GetNumbersFromInput();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    var newArray = new List<int>(numbers).ToArray();
                    newArray[1] = i;
                    newArray[2] = j;
                    var result = ProcessInputs(newArray);

                    if (result == 19690720)
                        return 100 * newArray[1] + newArray[2];
                }
            }

            return -1;
        }

        private int ProcessInputs(int[] inputs)
        {
            bool shouldStopProcessing = false; for (int i = 0; i < inputs.Length; i += 4)
            {

                switch (inputs[i])
                {
                    case 1: // sum
                        inputs[inputs[i + 3]] = inputs[inputs[i + 1]] + inputs[inputs[i + 2]];
                        break;
                    case 2: // multiplication
                        inputs[inputs[i + 3]] = inputs[inputs[i + 1]] * inputs[inputs[i + 2]];
                        break;
                    case 3: // take input and save at index

                        Console.WriteLine("Opcode 3. Give an input:");
                        var input = Console.ReadLine();
                        if (!int.TryParse(input, out var inputInt))
                        {
                            throw new ArgumentException();
                        }
                        inputs[inputs[i + 1]] = inputInt;
                        break;

                    case 99: // halt program
                        shouldStopProcessing = true;
                        break;
                    default:
                        throw new Exception($"something went wrong - found {inputs[i]}");
                }

                if (shouldStopProcessing)
                    break;
            }

            return inputs[0];
        }

        private async Task<int[]> GetNumbersFromInput()
        {
            using var fs = new FileStream(path: Path.Combine("inputs", "day2-1.txt"), mode: FileMode.Open);
            using var sr = new StreamReader(fs);
            var input = await sr.ReadToEndAsync();
            var splitNumbers = input.Split(',');
            return splitNumbers.Select(x => int.Parse(x)).ToArray();
        }
    }
}

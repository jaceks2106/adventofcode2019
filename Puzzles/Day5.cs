using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day5
    {
        public async Task Run1()
        {
            Console.WriteLine("Day 5 - puzzle 1");
            var numbers = await GetNumbersFromInput();
            ProcessInputs1(new List<int>(numbers).ToArray());
        }

        public async Task Run2()
        {
            Console.WriteLine("Day 5 - puzzle 2");
            var numbers = await GetNumbersFromInput();
            ProcessInputs2(new List<int>(numbers).ToArray());
        }

        private int ProcessInputs1(int[] inputs)
        {
            bool shouldStopProcessing = false;

            for (int i = 0; i < inputs.Length;)
            {
                // 0 - position mode
                // 1 - immediate mode
                var (firstParameterMode, secondParameterMode, thirdParameterMode, opCode) = GetParameters(inputs[i].ToString());
                inputs[i] = opCode;

                int firstParameter = -1;
                var secondParameter = -1;

                switch (inputs[i])
                {
                    case 1: // sum
                        firstParameter = firstParameterMode == 0 ? inputs[inputs[i + 1]] : inputs[i + 1];
                        secondParameter = secondParameterMode == 0 ? inputs[inputs[i + 2]] : inputs[i + 2];
                        if (thirdParameterMode == 0) inputs[inputs[i + 3]] = firstParameter + secondParameter;
                        else inputs[i + 3] = firstParameter + secondParameter;
                        i += 4;
                        break;
                    case 2: // multiplication
                        firstParameter = firstParameterMode == 0 ? inputs[inputs[i + 1]] : inputs[i + 1];
                        secondParameter = secondParameterMode == 0 ? inputs[inputs[i + 2]] : inputs[i + 2];
                        if (thirdParameterMode == 0) inputs[inputs[i + 3]] = firstParameter * secondParameter;
                        else inputs[i + 3] = firstParameter * secondParameter;
                        i += 4;
                        break;
                    case 3: // take input and save at index
                        Console.WriteLine("Opcode 3. Give an input:");
                        var input = Console.ReadLine();
                        if (!int.TryParse(input, out var inputInt))
                        {
                            throw new ArgumentException();
                        }
                        if (firstParameterMode == 0) inputs[inputs[i + 1]] = inputInt;
                        else inputs[i + 1] = inputInt;
                        i += 2;
                        break;
                    case 4: // print output
                        if (firstParameterMode == 0) Console.WriteLine(inputs[inputs[i + 1]]);
                        else Console.WriteLine(inputs[i + 1]);
                        i += 2;
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

        private int ProcessInputs2(int[] inputs)
        {
            bool shouldStopProcessing = false;

            for (int i = 0; i < inputs.Length;)
            {
                // 0 - position mode
                // 1 - immediate mode
                var (firstParameterMode, secondParameterMode, thirdParameterMode, opCode) = GetParameters(inputs[i].ToString());
                inputs[i] = opCode;

                var firstParameter = -1;
                var secondParameter = -1;
                var thirdParameter = -1;

                try
                {
                    firstParameter = firstParameterMode == 0 ? inputs[inputs[i + 1]] : inputs[i + 1];
                    secondParameter = secondParameterMode == 0 ? inputs[inputs[i + 2]] : inputs[i + 2];
                    thirdParameter = inputs[i + 3];
                }
                catch (Exception)
                {
                }

                switch (inputs[i])
                {
                    case 1: // sum
                        if (thirdParameterMode == 0) inputs[inputs[i + 3]] = firstParameter + secondParameter;
                        else inputs[i + 3] = firstParameter + secondParameter;
                        i += 4;
                        break;
                    case 2: // multiplication
                        if (thirdParameterMode == 0) inputs[inputs[i + 3]] = firstParameter * secondParameter;
                        else inputs[i + 3] = firstParameter * secondParameter;
                        i += 4;
                        break;
                    case 3: // take input and save at index
                        Console.WriteLine("Opcode 3. Give an input:");
                        var input = Console.ReadLine();
                        if (!int.TryParse(input, out var inputInt))
                        {
                            throw new ArgumentException();
                        }
                        if (firstParameterMode == 0) inputs[inputs[i + 1]] = inputInt;
                        else inputs[i + 1] = inputInt;
                        i += 2;
                        break;
                    case 4: // print input at index
                        if (firstParameterMode == 0) Console.WriteLine(inputs[inputs[i + 1]]);
                        else Console.WriteLine(inputs[i + 1]);
                        i += 2;
                        break;
                    case 5:
                        if (firstParameter != 0)
                        {
                            i = secondParameter;
                        }
                        else
                        {
                            i += 3;
                        }
                        break;
                    case 6:
                        if (firstParameter == 0)
                        {
                            i = secondParameter;
                        }
                        else
                        {
                            i += 3;
                        }
                        break;
                    case 7:
                        inputs[thirdParameter] = firstParameter < secondParameter ? 1 : 0;
                        i += 4;
                        break;
                    case 8:
                        inputs[thirdParameter] = firstParameter == secondParameter ? 1 : 0;
                        i += 4;
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


        private (int firstParameterMode, int secondParameterMode, int thirdParameterMode, int opCode) GetParameters(
            string firstInputAsString)
        {
            var populatedInputWith0 = new List<char>();
            populatedInputWith0.AddRange(firstInputAsString);
            for (int i = 0; i < 5 - firstInputAsString.Length; i++)
            {
                populatedInputWith0.Insert(0, '0');
            }
            firstInputAsString = new string(populatedInputWith0.ToArray());

            int firstParameterMode = 0;
            int secondParameterMode = 0;
            int thirdParameterMode = 0;
            var number = firstInputAsString.Length - 2; number = number >= 0 ? number : 0;
            var firstOpcode = int.Parse(firstInputAsString.Substring(number));

            for (int i = firstInputAsString.Length - 3; i >= 0; i--)
            {
                switch (i)
                {
                    case 2:
                        firstParameterMode = int.Parse(firstInputAsString[i].ToString());
                        break;
                    case 1:
                        secondParameterMode = int.Parse(firstInputAsString[i].ToString());
                        break;
                    case 0:
                        thirdParameterMode = int.Parse(firstInputAsString[i].ToString());
                        break;
                    default:
                        break;
                }
            }
            return (firstParameterMode, secondParameterMode, thirdParameterMode, firstOpcode);
        }

        private async Task<int[]> GetNumbersFromInput()
        {
            using var fs = new FileStream(Path.Combine("inputs", "day5-1.txt"), FileMode.Open);
            using var sr = new StreamReader(fs);
            var input = await sr.ReadLineAsync();
            var splitNumbers = input.Split(',');
            return splitNumbers.Select(x => int.Parse(x)).ToArray();
        }
    }
}

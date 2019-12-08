using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day7
    {
        public async Task Run()
        {
            var controllerProgram = (await GetInputsForAmplifiers()).ToArray();
            var result = ProcessInputs(controllerProgram);
        }

        private int ProcessInputs(int[] controllerProgram)
        {
            IEnumerable<int[]> allPhaseSettings = GetAllPossibilitesPhaseSettings();
            var maxResult = int.MinValue;

            // There are only 5 Amplifiers, A,B,C,D,E
            foreach (var phaseSettings in allPhaseSettings)
            {
                int? resultOfAmplifier = null;
                for (int k = 0; k < 5; k++) // Processing IOs for all amplifiers (A,B,C,D,E)
                {
                    bool shouldStopProcessing = false;
                    int timeInRowOfGivingInput = 0;

                    for (int i = 0; i < controllerProgram.Length;)
                    {
                        // 0 - position mode
                        // 1 - immediate mode
                        var (firstParameterMode, secondParameterMode, thirdParameterMode, opCode) = GetParameters(controllerProgram[i].ToString());
                        controllerProgram[i] = opCode;

                        var firstParameter = -1;
                        var secondParameter = -1;
                        var thirdParameter = -1;

                        try
                        {
                            firstParameter = firstParameterMode == 0 ? controllerProgram[controllerProgram[i + 1]] : controllerProgram[i + 1];
                            secondParameter = secondParameterMode == 0 ? controllerProgram[controllerProgram[i + 2]] : controllerProgram[i + 2];
                            thirdParameter = controllerProgram[i + 3];
                        }
                        catch (Exception)
                        {
                        }

                        switch (controllerProgram[i])
                        {
                            case 1: // sum
                                if (thirdParameterMode == 0) controllerProgram[controllerProgram[i + 3]] = firstParameter + secondParameter;
                                else controllerProgram[i + 3] = firstParameter + secondParameter;
                                i += 4;

                                break;
                            case 2: // multiplication
                                if (thirdParameterMode == 0) controllerProgram[controllerProgram[i + 3]] = firstParameter * secondParameter;
                                else controllerProgram[i + 3] = firstParameter * secondParameter;
                                i += 4;

                                break;
                            case 3: // take input and save at index
                                timeInRowOfGivingInput++;
                                int inputValue;
                                if (timeInRowOfGivingInput == 1) inputValue = phaseSettings[k];
                                else if (timeInRowOfGivingInput == 2) inputValue = resultOfAmplifier ?? 0;
                                else throw new ArgumentException("Wrong behavior: you gave an input 2 times.");

                                if (firstParameterMode == 0) controllerProgram[controllerProgram[i + 1]] = inputValue;
                                else controllerProgram[i + 1] = inputValue;
                                i += 2;

                                break;
                            case 4: // print input at index
                                if (firstParameterMode == 0) resultOfAmplifier = controllerProgram[controllerProgram[i + 1]];
                                else resultOfAmplifier = controllerProgram[i + 1];
                                i += 2;

                                break;
                            case 5:
                                if (firstParameter != 0) i = secondParameter;
                                else i += 3;

                                break;
                            case 6:
                                if (firstParameter == 0) i = secondParameter;
                                else i += 3;

                                break;
                            case 7:
                                controllerProgram[thirdParameter] = firstParameter < secondParameter ? 1 : 0;
                                i += 4;

                                break;
                            case 8:
                                controllerProgram[thirdParameter] = firstParameter == secondParameter ? 1 : 0;
                                i += 4;

                                break;
                            case 99: // halt program
                                shouldStopProcessing = true;

                                break;
                            default:
                                throw new Exception($"something went wrong - found {controllerProgram[i]}");
                        }

                        if (shouldStopProcessing)
                            break;
                    }
                }
                if (maxResult < resultOfAmplifier)
                    maxResult = resultOfAmplifier.Value;
            }

            return maxResult;
        }

        private IEnumerable<int[]> GetAllPossibilitesPhaseSettings()
        {
            return new int[][]
            {
                new int[]{ 4, 3, 2, 1, 0 }
            };
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

        private static async Task<IEnumerable<int>> GetInputsForAmplifiers()
        {
            var allPlanets = new List<Planet>();

            using var fs = new FileStream(Path.Combine("inputs", "day7.txt"), FileMode.Open);
            using var sr = new StreamReader(fs);
            var input = await sr.ReadLineAsync();
            var splitNumbers = input.Split(',');
            return splitNumbers.Select(x => int.Parse(x)).ToArray();
        }
    }
}

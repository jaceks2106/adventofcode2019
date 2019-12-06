using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day3
    {
        public async Task Run()
        {
            Console.WriteLine("Day 3 - puzzle 1 & 2");
            var lines = await GetCartesianPointsForAllLines();

            var samePoints = lines[0].Intersect(lines[1]);

            var shortestDistance = samePoints.Min(point => Math.Abs(point.X) + Math.Abs(point.Y));
            int minSteps = FindMinStepsForCombined(lines[0], lines[1], samePoints.ToArray());
            Console.WriteLine($"Result for puzzle 1 is: {shortestDistance}");
            Console.WriteLine($"Result for puzzle 2 is: {minSteps}");
        }

        private int FindMinStepsForCombined(CartesianPoint[] wire1, CartesianPoint[] wire2, CartesianPoint[] foundIntersections)
        {
            var amountOfIndexes = foundIntersections.Length;

            var indexesForWire1 = new int[amountOfIndexes];
            var indexesForWire2 = new int[amountOfIndexes];

            for (int i = 0; i < amountOfIndexes; i++)
            {
                for (int j = 0; j < wire1.Length; j++)
                {
                    if (foundIntersections[i].Equals(wire1[j]))
                    {
                        indexesForWire1[i] = j + 1;
                    }
                }

                for (int j = 0; j < wire2.Length; j++)
                {
                    if (foundIntersections[i].Equals(wire2[j]))
                    {
                        indexesForWire2[i] = j + 1;
                    }
                }
            }

            var sumIndexes = new int[amountOfIndexes];
            for (int i = 0; i < amountOfIndexes; i++)
            {
                sumIndexes[i] = indexesForWire1[i] + indexesForWire2[i];
            }
            return sumIndexes.Min();
        }

        private async Task<CartesianPoint[][]> GetCartesianPointsForAllLines()
        {
            using var fs = new FileStream("inputs/day3-1.txt", FileMode.Open);
            using var sr = new StreamReader(fs);
            return new[]
            {
                ParseToCartesianPoints(await sr.ReadLineAsync()),
                ParseToCartesianPoints(await sr.ReadLineAsync())
            };
        }

        private CartesianPoint[] ParseToCartesianPoints(string line)
        {
            var result = new List<CartesianPoint>();
            var movements = line.Split(',');
            var previousPoint = new CartesianPoint();
            foreach (var movement in movements)
            {
                var number = int.Parse(movement.Substring(1));
                previousPoint = movement.ToLower()[0] switch
                {
                    'u' => ((Func<CartesianPoint>)(() =>
                    {
                        for (int y = previousPoint.Y; y < previousPoint.Y + number; y++)
                        {
                            result.Add(new CartesianPoint
                            {
                                X = previousPoint.X,
                                Y = y + 1
                            });
                        }
                        return result.Last();
                    }))(),
                    'd' => ((Func<CartesianPoint>)(() =>
                    {
                        for (int y = previousPoint.Y; y > previousPoint.Y - number; y--)
                        {
                            result.Add(new CartesianPoint
                            {
                                X = previousPoint.X,
                                Y = y - 1
                            });
                        }
                        return result.Last();
                    }))(),
                    'l' => ((Func<CartesianPoint>)(() =>
                    {
                        for (int x = previousPoint.X; x > previousPoint.X - number; x--)
                        {
                            result.Add(new CartesianPoint
                            {
                                X = x - 1,
                                Y = previousPoint.Y
                            });
                        }
                        return result.Last();
                    }))(),
                    'r' => ((Func<CartesianPoint>)(() =>
                    {
                        for (int x = previousPoint.X; x < previousPoint.X + number; x++)
                        {
                            result.Add(new CartesianPoint
                            {
                                X = x + 1,
                                Y = previousPoint.Y
                            });
                        }
                        return result.Last();
                    }))(),
                    _ => throw new ArgumentException($"Unsupported direction: {movement}"),
                };
            }
            return result.ToArray();
        }
    }

    public struct CartesianPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

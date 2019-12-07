using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day6
    {
        public async Task Run()
        {
            Console.WriteLine("Day 6 - puzzle 1");
            var planetsRelations = await GetAllPlanets();

            int directAndIndirectRelations = CalculateDirectAndIndirectRelations(planetsRelations);
            Console.WriteLine($"Result for puzzle 1 is: {directAndIndirectRelations}");
        }

        private static int CalculateDirectAndIndirectRelations(IEnumerable<Planet> planetsRelations)
        {
            var mainPlanet = "com";
            var result = GetAmountOfRelationsArray(planetsRelations, mainPlanet);

            return result.Sum();
        }

        private static IEnumerable<int> GetAmountOfRelationsArray(
            IEnumerable<Planet> planetsRelations,
            string planetName,
            int iterator = -1)
        {
            var orbitals = planetsRelations.Where(x => x.CenterPlanetName == planetName);
            var count = orbitals.Count();
            iterator++;
            var result = new[] { iterator }.Concat(orbitals.SelectMany(orbital => GetAmountOfRelationsArray(planetsRelations, orbital.Name, iterator)));
            return result;
        }

        private static async Task<IEnumerable<Planet>> GetAllPlanets()
        {
            var allPlanets = new List<Planet>();

            using var fs = new FileStream(Path.Combine("inputs", "day6.txt"), FileMode.Open);
            using var sr = new StreamReader(fs);
            while (true)
            {
                var line = await sr.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                    break;

                var planetRelation = line.ToLower().Split(')');
                var newRelation = new Planet
                {
                    Name = planetRelation[1],
                    CenterPlanetName = planetRelation[0]
                };
                allPlanets.Add(newRelation);
            }

            return allPlanets;
        }
    }

    public struct Planet
    {
        public string Name { get; set; }
        public string CenterPlanetName { get; set; }
    }
}

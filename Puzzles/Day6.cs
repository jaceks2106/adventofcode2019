using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles
{
    public class Day6
    {
        private const string MainPlanet = "com";
        public async Task Run()
        {
            Console.WriteLine("Day 6 - puzzle 1");
            var planets = await GetAllPlanets();

            int directAndIndirectRelations = CalculateDirectAndIndirectRelations(planets);
            Console.WriteLine($"Result for puzzle 1 is: {directAndIndirectRelations}");

            int amountOfOrbitalTransfersBetween = CalculateOrbitalTransfersBetweenPlanets(planets: planets, from: "you", to: "san");
            Console.WriteLine($"Result for puzzle 2 is: {amountOfOrbitalTransfersBetween}");
        }

        private static int CalculateOrbitalTransfersBetweenPlanets(string from, string to, IEnumerable<Planet> planets)
        {
            var fromPlanet = planets.Single(x => x.Name == from);
            var toPlanet = planets.Single(x => x.Name == to);

            var allParentsOfFrom = GetAllParentsOfPlanet(fromPlanet, planets);
            var allParentsOfTo = GetAllParentsOfPlanet(toPlanet, planets);

            var list1 = new[] { fromPlanet }.Concat(allParentsOfFrom).Select(x => x.CenterPlanetName);
            var list2 = new[] { toPlanet }.Concat(allParentsOfTo).Select(x => x.CenterPlanetName);

            var result = list1
                .Concat(list2)
                .GroupBy(x => x);
            return result.Where(x => x.Count() == 1).Count();
        }

        private static IEnumerable<Planet> GetAllParentsOfPlanet(Planet fromPlanet, IEnumerable<Planet> planets)
        {
            var directParent = planets.Single(x => x.Name == fromPlanet.CenterPlanetName);
            if (directParent.CenterPlanetName == MainPlanet)
            {
                return new[] { directParent };
            }
            else
            {
                return new[] { directParent }.Concat(GetAllParentsOfPlanet(directParent, planets));
            }
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

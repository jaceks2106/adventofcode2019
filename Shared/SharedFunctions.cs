using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shared
{
    public static class SharedFunctions
    {
        public static async Task<double[]> GetDoublesFromInput(string inputPath)
        {
            using var fs = new FileStream(path: Path.Combine("inputs", inputPath), mode: FileMode.Open);
            using var sr = new StreamReader(fs);
            var result = new List<double>();
            while (true)
            {
                var newLine = await sr.ReadLineAsync();
                if (string.IsNullOrEmpty(newLine))
                {
                    break;
                }
                result.Add(double.Parse(newLine));
            }
            return result.ToArray();
        }
    }
}

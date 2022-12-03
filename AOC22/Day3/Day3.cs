using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day3
{
    internal class Day3 : Day
    {
        public Day3(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var lines = File.ReadAllLines(@$"{Path}\data-day3.txt");
            var sum = lines.Select(line =>
            {
                var half = line.Length / 2;
                var left = line.Substring(0, half);
                var right = line.Substring(half);
                var letter = left.First(l => right.Contains(l));
                return GetLetterPriority(letter);
            }).Sum();

            Console.WriteLine($"Sum of priorities of item types that appears in both compartments: {sum}");

            sum = lines
                .Chunk(3)
                .Select(lines =>
                {
                    var shortest = lines.Aggregate((current, next) => next.Length > current.Length ? current : next);
                    var letter = shortest.First(l => lines.All(line => line.Contains(l)));
                    return GetLetterPriority(letter);
                })
                .Sum();

            Console.WriteLine($"Sum of priorities of item types that appears in each three-Elf group: {sum}");
        }

        int GetLetterPriority(char letter)
        {
            var num = (byte)letter;
            if (num > 96)
                return num - 96;
            else
                return num - 38;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day4
{
    internal class Day4 : Day
    {
        public Day4(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var lines = File.ReadAllLines(@$"{Path}\data-day4.txt");
            var result = lines
                .Select(line => ToPairs(line))
                .Count(pair => FullyContains(pair[0], pair[1]));

            Console.WriteLine($"Amount of pairs whose range fully contain the other: {result}");

            result = lines
                .Select(line => ToPairs(line))
                .Count(pair => Overlaps(pair[0], pair[1]));

            Console.WriteLine($"Amount of pairs whose range overlaps the other: {result}");
        }

        int[][] ToPairs(string line)
        {
            return line
                .Split(',')
                .Select(assignment => assignment.Split('-').Select(num => int.Parse(num)).ToArray())
                .ToArray();
        }

        bool FullyContains(int[] pair1, int[] pair2)
        {
            return
                pair1[0] >= pair2[0] && pair1[1] <= pair2[1] ||
                pair2[0] >= pair1[0] && pair2[1] <= pair1[1];
        }

        bool Overlaps(int[] pair1, int[] pair2)
        {
            return
                pair1[0] >= pair2[0] && pair1[0] <= pair2[1] ||
                pair1[1] >= pair2[0] && pair1[1] <= pair2[1] ||
                pair2[0] >= pair1[0] && pair2[0] <= pair1[1] ||
                pair2[1] >= pair1[0] && pair2[1] <= pair1[1];
        }

    }
}

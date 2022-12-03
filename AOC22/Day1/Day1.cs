using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day1
{
    internal class Day1 : Day
    {
        public Day1(string path) : base(path)
        {
            
        }

        public override void Execute()
        {
            var data = File.ReadAllText(@$"{Path}\data-day1.txt");
            var calories = data.Split("\r\n\r\n")
                .Select(line => line.Split("\r\n")
                .Select<string, int>(calorie => int.Parse(calorie))
                .Aggregate(0, (total, next) => total + next))
                .ToArray();
            var max = calories.Max();
            Console.WriteLine($"Max: {max}");

            var topThreeSum = calories.OrderByDescending(num => num).Take(3).Sum();
            Console.WriteLine($"Top 3 sum: {topThreeSum}");
        }
    }
}

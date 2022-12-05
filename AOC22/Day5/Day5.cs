using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day5
{
    internal class Day5 : Day
    {
        public Day5(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var lines = File.ReadAllLines(@$"{Path}\data-day5.txt");

            var stackLines = lines.TakeWhile(line => line.Contains('[')).Reverse().ToArray();
            var stackCount = (stackLines.Last().Length / 4) + 2; // make stack start index == 1 to reduce calculations below

            var stacks9000 = new Stack<char>[stackCount];
            var stacks9001 = new Stack<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                stacks9000[i] = new Stack<char>();
                stacks9001[i] = new Stack<char>();
            }

            // fill default stack list
            foreach (var line in stackLines)
            {
                var i = 1;
                var stackIndex = 1;
                while (i < line.Length)
                {
                    if (!char.IsWhiteSpace(line[i]))
                    {
                        stacks9000[stackIndex].Push(line[i]);
                        stacks9001[stackIndex].Push(line[i]);
                    }

                    i += 4;
                    stackIndex++;
                }
            }

            var temp = new Stack<char>();
            lines.Skip(stackLines.Length + 2).ToList().ForEach(line =>
            {
                var parts = line.Split(' ');
                var amount = int.Parse(parts[1]);
                var from = int.Parse(parts[3]);
                var to = int.Parse(parts[5]);

                // part 1
                for (int i = 0; i < amount; i++)
                {
                    stacks9000[to].Push(stacks9000[from].Pop());
                }

                // part 2
                for (int i = 0; i < amount; i++)
                {
                    temp.Push(stacks9001[from].Pop());
                }
                for (int i = 0; i < amount; i++)
                {
                    stacks9001[to].Push(temp.Pop());
                }
            });

            var result = string.Join("", stacks9000.Skip(1).Select(stack => stack.Peek()).ToArray());
            Console.WriteLine($"Crates on top of each stack (using CrateMover 9000): {result}");

            result = string.Join("", stacks9001.Skip(1).Select(stack => stack.Peek()).ToArray());
            Console.WriteLine($"Crates on top of each stack (using CrateMover 9001): {result}");
        }
    }
}

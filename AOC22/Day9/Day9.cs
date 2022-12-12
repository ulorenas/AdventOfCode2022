using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day9
{
    internal class Day9 : Day
    {
        public Day9(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var lines = File.ReadAllLines(@$"{Path}\data-day9.txt");
            var positions = new List<Position>();
            var head = new Position(0, 0);
            var tail = new Position(0, 0);
            positions.Add(tail);
            foreach (var line in lines)
            {
                var direction = line[0];
                var steps = int.Parse(line.Substring(2));
                for (int step = 0; step < steps; step++)
                {
                    head = MoveHead(head, direction);
                    tail = MoveTail(tail, head);

                    if (!positions.Contains(tail))
                        positions.Add(tail);
                }
                
            }
            Console.WriteLine($"Rope visits {positions.Count} positions atleast once");

            positions = new List<Position>();
            var knots = new Position[10];
            positions.Add(knots[knots.Length - 1]);
            foreach (var line in lines)
            {
                var direction = line[0];
                var steps = int.Parse(line.Substring(2));
                for (int step = 0; step < steps; step++)
                {
                    knots[0] = MoveHead(knots[0], direction);
                    for (int i = 1; i < knots.Length; i++)
                    {
                        knots[i] = MoveTail(knots[i], knots[i - 1]);
                    }

                    if (!positions.Contains(knots[knots.Length - 1]))
                        positions.Add(knots[knots.Length - 1]);
                }

            }
            Console.WriteLine($"Tail of the rope visits {positions.Count} positions atleast once");
        }

        Position MoveHead(Position head, char direction)
        {
            switch (direction)
            {
                case 'R':
                    return new Position(head.X + 1, head.Y);
                case 'L':
                    return new Position(head.X - 1, head.Y);
                case 'U':
                    return new Position(head.X, head.Y + 1);
                case 'D':
                    return new Position(head.X, head.Y - 1);
                default:
                    throw new ArgumentException($"Invalid direction received: {direction}");
            }
        }

        Position MoveTail(Position tail, Position head)
        {
            var distanceX = head.X - tail.X;
            var distanceY = head.Y - tail.Y;

            var absDistanceX = Math.Abs(distanceX);
            var absDistanceY = Math.Abs(distanceY);

            if (absDistanceX > 1 && absDistanceY > 0 || absDistanceX > 0 && absDistanceY > 1)
                return new Position(tail.X + absDistanceX / distanceX, tail.Y + absDistanceY / distanceY);

            if (absDistanceX > 1)
                return new Position(tail.X + absDistanceX / distanceX, tail.Y);

            if (absDistanceY > 1)
                return new Position(tail.X, tail.Y + absDistanceY / distanceY);

            return tail;
        }
    }

    struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

}

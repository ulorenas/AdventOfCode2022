using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day8
{
    internal class Day8 : Day
    {
        public Day8(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var lines = File.ReadAllLines(@$"{Path}\data-day8.txt");
            var grid = new int[lines.Length, lines[0].Length];
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    grid[row, col] = int.Parse(lines[row][col].ToString());
                }
            }

            var visible = lines.Length * 2 + lines[0].Length * 2 - 4;

            for (int row = 1; row < lines.Length - 1; row++)
            {
                for (int col = 1; col < lines[row].Length - 1; col++)
                {
                    if (VisibleFromOutside(grid, row, col))
                        visible++;
                }
            }

            Console.WriteLine($"{visible} trees are visible from outside the grid");

            var largest = 0;
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    var score = CalculateScenicScore(grid, row, col);
                    if (score > largest)
                        largest = score;
                }
            }

            Console.WriteLine($"Largest scenic score is {largest}");
        }

        bool VisibleFromOutside(int[,] grid, int row, int col)
        {
            var height = grid[row, col];
            var visible = true;
            for (int y = 0; y < row; y++)
            {
                if (grid[y, col] >= height)
                {
                    visible = false;
                    break;
                }
            }

            if (visible)
                return visible;
            visible = true;

            for (int y = row + 1; y < grid.GetLength(0); y++)
            {
                if (grid[y, col] >= height)
                {
                    visible = false;
                    break;
                }
            }

            if (visible)
                return visible;
            visible = true;

            for (int x = 0; x < col; x++)
            {
                if (grid[row, x] >= height)
                {
                    visible = false;
                    break;
                }
            }

            if (visible)
                return visible;
            visible = true;

            for (int x = col + 1; x < grid.GetLength(1); x++)
            {
                if (grid[row, x] >= height)
                {
                    visible = false;
                    break;
                }
            }

            return visible;
        }

        int CalculateScenicScore(int[,] grid, int row, int col)
        {
            var height = grid[row, col];
            var scores = new int[4];
            for (int y = row - 1; y >= 0; y--)
            {
                if (grid[y, col] < height)
                {
                    scores[0]++;
                }
                else
                {
                    scores[0]++;
                    break;
                }
            }

            for (int y = row + 1; y < grid.GetLength(0); y++)
            {
                if (grid[y, col] < height)
                {
                    scores[1]++;
                }
                else
                {
                    scores[1]++;
                    break;
                }
            }

            for (int x = col - 1; x >= 0; x--)
            {
                if (grid[row, x] < height)
                {
                    scores[2]++;
                }
                else
                {
                    scores[2]++;
                    break;
                }
            }

            for (int x = col + 1; x < grid.GetLength(1); x++)
            {
                if (grid[row, x] < height)
                {
                    scores[3]++;
                }
                else
                {
                    scores[3]++;
                    break;
                }
            }

            return scores.Aggregate((curr, next) => curr * next);
        }
    }
}

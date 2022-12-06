using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day6
{
    internal class Day6 : Day
    {
        public Day6(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var line = File.ReadAllText(@$"{Path}\data-day6.txt");
            var decoder = new Decoder(4);
            for (int i = 0; i < line.Length; i++)
            {
                decoder.AddData(line[i]);
                if (decoder.AllDistinct())
                {
                    Console.WriteLine($"Start-of-packet marker is at index {i + 1}");
                    break;
                }
            }

            decoder = new Decoder(14);
            for (int i = 0; i < line.Length; i++)
            {
                decoder.AddData(line[i]);
                if (decoder.AllDistinct())
                {
                    Console.WriteLine($"Start-of-message marker is at index {i + 1}");
                    break;
                }
            }
        }
    }

    class Decoder
    {
        public Decoder(int size)
        {
            Size = size;
        }

        public int Size { get; private set; }

        public List<char> Data { get; private set; } = new List<char>();

        public void AddData(char c)
        {
            Data.Add(c);
            if (Data.Count > Size)
                Data.RemoveAt(0);
        }

        public bool AllDistinct()
        {
            return Data.Distinct().Count() == Size;
        }
    }
}

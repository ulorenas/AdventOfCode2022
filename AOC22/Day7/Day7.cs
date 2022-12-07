using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC22.Day7
{
    internal class Day7 : Day
    {
        public Day7(string path) : base(path)
        {
        }

        public override void Execute()
        {
            var lines = File.ReadAllLines(@$"{Path}\data-day7.txt");
            var home = new DirEntry() { Name = "/" };
            var cwd = home;

            var navigation = new List<DirEntry>();
            navigation.Add(home);

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Equals("$ ls"))
                {
                    // do nothin
                }
                else if (line.Equals("$ cd .."))
                {
                    navigation.RemoveAt(navigation.Count - 1);
                    cwd = navigation[navigation.Count - 1];
                }
                else if (line.StartsWith("$ cd"))
                {
                    var match = Regex.Match(line, @"\$ cd (.*)");
                    var name = match.Groups[1].Value;
                    var dir = new DirEntry() { Name = name };
                    cwd.Entries.Add(dir);
                    cwd = dir;
                    navigation.Add(dir);
                }
                else
                {
                    var parts = line.Split(' ');
                    if (!parts[0].Equals("dir"))
                    {
                        var file = new FileEntry() { Name = parts[1], Size = int.Parse(parts[0]) };
                        cwd.Entries.Add(file);
                    }
                }

            }

            var sumOfSmallDirs = 0;
            var total = FindDirectorySizeWithLimit(home, ref sumOfSmallDirs);
            Console.WriteLine($"Sum of all directories with total size of at most 100000: {sumOfSmallDirs}");

            var sizes = new List<int>();
            var spaceNeeded = 30000000 - (70000000 - total);
            GetDirectorySizes(home, sizes);
            sizes.Sort();
            var toDelete = sizes.First(size => size >= spaceNeeded);
            Console.WriteLine($"Directory with size of {toDelete} should be deleted");
        }

        int FindDirectorySizeWithLimit(DirEntry dir, ref int total)
        {
            var size = 0;
            foreach (var entry in dir.Entries)
            {
                if (entry is FileEntry)
                {
                    size += ((FileEntry)entry).Size;
                }
                else
                {
                    size += FindDirectorySizeWithLimit((DirEntry)entry, ref total);
                }
            }
            if (size < 100000)
                total += size;

            return size;
        }

        int GetDirectorySizes(DirEntry dir, List<int> sizes)
        {
            var size = 0;
            foreach (var entry in dir.Entries)
            {
                if (entry is FileEntry)
                {
                    size += ((FileEntry)entry).Size;
                }
                else
                {
                    size += GetDirectorySizes((DirEntry)entry, sizes);
                }
            }

            sizes.Add(size);

            return size;
        }
    }

    abstract class Entry
    {
        public string Name { get; set; }
    }

    class FileEntry : Entry
    {
        public int Size { get; set; }
    }

    class DirEntry : Entry
    {
        public List<Entry> Entries { get; set; } = new List<Entry>();
    }
}

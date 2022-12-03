using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    internal abstract class Day
    {
        public Day(string path)
        {
            Path = $@"{path}\{this.GetType().Name}";
        }

        public string Path { get; private set; }

        public abstract void Execute();
    }
}

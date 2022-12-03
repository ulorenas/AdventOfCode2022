using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22.Day2
{
    enum GameOption
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    internal class Day2 : Day
    {
        public Day2(string path) : base(path)
        {
        }

        Dictionary<string, GameOption> _letterToMove = new Dictionary<string, GameOption>();
        Dictionary<GameOption, string> _moveToLetter = new Dictionary<GameOption, string>();

        public override void Execute()
        {
            _letterToMove.Add("A", GameOption.Rock);
            _letterToMove.Add("B", GameOption.Paper);
            _letterToMove.Add("C", GameOption.Scissors);
            _letterToMove.Add("X", GameOption.Rock);
            _letterToMove.Add("Y", GameOption.Paper);
            _letterToMove.Add("Z", GameOption.Scissors);

            _moveToLetter.Add(GameOption.Rock, "X");
            _moveToLetter.Add(GameOption.Paper, "Y");
            _moveToLetter.Add(GameOption.Scissors, "Z");

            var lines = File.ReadAllLines(@$"{Path}\data-day2.txt");

            Console.WriteLine("Part 1:");
            var score = lines
                .Select(line =>
                {
                    var moves = line.Split(" ");
                    return CalculateScore(moves[0], moves[1]);
                })
                .Sum();
            Console.WriteLine(score);
            Console.WriteLine();

            Console.WriteLine("Part 2:");
            score = lines
                .Select(line =>
                {
                    var moves = line.Split(" ");
                    return CalculateScore(moves[0], TransformMove(moves[0],moves[1]));
                })
                .Sum();
            Console.WriteLine(score);
        }

        string TransformMove(string opponentLetter, string yourLetter)
        {
            var opp = _letterToMove[opponentLetter];

            if (yourLetter == "Y")
            {
                return opponentLetter;
            }
            else if (yourLetter == "X")
            {
                return _moveToLetter[GetLosingOptionFor(opp)];
            }
            else if (yourLetter == "Z")
            {
                return _moveToLetter[GetWinningOptionFor(opp)];
            }
            else
            {
                throw new Exception();
            }
        }

        GameOption GetWinningOptionFor(GameOption opponent)
        {
            switch (opponent)
            {
                case GameOption.Rock:
                    return GameOption.Paper;
                case GameOption.Paper:
                    return GameOption.Scissors;
                case GameOption.Scissors:
                    return GameOption.Rock;
                default:
                    throw new Exception();
            }
        }

        GameOption GetLosingOptionFor(GameOption opponent)
        {
            switch (opponent)
            {
                case GameOption.Rock:
                    return GameOption.Scissors;
                case GameOption.Paper:
                    return GameOption.Rock;
                case GameOption.Scissors:
                    return GameOption.Paper;
                default:
                    throw new Exception();
            }
        }

        int CalculateScore(string opponentLetter, string yourLetter)
        {
            var opp = _letterToMove[opponentLetter];
            var you = _letterToMove[yourLetter];
            if (opp == you)
            {
                return 3 + (int)you;
            }
            else if (opp == GameOption.Rock && you == GameOption.Paper)
            {
                return 6 + (int)you;
            }
            else if (opp == GameOption.Rock && you == GameOption.Scissors)
            {
                return 0 + (int)you;
            }
            else if (opp == GameOption.Paper && you == GameOption.Rock)
            {
                return 0 + (int)you;
            }
            else if (opp == GameOption.Paper && you == GameOption.Scissors)
            {
                return 6 + (int)you;
            }
            else if (opp == GameOption.Scissors && you == GameOption.Rock)
            {
                return 6 + (int)you;
            }
            else if (opp == GameOption.Scissors && you == GameOption.Paper)
            {
                return 0 + (int)you;
            }
            else
            {
                throw new Exception();
            }
        }

    }

}

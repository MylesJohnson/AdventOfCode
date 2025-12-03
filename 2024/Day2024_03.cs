using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day2024_03 : PuzzleDay
    {
        private readonly string input;

        public Day2024_03()
        {
            input = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            int answer = 0;

            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            foreach (Match m in Regex.Matches(input, pattern))
            {
                answer += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
            }

            return answer;
        }

        private int Solve_2_First_Approach()
        {
            return 0;
        }
    }
}

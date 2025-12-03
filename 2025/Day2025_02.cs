using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day2025_02 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");
        private long Solve_1_First_Approach()
        {
            long result = 0;

            string[] ranges = _input[0].Split(',');
            foreach(string range in ranges)
            {
                long low = long.Parse(range.Split('-')[0]);
                long high = long.Parse(range.Split('-')[1]);
                
                for(long i = low; i <= high; i++)
                {
                    string number = i.ToString();
                    int halfLength = number.Length / 2;
                    string firstHalf = number[..halfLength];
                    string secondHalf = number[halfLength..];

                    if (firstHalf == secondHalf)
                    {
                        result += i;
                    }
                }
            }

            return result;
        }

        private long Solve_2_First_Approach()
        {
            Regex re = new Regex(@"^(\d+)(?:\1)+$", RegexOptions.Compiled);
            long result = 0;

            string[] ranges = _input[0].Split(',');
            foreach (string range in ranges)
            {
                long low = long.Parse(range.Split('-')[0]);
                long high = long.Parse(range.Split('-')[1]);

                for (long i = low; i <= high; i++)
                {
                    string number = i.ToString();
                    if(re.IsMatch(number))
                    {
                        result += i;
                    }
                }
            }

            return result;
        }
    }
}

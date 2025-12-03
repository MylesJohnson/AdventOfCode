namespace AdventOfCode
{
    public class Day2025_01 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_01()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        // I learned today that C#'s % operator is remainder, not modulo. Here's a proper modulo function
        private static int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        private int Solve_1_First_Approach()
        {
            int result = 0;

            int currentPosition = 50;
            for (int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];

                int sign = line[0] == 'R' ? 1 : -1;
                int value = int.Parse(line[1..]);
                currentPosition = mod(currentPosition + (sign * value), 100);
                if (currentPosition == 0)
                {
                    result++;
                }
            }

            return result;
        }

        private int Solve_2_First_Approach()
        {
            int result = 0;

            int currentPosition = 50;
            for (int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];

                int sign = line[0] == 'R' ? 1 : -1;
                int value = int.Parse(line[1..]);

                for (int j = 0; j < value; j++)
                {
                    currentPosition += sign;
                    currentPosition = currentPosition switch
                    {
                        100 => 0,
                        -1 => 99,
                        _ => currentPosition
                    };
                    if (currentPosition == 0)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private int Solve_2_Second_Approach()
        {
            // I'd love to be able to prove this approach works, but I couldn't get it to match the brute-force approach.
            // I think I'm missing the edge case where I start on 0 and go left, but I can't wrap my head around it right now.
            int result = 0;

            int currentPosition = 50;
            for (int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];

                int sign = line[0] == 'R' ? 1 : -1;
                int value = int.Parse(line[1..]);

                result += (int)Math.Abs(Math.Floor((currentPosition + (sign * value) - 1) / 100.0));
                currentPosition = mod(currentPosition + (sign * value), 100);
            }

            return result; // 6166
        }
    }
}

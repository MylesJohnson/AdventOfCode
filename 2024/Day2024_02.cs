namespace AdventOfCode
{
    public class Day2024_02 : PuzzleDay
    {
        private readonly string[] input;

        public Day2024_02()
        {
            input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            int safe = 0;

            foreach (string report in input)
            {
                var levels = report.Split(' ').Select(int.Parse).ToArray();

                bool? increasing = null;
                for (int i = 0; i < levels.Length - 1; i++)
                {
                    if (increasing == null)
                    {
                        increasing = levels[i] < levels[i + 1];
                    }
                    else if (increasing.Value && levels[i] > levels[i + 1])
                    {
                        safe -= 1; // Since we always increment safe, we need to decrease it when its unsafe to cancel it out
                        break;
                    }
                    else if (!increasing.Value && levels[i] < levels[i + 1])
                    {
                        safe -= 1; // Since we always increment safe, we need to decrease it when its unsafe to cancel it out
                        break;
                    }

                    if (Math.Abs(levels[i] - levels[i + 1]) > 3 || Math.Abs(levels[i] - levels[i + 1]) < 1)
                    {
                        safe -= 1; // Since we always increment safe, we need to decrease it when its unsafe to cancel it out
                        break;
                    }
                }

                safe += 1;
            }

            return safe;
        }

        private int Solve_2_First_Approach()
        {
            return 0;
        }
    }
}

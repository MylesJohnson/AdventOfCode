namespace AdventOfCode
{
    public class Day2024_01 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2024_01()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            List<int> left = new();
            List<int> right = new();

            foreach (var line in _input)
            {
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries ^ StringSplitOptions.TrimEntries);

                left.Add(int.Parse(split[0]));
                right.Add(int.Parse(split[1]));
            }

            left.Sort();
            right.Sort();

            int distance = 0;
            for (int i = 0; i < left.Count; i++)
            {
                distance += Math.Abs(left[i] - right[i]);
            }

            return distance;
        }

        private int Solve_2_First_Approach()
        {
            List<int> left = new();
            Dictionary<int, int> right = new(); // number, count

            foreach (var line in _input)
            {
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries ^ StringSplitOptions.TrimEntries);

                left.Add(int.Parse(split[0]));

                int rightValue = int.Parse(split[1]);
                right[rightValue] = right.GetValueOrDefault(rightValue, 0) + 1;
            }

            int similarity = 0;
            foreach (var number in left)
            {
                similarity += number * right.GetValueOrDefault(number, 0);
            }

            return similarity;
        }
    }
}

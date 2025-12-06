namespace AdventOfCode
{
    public class Day2025_06 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_06()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private long Solve_1_First_Approach()
        {
            long result = 0;
            int[][] numbers = new int[_input.Length - 1][];

            for (int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];
                if (line.Contains('+'))
                {
                    // its the operation line
                    string[] operations = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < operations.Length; j++)
                    {
                        string operation = operations[j];
                        if (operation == "+")
                        {
                            result += numbers.Select(x => x[j]).Sum();
                        }
                        else
                        {
                            result += numbers.Select(x => x[j]).Aggregate(1L, (a, b) => a * b);
                        }
                    }
                }
                else
                {
                    // its a numbers line
                    numbers[i] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }
            }

            return result;
        }

        private int Solve_2_First_Approach()
        {
            int result = 0;

            return result;
        }
    }
}

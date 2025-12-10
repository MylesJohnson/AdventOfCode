

namespace AdventOfCode
{
    public class Day2025_09 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_09()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private long Solve_1_First_Approach()
        {
            long largestArea = 0;
            IList<(int x, int y)> points = _input.Select(line => line.Split(',').Select(int.Parse).ToArray()).Select(arr => (arr[0], arr[1])).ToList();

            foreach (var pointA in points)
            {
                foreach (var pointB in points)
                {
                    if (pointA == pointB)
                        continue;
                    long area = ((long)Math.Abs(pointA.x - pointB.x) + 1) * (Math.Abs(pointA.y - pointB.y) + 1);
                    if (area > largestArea)
                        largestArea = area;
                }
            }

            return largestArea;
        }

        private int Solve_2_First_Approach()
        {
            int result = 0;

            return result;
        }
    }
}

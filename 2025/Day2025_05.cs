namespace AdventOfCode
{
    public class Day2025_05 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_05()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            int result = 0;
            HashSet<Range> freshIngredients = new HashSet<Range>();

            var inputs = _input.AsEnumerable().GetEnumerator();
            while (inputs.MoveNext())
            {
                string line = inputs.Current;

                // Done ranges
                if (string.IsNullOrWhiteSpace(line))
                    break;

                string[] parts = line.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);
                Range range = new Range(start, end);
                freshIngredients.Add(range);
            }

            while (inputs.MoveNext())
            {
                string line = inputs.Current;

                long id = long.Parse(line);
                foreach (var range in freshIngredients)
                {
                    if (range.Includes(id))
                    {
                        result++;
                        break;
                    }
                }
            }

            return result;
        }

        private long Solve_2_First_Approach()
        {
            HashSet<Range> freshIngredients = new HashSet<Range>();

            foreach (var line in _input)
            {
                // Done ranges
                if (string.IsNullOrWhiteSpace(line))
                    break;

                string[] parts = line.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);
                Range newRange = new Range(start, end);

                // Merge overlapping ranges
                var overlappingRanges = freshIngredients.Where(r => r.Overlaps(newRange)).ToList();
                foreach (var range in overlappingRanges)
                {
                    newRange = newRange.Merge(range);
                    freshIngredients.Remove(range);
                }

                freshIngredients.Add(newRange);
            }

            return freshIngredients.Select(r => r.Length()).Sum();
        }

        record Range(long Start, long End)
        {
            public bool Includes(long value) => Start <= value && value <= End;
            public long Length() => End - Start + 1;
            public bool Overlaps(Range other) => !(other.End < Start || other.Start > End);
            public Range Merge(Range other) => new(Math.Min(Start, other.Start), Math.Max(End, other.End) );
        }
    }
}

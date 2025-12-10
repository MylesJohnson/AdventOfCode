

namespace AdventOfCode
{
    public class Day2025_07 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_07()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            int splits = 0;
            ICollection<int> currentBeamPositions = new HashSet<int>();

            IEnumerator<string> lines = _input.AsEnumerable().GetEnumerator();
            currentBeamPositions.Add(_input[0].IndexOf('S'));

            while (lines.MoveNext()) {
                string line = lines.Current;
                foreach(int beamPosition in currentBeamPositions.ToArray())
                {
                    if (line[beamPosition] == '^')
                    {
                        currentBeamPositions.Remove(beamPosition);
                        currentBeamPositions.Add(beamPosition - 1);
                        currentBeamPositions.Add(beamPosition + 1);
                        splits++;
                    }
                }
            }

            return splits;
        }

        private long Solve_2_First_Approach()
        {
            int splits = 0;
            Dictionary<int, long> currentBeamPositions = new Dictionary<int, long>();

            IEnumerator<string> lines = _input.AsEnumerable().GetEnumerator();
            currentBeamPositions.Add(_input[0].IndexOf('S'), 1);

            while (lines.MoveNext())
            {
                string line = lines.Current;
                Dictionary<int, long> newBeamPositions = new Dictionary<int, long>();
                foreach (var (beamPosition, count) in currentBeamPositions)
                {
                    if (line[beamPosition] == '^')
                    {
                        newBeamPositions[beamPosition - 1] = newBeamPositions.GetValueOrDefault(beamPosition - 1, 0) + count;
                        newBeamPositions[beamPosition + 1] = newBeamPositions.GetValueOrDefault(beamPosition + 1, 0) + count;
                        splits++;
                    }
                    else
                    {
                        newBeamPositions[beamPosition] = newBeamPositions.GetValueOrDefault(beamPosition, 0) + count;
                    }
                }
                currentBeamPositions = newBeamPositions;
            }

            return currentBeamPositions.Values.Sum();
        }
    }
}

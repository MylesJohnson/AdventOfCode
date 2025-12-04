namespace AdventOfCode
{
    public class Day2025_04 : PuzzleDay
    {
        private char[][] grid;

        public Day2025_04()
        {
            var input = File.ReadAllLines(InputFilePath);
            grid = input.Select(line => line.ToCharArray()).ToArray();
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int[][] GenerateAdjacentCounts()
        {
            int[][] adjacentCounts = new int[grid.Length][];

            for (int row = 0; row < grid.Length; row++)
            {
                var line = grid[row];
                adjacentCounts[row] = new int[line.Length];
                for (int col = 0; col < line.Length; col++)
                {
                    // only need to check for occupied cells
                    if (grid[row][col] != '@')
                    {
                        adjacentCounts[row][col] = int.MaxValue;
                        continue;
                    }

                    // check the eight directions
                    for (int dRow = -1; dRow <= 1; dRow++)
                    {
                        for (int dCol = -1; dCol <= 1; dCol++)
                        {
                            if (dRow == 0 && dCol == 0)
                                continue;
                            int nRow = row + dRow;
                            int nCol = col + dCol;
                            if (nRow >= 0 && nRow < grid.Length && nCol >= 0 && nCol < line.Length)
                            {
                                if (grid[nRow][nCol] == '@')
                                {
                                    adjacentCounts[row][col]++;
                                }
                            }
                        }
                    }
                }
            }

            return adjacentCounts;
        }

        private int Solve_1_First_Approach()
        {
            int[][] adjacentCounts = GenerateAdjacentCounts();

            return adjacentCounts.Select(row => row.Count(cell => cell < 4)).Sum();
        }

        private int Solve_2_First_Approach()
        {
            int result = 0;

            while (true)
            {
                int removed = 0;
                int[][] adjacentCounts = GenerateAdjacentCounts(); // I could optimize this by only updating affected cells
                for (int row = 0; row < adjacentCounts.Length; row++)
                {
                    for (int col = 0; col < adjacentCounts[row].Length; col++)
                    {
                        if (adjacentCounts[row][col] < 4)
                        {
                            grid[row][col] = '.';
                            removed++;
                        }
                    }
                }
                if (removed == 0)
                {
                    break;
                }
                result += removed;
            }

            return result;
        }
    }
}

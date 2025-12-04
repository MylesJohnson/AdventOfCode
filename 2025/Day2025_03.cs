namespace AdventOfCode
{
    public class Day2025_03 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_03()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_Second_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            int result = 0;

            foreach (string line in _input)
            {
                char firstMax = '0';
                int indexOfMax = -1;
                for (int j = 0; j < line.Length - 1; j++) // If the last character is the highest, we can't use it anyway
                {
                    if(line[j] > firstMax) // We can technically avoid parsing into int since the ascii values are in order
                    {
                        firstMax = line[j];
                        indexOfMax = j;
                    }
                }

                char secondMax = '0';
                for (int k = indexOfMax + 1; k < line.Length; k++)
                {
                    if (line[k] > secondMax) // We can technically avoid parsing into int since the ascii values are in order
                    {
                        secondMax = line[k];
                    }
                }

                result += int.Parse($"{firstMax}{secondMax}");
            }

            return result;
        }

        private static (int index, int digit) FindMaxDigit(string str, int startIndex, int endOffset)
        {
            char maxChar = '0';
            int maxIndex = -1;
            for(int i = startIndex; i < str.Length - endOffset; i++)
            {
                if(str[i] > maxChar)
                {
                    maxChar = str[i];
                    maxIndex = i;
                }
            }
            return (maxIndex, maxChar - '0');
        }

        static long MaxJolt(string bank, int numberOfBatteries)
        {
            long jolt = 0;
            int index = 0;

            for (int i = 0; i < numberOfBatteries; i++)
            {
                var (newIndex, digit) = FindMaxDigit(bank, index, numberOfBatteries - i - 1); // leave enough digits for remaining batteries
                jolt = jolt * 10 + (digit);
                index = newIndex + 1;
            }

            return jolt;
        }

        private long Solve_1_Second_Approach()
        {
            long result = 0;

            foreach (string line in _input)
            {
                result += MaxJolt(line, 2);
            }

            return result;
        }

        private long Solve_2_First_Approach()
        {
            long result = 0;

            foreach (string line in _input)
            {
                result += MaxJolt(line, 12);
            }

            return result;
        }
    }
}

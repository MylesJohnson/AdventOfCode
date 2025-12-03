namespace AdventOfCode
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Action<SolverConfiguration>? options = opt =>
            {
                opt.ShowConstructorElapsedTime = true;
                opt.ShowTotalElapsedTimePerDay = true;
            };

            //await Solver.SolveAll(options);
            await Solver.Solve<Day2024_01>(options);
            //await Solver.SolveLast(options);
        }
    }
}

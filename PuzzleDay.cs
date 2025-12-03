using System.Net;

namespace AdventOfCode
{
    /// <summary>
    /// Provides base functionality for an Advent of Code puzzle day, including automatic input retrieval.
    /// Also handles mixing years in the same project.
    /// </summary>
    public abstract class PuzzleDay : BaseDay
    {
        protected PuzzleDay()
        {
            if (!File.Exists(InputFilePath))
            {
                string sessionCookie;
                try
                {
                    sessionCookie = File.ReadAllText("AoCSessionCookie.txt").Trim();

                    if (string.IsNullOrEmpty(sessionCookie) || sessionCookie == "ReplaceMe")
                    {
                        throw new InvalidOperationException("Session cookie in 'AoCSessionCookie.txt' is empty. Please add your Advent of Code session cookie to this file.");
                    }
                }
                catch (FileNotFoundException e)
                {
                    throw new InvalidOperationException("Session cookie file 'AoCSessionCookie.txt' not found. Please create this file and add your Advent of Code session cookie to it.", e);
                }

                Directory.CreateDirectory(InputFileDirPath);
                File.WriteAllText(InputFilePath, FetchInput(CalculateYear(), CalculateIndex(), sessionCookie).GetAwaiter().GetResult());
            }
        }

        public uint CalculateYear()
        {
            var typeName = GetType().Name;
            var dateSplit = typeName.Substring(typeName.IndexOf(ClassPrefix) + ClassPrefix.Length).Split('_');

            return uint.TryParse(dateSplit[0], out var index) ? index : default;
        }

        public override uint CalculateIndex()
        {
            var typeName = GetType().Name;
            var dateSplit = typeName.Substring(typeName.IndexOf(ClassPrefix) + ClassPrefix.Length).Split('_');

            return uint.TryParse(dateSplit[1], out var index) ? index : default;
        }

        public override string InputFilePath
        {
            get
            {
                var index = CalculateIndex().ToString("D2");

                return Path.Combine(InputFileDirPath, $"{CalculateYear()}_{index}{InputFileExtension}");
            }
        }

        private static async Task<string> FetchInput(uint year, uint day, string sessionCookie)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie
            {
                Name = "session",
                Domain = ".adventofcode.com",
                Value = sessionCookie
            });
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = new Uri("https://adventofcode.com/") })
            {
                var response = await client.GetAsync($"{year}/day/{day}/input");
                return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiDirectoryParser
{
    /// <summary>
    /// Parses directories and calls FileAnalyser to process files.
    /// </summary>
    class MultiDirectoryParser
    {
        /// <summary>
        /// Parses all directoriesm looking for TESTS dirs and
        /// populates a SortedSet with the XYZ files
        /// </summary>
        /// <param name="rootDirectory">The root entry point</param>
        /// <param name="excludeDirs">Exclusion Keyword List</param>
        /// <returns></returns>
        public static IEnumerable<FileDetails> ParseDirectories(string rootDirectory, List<string> excludeDirs)
        {
            var fileList = new SortedSet<FileDetails>();

            try
            {
                var dir_info = new DirectoryInfo(rootDirectory);
                foreach (var dir in dir_info.EnumerateDirectories("TESTS", SearchOption.AllDirectories))
                {
                    if (!excludeDirs.Any(x => dir.FullName.Contains(x)))
                    {
                        var allFiles = dir.EnumerateFiles("*.XYZ");
                        foreach (FileInfo file in allFiles)
                        {
                            fileList.Add(new FileDetails(file.FullName, file.Length));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return fileList;

        }
        static void Main(string[] args)
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            var excludeDirs = new List<string>() { "SRC", "DATA" };
            var fileList = ParseDirectories(rootDirectory, excludeDirs);

            ILogger log = new Logger(rootDirectory);  
            ILineReader lineReader = new LineReader();

            FileAnalyser f = new FileAnalyser(log, lineReader);
            f.AnalyzeAllFiles(fileList);
            log.Log(f.GetSummary());
        }
    }
}

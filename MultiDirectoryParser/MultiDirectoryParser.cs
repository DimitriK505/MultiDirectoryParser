using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace MultiDirectoryParser
{

    class MultiDirectoryParser
    {
        static void Main(string[] args)
        {
            var rootDirectory = @"c:\test-data";

            ILogger log = new Logger(rootDirectory);
            ILineReader lineReader = new LineReader();

            var excludeDirs = new List<string>() { "Dimitri" };
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
            FileAnalyser f = new FileAnalyser(log, lineReader);
            f.AnalyzeAllFiles(fileList);
            var summary = f.GetSummary();
            Console.WriteLine(summary);
            log.Log(summary);
        }
    }
}

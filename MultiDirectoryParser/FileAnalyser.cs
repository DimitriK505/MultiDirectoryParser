using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MultiDirectoryParser
{
    public class FileAnalyser
    {
        private readonly ILogger logger;
        private readonly ILineReader lineReader;

        public int TotalLines { get ; private set; }
        public int TotalEmptyLines { get; private set; }
        public int TotalDashLines { get; private set; }

        public FileAnalyser(ILogger logger, ILineReader lineReader)
        {
            this.logger = logger;
            this.lineReader = lineReader;
        }

        public void AnalyzeAllFiles(SortedSet<FileDetails> allFiles)
        {
            foreach (var file in allFiles)
            {
                AnalyzeLines(file);
            }
        }

        public void AnalyzeAllFilesAsync(SortedSet<FileDetails> allFiles)
        {
            Parallel.ForEach(allFiles, (currentFile) =>
            {
                AnalyzeLines(currentFile);
            });
        }

        public void AnalyzeLines(object data)
        {
            FileDetails currentFile = (FileDetails)data;
          
            foreach (var line in lineReader.GetLines(currentFile.FileName))
            {
                currentFile.NumberOfLines++;
                if (line.StartsWith("--"))
                {
                    currentFile.NumberOfDashLines++;
                }

                if (line == string.Empty)
                {
                    currentFile.NumberOfEmptyLines++;
                }
            }
            Console.WriteLine(currentFile);
            logger.Log(currentFile.ToString());
            TotalLines += currentFile.NumberOfLines;
            TotalEmptyLines += currentFile.NumberOfEmptyLines;
            TotalDashLines += currentFile.NumberOfDashLines;
        }

        public string GetSummary()
        {
            return "\n\nSummary: Total Number Of Lines: " + TotalLines + "\n" +
                    " Total Number of Empty Lines: " + TotalEmptyLines + "\n" +
                    " Total Number of Dash Lines: " + TotalDashLines + "\n";
        }
    }
}

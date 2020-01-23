using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MultiDirectoryParser
{
    /// <summary>
    /// FileAnalyser is responsible for parsing file contents
    /// and storing statistics
    /// </summary>
    public class FileAnalyser
    {
        private readonly ILogger logger;
        private readonly ILineReader lineReader;

        public int TotalLines { get ; private set; }
        public int TotalEmptyLines { get; private set; }
        public int TotalDashLines { get; private set; }

        /// <summary>
        /// FileAnalyser constructor
        /// </summary>
        /// <param name="logger">ILogger instance</param>
        /// <param name="lineReader">ILinerReader instance</param>
        public FileAnalyser(ILogger logger, ILineReader lineReader)
        {
            this.logger = logger;
            this.lineReader = lineReader;
        }

        /// <summary>
        /// Parses all files in serialized manner
        /// </summary>
        /// <param name="allFiles"></param>
        public void AnalyzeAllFiles(IEnumerable<FileDetails> allFiles)
        {
            foreach (var file in allFiles)
            {
                AnalyzeLines(file);
            }
        }

        /// <summary>
        /// Parses all files using thread parallelization 
        /// </summary>
        /// <param name="allFiles"></param>
        public void AnalyzeAllFilesAsync(IEnumerable<FileDetails> allFiles)
        {
            Parallel.ForEach(allFiles, (currentFile) =>
            {
                AnalyzeLines(currentFile);
            });
        }

        /// <summary>
        /// Counts the lines dash lines and empty lines
        /// </summary>
        /// <param name="data">The XYZ file</param>
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

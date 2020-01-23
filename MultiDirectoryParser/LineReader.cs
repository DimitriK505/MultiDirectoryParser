using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiDirectoryParser
{
    public class LineReader : ILineReader
    {
        public LineReader() { }
        public IEnumerable<string> GetLines(string filename)
        {
            return File.ReadLines(filename);
        }
    }
}

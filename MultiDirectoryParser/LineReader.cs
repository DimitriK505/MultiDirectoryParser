using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiDirectoryParser
{
    public class LineReader : ILineReader
    {
        /// <summary>
        /// Wrapper method
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IEnumerable<string> GetLines(string filename)
        {
            return File.ReadLines(filename);
        }
    }
}

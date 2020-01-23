using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDirectoryParser
{
    public interface ILineReader
    {
        IEnumerable<string> GetLines(string filename);
    }
}

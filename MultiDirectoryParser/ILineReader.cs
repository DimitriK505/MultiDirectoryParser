using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDirectoryParser
{
    /// <summary>
    /// Line Reader interface
    /// </summary>
    public interface ILineReader
    {
        IEnumerable<string> GetLines(string filename);
    }
}

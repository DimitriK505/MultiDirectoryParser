using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MultiDirectoryParser
{
    /// <summary>
    /// Data class for the file details
    /// </summary>
    public class FileDetails : IComparable<FileDetails>
    {
        public string FileName { get; private set; }
        public long FileSize { get; private set; }
        public int NumberOfLines { get;  set; }
        public int NumberOfEmptyLines { get;  set; }
        public int NumberOfDashLines { get;  set; }
        public FileDetails(string name, long size)
        {
            FileName = name;
            FileSize = size;
        }

        public int CompareTo(FileDetails other)
        {
            return this.FileSize.CompareTo(other.FileSize);
        }
        public override string ToString()
        {
            return "File: " + FileName + " Size: " + FileSize + " Number of Lines: " + NumberOfLines +
               " Empty Lines: " + NumberOfEmptyLines + " Dash Lines: " + NumberOfDashLines;
        }
    }
}

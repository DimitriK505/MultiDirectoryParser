using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiDirectoryParser
{
    class Logger : ILogger
    {
        private readonly string logFileName;
        private readonly object mtxLock = new object();

        public Logger(string rootDir) 
        {
            lock (mtxLock)
            {
                logFileName = rootDir + "\\Log.txt";
                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(logFileName))
                    {
                        streamWriter.WriteLine("-----Initializing Logger------\n\n");
                        streamWriter.Close();
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Log Directory does not exist!");
                    throw;
                }
            }
        }

        public void Log(string message)
        {
            lock (mtxLock)
            {
                using (StreamWriter streamWriter = File.AppendText(logFileName))
                {
                    streamWriter.WriteLine($"{DateTime.Now.ToLongTimeString()}> {message}");
                    streamWriter.Close();
                }
            }
        }
    }
}

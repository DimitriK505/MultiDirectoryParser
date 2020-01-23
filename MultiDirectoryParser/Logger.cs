using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiDirectoryParser
{    
    /// <summary>
    /// Logger class inherits from ILogger
    /// </summary>
    class Logger : ILogger
    {
        private readonly string logFileName;
        private readonly object mtxLock = new object();

        /// <summary>
        /// Logger class constructor. Puts the log at the same dir as the binary
        /// </summary>
        /// <param name="rootDir"></param>
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
        /// <summary>
        /// Appends a string to the log file
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
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
